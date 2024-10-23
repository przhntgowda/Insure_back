using AlShamil.Model.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace AlShamilEntityData
{
    public class AlShamilDbContext:DbContext
    {
        public AlShamilDbContext(DbContextOptions<AlShamilDbContext> options) : base(options)
        {

        }
        public DbSet<RoleDto> Role { get; set; }
        public DbSet<UserDto> User { get; set; }
        public DbSet<CountryDto> Country { get; set; }
        public DbSet<StateDto> State { get; set; }
        public DbSet<CurrencyDto> Currency { get; set; }
        public DbSet<CompanyDto> Company { get; set; }

        public DbSet<AccountGroupDto> AccountGroup { get; set; }
        public DbSet<AccountLedgerDto> AccountLedger { get; set; }
        public DbSet<PaymentTypeDto> PaymentType { get; set; }
        public DbSet<TransactionDto> Transaction { get; set; }
        public DbSet<PageDto> Page { get; set; }
        public DbSet<RoleAccessDto> RoleAccess { get; set; }
        public DbSet<PasswordResetTokenDto> PasswordResetToken { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserDto>()
                .Property(b => b.Guid)
                .HasDefaultValueSql("NEWID()");
            

            //var baseDtoTypes = Assembly.GetExecutingAssembly().GetTypes()
            //.Where(type => !type.IsAbstract && type.IsSubclassOf(typeof(BaseDto)));

            //foreach (var type in baseDtoTypes)
            //{
            //    // Shadow property for CreatedOn
            //    modelBuilder.Entity(type)
            //        .Property<DateTime>("CreatedOn")
            //        .HasDefaultValueSql("GETDATE()");

            //    // Shadow property for IsActive
            //    modelBuilder.Entity(type)
            //        .Property<bool>("IsActive")
            //        .HasDefaultValue(true); // Or HasDefaultValueSql("1") for SQL Server
            //}
        }
    }
}
