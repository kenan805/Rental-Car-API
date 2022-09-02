using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private readonly ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public void Add(Car car)
        {
            if (car.Brand.Name.Length < 2 || car.DailyPrice <= 0)
            {
                Console.WriteLine("Enter the information correctly !!!\n- Brand name must be at least two in length!\n- Daily price must be greater than zero!");
            }
            else
            {
                _carDal.Add(car);
            }
        }

        public void Delete(Car car) => _carDal.Delete(car);

        public List<Car> GetAll() => _carDal.GetAll();

        public Car GetById(int id) => _carDal.Get(c => c.Id == id);

        public List<Car> GetCarsByBrandId(int id) => _carDal.GetAll(c => c.BrandId == id);

        public List<Car> GetCarsByColorId(int id) => _carDal.GetAll(c => c.ColorId == id);

        public void Update(Car car) => _carDal.Update(car);
    }
}
