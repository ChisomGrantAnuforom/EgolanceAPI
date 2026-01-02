using Egolance.Application.DTOs.ServiceCategories;
using Egolance.Domain.Entities;
using Egolance.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;


    namespace Egolance.Application.Services
    {
        public class ServiceCategoryService
        {
            private readonly EgolanceDbContext _db;

            public ServiceCategoryService(EgolanceDbContext db)
            {
                _db = db;
            }

            public async Task<ServiceCategoryResponse> CreateAsync(ServiceCategoryInput input)
            {
                var category = new ServiceCategory
                {
                    Name = input.Name
                };

                _db.ServiceCategories.Add(category);
                await _db.SaveChangesAsync();

                return new ServiceCategoryResponse
                {
                    Id = category.ServiceCategoryId,
                    Name = category.Name
                };
            }

            public async Task<List<ServiceCategoryResponse>> GetAllAsync()
            {
                return await _db.ServiceCategories
                    .Select(c => new ServiceCategoryResponse
                    {
                        Id = c.ServiceCategoryId,
                        Name = c.Name
                    })
                    .ToListAsync();
            }

            public async Task<ServiceCategoryResponse?> GetByIdAsync(Guid id)
            {
                var category = await _db.ServiceCategories.FindAsync(id);
                if (category == null) return null;

                return new ServiceCategoryResponse
                {
                    Id = category.ServiceCategoryId,
                    Name = category.Name
                };
            }

            public async Task<ServiceCategoryResponse?> UpdateAsync(Guid id, ServiceCategoryUpdate input)
            {
                var category = await _db.ServiceCategories.FindAsync(id);
                if (category == null) return null;

                category.Name = input.Name;

                await _db.SaveChangesAsync();

                return new ServiceCategoryResponse
                {
                    Id = category.ServiceCategoryId,
                    Name = category.Name
                };
            }

            public async Task<bool> DeleteAsync(Guid id)
            {
                var category = await _db.ServiceCategories.FindAsync(id);
                if (category == null) return false;

                _db.ServiceCategories.Remove(category);
                await _db.SaveChangesAsync();

                return true;
            }
        }
    }



