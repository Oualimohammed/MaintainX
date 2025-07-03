using Pri.Ek2.Core.Data;
using Pri.Ek2.Core.Dtos.RequestDtos;
using Pri.Ek2.Core.Dtos.ResponseDtos;
using Pri.Ek2.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Services.Implementations
{
    public class LocationService : ILocationService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public LocationService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Task<LocationResponseDto> AddAsync(LocationRequestDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Location request DTO cannot be null.");
            }
            var location = new Entities.Location
            {
                Name = dto.Name,
                Address = dto.Address,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude
            };
            _applicationDbContext.Locations.Add(location);
            _applicationDbContext.SaveChanges();
            return Task.FromResult(new LocationResponseDto
            {
                Id = location.Id,
                Name = location.Name,
                Address = location.Address,
                Latitude = location.Latitude,
                Longitude = location.Longitude
            });
        }

        public Task DeleteAsync(int id)
        {
            var location = _applicationDbContext.Locations.Find(id);
            if (location == null)
            {
                throw new KeyNotFoundException($"Location with ID {id} not found.");
            }
            _applicationDbContext.Locations.Remove(location);
            _applicationDbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public Task<IEnumerable<LocationResponseDto>> GetAllAsync()
        {
            var locations = _applicationDbContext.Locations
                .Select(l => new LocationResponseDto
                {
                    Id = l.Id,
                    Name = l.Name,
                    Address = l.Address,
                    Latitude = l.Latitude,
                    Longitude = l.Longitude
                }).ToList();
            return Task.FromResult<IEnumerable<LocationResponseDto>>(locations);
        }

        public Task<LocationResponseDto> GetByIdAsync(int id)
        {
            var location = _applicationDbContext.Locations.Find(id);
            if (location == null)
            {
                throw new KeyNotFoundException($"Location with ID {id} not found.");
            }
            return Task.FromResult(new LocationResponseDto
            {
                Id = location.Id,
                Name = location.Name,
                Address = location.Address,
                Latitude = location.Latitude,
                Longitude = location.Longitude
            });
        }

        public Task<IEnumerable<LocationResponseDto>> GetLocationsByCriteriaAsync(string criteria)
        {
            if (string.IsNullOrEmpty(criteria))
            {
                throw new ArgumentException("Criteria cannot be null or empty.", nameof(criteria));
            }
            var locations = _applicationDbContext.Locations
                .Where(l => l.Name.Contains(criteria) || l.Address.Contains(criteria))
                .Select(l => new LocationResponseDto
                {
                    Id = l.Id,
                    Name = l.Name,
                    Address = l.Address,
                    Latitude = l.Latitude,
                    Longitude = l.Longitude
                }).ToList();
            return Task.FromResult<IEnumerable<LocationResponseDto>>(locations);
        }

        public Task<LocationResponseDto> UpdateAsync(int id, LocationRequestDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Location request DTO cannot be null.");
            }
            var location = _applicationDbContext.Locations.Find(id);
            if (location == null)
            {
                throw new KeyNotFoundException($"Location with ID {id} not found.");
            }
            // Update properties
            location.Name = dto.Name;
            location.Address = dto.Address;
            location.Latitude = dto.Latitude;
            location.Longitude = dto.Longitude;
            _applicationDbContext.SaveChanges();
            return Task.FromResult(new LocationResponseDto
            {
                Id = location.Id,
                Name = location.Name,
                Address = location.Address,
                Latitude = location.Latitude,
                Longitude = location.Longitude
            });
        }
    }
}
