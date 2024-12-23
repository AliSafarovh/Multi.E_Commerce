using Dapper;
using MultiShop.Discount.Context;
using MultiShop.Discount.Dtos;

namespace MultiShop.Discount.Services;

public class DiscountService : IDiscountService
{
    private readonly DapperContext _dapperContext;

    public DiscountService(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task CreateDiscountCouponAsync(CreateDiscountCouponDto createCouponDto)
    {

        string query = "insert into Coupons (Code,Rate,IsActive,ValidDate) values (@code,@rate,@isActive,@validDate)";
        var parameters = new DynamicParameters();
        parameters.Add("@code", createCouponDto.Code);
        parameters.Add("@rate", createCouponDto.Rate);
        parameters.Add("@isActive", createCouponDto.IsActive);
        parameters.Add("@validDate", createCouponDto.ValidDate);

        using (var connection = _dapperContext.CreateConnection())
        {
            await connection.ExecuteAsync(query, parameters);
        }
    }

    public async Task DeleteDiscountCouponAsync(int couponId)
    {
        string query = "Delete From Coupons where CouponId=@couponId";
        var parameters = new DynamicParameters();
        parameters.Add("couponId", couponId);
        using var connection = _dapperContext.CreateConnection();
        {
            await connection.ExecuteAsync(query, parameters); 
        }
    }

    public async Task<List<ResultDiscountCouponDto>> GetAllDiscountCouponsAsync()
    {
        string query = "Select * From Coupons";
        using(var connection = _dapperContext.CreateConnection())
        {
            var values = await connection.QueryAsync<ResultDiscountCouponDto>(query);
            return values.ToList();
        }
    }

    public async Task<GetByIdDiscountCouponDto> GetByIdDiscountCouponAsync(int couponId)
    {
        string query = "Select * From Coupons Where CouponId=@couponId";
        var paramaters = new DynamicParameters();
        paramaters.Add("@couponId", couponId);
        using(var connection = _dapperContext.CreateConnection())
        {
            var values = await connection.QueryFirstOrDefaultAsync<GetByIdDiscountCouponDto>(query,paramaters);
            return values;
        }
    }

    public async Task UpdateDiscountCouponAsync(UpdateDiscountCouponDto updateCouponDto)
    {
        string query = "Update Coupons Set Code=@code,Rate=@rate,IsActive=@isActive,ValidDate=@validDate where CouponId=@couponId";
            var parameters = new DynamicParameters();
        parameters.Add("@code", updateCouponDto.Code);
        parameters.Add("@rate", updateCouponDto.Rate);
        parameters.Add("@isActive", updateCouponDto.IsActive);
        parameters.Add("@validDate", updateCouponDto.ValidDate);
        parameters.Add("@couponId", updateCouponDto.CouponId);
        using (var connection = _dapperContext.CreateConnection())
        {
            await connection.ExecuteAsync(query, parameters);
        }
    }
}
