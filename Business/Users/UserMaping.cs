
using DataAccess.Entities;
using DataAccess.Models;

namespace Business.Users
{
    public static class UserMaping
    {
        public static UserEntity? MapModelToEntity(this UserModel userModel)
        {
            if (userModel == null) return null;
            var userEntity = new UserEntity
            {
                Email = userModel.Email,
                Name = userModel.Name,
                IsIdentityHidden = userModel.IsIdentityHidden,
                IsSeller = userModel.IsSeller,
                ModifiedOn = userModel.ModifiedOn,
                IsVerifiedSeller = userModel.IsVerifiedSeller,  
                PhoneNumber = userModel.PhoneNumber,                
                CreatedBy = "-",
                ModifiedBy = "-",
                CreatedOn = DateTimeOffset.Now,
            };
            return userEntity;
        }

        public static UserModelBo? MapEntityToBo(this UserEntity userEntity)
        {
            if (userEntity == null) return null;
            var userBo = new UserModelBo
            {
                Id = userEntity.Id,
                Name = userEntity.Name,
                PhoneNumber = userEntity.PhoneNumber,
                IsVerifiedSeller = userEntity.IsVerifiedSeller,
                IsSeller = userEntity.IsSeller,
                IsIdentityHidden = !userEntity.IsIdentityHidden,
                Email = userEntity.Email,
                IsDeleted = userEntity.IsDeleted,
                CreatedBy= userEntity.CreatedBy,
                CreatedOn = userEntity.CreatedOn,
                ModifiedBy= userEntity.ModifiedBy,
                ModifiedOn = userEntity.ModifiedOn
            };
            return userBo;
        }
    }
}
