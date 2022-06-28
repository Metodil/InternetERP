using InternetERP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InternetERP.Data
{
    public class InternetERPDbContext : IdentityDbContext<IdentityUser>
    {
        public InternetERPDbContext()
        {
        }
        public InternetERPDbContext(DbContextOptions<InternetERPDbContext> options)
            : base(options)
        {
        }

        public DbSet<StatusFailure> StatusFailures { get; set; }
        public DbSet<TypeAccount> TypeAccounts { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Failure> Failures { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<FailurePart> FailuresParts { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=InternetERP;Integrated Security = true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) // ModelBuilder
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StatusFailure>()
                .HasIndex(sf => sf.Name)
                .IsUnique();

            modelBuilder.Entity<TypeAccount>()
                .HasIndex(ta => ta.Name)
                .IsUnique();

            modelBuilder.Entity<Town>()
                .HasIndex(t => t.Name)
                .IsUnique();

            modelBuilder.Entity<Account>()
                .HasIndex(t => t.UserName)
                .IsUnique();

            modelBuilder.Entity<Account>()
            .Property(a => a.ExparedDate)
            .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Account>()
            .Property(a => a.CreateDate)
            .HasDefaultValueSql("getdate()");
 
            modelBuilder.Entity<Failure>()
             .Property(a => a.CreateDate)
             .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<FailurePart>().HasKey(u => new
            {
                u.FailureId, u.PartId
            });

            modelBuilder.Entity<Feedback>()
             .Property(a => a.CreateDate)
             .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Comment>()
             .Property(a => a.CreateDate)
             .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Payment>()
             .Property(a => a.CreateDate)
             .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Payment>(x =>
            {
                x.HasOne(x => x.Account)
                .WithMany(x => x.Payments)
                .HasForeignKey(x => x.AccountId)
                .OnDelete(DeleteBehavior.NoAction);
            });
        }

    }
}
