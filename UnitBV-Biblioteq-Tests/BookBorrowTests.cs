using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnitBV_Biblioteq.Core.DomainModel;
using UnitBV_Biblioteq.Core.Repositories;
using UnitBV_Biblioteq.Persistence;

namespace UnitBV_Biblioteq_Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class BookBorrowTests
    {
        private BookEdition CreateBookEditionForTests()
        {
            var book = new Book()
            {
                Title = "Fake Title For Test",
                Authors = new List<Author>() { new Author() { Firstname = "FNTestCase", Lastname = "LNTestCase" } },
                Domains = new List<Domain>() { new Domain() { Name = "DomainTestCase" } }
            };

            var publisher = new Publisher()
            {
                Name = "PublisherTestCase"
            };

            var edition = new BookEdition()
            {
                Book = book,
                Publisher = publisher,
                Copies = 50,
                CopiesLibrary = 5,
                Pages = 100,
                Type = BookType.PaperBack,
                Year = "2000"
            };

            return edition;
        }
        private User CreateUserForTests(UserType userType)
        {
            var user = new User()
            {
                Firstname = "UserFNTestCase",
                Lastname = "UserLNTestCase",
                Email = "useremailtest@test.com",
                UserType = userType
            };

            return user;
        }

        [TestMethod]
        public void AddBookBorrowMockTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var mock = new Mock<IRepository<BookBorrow>>();
                mock.Setup(m => m.Add(It.IsAny<BookBorrow>())).Returns(true);

                var borrowBook = new BookBorrow();
                var obj = mock.Object;


                var result = obj.Add(borrowBook);

                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void EditBookBorrowMockTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var mock = new Mock<IBookBorrowRepository>();
                mock.Setup(m => m.EditBookBorrow(It.IsAny<BookBorrow>())).Returns(true);

                var borrowBook = new BookBorrow();
                var obj = mock.Object;

                var result = obj.EditBookBorrow(borrowBook);

                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void ReturnBorrowMockTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var mock = new Mock<IBookBorrowRepository>();
                mock.Setup(m => m.ReturnBooks(It.IsAny<BookBorrow>())).Returns(true);

                var borrowBook = new BookBorrow();
                var obj = mock.Object;

                var result = obj.ReturnBooks(borrowBook);

                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void ReBorrowMockTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var mock = new Mock<IBookBorrowRepository>();
                mock.Setup(m => m.ReBorrowBook(It.IsAny<BookBorrow>())).Returns(true);

                var borrowBook = new BookBorrow();
                var obj = mock.Object;

                var result = obj.ReBorrowBook(borrowBook);

                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void AddValidBookBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var borrowBook = new BookBorrow()
                {
                    Books = new List<BookEdition>(){ CreateBookEditionForTests() },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = CreateUserForTests(UserType.Reader),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                var result = unitOfWork.BookBorrows.Add(borrowBook);

                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void AddEmptyBookBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var borrowBook = new BookBorrow();

                var result = unitOfWork.BookBorrows.Add(borrowBook);

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddNullBookBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var result = unitOfWork.BookBorrows.Add(null);

                Assert.AreEqual(false, result);
            }
        }
    }
}
