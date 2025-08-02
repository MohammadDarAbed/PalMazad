
using Business.CartShopping;
using Business.Products;
using Business.Users;
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
                    Product = new ProductModelDto
                    {
                        Id = item.Product.Id,
                        Name = item.Product.Name,
                        Price = item.Product.Price,
                        Seller = new UserModelDto
                        {
                            Id = item.Product.Seller.Id,
                            Name = item.Product.Seller.Name,
                            PhoneNumber = item.Product.Seller.PhoneNumber,
                        },
                    },
                    Quantity = item.Quantity,
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
