using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using CascadeFinTech.Application.Configuration.Emails;
using CascadeFinTech.Infrastructure;
using Serilog.Core;

namespace CascadeFinTech.IntegrationTests.SeedWork
{
    public class TestBase
    {
        protected string ConnectionString;

        protected EmailsSettings EmailsSettings;

        protected IEmailSender EmailSender;

        protected ExecutionContextMock ExecutionContext;

        [SetUp]
        public async Task BeforeEachTest()
        {
            const string connectionStringEnvironmentVariable =
                "ASPNETCORE_CascadeFinTech_IntegrationTests_ConnectionString";
            ConnectionString = Environment.GetEnvironmentVariable(connectionStringEnvironmentVariable);

            //if you wish to avoid using an enviornment variable, comment out the above and set the below
            //ConnectionString = @"Server=OMNIAPRIMELAPTO\SQL2019;Database=CascadeFinTech;User Id=sa;Password=timothy79;";

            if (ConnectionString == null)
            {
                throw new ApplicationException(
                    $"Define connection string to integration tests database using environment variable: {connectionStringEnvironmentVariable}");
            }

            await using var sqlConnection = new SqlConnection(ConnectionString);

            await ClearDatabase(sqlConnection);

            EmailsSettings = new EmailsSettings {FromAddressEmail = "from@mail.com"};

            EmailSender = Substitute.For<IEmailSender>();

            ExecutionContext = new ExecutionContextMock();

            ApplicationStartup.Initialize(
                new ServiceCollection(),
                ConnectionString, 
                new CacheStore(),
                EmailSender,
                EmailsSettings,
                Logger.None,
                ExecutionContext,
                runQuartz:false);
        }

        private static async Task ClearDatabase(IDbConnection connection)
        {
            const string sql = "DELETE FROM [app].[InternalCommand] " +
                               "DELETE FROM [app].[OutboxMessage] " +
                               "DELETE FROM [orders].[OrderBook] " +
                               "DELETE FROM [payments].[Payment] " +
                               "DELETE FROM [orders].[Order] " +
                               "DELETE customers " +
                               "  FROM [orders].[Customer] customers " +
                               " WHERE (customers.Email NOT IN ('johndoe@mail.com', 'janedoe@mail.com'))";

            await connection.ExecuteScalarAsync(sql);
        }
    }
}