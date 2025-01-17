using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.BusinessLayer.Concrete
{
    public class CargoDetailManager : ICargoDetailService
    {
        private readonly ICargoDetailDal _cargoDetailDal;

        public CargoDetailManager(ICargoDetailDal cargoDetailDal)
        {
            _cargoDetailDal = cargoDetailDal;
        }

        public async Task TDelete(int id)
        {
             await _cargoDetailDal.Delete(id);
        }

        public async Task<List<CargoDetail>> TGetAllAsync()
        {
           return await _cargoDetailDal.GetAllAsync();
        }

        public async Task<CargoDetail> TGetByIdAsync(int id)
        {
            return await _cargoDetailDal.GetByIdAsync(id);
        }

        public async Task TInsert(CargoDetail entity)
        {
            await _cargoDetailDal.Insert(entity);
        }

        public async Task TUpdate(CargoDetail entity)
        {
            await _cargoDetailDal.Update(entity);
        }
    }
}
