using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
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

        [CacheAspect]
        [PerformanceAspect(3)]
        public IDataResult<List<Car>> GetAll()
            => new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarListed);

        [CacheAspect]
        [PerformanceAspect(3)]
        public IDataResult<Car> GetById(int id)
            => new SuccessDataResult<Car>(_carDal.Get(c => c.Id == id));

        public IDataResult<List<CarDetailDto>> GetCarDetails()
            => new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
            => new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == id));

        public IDataResult<List<Car>> GetCarsByColorId(int id)
            => new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == id));

        //[SecuredOperation("car.add,admin")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        [TransactionScopeAspect]
        public IResult Add(Car car)
        {
            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);
        }

        [CacheRemoveAspect("ICarService.Get")]
        [TransactionScopeAspect]
        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }

        [CacheRemoveAspect("ICarService.Get")]
        [TransactionScopeAspect]
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);
        }
    }
}
