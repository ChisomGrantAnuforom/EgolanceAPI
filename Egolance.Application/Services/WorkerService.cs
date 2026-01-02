
    using Egolance.Application.DTOs.Workers;
    using Egolance.Domain.Entities;
    using Egolance.Domain.Enums;
    using Egolance.Infrastructure.Database;
    using Microsoft.EntityFrameworkCore;

    namespace Egolance.Application.Services
    {
        public class WorkerService
        {
            private readonly EgolanceDbContext _db;

            public WorkerService(EgolanceDbContext db)
            {
                _db = db;
            }

            // CREATE
            public async Task<Worker> CreateWorkerAsync(Guid userId, CreateWorkerRequest request)
            {
                if (await _db.Workers.AnyAsync(w => w.WorkerId == userId))
                    throw new Exception("Worker profile already exists");

                var worker = new Worker
                {
                    WorkerId = userId,
                    Bio = request.Bio,
                    HourlyRate = request.HourlyRate,
                    ServiceCategoryId = request.ServiceCategoryId,
                    ExperienceYears = request.ExperienceYears,
                    Rating = 0,
                    IsAvailable = true,
                    LocationLat = request.LocationLat,
                    LocationLng = request.LocationLng,
                    VerificationStatus = VerificationStatus.Pending
                };

                _db.Workers.Add(worker);
                await _db.SaveChangesAsync();

                return worker;
            }

            // READ ALL
            public async Task<List<Worker>> GetAllAsync()
            {
                return await _db.Workers.ToListAsync();
            }

            // READ ONE
            public async Task<Worker?> GetByIdAsync(Guid workerId)
            {
                return await _db.Workers.FindAsync(workerId);
            }

            // UPDATE
            public async Task<Worker?> UpdateAsync(Guid workerId, WorkerUpdate input)
            {
                var worker = await _db.Workers.FindAsync(workerId);
                if (worker == null) return null;

                worker.Bio = input.Bio;
                worker.HourlyRate = input.HourlyRate;
                worker.ServiceCategoryId = input.ServiceCategoryId;
                worker.ExperienceYears = input.ExperienceYears;
                worker.IsAvailable = input.IsAvailable;
                worker.LocationLat = input.LocationLat;
                worker.LocationLng = input.LocationLng;

                await _db.SaveChangesAsync();
                return worker;
            }

            // DELETE
            public async Task<bool> DeleteAsync(Guid workerId)
            {
                var worker = await _db.Workers.FindAsync(workerId);
                if (worker == null) return false;

                _db.Workers.Remove(worker);
                await _db.SaveChangesAsync();
                return true;
            }
        }
    }


