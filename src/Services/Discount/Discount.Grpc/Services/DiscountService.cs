using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger)
        :DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {

            var coupen= await dbContext.Coupen.FirstOrDefaultAsync(c=>c.ProductName==request.ProductName);

            if (coupen == null)
            {
                coupen=new Models.Coupen { ProductName = "No Discount" , Amount=0, Description="No Discount desc"};  
            }
            logger.LogInformation("Dicount is retrieved for ProductName: {productName}, Amount: {amount}", coupen.ProductName, coupen.Amount);

            var coupenModel = coupen.Adapt<CouponModel>();

            return coupenModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupen= request.Coupon.Adapt<Models.Coupen>();

            if (coupen == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));
            }

            dbContext.Coupen.Add(coupen);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Coupen was successfully created for ProductName: {ProductName}", coupen.ProductName);

            return coupen.Adapt<CouponModel>();

        }

        public async override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupen = request.Coupon.Adapt<Models.Coupen>();

            if (coupen == null)
            {
                
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));
            }
            var coupenInDB= await dbContext.Coupen.FirstOrDefaultAsync(c => c.ProductName == coupen.ProductName);

            if (coupenInDB == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Product Not Found"));
            }

            dbContext.Coupen.Update(coupen);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Coupen was successfully updated for ProductName: {ProductName}", coupen.ProductName);

            return coupen.Adapt<CouponModel>();
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupen= await dbContext.Coupen.FirstOrDefaultAsync(c=>c.ProductName == request.ProductName);

            if (coupen == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Product Not Found"));
            }

            dbContext.Coupen.Remove(coupen);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Coupen was successfully deleted for ProductName: {ProductName}", request.ProductName);

            return new DeleteDiscountResponse{Success=true };
        }
    }
}
