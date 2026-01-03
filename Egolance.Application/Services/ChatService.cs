using Egolance.Application.DTOs.Booking;
using Egolance.Domain.Entities;
using Egolance.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Egolance.Application.Services
{
    public class ChatService
    {
        private readonly EgolanceDbContext _db;
        private readonly NotificationService _notifications;

        public ChatService(EgolanceDbContext db, NotificationService notifications)
        {
            _db = db;
            _notifications = notifications;
        }

        public async Task<ChatMessage?> SendMessageAsync(Guid bookingId, Guid senderId, SendMessageRequest input)
        {
            var booking = await _db.Bookings.FindAsync(bookingId);
            if (booking == null)
                return null;

            // Only customer or worker can send messages
            if (senderId != booking.CustomerId && senderId != booking.WorkerId)
                return null;

            var message = new ChatMessage
            {
                MessageId = Guid.NewGuid(),
                BookingId = bookingId,
                SenderId = senderId,
                MessageText = input.MessageText,
                SentAt = DateTime.UtcNow,
                IsRead = false
            };

            _db.ChatMessages.Add(message);
            await _db.SaveChangesAsync();

            await _notifications.NotifyBookingStatusChangeAsync(booking);


            return message;
        }

        public async Task<List<ChatMessage>> GetMessagesAsync(Guid bookingId, Guid userId)
        {
            var booking = await _db.Bookings.FindAsync(bookingId);
            if (booking == null)
                return new List<ChatMessage>();

            // Only participants can view chat
            if (userId != booking.CustomerId && userId != booking.WorkerId)
                return new List<ChatMessage>();

            return await _db.ChatMessages
                .Where(m => m.BookingId == bookingId)
                .OrderBy(m => m.SentAt)
                .ToListAsync();
        }
    }

}
