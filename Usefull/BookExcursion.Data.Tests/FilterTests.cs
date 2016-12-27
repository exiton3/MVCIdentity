using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Arch.Framework.DataAccess.Infrastructure;
using BookExcursion.Data.Repositories;
using BookExcursion.Domain.Model;
using NUnit.Framework;

namespace BookExcursion.Data.Tests
{
    [TestFixture]
    public class FilterTests
    {

        [Test]
        public void Filters()
        {
            IFilterData filterData = new FilterData();

            
        }
        [Test]
        public void TestName()
        {
            var unitOfWork = new UnitOfWork(new DbContextFactory());
            UserRepository userRepository = new UserRepository(unitOfWork);
            Console.WriteLine( unitOfWork.Context.Database.Connection.ConnectionString);
            HotelRepository repository = new HotelRepository(unitOfWork);

            var name = "COCO BEACH RESORT 3*";
           // var specification = new SimpleSpecification<Hotel>(name);
            var specification = new MultiSpecification<Hotel>(name);

            specification.Property = "Name";

            var hotels = repository.Find(specification);

            foreach (var hotel in hotels)
            {
                Console.WriteLine("{0} - {1}", hotel.Name, hotel.Id);
            }
            Assert.That(2, Is.EqualTo(2));
        }
    }
}
