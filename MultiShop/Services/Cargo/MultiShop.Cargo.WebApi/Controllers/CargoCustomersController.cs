using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.CargoCustomerDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoCustomersController : ControllerBase
    {
        private readonly ICargoCustomerService _cargoCustomerService;

        public CargoCustomersController(ICargoCustomerService cargoCustomerService)
        {
            _cargoCustomerService = cargoCustomerService;
        }

        [HttpGet]
        public async Task<IActionResult> CargoCustomerList()
        {
            var values = await _cargoCustomerService.TGetAllAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCargoCustomer(CreateCargoCustomerDto createCargoCustomerDto)
        {
            CargoCustomer cargoCustomer = new CargoCustomer()
            {
                Address = createCargoCustomerDto.Address,
                City = createCargoCustomerDto.City,
                District = createCargoCustomerDto.District,
                Email = createCargoCustomerDto.Email,
                Name = createCargoCustomerDto.Name,
                Phone = createCargoCustomerDto.Phone,
                Surname = createCargoCustomerDto.Surname
            };
            await _cargoCustomerService.TInsert(cargoCustomer);
            return Ok("Karqo müştəri əlavə etmə əməliyyatı uğurla edildi");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveCargoCustomer(int id)
        {
            await _cargoCustomerService.TDelete(id);
            return Ok("Karqo müştəri silmə əməliyyatı uğurla edildi");
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetCargoCustomerById(int id)
        {
            var value = await _cargoCustomerService.TGetByIdAsync(id);
            return Ok(value);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCargoCustomer(UpdateCargoCustomerDto updateCargoCustomerDto)
        {
            CargoCustomer cargoCustomer = new CargoCustomer()
            {
                CargoCustomerId = updateCargoCustomerDto.CargoCustomerId,
                Address = updateCargoCustomerDto.Address,
                Surname = updateCargoCustomerDto.Surname,
                Phone = updateCargoCustomerDto.Phone,
                Name = updateCargoCustomerDto.Name,
                Email = updateCargoCustomerDto.Email,
                District = updateCargoCustomerDto.District,
                City = updateCargoCustomerDto.City
            };
            await _cargoCustomerService.TUpdate(cargoCustomer);
            return Ok("Karqo müştəri əlavə etmə əməliyyatı uğurla edildi");
        }
    }
}
