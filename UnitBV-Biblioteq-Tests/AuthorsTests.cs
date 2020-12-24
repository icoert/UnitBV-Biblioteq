using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using Moq;
using UnitBV_Biblioteq.Core.DomainModel;
using UnitBV_Biblioteq.Core.Repositories;
using UnitBV_Biblioteq.Persistence;


namespace UnitBV_Biblioteq_Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class AuthorsTests
    {
        [TestMethod]
        public void AddAuthorMockTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var mock = new Mock<IRepository<Author>>();

                mock.Setup(m => m.Add(It.IsAny<Author>())).Returns(true);

                var author = new Author();
                var repo = mock.Object;

                var result = repo.Add(author);

                if (result)
                {
                    unitOfWork.Complete();
                }

                Assert.AreEqual(true, result);
            }

        }

        [TestMethod]
        public void AddAuthorTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var author = new Author() {Firstname = "Test11", Lastname = "Test12"};

                var result = unitOfWork.Authors.Add(author);

                if (result)
                {
                    unitOfWork.Complete();
                }

                Assert.AreEqual(true, result);
            }
        }
    }

}
