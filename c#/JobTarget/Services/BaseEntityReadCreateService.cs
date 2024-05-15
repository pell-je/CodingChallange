using JobTargetCodingChallange.Entity;
using JobTargetCodingChallange.Entity.Sale;
using JobTargetCodingChallange.Services;
using JobTargetCodingChallange.Validation;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace JobTargetCodingChallange
{
    public class BaseEntityReadCreateService<TEnity> : IReadCreateService<TEnity> where TEnity : BaseEntity
    {

        private readonly AppDbContext _context;

        private readonly ILogger<BaseEntityReadCreateService<TEnity>> _logger;

        public BaseEntityReadCreateService(AppDbContext context, ILogger<BaseEntityReadCreateService<TEnity>> logger)
        {
            _context = context;
            _logger = logger;
        }

        public virtual async Task<TEnity> Create(TEnity entity)
        {
            ValidationService.ThrowIfEntityIsInvalid(entity);
            _context.Set<TEnity>().Add(entity);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Created {entity.GetType()} {JsonSerializer.Serialize(entity)}"); 
            return entity;
        }

        public virtual async Task<TEnity?> GetById(long id)
        {
            var entity =  await _context.Set<TEnity>().FindAsync(id);

            if (entity == null) {
                _logger.LogInformation($"Did not find {typeof(TEnity)} with id of {id}");
            }
            else
            {
                _logger.LogInformation($"Found {entity.GetType()} {JsonSerializer.Serialize(entity)}");
            }

            return entity;
        }

    }
}