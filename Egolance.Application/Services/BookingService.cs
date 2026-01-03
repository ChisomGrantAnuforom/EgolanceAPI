using Egolance.Application.DTOs.Booking;
using Egolance.Domain.Entities;
using Egolance.Domain.Enums;
using Egolance.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Egolance.Application.Services
{
    public class BookingService
    {
        //private readonly EgolanceDbContext _db;

        //public BookingService(EgolanceDbContext db)
        //{
        //    _db = db;
        //}

        private readonly EgolanceDbContext _db;
        private readonly NotificationService _notifications;

        public BookingService(EgolanceDbContext db, NotificationService notifications)
        {
            _db = db;
            _notifications = notifications;
        }



        // CUSTOMER creates booking
        public async Task<Booking> CreateBookingAsync(Guid customerId, CreateBookingRequest input)
        {
            var booking = new Booking
            {
                BookingId = Guid.NewGuid(),
                CustomerId = customerId,
                WorkerId = input.WorkerId,
                ServiceCategoryId = input.ServiceCategoryId,
                Description = input.Description,
                Address = input.Address,
                ScheduledDate = input.ScheduledDate,
                Price = input.Price,
                Status = BookingStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _db.Bookings.Add(booking);
            await _db.SaveChangesAsync();

            await _notifications.NotifyBookingStatusChangeAsync(booking);


            return booking;
        }

        // WORKER accepts booking
        public async Task<Booking?> AcceptBookingAsync(Guid bookingId, Guid workerId)
        {
            var booking = await _db.Bookings.FindAsync(bookingId);
            if (booking == null || booking.WorkerId != workerId)
                return null;

            booking.Status = BookingStatus.Accepted;
            booking.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();

            await _notifications.NotifyBookingStatusChangeAsync(booking);


            return booking;
        }

        // WORKER rejects booking
        public async Task<Booking?> RejectBookingAsync(Guid bookingId, Guid workerId)
        {
            var booking = await _db.Bookings.FindAsync(bookingId);
            if (booking == null || booking.WorkerId != workerId)
                return null;

            booking.Status = BookingStatus.Rejected;
            booking.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();

            await _notifications.NotifyBookingStatusChangeAsync(booking);


            return booking;
        }

        // CUSTOMER cancels booking
        public async Task<Booking?> CancelBookingAsync(Guid bookingId, Guid customerId)
        {
            var booking = await _db.Bookings.FindAsync(bookingId);
            if (booking == null || booking.CustomerId != customerId)
                return null;

            booking.Status = BookingStatus.Cancelled;
            booking.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();

            await _notifications.NotifyBookingStatusChangeAsync(booking);


            return booking;
        }

        // WORKER marks booking as completed
        public async Task<Booking?> CompleteBookingAsync(Guid bookingId, Guid workerId)
        {
            var booking = await _db.Bookings.FindAsync(bookingId);
            if (booking == null || booking.WorkerId != workerId)
                return null;

            booking.Status = BookingStatus.Completed;
            booking.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();


            await _notifications.NotifyBookingStatusChangeAsync(booking);


            return booking;
        }

        // GET bookings for customer
        public async Task<List<Booking>> GetCustomerBookingsAsync(Guid customerId)
        {
            return await _db.Bookings
                .Where(b => b.CustomerId == customerId)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        // GET bookings for worker
        public async Task<List<Booking>> GetWorkerBookingsAsync(Guid workerId)
        {
            return await _db.Bookings
                .Where(b => b.WorkerId == workerId)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }
    }

}
