using Egolance.Application.DTOs.Workers;
using Egolance.Domain.Entities;
using Egolance.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Egolance.Application.Services
{

    public class PortfolioService
    {
        private readonly EgolanceDbContext _db;

        public PortfolioService(EgolanceDbContext db)
        {
            _db = db;
        }

        public async Task<PortfolioItem> AddItemAsync(Guid workerId, PortfolioItemInput input)
        {
            var item = new PortfolioItem
            {
                WorkerId = workerId,
                Title = input.Title,
                FileUrl = input.FileUrl,
                FileType = input.FileType
            };

            _db.PortfolioItems.Add(item);
            await _db.SaveChangesAsync();

            return item;
        }

        public async Task<List<PortfolioItem>> GetWorkerPortfolioAsync(Guid workerId)
        {
            return await _db.PortfolioItems
                .Where(p => p.WorkerId == workerId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<PortfolioItem?> UpdateItemAsync(Guid itemId, PortfolioItemUpdate input)
        {
            var item = await _db.PortfolioItems.FindAsync(itemId);
            if (item == null) return null;

            item.Title = input.Title;
            item.FileUrl = input.FileUrl;
            item.FileType = input.FileType;

            await _db.SaveChangesAsync();
            return item;
        }


        public async Task<bool> DeleteItemAsync(Guid itemId)
        {
            var item = await _db.PortfolioItems.FindAsync(itemId);
            if (item == null) return false;

            _db.PortfolioItems.Remove(item);
            await _db.SaveChangesAsync();
            return true;
        }
    }


}
