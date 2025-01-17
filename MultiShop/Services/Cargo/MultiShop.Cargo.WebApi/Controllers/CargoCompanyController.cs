using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.CargoCompanyDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [AllowAnonymous]
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoCompaniesController : ControllerBase
    {
        private readonly ICargoCompanyService _cargoCompanyService;

        public CargoCompaniesController(ICargoCompanyService cargoCompanyService)
        {
            _cargoCompanyService = cargoCompanyService;
        }

        [HttpGet]
        public async Task<IActionResult> CargoCompanyList()
        {
            var values = await _cargoCompanyService.TGetAllAsync();
            return Ok(values); 
        }

        [HttpPost]
        public async Task<IActionResult> CreateCargoCompany(CreateCargoCompanyDto createCargoCompanyDto)
        {
            CargoCompany cargoCompany = new CargoCompany()
            {
                CargoCompanyName = createCargoCompanyDto.CargoCompanyName
            };
            await _cargoCompanyService.TInsert(cargoCompany);
            return Ok("Karqo şirkəti uğurla yaradıldı");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveCargoCompany(int id)
        {
           await _cargoCompanyService.TDelete(id); 
            return Ok("Karqo şirkəti uğurla silindi");
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetCargoCompanyById(int id)
        {
           var value = await _cargoCompanyService.TGetByIdAsync(id);
            return Ok(value);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCargoCompany(UpdateCargoCompanyDto updateCargoCompanyDto)
        {
            CargoCompany cargoCompany = new CargoCompany()
            {
                CargoCompanyId = updateCargoCompanyDto.CargoCompanyId,
                CargoCompanyName = updateCargoCompanyDto.CargoCompanyName
            };
            await _cargoCompanyService.TUpdate(cargoCompany);
            return Ok("Karqo şirkəti uğurla dəyişdirildi");
        }

    }
}
