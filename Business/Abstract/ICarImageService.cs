using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IResult Add(IFormFile formFile, CarImage carImage, string rootPath);
        IDataResult<List<CarImage>> GetAll();
        IResult Update(IFormFile formFile, CarImage carImage, string rootPath);
        IResult Delete(CarImage carImage, string rootPath);
        IDataResult<List<CarImage>> GetCarImagesByCarId(int carId);
    }
}
