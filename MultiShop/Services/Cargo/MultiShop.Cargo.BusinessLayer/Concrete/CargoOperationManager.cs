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
    public class CargoOperationManager : ICargoOperationService
    {
        private readonly ICargoOperationDal _cargoOperationDal;

        public CargoOperationManager(ICargoOperationDal cargoOperationDal)
        {
            _cargoOperationDal = cargoOperationDal;
        }

        public async Task TDelete(int id)
        {
            await _cargoOperationDal.Delete(id);
        }

        public async Task<List<CargoOperation>> TGetAllAsync()
        {
            return await _cargoOperationDal.GetAllAsync();
        }

        public async Task<CargoOperation> TGetByIdAsync(int id)
        {
            return await _cargoOperationDal.GetByIdAsync(id);
        }

        public async Task TInsert(CargoOperation entity)
        {
            await _cargoOperationDal.Insert(entity);
        }

        public async Task TUpdate(CargoOperation entity)
        {
            await _cargoOperationDal.Update(entity);
        }
    }
}
