using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CascadeFinTech.Infrastructure.Database;

namespace CascadeFinTech.Infrastructure.Processing.InternalCommands
{
    internal sealed class InternalCommandEntityTypeConfiguration : IEntityTypeConfiguration<InternalCommand>
    {
        public void Configure(EntityTypeBuilder<InternalCommand> builder)
        {
            builder.ToTable("InternalCommand", SchemaNames.Application);
            
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedNever();
        }
    }
}