using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private readonly ICarImageDal _carImageDal;
        private readonly IFileHelper _fileHelper;

        public CarImageManager(ICarImageDal carImageDal, IFileHelper fileHelper)
        {
            _carImageDal = carImageDal;
            _fileHelper = fileHelper;
        }

        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Add(IFormFile formFile, CarImage carImage, string rootPath)
        {
            var result = BusinessRules.Run(CheckIfCarImageLimitExceded(carImage.CarId));

            if (result != null)
                return result;

            _fileHelper.Add(formFile, Path.Combine(rootPath, PathConstants.ImagesPath));
            carImage.Date = DateTime.Now;
            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.CarImageAdded);
        }

        public IResult Delete(CarImage carImage, string rootPath)
        {
            _fileHelper.Delete(Path.Combine(Path.Combine(rootPath, PathConstants.ImagesPath), carImage.ImagePath));
            _carImageDal.Delete(carImage);
            return new SuccessResult(Messages.CarImageDeleted);
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            var result = _carImageDal.GetAll();
            return new SuccessDataResult<List<CarImage>>(result, Messages.CarImageListed);
        }

        public IDataResult<List<CarImage>> GetCarImagesByCarId(int carId)
        {
            var result = BusinessRules.Run(CheckIfCarImageAvailable(carId));

            if (result != null)
                return new SuccessDataResult<List<CarImage>>(CreateDefaultCarImage().Data);

            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(image => image.CarId == carId));
        }

        public IResult Update(IFormFile formFile, CarImage carImage, string rootPath)
        {
            _fileHelper.Update(formFile, carImage.ImagePath, Path.Combine(rootPath, PathConstants.ImagesPath));
            carImage.Date = DateTime.Now;
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.CarImageUpdated);
        }

        private IResult CheckIfCarImageLimitExceded(int carId)
        {
            var result = _carImageDal.GetAll(image => image.CarId == carId).Count;

            if (result > 5)
                return new ErrorResult(Messages.CarImageLimitExceded);

            return new SuccessResult();
        }

        private IResult CheckIfCarImageAvailable(int carId)
        {
            var result = _carImageDal.GetAll(image => image.CarId == carId).Count;

            if (result == 0)
                return new ErrorResult();

            return new SuccessResult();
        }

        private IDataResult<List<CarImage>> CreateDefaultCarImage()
        {
            List<CarImage> carImages = new()
            {
                new() { Date = DateTime.Now, ImagePath = "default-car.jpg" }
            };
            return new SuccessDataResult<List<CarImage>>(carImages);
        }



    }
}
