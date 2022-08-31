using Business.Concrete;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new InMemoryCarDal());

            var getCar = carManager.GetById(1);
            Console.WriteLine(getCar.DailyPrice + " AZN");
            
            foreach (var car in carManager.GetAll())
            {
                Console.WriteLine($"\nId: {car.Id}\nModel year: {car.ModelYear}\nDescription: {car.Description}\nDaily price: {car.DailyPrice} AZN");
            }



        }
    }
}
