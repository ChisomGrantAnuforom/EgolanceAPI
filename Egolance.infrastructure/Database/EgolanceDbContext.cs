using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Egolance.Domain.Entities;

namespace Egolance.Infrastructure.Database
{

    public class EgolanceDbContext : DbContext
    {
        public EgolanceDbContext(DbContextOptions<EgolanceDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Worker> Workers => Set<Worker>();
        public DbSet<ServiceCategory> ServiceCategories => Set<ServiceCategory>();
        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<ChatMessage> ChatMessages => Set<ChatMessage>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<WorkerDocument> WorkerDocuments => Set<WorkerDocument>();
        public DbSet<Notification> Notifications => Set<Notification>();

        public DbSet<PortfolioItem> PortfolioItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // USER
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.UserId);

                entity.Property(x => x.Role)
                      .HasConversion<string>();

                entity.HasOne(x => x.WorkerProfile)
                      .WithOne(x => x.User)
                      .HasForeignKey<Worker>(x => x.WorkerId);
            });

            // WORKER
            modelBuilder.Entity<Worker>(entity =>
            {
                entity.HasKey(x => x.WorkerId);

                entity.Property(x => x.VerificationStatus)
                      .HasConversion<string>();

                entity.HasOne(x => x.ServiceCategory)
                      .WithMany(x => x.Workers)
                      .HasForeignKey(x => x.ServiceCategoryId);
            });

            // SERVICE CATEGORY
            modelBuilder.Entity<ServiceCategory>(entity =>
            {
                entity.HasKey(x => x.ServiceCategoryId);
            });

            // BOOKING
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(x => x.BookingId);

                entity.Property(x => x.Status)
                      .HasConversion<string>();

                entity.HasOne(x => x.Customer)
                      .WithMany(x => x.CustomerBookings)
                      .HasForeignKey(x => x.CustomerId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Worker)
                      .WithMany(x => x.Bookings)
                      .HasForeignKey(x => x.WorkerId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.ServiceCategory)
                      .WithMany(x => x.Bookings)
                      .HasForeignKey(x => x.ServiceCategoryId);
            });

            // CHAT MESSAGE
            modelBuilder.Entity<ChatMessage>(entity =>
            {
                entity.HasKey(x => x.MessageId);

                entity.HasOne(x => x.Booking)
                      .WithMany(x => x.ChatMessages)
                      .HasForeignKey(x => x.BookingId);

                entity.HasOne(x => x.Sender)
                      .WithMany()
                      .HasForeignKey(x => x.SenderId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // REVIEW
            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(x => x.ReviewId);

                entity.HasOne(x => x.Booking)
                      .WithOne(x => x.Review)
                      .HasForeignKey<Review>(x => x.BookingId);

                entity.HasOne(x => x.Customer)
                      .WithMany()
                      .HasForeignKey(x => x.CustomerId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Worker)
                      .WithMany()
                      .HasForeignKey(x => x.WorkerId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // PAYMENT
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(x => x.PaymentId);

                entity.Property(x => x.Status)
                      .HasConversion<string>();

                entity.HasOne(x => x.Booking)
                      .WithOne(x => x.Payment)
                      .HasForeignKey<Payment>(x => x.BookingId);

                entity.HasOne(x => x.Customer)
                      .WithMany()
                      .HasForeignKey(x => x.CustomerId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Worker)
                      .WithMany()
                      .HasForeignKey(x => x.WorkerId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // WORKER DOCUMENT
            modelBuilder.Entity<WorkerDocument>(entity =>
            {
                entity.HasKey(x => x.DocumentId);

                entity.Property(x => x.DocumentType)
                      .HasConversion<string>();

                entity.Property(x => x.Status)
                      .HasConversion<string>();

                entity.HasOne(x => x.Worker)
                      .WithMany(x => x.Documents)
                      .HasForeignKey(x => x.WorkerId);
            });

            // NOTIFICATION
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(x => x.NotificationId);

                entity.Property(x => x.Type)
                      .HasConversion<string>();

                entity.HasOne(x => x.User)
                      .WithMany(x => x.Notifications)
                      .HasForeignKey(x => x.UserId);
            });
        }
    }

}
