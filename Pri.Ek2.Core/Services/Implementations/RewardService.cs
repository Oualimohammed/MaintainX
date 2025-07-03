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
    public class RewardService : IRewardService
    {

        private readonly ApplicationDbContext _applicationDbContext;

        public RewardService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Task<RewardResponseDto> AddAsync(RewardRequestDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Reward request DTO cannot be null.");
            }
            var reward = new Reward
            {
                Name = dto.Name,
                Description = dto.Description,
                IconPath = dto.IconPath,
            };
            _applicationDbContext.Rewards.Add(reward);
            _applicationDbContext.SaveChanges();
            return Task.FromResult(new RewardResponseDto
            {
                Id = reward.Id,
                Name = reward.Name,
                Description = reward.Description,
                IconPath = reward.IconPath
            });
        }

        public Task DeleteAsync(int id)
        {
            var reward = _applicationDbContext.Rewards.Find(id);
            if (reward == null)
            {
                throw new KeyNotFoundException($"Reward with ID {id} not found.");
            }
            _applicationDbContext.Rewards.Remove(reward);
            _applicationDbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public Task<IEnumerable<RewardResponseDto>> GetAllAsync()
        {
            var rewards = _applicationDbContext.Rewards.Select(r => new RewardResponseDto
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                IconPath = r.IconPath
            }).ToList();
            return Task.FromResult<IEnumerable<RewardResponseDto>>(rewards);
        }

        public Task<RewardResponseDto> GetByIdAsync(int id)
        {
            var reward = _applicationDbContext.Rewards.Find(id);
            if (reward == null)
            {
                throw new KeyNotFoundException($"Reward with ID {id} not found.");
            }
            return Task.FromResult(new RewardResponseDto
            {
                Id = reward.Id,
                Name = reward.Name,
                Description = reward.Description,
                IconPath = reward.IconPath
            });
        }

        public Task<IEnumerable<RewardResponseDto>> GetRewardsByCriteriaAsync(string criteria)
        {
            var rewards = _applicationDbContext.Rewards
                .Where(r => r.Name.Contains(criteria) || r.Description.Contains(criteria))
                .Select(r => new RewardResponseDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    IconPath = r.IconPath
                }).ToList();
            return Task.FromResult<IEnumerable<RewardResponseDto>>(rewards);
        }

        public Task<RewardResponseDto> UpdateAsync(int id, RewardRequestDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Reward request DTO cannot be null.");
            }
            var reward = _applicationDbContext.Rewards.Find(id);
            if (reward == null)
            {
                throw new KeyNotFoundException($"Reward with ID {id} not found.");
            }
            reward.Name = dto.Name;
            reward.Description = dto.Description;
            reward.IconPath = dto.IconPath;
            _applicationDbContext.SaveChanges();
            return Task.FromResult(new RewardResponseDto
            {
                Id = reward.Id,
                Name = reward.Name,
                Description = reward.Description,
                IconPath = reward.IconPath
            });
        }
    }
}
