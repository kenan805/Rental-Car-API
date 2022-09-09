using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CustomerManager customerManager = new(new EfCustomerDal());
            UserManager userManager = new(new EfUserDal());
            RentalManager rentalManager = new(new EfRentalDal());

            //userManager.Add(new()
            //{
            //    FirstName = "Kenan",
            //    LastName = "Idayatov",
            //    Email = "kenan1@gmail.com",
            //    Password = "kenan123"
            //});

            //customerManager.Add(new()
            //{
            //    UserId = 1,
            //    CompanyName = "AzInTelecom"
            //});

            rentalManager.Add(new()
            {
                CustomerId = 1,
                CarId = 2,
                RentDate = DateTime.Parse("12/09/2022"),
                ReturnDate = DateTime.Parse("14/09/2022")
            });






        }
    }
}
