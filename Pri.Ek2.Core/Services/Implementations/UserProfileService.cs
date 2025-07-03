using Pri.Ek2.Core.Data;
using Pri.Ek2.Core.Dtos.RequestDtos;
using Pri.Ek2.Core.Dtos.ResponseDtos;
using Pri.Ek2.Core.Entities;
using Pri.Ek2.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Services.Implementations
{
    public class UserProfileService : IUserProfileService
    {
        private readonly ApplicationDbContext _applicationDbContext;
    
        public UserProfileService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Task<UserProfileResponseDto> CreateUserProfileAsync(string userId, UserProfileRequestDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "User profile data cannot be null.");
            }
            var userProfile = new UserProfile
            {
                UserId = userId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthData = dto.BirthData,
                ProfileImagePath = dto.ProfileImagePath
            };
            
            _applicationDbContext.UserProfiles.Add(userProfile);
            _applicationDbContext.SaveChanges();

            return Task.FromResult(new UserProfileResponseDto
            {
                Id = userProfile.Id,
                UserId = userProfile.UserId,
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                BirthData = userProfile.BirthData,
                ProfileImagePath = userProfile.ProfileImagePath
            });
        }

        public Task<bool> DeleteUserProfileAsync(string userId)
        {
            var userProfile = _applicationDbContext.UserProfiles.FirstOrDefault(up => up.UserId == userId);
            if (userProfile == null)
            {
                throw new KeyNotFoundException($"User profile with UserId {userId} not found.");
            }
            _applicationDbContext.UserProfiles.Remove(userProfile);
            _applicationDbContext.SaveChanges();
            return Task.FromResult(true);
        }

        public Task<IEnumerable<UserProfileResponseDto>> GetAllUserProfilesAsync()
        {
            var userProfiles = _applicationDbContext.UserProfiles
                .Select(up => new UserProfileResponseDto
                {
                    Id = up.Id,
                    UserId = up.UserId,
                    FirstName = up.FirstName,
                    LastName = up.LastName,
                    BirthData = up.BirthData,
                    ProfileImagePath = up.ProfileImagePath
                }).ToList();
            return Task.FromResult<IEnumerable<UserProfileResponseDto>>(userProfiles);
        }

        public Task<UserProfileResponseDto?> GetUserProfileAsync(string userId)
        {
            var userProfile = _applicationDbContext.UserProfiles
                .Where(up => up.UserId == userId)
                .Select(up => new UserProfileResponseDto
                {
                    Id = up.Id,
                    UserId = up.UserId,
                    FirstName = up.FirstName,
                    LastName = up.LastName,
                    BirthData = up.BirthData,
                    ProfileImagePath = up.ProfileImagePath
                }).FirstOrDefault();
            return Task.FromResult(userProfile);
        }

        public Task<UserProfileResponseDto> UpdateUserProfileAsync(string userId, UserProfileRequestDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "User profile data cannot be null.");
            }
            var userProfile = _applicationDbContext.UserProfiles.FirstOrDefault(up => up.UserId == userId);
            if (userProfile == null)
            {
                throw new KeyNotFoundException($"User profile with UserId {userId} not found.");
            }
            userProfile.FirstName = dto.FirstName;
            userProfile.LastName = dto.LastName;
            userProfile.BirthData = dto.BirthData;
            userProfile.ProfileImagePath = dto.ProfileImagePath;
            _applicationDbContext.SaveChanges();
            return Task.FromResult(new UserProfileResponseDto
            {
                Id = userProfile.Id,
                UserId = userProfile.UserId,
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                BirthData = userProfile.BirthData,
                ProfileImagePath = userProfile.ProfileImagePath
            });
        }
    }
}
