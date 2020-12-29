using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnitBV_Biblioteq.Core.DomainModel;
using UnitBV_Biblioteq.Core.Repositories;
using UnitBV_Biblioteq.Persistence;

namespace UnitBV_Biblioteq_Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class DomainsTests
    {
        [TestMethod]
        public void AddDomainMockTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var mock = new Mock<IRepository<Domain>>();

                mock.Setup(m => m.Add(It.IsAny<Domain>())).Returns(true);

                var domain = new Domain();
                var obj = mock.Object;

                var result = obj.Add(domain);

                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void EditDomainMockTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var mock = new Mock<IDomainRepository>();

                mock.Setup(m => m.EditDomain(It.IsAny<Domain>())).Returns(true);

                var domain = new Domain();
                var obj = mock.Object;

                var result = obj.EditDomain(domain);

                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void DeleteDomainMockTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var mock = new Mock<IRepository<Domain>>();

                mock.Setup(m => m.Remove(It.IsAny<Domain>())).Returns(true);

                var domain = new Domain();
                var obj = mock.Object;

                var result = obj.Remove(domain);

                Assert.AreEqual(true, result);
            }
        }
    }
}
