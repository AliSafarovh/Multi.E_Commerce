using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.CargoOperationDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoOperationsController : ControllerBase
    {
        private readonly ICargoOperationService _cargoOperationService;

        public CargoOperationsController(ICargoOperationService cargoOperationService)
        {
            _cargoOperationService = cargoOperationService;
        }

        [HttpGet]
        public async Task<IActionResult> CargoOperationList()
        {
            var values = await _cargoOperationService.TGetAllAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCargoOperation(CreateCargoOperationDto createCargoOperationDto)
        {
            CargoOperation cargoOperation = new CargoOperation()
            {
                Barcode = createCargoOperationDto.Barcode,
                Description = createCargoOperationDto.Description,
                OperationDate = createCargoOperationDto.OperationDate
            };
            await _cargoOperationService.TInsert(cargoOperation);
            return Ok("Karqo prosesi uğurla yaradıldı");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveCargoOperation(int id)
        {
            await _cargoOperationService.TDelete(id);
            return Ok("Karqo prosesi uğurla silindi");
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetCargoOperationById(int id)
        {
            var value = await _cargoOperationService.TGetByIdAsync(id);
            return Ok(value);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCargoOperation(UpdateCargoOperationDto updateCargoOperationDto)
        {
            CargoOperation cargoOperation = new CargoOperation()
            {
                CargoOperationId = updateCargoOperationDto.CargoOperationId,
                Barcode = updateCargoOperationDto.Barcode,
                Description = updateCargoOperationDto.Description,
                OperationDate = updateCargoOperationDto.OperationDate
            };
            await _cargoOperationService.TUpdate(cargoOperation);
            return Ok("Karqo prosesi uğurla dəyişdirildi");
        } 
    }
}
