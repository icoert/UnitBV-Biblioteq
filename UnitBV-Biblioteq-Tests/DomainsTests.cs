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

        [TestMethod]
        public void AddDomainTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var domain = new Domain
                {
                    Name = "DomainTestCase"
                };

                var result = unitOfWork.Domains.Add(domain);
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void AddEmptyDomainTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var domain = new Domain();

                var result = unitOfWork.Domains.Add(domain);
                Assert.AreEqual(false, result);
            }
        }
        
        [TestMethod]
        public void AddNullDomainTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var result = unitOfWork.Domains.Add(null);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddDomainWithInvalidNameTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var domain = new Domain
                {
                    Name = "d"
                };

                var result = unitOfWork.Domains.Add(domain);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddDomainWithEmptyNameTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var domain = new Domain
                {
                    Name = ""
                };

                var result = unitOfWork.Domains.Add(domain);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddDomainWithInvalidParentTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var parent = new Domain();
                var domain = new Domain
                {
                    Name = "DomainTestCase",
                    Parent = parent
                };

                var result = unitOfWork.Domains.Add(domain);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditDomainNameTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var domain = new Domain
                {
                    Name = "DomainTestCase"
                };

                unitOfWork.Domains.Add(domain);
                domain.Name = "NewDomainTestCase";
                
                var result = unitOfWork.Domains.EditDomain(domain);
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void EditDomainNameToInvalidTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var domain = new Domain
                {
                    Name = "DomainTestCase"
                };

                unitOfWork.Domains.Add(domain);
                domain.Name = "d";

                var result = unitOfWork.Domains.EditDomain(domain);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditDomainNameToEmptyTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var domain = new Domain
                {
                    Name = "DomainTestCase"
                };

                unitOfWork.Domains.Add(domain);
                domain.Name = "";

                var result = unitOfWork.Domains.EditDomain(domain);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditDomainNameToNullTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var domain = new Domain
                {
                    Name = "DomainTestCase"
                };

                unitOfWork.Domains.Add(domain);
                domain.Name = null;

                var result = unitOfWork.Domains.EditDomain(domain);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void DeleteDomainTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var domain = new Domain
                {
                    Name = "DomainTestCase"
                };

                unitOfWork.Domains.Add(domain);

                var result = unitOfWork.Domains.Remove(domain);
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void DeleteEmptyDomainTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var domain = new Domain();

                var result = unitOfWork.Domains.Remove(domain);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void DeleteNullDomainTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var result = unitOfWork.Domains.Remove(null);
                Assert.AreEqual(false, result);
            }
        }

    }
}
