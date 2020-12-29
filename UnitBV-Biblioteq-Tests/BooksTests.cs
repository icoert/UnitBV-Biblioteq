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
    public class BooksTests
    {
        [TestMethod]
        public void AddBookMockTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var mock = new Mock<IRepository<Book>>();

                mock.Setup(m => m.Add(It.IsAny<Book>())).Returns(true);

                var book = new Book();
                var obj = mock.Object;

                var result = obj.Add(book);

                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void EditBookEditionMockTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var mock = new Mock<IBookRepository>();

                mock.Setup(m => m.EditBook(It.IsAny<Book>())).Returns(true);

                var book = new Book();
                var obj = mock.Object;

                var result = obj.EditBook(book);

                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void DeleteBookEditionMockTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var mock = new Mock<IRepository<Book>>();

                mock.Setup(m => m.Remove(It.IsAny<Book>())).Returns(true);

                var book = new Book();
                var obj = mock.Object;

                var result = obj.Remove(book);

                Assert.AreEqual(true, result);
            }
        }
    }
}
