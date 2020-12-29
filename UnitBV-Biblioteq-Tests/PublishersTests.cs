using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnitBV_Biblioteq.Core.DomainModel;
using UnitBV_Biblioteq.Core.Repositories;
using UnitBV_Biblioteq.Persistence;

namespace UnitBV_Biblioteq_Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class PublishersTests
    {
        [TestMethod]
        public void AddPublisherMockTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var mock = new Mock<IRepository<Publisher>>();

                mock.Setup(m => m.Add(It.IsAny<Publisher>())).Returns(true);

                var publisher = new Publisher();
                var obj = mock.Object;

                var result = obj.Add(publisher);

                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void EditPublisherMockTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var mock = new Mock<IPublisherRepository>();

                mock.Setup(m => m.EditPublisher(It.IsAny<Publisher>())).Returns(true);

                var publisher = new Publisher();
                var obj = mock.Object;

                var result = obj.EditPublisher(publisher);

                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void DeletePublisherMockTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var mock = new Mock<IRepository<Publisher>>();

                mock.Setup(m => m.Remove(It.IsAny<Publisher>())).Returns(true);

                var publisher = new Publisher();
                var obj = mock.Object;

                var result = obj.Remove(publisher);

                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void AddPublisherTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var publisher = new Publisher
                {
                    Name = "PublisherTestCase"
                };

                var result = unitOfWork.Publishers.Add(publisher);
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void AddPublisherWithEmptyNameTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var publisher = new Publisher
                {
                    Name = ""
                };

                var result = unitOfWork.Publishers.Add(publisher);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddPublisherWithNullNameTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var publisher = new Publisher
                {
                    Name = null
                };

                var result = unitOfWork.Publishers.Add(publisher);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddEmptyPublisherTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var publisher = new Publisher();

                var result = unitOfWork.Publishers.Add(publisher);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddNullPublisherTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var result = unitOfWork.Publishers.Add(null);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditPublisherTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var publisher = new Publisher
                {
                    Name = "PublisherTestCase"
                };

                unitOfWork.Publishers.Add(publisher);

                publisher.Name = "NewPublisherTestCase";

                var result = unitOfWork.Publishers.EditPublisher(publisher);
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void EditPublisherToEmptyNameTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var publisher = new Publisher
                {
                    Name = "PublisherTestCase"
                };

                unitOfWork.Publishers.Add(publisher);

                publisher.Name = "";

                var result = unitOfWork.Publishers.EditPublisher(publisher);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditPublisherToNullNameTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var publisher = new Publisher
                {
                    Name = "PublisherTestCase"
                };

                unitOfWork.Publishers.Add(publisher);

                publisher.Name = null;

                var result = unitOfWork.Publishers.EditPublisher(publisher);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditEmptyPublisherTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var publisher = new Publisher();

                var result = unitOfWork.Publishers.EditPublisher(publisher);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditNullPublisherTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var result = unitOfWork.Publishers.EditPublisher(null);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void DeletePublisherTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var publisher = new Publisher
                {
                    Name = "PublisherTestCase"
                };

                unitOfWork.Publishers.Add(publisher);
                
                var result = unitOfWork.Publishers.Remove(publisher);
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void DeleteEmptyPublisherTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var publisher = new Publisher();

                var result = unitOfWork.Publishers.Remove(publisher);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void DeleteNullPublisherTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var result = unitOfWork.Publishers.Remove(null);
                Assert.AreEqual(false, result);
            }
        }
    }
}
