using Egolance.Application.DTOs.Booking;
using Egolance.Application.Services;
using Egolance.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Egolance.Api.Controllers
{
    [ApiController]
    [Route("api/bookings/{bookingId}/chat")]
    public class ChatController : ControllerBase
    {
        private readonly ChatService _service;

        public ChatController(ChatService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SendMessage(Guid bookingId, SendMessageRequest input)
        {
            var senderId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var message = await _service.SendMessageAsync(bookingId, senderId, input);
            if (message == null) return Unauthorized();

            return Ok(new ChatMessageResponse
            {
                MessageId = message.MessageId,
                SenderId = message.SenderId,
                MessageText = message.MessageText,
                SentAt = message.SentAt,
                IsRead = message.IsRead
            });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetMessages(Guid bookingId)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var messages = await _service.GetMessagesAsync(bookingId, userId);

            return Ok(messages.Select(m => new ChatMessageResponse
            {
                MessageId = m.MessageId,
                SenderId = m.SenderId,
                MessageText = m.MessageText,
                SentAt = m.SentAt,
                IsRead = m.IsRead
            }));
        }
    }

}
