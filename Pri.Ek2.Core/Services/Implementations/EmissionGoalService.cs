using Microsoft.EntityFrameworkCore;
using Pri.Ek2.Core.Data;
using Pri.Ek2.Core.Dtos.RequestDtos;
using Pri.Ek2.Core.Dtos.ResponseDtos;
using Pri.Ek2.Core.Entities;
using Pri.Ek2.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Services.Implementations
{
    public class EmissionGoalService : IEmissionGoalService
    {
        private readonly ApplicationDbContext _context;

        public EmissionGoalService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EmissionGoalResponseDto> AddAsync(EmissionGoalRequestDto dto)
        {
            // Validatie
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (dto.EndDate <= dto.StartDate)
                throw new ArgumentException("End date must be after start date");

            if (!await UserExistsAsync(dto.UserId))
                throw new ArgumentException("User does not exist");

            // Creëer nieuw doel
            var goal = new EmissionGoal
            {
                UserId = dto.UserId,
                TargetEmissionsKg = dto.TargetEmissionKg,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                CurrentEmissionsKg = 0 // Startwaarde
            };

            await _context.EmissionGoals.AddAsync(goal);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(goal.Id); // Hergebruik bestaande methode
        }

        public async Task DeleteAsync(int id)
        {
            var goal = await _context.EmissionGoals.FindAsync(id);
            if (goal == null)
                throw new KeyNotFoundException($"Emission goal {id} not found");

            _context.EmissionGoals.Remove(goal);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmissionGoalResponseDto>> GetAllAsync()
        {
            return await _context.EmissionGoals
                .Include(g => g.UserProfile) // Laad gerelateerde user data
                .Select(g => new EmissionGoalResponseDto
                {
                    Id = g.Id,
                    UserId = g.UserId,
                    TargetEmissionsKg = g.TargetEmissionsKg,
                    StartDate = g.StartDate,
                    EndDate = g.EndDate,
                    CurrentEmissionsKg = g.CurrentEmissionsKg,
                    IsAchieved = g.CurrentEmissionsKg <= g.TargetEmissionsKg
                })
                .AsNoTracking() // Optimalisatie voor read-only
                .ToListAsync();
        }

        public async Task<EmissionGoalResponseDto> GetByIdAsync(int id)
        {
            var goal = await _context.EmissionGoals
                .Include(g => g.UserProfile)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (goal == null)
                throw new KeyNotFoundException($"Emission goal {id} not found");

            return new EmissionGoalResponseDto
            {
                Id = goal.Id,
                UserId = goal.UserId,
                TargetEmissionsKg = goal.TargetEmissionsKg,
                StartDate = goal.StartDate,
                EndDate = goal.EndDate,
                CurrentEmissionsKg = goal.CurrentEmissionsKg,
                IsAchieved = goal.CurrentEmissionsKg <= goal.TargetEmissionsKg
            };
        }

        public async Task<IEnumerable<EmissionGoalResponseDto>> GetGoalsByCriteriaAsync(string criteria)
        {
            if (string.IsNullOrWhiteSpace(criteria))
                return await GetAllAsync();

            return await _context.EmissionGoals
                .Include(g => g.UserProfile)
                .Where(g => g.UserProfile.IdentityUser.Email.Contains(criteria) ||
                           g.TargetEmissionsKg.ToString().Contains(criteria))
                .Select(g => MapToDto(g))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<EmissionGoalResponseDto> UpdateAsync(int id, EmissionGoalRequestDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var goal = await _context.EmissionGoals.FindAsync(id);
            if (goal == null)
                throw new KeyNotFoundException($"Emission goal {id} not found");

            // Update velden
            goal.TargetEmissionsKg = dto.TargetEmissionKg;
            goal.StartDate = dto.StartDate;
            goal.EndDate = dto.EndDate;

            await _context.SaveChangesAsync();
            return await GetByIdAsync(id);
        }

        // Hulpmethodes
        private async Task<bool> UserExistsAsync(string userId)
        {
            return await _context.UserProfiles
                .AnyAsync(up => up.UserId == userId);
        }

        private EmissionGoalResponseDto MapToDto(EmissionGoal goal)
        {
            return new EmissionGoalResponseDto
            {
                Id = goal.Id,
                UserId = goal.UserId,
                TargetEmissionsKg = goal.TargetEmissionsKg,
                StartDate = goal.StartDate,
                EndDate = goal.EndDate,
                CurrentEmissionsKg = goal.CurrentEmissionsKg,
                IsAchieved = goal.CurrentEmissionsKg <= goal.TargetEmissionsKg
            };
        }
    }
}