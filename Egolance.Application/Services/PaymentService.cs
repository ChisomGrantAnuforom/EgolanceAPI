using Egolance.Domain.Entities;
using Egolance.Domain.Enums;
using Egolance.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stripe;
using Microsoft.EntityFrameworkCore;

namespace Egolance.Application.Services
{
   

    public class PaymentService
    {
        private readonly EgolanceDbContext _db;
        private readonly IConfiguration _config;
        private readonly StripeClient _stripeClient;

        public PaymentService(EgolanceDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;

            // NEW Stripe client (replaces StripeConfiguration)
            _stripeClient = new StripeClient(_config["Stripe:SecretKey"]);
        }

        public async Task<(Payment payment, string clientSecret)> CreatePaymentIntentAsync(Guid bookingId, Guid customerId)
        {
            var booking = await _db.Bookings.FindAsync(bookingId);
            if (booking == null)
                throw new Exception("Booking not found");

            if (booking.CustomerId != customerId)
                throw new Exception("You can only pay for your own bookings");

            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(booking.Price * 100),
                Currency = "eur",
                Metadata = new Dictionary<string, string>
            {
                { "bookingId", bookingId.ToString() },
                { "customerId", customerId.ToString() },
                { "workerId", booking.WorkerId.ToString() }
            }
            };

            var intentService = new PaymentIntentService(_stripeClient);
            var intent = await intentService.CreateAsync(options);

            var payment = new Payment
            {
                PaymentId = Guid.NewGuid(),
                BookingId = bookingId,
                CustomerId = customerId,
                WorkerId = booking.WorkerId,
                Amount = booking.Price,
                Currency = "EUR",
                StripePaymentIntentId = intent.Id,
                Status = PaymentStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            _db.Payments.Add(payment);
            await _db.SaveChangesAsync();

            return (payment, intent.ClientSecret);
        }

        public async Task UpdatePaymentStatusAsync(string paymentIntentId, PaymentStatus status)
        {
            var payment = await _db.Payments
                .FirstOrDefaultAsync(p => p.StripePaymentIntentId == paymentIntentId);

            if (payment == null)
                return;

            payment.Status = status;
            await _db.SaveChangesAsync();
        }
    }


}
