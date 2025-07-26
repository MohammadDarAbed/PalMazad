
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public interface IUserRepository
    {
        Task<List<UserEntity>> GetAllUsersAsync();
        Task<UserEntity> GetByIdAsync(int id);
        Task CreateUserAsync(UserEntity entity);
        Task<UserEntity> UpdateUserAsync(UserEntity entity);
        Task DeleteUserAsync(int id);
        Task<bool> IsExistAsync(Expression<Func<UserEntity, bool>> filters);

    }
    public class UserRepository : IUserRepository
    {
        private readonly IRepository<UserEntity> _userRepo;
        public UserRepository(IRepository<UserEntity> repo) 
        {
            _userRepo = repo;
        }

        public async Task<List<UserEntity>> GetAllUsersAsync()
        {
            var users = await _userRepo.GetItemsAsync();
            return users;
        }

        public async Task<UserEntity> GetByIdAsync(int id)
        {
            return await _userRepo.GetByIdAsync(id);
        }

        public async Task CreateUserAsync(UserEntity entity)
        {
            await _userRepo.CreateAsync(entity);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepo.DeleteAsync(id);
        }

        public async Task<UserEntity> UpdateUserAsync(UserEntity entity)
        {
           return await _userRepo.UpdateAsync(entity);
        }

        public async Task<bool> IsExistAsync(Expression<Func<UserEntity, bool>> filters)
        {
            return await _userRepo.ExistsAsync(new[] { filters });
        }
    }
}
