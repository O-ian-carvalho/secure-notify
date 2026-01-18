using Auth.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Email)
              .HasConversion(new ValueConverter<Email, string>(
                  email => email.ToString(),   
                  value => new Email(value)    
              ))
              .HasColumnName("Email")
              .IsRequired();

            builder.OwnsOne(u => u.Status, status =>
            {
                status.Property(s => s.IsBanned).HasColumnName("IsBanned").IsRequired();
                status.Property(s => s.SuspentionEnd).HasColumnName("SuspentionEnd");
            });

        }
    }
}
