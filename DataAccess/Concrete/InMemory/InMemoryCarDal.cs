using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;
        public InMemoryCarDal()
        {
            _cars = new List<Car>
            {
                new Car{ Id=1, BrandId=1, ColorId=1, DailyPrice = 60, ModelYear = 2016, Description="Description 1" },
                new Car{ Id=2, BrandId=3, ColorId=3, DailyPrice = 70, ModelYear = 2015, Description="Description 2" },
                new Car{ Id=3, BrandId=2, ColorId=3, DailyPrice = 85, ModelYear = 2018, Description="Description 3" },
                new Car{ Id=4, BrandId=2, ColorId=1, DailyPrice = 299, ModelYear = 2019, Description="Description 4" },
                new Car{ Id=5, BrandId=5, ColorId=5, DailyPrice = 120, ModelYear = 2018, Description="Description 5" },
            };
        }
        public void Add(Car car) => _cars.Add(car);

        public void Delete(Car car)
        {
            var deletedCar = _cars.FirstOrDefault(c => c.Id == car.Id);
            _cars.Remove(deletedCar);
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll() => _cars;

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Car GetById(int id) => _cars.FirstOrDefault(c => c.Id == id);

        public List<CarDetailDto> GetCarDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Car car)
        {
            var updatedCar = _cars.FirstOrDefault(c => c.Id == car.Id);
            updatedCar.BrandId = car.BrandId;
            updatedCar.ColorId = car.ColorId;
            updatedCar.DailyPrice = car.DailyPrice;
            updatedCar.Description = car.Description;
            updatedCar.ModelYear = car.ModelYear;
        }
    }
}
