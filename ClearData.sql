DELETE FROM [payments].[Payments]
GO

DELETE FROM [orders].[OrderBooks]
GO

DELETE FROM [orders].[Orders]
GO

DELETE customers
  FROM [orders].[Customers] customers
 WHERE (customers.Email NOT IN ('johndoe@mail.com', 'janedoe@mail.com'))
GO

DELETE FROM [app].[OutboxMessages]
GO

DELETE FROM [app].[InternalCommands]
GO