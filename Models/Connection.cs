using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApplication2.Models;
using WebApplication2.ViewModels;

namespace WebApplication2.Models
{
    public class Connection : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.;Database=E_project;User Id=sa;Password=useradmin;TrustServerCertificate=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.TreatmentRequest)
                .WithMany(tr => tr.Notifications)
                .HasForeignKey(n => n.TreatmentRequestId);

            modelBuilder.Entity<Employee>()
              .Property(e => e.Email)
              .HasColumnName("Email"); // Ensure this matches your database column

            modelBuilder.Entity<AdminLogin>()
                .Property(a => a.Email)
                .HasColumnName("Email");

            modelBuilder.Entity<FinanceManager>()
                .Property(f => f.Email)
                .HasColumnName("Email");

        }

        public DbSet<AdminLogin> Admin { get; set; }

        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Employee> EmpRegisters { get; set; }

        public DbSet<FinanceManager> FinanceManagers { get; set; }

        public DbSet<VisitRequest> VisitRequest { get; set; }

        public DbSet<TreatmentRequest> TreatmentRequests { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Per_Hospital> Per_Hospitals { get; set; }
        public DbSet<Policy> Policies { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Medical_Invoice> Medical_Invoice { get; set; }

    }
  
     
    }
