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
    public class EmissionReportService : IEmissionReportService
    {
        private readonly ApplicationDbContext _context;

        public EmissionReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EmissionReportResponseDto> AddAsync(EmissionReportRequestDto dto)
        {
            // Validatie
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (dto.PeriodEnd <= dto.PeriodStart)
                throw new ArgumentException("Period end must be after period start");

            if (!await _context.UserProfiles.AnyAsync(u => u.UserId == dto.UserId))
                throw new ArgumentException("User does not exist");

            // Nieuw rapport aanmaken
            var report = new EmissionReport
            {
                UserId = dto.UserId,
                TotalEmissionsKg = dto.TotalEmissionsKg,
                PeriodStart = dto.PeriodStart,
                PeriodEnd = dto.PeriodEnd,
                Notes = dto.Notes
            };

            await _context.EmissionReports.AddAsync(report);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(report.Id);
        }

        public async Task DeleteAsync(int id)
        {
            var report = await _context.EmissionReports.FindAsync(id);
            if (report == null)
                throw new KeyNotFoundException($"Report {id} not found");

            _context.EmissionReports.Remove(report);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmissionReportResponseDto>> GetAllAsync()
        {
            return await _context.EmissionReports
                .Include(r => r.UserProfile)
                .ThenInclude(up => up.IdentityUser)
                .Select(r => new EmissionReportResponseDto
                {
                    Id = r.Id,
                    UserId = r.UserId,
                    TotalEmissionsKg = r.TotalEmissionsKg,
                    PeriodStart = r.PeriodStart,
                    PeriodEnd = r.PeriodEnd,
                    Notes = r.Notes
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<EmissionReportResponseDto> GetByIdAsync(int id)
        {
            var report = await _context.EmissionReports
                .Include(r => r.UserProfile)
                .ThenInclude(up => up.IdentityUser)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (report == null)
                throw new KeyNotFoundException($"Report {id} not found");

            return new EmissionReportResponseDto
            {
                Id = report.Id,
                UserId = report.UserId,
                TotalEmissionsKg = report.TotalEmissionsKg,
                PeriodStart = report.PeriodStart,
                PeriodEnd = report.PeriodEnd,
                Notes = report.Notes
            };
        }

        public async Task<IEnumerable<EmissionReportResponseDto>> GetReportsByCriteriaAsync(string criteria)
        {
            if (string.IsNullOrWhiteSpace(criteria))
                return await GetAllAsync();

            return await _context.EmissionReports
                .Include(r => r.UserProfile)
                .ThenInclude(up => up.IdentityUser)
                .Where(r => r.UserProfile.IdentityUser.Email.Contains(criteria) ||
                           r.Notes.Contains(criteria) ||
                           r.TotalEmissionsKg.ToString().Contains(criteria))
                .Select(r => new EmissionReportResponseDto
                {
                    Id = r.Id,
                    UserId = r.UserId,
                    TotalEmissionsKg = r.TotalEmissionsKg,
                    PeriodStart = r.PeriodStart,
                    PeriodEnd = r.PeriodEnd,
                    Notes = r.Notes
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<EmissionReportResponseDto> UpdateAsync(int id, EmissionReportRequestDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var report = await _context.EmissionReports.FindAsync(id);
            if (report == null)
                throw new KeyNotFoundException($"Report {id} not found");

            // Update velden
            report.TotalEmissionsKg = dto.TotalEmissionsKg;
            report.PeriodStart = dto.PeriodStart;
            report.PeriodEnd = dto.PeriodEnd;
            report.Notes = dto.Notes;

            await _context.SaveChangesAsync();
            return await GetByIdAsync(id);
        }
    }
}