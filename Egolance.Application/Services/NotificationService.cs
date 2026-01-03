using Egolance.Application.DTOs.Notifications;
using Egolance.Domain.Entities;
using Egolance.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Egolance.Application.Services
{

    public class NotificationService
    {
        private readonly EgolanceDbContext _db;
        private readonly EmailService _email;

        public NotificationService(EgolanceDbContext db, EmailService email)
        {
            _db = db;
            _email = email;
        }

        public async Task NotifyUserAsync(NotificationMessage message)
        {
            var user = await _db.Users.FindAsync(message.UserId);
            if (user == null) return;

            await _email.SendEmailAsync(user.Email, message.Subject, message.Body);
        }

        public async Task NotifyBookingStatusChangeAsync(Booking booking)
        {
            var customer = await _db.Users.FindAsync(booking.CustomerId);
            var worker = await _db.Users.FindAsync(booking.WorkerId);

            string subject = $"Booking {booking.Status}";
            string body = $"Your booking with ID {booking.BookingId} is now {booking.Status}.";

            // Notify both parties
            await _email.SendEmailAsync(customer.Email, subject, body);
            await _email.SendEmailAsync(worker.Email, subject, body);
        }

        public async Task NotifyNewChatMessageAsync(ChatMessage message)
        {
            var booking = await _db.Bookings.FindAsync(message.BookingId);
            if (booking == null) return;

            Guid receiverId = message.SenderId == booking.CustomerId
                ? booking.WorkerId
                : booking.CustomerId;

            var receiver = await _db.Users.FindAsync(receiverId);
            if (receiver == null) return;

            await _email.SendEmailAsync(
                receiver.Email,
                "New Chat Message",
                $"You received a new message: {message.MessageText}"
            );
        }
    }
}
