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
    public class CargoCustomerManager : ICargoCustomerService
    {
        private readonly ICargoCustomerDal _cargoCustomerDal;

        public CargoCustomerManager(ICargoCustomerDal cargoCustomerDal)
        {
            _cargoCustomerDal = cargoCustomerDal;
        }
        public async Task TDelete(int id)
        {
            await _cargoCustomerDal.Delete(id);
        }

        public async Task<List<CargoCustomer>> TGetAllAsync()
        {
            return await _cargoCustomerDal.GetAllAsync();
        }

        public async Task<CargoCustomer> TGetByIdAsync(int id)
        {
            return await _cargoCustomerDal.GetByIdAsync(id);
        }

        public async Task TInsert(CargoCustomer entity)
        {
            await _cargoCustomerDal.Insert(entity);
        }

        public async Task TUpdate(CargoCustomer entity)
        {
            await _cargoCustomerDal.Update(entity);
        }
    }
}
