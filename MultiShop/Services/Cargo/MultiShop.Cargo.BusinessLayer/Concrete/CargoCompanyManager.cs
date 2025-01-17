using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.EntityFramework;
using MultiShop.Cargo.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.BusinessLayer.Concrete
{
    public class CargoCompanyManager : ICargoCompanyService
    {
        private readonly ICargoCompanyDal _cargoCompanyDal;

        public CargoCompanyManager(ICargoCompanyDal cargoCompanyDal)
        {
            _cargoCompanyDal = cargoCompanyDal;
        }

        public async Task TDelete(int id)
        {
            await _cargoCompanyDal.Delete(id);
        }

        public async Task<List<CargoCompany>> TGetAllAsync()
        {
            return await _cargoCompanyDal.GetAllAsync();
        }

        public async Task<CargoCompany> TGetByIdAsync(int id)
        {
            return await _cargoCompanyDal.GetByIdAsync(id);
        }

        public async Task TInsert(CargoCompany entity)
        {
            await _cargoCompanyDal.Insert(entity);
        }

        public async Task TUpdate(CargoCompany entity)
        {
            await _cargoCompanyDal.Update(entity);
        }
    }
}
