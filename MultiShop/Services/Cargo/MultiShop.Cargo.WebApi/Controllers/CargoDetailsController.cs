using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.CargoDetailDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoDetailsController : ControllerBase
    {
        private readonly ICargoDetailService _cargoDetailService;

        public CargoDetailsController(ICargoDetailService cargoDetailService)
        {
            _cargoDetailService = cargoDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> CargoDetailList()
        {
            var values = await _cargoDetailService.TGetAllAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCargoDetail(CreateCargoDetailDto createCargoDetailDto)
        {
            CargoDetail cargoDetail = new CargoDetail()
            {
                Barcode = createCargoDetailDto.Barcode,
                CargoCompanyId = createCargoDetailDto.CargoCompanyId,
                ReceiverCustomer = createCargoDetailDto.SenderCustomer, 
                SenderCustomer = createCargoDetailDto.SenderCustomer
            };
            await _cargoDetailService.TInsert(cargoDetail);
            return Ok("Karqo detalları uğurla yaradıldı");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveCargoDetail(int id)
        {
            await _cargoDetailService.TDelete(id);
            return Ok("Karqo detalları uğurla silindi");
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetCargoDetailById(int id)
        {
            var value = await _cargoDetailService.TGetByIdAsync(id);
            return Ok(value);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCargoDetail(UpdateCargoDetailDto updateCargoDetailDto)
        {
            CargoDetail cargoDetail = new CargoDetail()
            {
                CargoDetailId = updateCargoDetailDto.CargoDetailId,
                Barcode = updateCargoDetailDto.Barcode,
                CargoCompanyId = updateCargoDetailDto.CargoCompanyId,
                ReceiverCustomer = updateCargoDetailDto.SenderCustomer,
                SenderCustomer = updateCargoDetailDto.SenderCustomer,
            };
            await _cargoDetailService.TUpdate(cargoDetail);
            return Ok("Karqo detalları uğurla dəyişdirildi");
        }
    }
}
