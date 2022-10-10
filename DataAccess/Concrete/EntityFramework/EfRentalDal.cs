using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, RentACarContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails()
        {
            using (RentACarContext context = new())
            {
                return (from r in context.Rentals
                        join c in context.Cars
                        on r.CarId equals c.Id
                        join b in context.Brands
                        on c.BrandId equals b.Id
                        join customer in context.Customers
                        on r.CustomerId equals customer.Id
                        join user in context.Users
                        on customer.UserId equals user.Id
                        select new RentalDetailDto { RentalId = r.Id, BrandName = b.Name, CustomerFullName = user.FirstName + " " + user.LastName, RentDate = r.RentDate, ReturnDate = r.ReturnDate }).ToList();

            }
        }
    }
}
