using Egolance.Application.DTOs.Workers;
using Egolance.Domain.Entities;
using Egolance.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Egolance.Application.Services
{
    public class ReviewService
    {
        private readonly EgolanceDbContext _db;

        public ReviewService(EgolanceDbContext db)
        {
            _db = db;
        }

        public async Task<Review?> AddReviewAsync(Guid workerId, Guid customerId, CreateReviewRequest input)
        {
            if (workerId == customerId)
                throw new Exception("Workers cannot review themselves");

            bool alreadyReviewed = await _db.Reviews
                .AnyAsync(r => r.WorkerId == workerId && r.CustomerId == customerId);

            if (alreadyReviewed)
                throw new Exception("You have already reviewed this worker");

            var review = new Review
            {
                WorkerId = workerId,
                CustomerId = customerId,
                Rating = input.Rating,
                Comment = input.Comment
            };

            _db.Reviews.Add(review);
            await _db.SaveChangesAsync();

            await UpdateWorkerRating(workerId);

            return review;
        }

        public async Task<List<Review>> GetWorkerReviewsAsync(Guid workerId)
        {
            return await _db.Reviews
                .Where(r => r.WorkerId == workerId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        private async Task UpdateWorkerRating(Guid workerId)
        {
            var reviews = await _db.Reviews
                .Where(r => r.WorkerId == workerId)
                .ToListAsync();

            double avg = reviews.Any() ? reviews.Average(r => r.Rating) : 0;

            var worker = await _db.Workers.FindAsync(workerId);
            worker.Rating = avg;

            await _db.SaveChangesAsync();
        }
    }

}
