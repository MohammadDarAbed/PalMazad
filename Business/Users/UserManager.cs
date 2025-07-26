
using Business.Shared;
using DataAccess.Models;
using DataAccess.Repositories;

namespace Business.Users
{
    public interface IUserManager
    {
        Task<List<UserModelBo>> GetUsers();
        Task<UserModelBo> GetUserById(int userId);
        Task<UserModelBo> CreateUser(UserModel userModel);
        Task<UserModelBo> UpdateUser(int userId, UserModel userModel);
        Task DeleteUser(int id);
    }

    public class UserManager(IUserRepository _userRepo) : IUserManager
    {        
        public async Task<List<UserModelBo>> GetUsers()
        {
            var users = await _userRepo.GetAllUsersAsync();
            return users.Select(p => p.MapEntityToBo()).ToList();

        }

        public async Task<UserModelBo> GetUserById(int userId)
        {
            await CheckIfNotExists(userId);
            var user = await _userRepo.GetByIdAsync(userId);
            Validations.CheckIfEntityDeleted(user.IsDeleted, userId, "User");
            return user.MapEntityToBo();
        }

        public async Task<UserModelBo> CreateUser(UserModel userModel)
        {
            var entity = userModel.MapModelToEntity();
            await _userRepo.CreateUserAsync(entity);
            return entity.MapEntityToBo();
        }

        public async Task DeleteUser(int id)
        {
            await _userRepo.DeleteUserAsync(id);
        }

        public async Task<UserModelBo> UpdateUser(int userId, UserModel userModel)
        {
            await CheckIfNotExists(userId);
            var entity = userModel.MapModelToEntity();
            entity.Id = userId;
            var updatedUser = await _userRepo.UpdateUserAsync(entity);
            return updatedUser.MapEntityToBo();
        }

        // Helpers:

        private async Task CheckIfNotExists(int id)
        {
            var isExist = await _userRepo.IsExistAsync(f => f.Id == id);
            if (!isExist)
            {
                // if record delete
                ExceptionManager.ThrowItemNotFoundException("User", id);
            }
        }
    }
}
