using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
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


        public IDataResult<List<Car>> GetAll()
            => new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarListed);

        public IDataResult<Car> GetById(int id)
            => new SuccessDataResult<Car>(_carDal.Get(c => c.Id == id));

        public IDataResult<List<CarDetailDto>> GetCarDetails()
            => new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
            => new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == id));

        public IDataResult<List<Car>> GetCarsByColorId(int id)
            => new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == id));

        public IResult Add(Car car)
        {
            if (car.Brand.Name.Length < 2 || car.DailyPrice <= 0)
                return new ErrorResult(Messages.CarInfoInvalid);
            else
            {
                _carDal.Add(car);
                return new SuccessResult(Messages.CarAdded);
            }
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }

        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);
        }
    }
}
