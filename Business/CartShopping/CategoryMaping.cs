
using Business.CartShopping;
using DataAccess.Entities;
using DataAccess.Models;

namespace Business.Carts
{
    public static class CartMaping
    {
        public static CartEntity? MapModelToEntity(this CartModel cartModel)
        {
            if (cartModel == null) return null;
            var cartEntity = new CartEntity
            {
                UserId = cartModel.UserId,
                CreatedBy = "-",
                ModifiedBy = "-",
                CreatedOn = DateTimeOffset.Now,
            };
            return cartEntity;
        }

        public static CartModelBo? MapEntityToBo(this CartEntity cartEntity) 
        {
            if (cartEntity == null) return null;
            var cartBo = new CartModelBo
            {
                Id = cartEntity.Id,
                Items = cartEntity.Items.Select(item => new CartItemModelBo
                {
                    CartId = item.CartId,
                    Quantity = item.Quantity,
                    ProductId = item.Product?.Id ?? 0,
                    ProductName = item.Product?.Name ?? string.Empty
                }).ToList(),
                //UserId = cartEntity.User.Id,
                UserId = 1, // TODO: userId
                IsDeleted = cartEntity.IsDeleted,
                CreatedBy= cartEntity.CreatedBy,
                CreatedOn = cartEntity.CreatedOn,
                ModifiedBy= cartEntity.ModifiedBy,
                ModifiedOn = cartEntity.ModifiedOn
            };
            return cartBo;
        }
    }
}
