using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RentACarContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using (RentACarContext context = new())
            {
                return (from c in context.Cars
                        join b in context.Brands
                        on c.BrandId equals b.Id
                        join color in context.Colors
                        on c.ColorId equals color.Id
                        select new CarDetailDto { CarId = c.Id, BrandName = b.Name, ColorName = color.Name, DailyPrice = c.DailyPrice })
                        .ToList();
            }
        }
    }
}
