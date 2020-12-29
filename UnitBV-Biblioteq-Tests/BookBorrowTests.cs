using System;
using System.Collections.Generic;
using System.Configuration;
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
    public class BookBorrowTests
    {
        private BookEdition CreateBookEditionForTests()
        {
            var book = new Book
            {
                Title = "Fake Title For Test",
                Authors = new List<Author> { new Author() { Firstname = "FNTestCase", Lastname = "LNTestCase" } },
                Domains = new List<Domain> { new Domain() { Name = "DomainTestCase" } }
            };

            var publisher = new Publisher
            {
                Name = "PublisherTestCase"
            };

            var edition = new BookEdition
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
            var user = new User
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
            var mock = new Mock<IRepository<BookBorrow>>();
                mock.Setup(m => m.Add(It.IsAny<BookBorrow>())).Returns(true);

                var borrowBook = new BookBorrow();
                var obj = mock.Object;


                var result = obj.Add(borrowBook);

                Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void EditBookBorrowMockTest()
        {
            var mock = new Mock<IBookBorrowRepository>();
                mock.Setup(m => m.EditBookBorrow(It.IsAny<BookBorrow>())).Returns(true);

                var borrowBook = new BookBorrow();
                var obj = mock.Object;

                var result = obj.EditBookBorrow(borrowBook);

                Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void ReturnBorrowMockTest()
        {
            var mock = new Mock<IBookBorrowRepository>();
                mock.Setup(m => m.ReturnBooks(It.IsAny<BookBorrow>())).Returns(true);

                var borrowBook = new BookBorrow();
                var obj = mock.Object;

                var result = obj.ReturnBooks(borrowBook);

                Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void ReBorrowMockTest()
        {
            var mock = new Mock<IBookBorrowRepository>();
                mock.Setup(m => m.ReBorrowBook(It.IsAny<BookBorrow>())).Returns(true);

                var borrowBook = new BookBorrow();
                var obj = mock.Object;

                var result = obj.ReBorrowBook(borrowBook);

                Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void DeleteMockTest()
        {
            var mock = new Mock<IBookBorrowRepository>();
            mock.Setup(m => m.Remove(It.IsAny<BookBorrow>())).Returns(true);

            var borrowBook = new BookBorrow();
            var obj = mock.Object;

            var result = obj.Remove(borrowBook);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void AddValidBookBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var borrowBook = new BookBorrow
                {
                    Books = new List<BookEdition> { CreateBookEditionForTests() },
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

        [TestMethod]
        public void AddOverLimitOfBorrowsBookBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var borrowsLimit = int.Parse(ConfigurationManager.AppSettings["ReBorrowLimit"]);

                var borrowBook = new BookBorrow()
                {
                    Books = new List<BookEdition>() { CreateBookEditionForTests() },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = CreateUserForTests(UserType.Reader),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = borrowsLimit + 1,
                    ReturnDate = null
                };

                var result = unitOfWork.BookBorrows.Add(borrowBook);

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddSameBookBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = CreateBookEditionForTests();
                var borrowBook = new BookBorrow()
                {
                    Books = new List<BookEdition>() { book, book },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = CreateUserForTests(UserType.Reader),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                var result = unitOfWork.BookBorrows.Add(borrowBook);

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddTooManyBooksBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var bookList = new List<BookEdition>();
                var maxBooksPerBorrow = int.Parse(ConfigurationManager.AppSettings["MaxBooksPerBorrow"]);
                for (var i = 0; i <= maxBooksPerBorrow; i++)
                {
                    bookList.Add(CreateBookEditionForTests());
                }
                
                var borrowBook = new BookBorrow()
                {
                    Books = bookList,
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = CreateUserForTests(UserType.Reader),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                var result = unitOfWork.BookBorrows.Add(borrowBook);

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddEmptyListOfBooksBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var bookList = new List<BookEdition>();

                var borrowBook = new BookBorrow()
                {
                    Books = bookList,
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = CreateUserForTests(UserType.Reader),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                var result = unitOfWork.BookBorrows.Add(borrowBook);

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddNullBooksBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var borrowBook = new BookBorrow()
                {
                    Books = null,
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = CreateUserForTests(UserType.Reader),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                var result = unitOfWork.BookBorrows.Add(borrowBook);

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddReturnedBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var borrowBook = new BookBorrow()
                {
                    Books = new List<BookEdition>() { CreateBookEditionForTests() },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = CreateUserForTests(UserType.Reader),
                    BorrowDate = DateTime.Now,
                    IsReturned = true,
                    ReBorrows = 0,
                    ReturnDate = DateTime.Now.AddDays(-2)
                };

                var result = unitOfWork.BookBorrows.Add(borrowBook);

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddFutureBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var borrowBook = new BookBorrow()
                {
                    Books = new List<BookEdition>() { CreateBookEditionForTests() },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = CreateUserForTests(UserType.Reader),
                    BorrowDate = DateTime.Now.AddDays(2),
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                var result = unitOfWork.BookBorrows.Add(borrowBook);

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddNullReaderBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var borrowBook = new BookBorrow()
                {
                    Books = new List<BookEdition>() { CreateBookEditionForTests() },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = null,
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                var result = unitOfWork.BookBorrows.Add(borrowBook);

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddNullEmployeeBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var borrowBook = new BookBorrow()
                {
                    Books = new List<BookEdition>() { CreateBookEditionForTests() },
                    Employee = null,
                    Reader = CreateUserForTests(UserType.Reader),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                var result = unitOfWork.BookBorrows.Add(borrowBook);

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddNullEmployeeAndReaderBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var borrowBook = new BookBorrow()
                {
                    Books = new List<BookEdition>() { CreateBookEditionForTests() },
                    Employee = null,
                    Reader = null,
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                var result = unitOfWork.BookBorrows.Add(borrowBook);

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddInvalidEmployeeBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var borrowBook = new BookBorrow()
                {
                    Books = new List<BookEdition>() { CreateBookEditionForTests() },
                    Employee = new User(),
                    Reader = CreateUserForTests(UserType.Reader),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                var result = unitOfWork.BookBorrows.Add(borrowBook);

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddInvalidReaderBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var borrowBook = new BookBorrow()
                {
                    Books = new List<BookEdition>() { CreateBookEditionForTests() },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = new User(),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                var result = unitOfWork.BookBorrows.Add(borrowBook);

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddInvalidEmployeeAndReaderBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var borrowBook = new BookBorrow()
                {
                    Books = new List<BookEdition>() { CreateBookEditionForTests() },
                    Employee = new User(),
                    Reader = new User(),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                var result = unitOfWork.BookBorrows.Add(borrowBook);

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddReaderAsEmployeeBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var borrowBook = new BookBorrow()
                {
                    Books = new List<BookEdition>() { CreateBookEditionForTests() },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = CreateUserForTests(UserType.Employee),
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
        public void AddEmployeeAsReaderBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var borrowBook = new BookBorrow()
                {
                    Books = new List<BookEdition>() { CreateBookEditionForTests() },
                    Employee = CreateUserForTests(UserType.Reader),
                    Reader = CreateUserForTests(UserType.Reader),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                var result = unitOfWork.BookBorrows.Add(borrowBook);

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void Add3BooksWithSameDomainBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var domain = new Domain() { Name = "DomainTestCase" };
                var bookList = new List<BookEdition>()
                {
                    CreateBookEditionForTests(),
                    CreateBookEditionForTests(),
                    CreateBookEditionForTests(),
                };

                foreach (var book in bookList)
                {
                    book.Book.Domains = new List<Domain>() {domain};
                }
                var borrowBook = new BookBorrow()
                {
                    Books = bookList,
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = CreateUserForTests(UserType.Reader),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                var result = unitOfWork.BookBorrows.Add(borrowBook);

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void Add3BooksWithSameDomainParentBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var domain1 = new Domain() { Name = "DomainTestCase1" };
                var domain2 = new Domain() { Name = "DomainTestCase2", Parent = domain1};
                var domain3 = new Domain() { Name = "DomainTestCase3", Parent = domain1};
                var bookList = new List<BookEdition>()
                {
                    CreateBookEditionForTests(),
                    CreateBookEditionForTests(),
                    CreateBookEditionForTests(),
                };

                bookList[0].Book.Domains = new List<Domain>() {domain1};
                bookList[1].Book.Domains = new List<Domain>() {domain2};
                bookList[2].Book.Domains = new List<Domain>() {domain3};

                var borrowBook = new BookBorrow()
                {
                    Books = bookList,
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = CreateUserForTests(UserType.Reader),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                var result = unitOfWork.BookBorrows.Add(borrowBook);

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddOverMaxBooksPerPeriodBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var maxBooksPerPeriod = int.Parse(ConfigurationManager.AppSettings["MaxBooksPerPeriod"]); 
                var periodInDaysForMaxBooksPerPeriod = int.Parse(ConfigurationManager.AppSettings["PeriodInDaysForMaxBooksPerPeriod"]); 
                var reader = CreateUserForTests(UserType.Reader);

                for (var i = 0; i < periodInDaysForMaxBooksPerPeriod; i++) // 14 days in this initial case
                {
                    var bookBorrow1 = new BookBorrow()
                    {
                        Books = new List<BookEdition>() { CreateBookEditionForTests(), CreateBookEditionForTests(), CreateBookEditionForTests() },
                        Employee = CreateUserForTests(UserType.Employee),
                        Reader = reader,
                        BorrowDate = DateTime.Now.AddDays(-i), // 3 books per day in this case
                        IsReturned = false,
                        ReBorrows = 0,
                        ReturnDate = null
                    };

                    if ((i+1) * 3 < maxBooksPerPeriod)
                    {
                        unitOfWork.BookBorrows.Add(bookBorrow1);
                    }
                }

                var borrowBook2 = new BookBorrow()
                {
                    Books = new List<BookEdition>(){CreateBookEditionForTests(), CreateBookEditionForTests(), CreateBookEditionForTests() },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = reader,
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                var result = unitOfWork.BookBorrows.Add(borrowBook2);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddOverMaxBooksInSameDomainBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var maxBooksInSameDomain = int.Parse(ConfigurationManager.AppSettings["MaxBooksInSameDomain"]);
                var reader = CreateUserForTests(UserType.Reader);
                var domain = new Domain() {Name = "DomainTestCase"};

                for (var i = 0; i < maxBooksInSameDomain; i++)
                {
                    var book1 = CreateBookEditionForTests();
                    book1.Book.Domains.Add(domain);
                    var bookBorrow1 = new BookBorrow()
                    {
                        Books = new List<BookEdition>() { book1 },
                        Employee = CreateUserForTests(UserType.Employee),
                        Reader = reader,
                        BorrowDate = DateTime.Now.AddDays(-i),
                        IsReturned = false,
                        ReBorrows = 0,
                        ReturnDate = null
                    };

                    unitOfWork.BookBorrows.Add(bookBorrow1);
                }

                var book2= CreateBookEditionForTests();
                book2.Book.Domains.Add(domain);
                var borrowBook2 = new BookBorrow()
                {
                    Books = new List<BookEdition>() { book2 },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = reader,
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                var result = unitOfWork.BookBorrows.Add(borrowBook2);

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddValidSameBookBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var periodInDaysForSameBookBorrowing = int.Parse(ConfigurationManager.AppSettings["PeriodInDaysForSameBookBorrowing"]); // 20 in initial case
                var reader = CreateUserForTests(UserType.Reader);

                var book = CreateBookEditionForTests();
                var bookBorrow = new BookBorrow
                {
                    Books = new List<BookEdition> { book },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = reader,
                    BorrowDate = DateTime.Now.AddDays(-periodInDaysForSameBookBorrowing - 1),
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                unitOfWork.BookBorrows.Add(bookBorrow);

                var sameBookBorrow = new BookBorrow
                {
                    Books = new List<BookEdition>() { book },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = reader,
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                var result = unitOfWork.BookBorrows.Add(sameBookBorrow);

                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void AddInvalidSameBookBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var reader = CreateUserForTests(UserType.Reader);

                var book = CreateBookEditionForTests();
                var bookBorrow = new BookBorrow
                {
                    Books = new List<BookEdition> { book },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = reader,
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                unitOfWork.BookBorrows.Add(bookBorrow);

                var sameBookBorrow = new BookBorrow
                {
                    Books = new List<BookEdition> { book },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = reader,
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                var result = unitOfWork.BookBorrows.Add(sameBookBorrow);

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddOverMaxBooksPerDayBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var maxBooksPerDay = int.Parse(ConfigurationManager.AppSettings["MaxBooksPerPeriod"]);
                var reader = CreateUserForTests(UserType.Reader);

                for (var i = 0; i < maxBooksPerDay; i++) // 10 books per day in initial case
                {
                    var bookBorrow1 = new BookBorrow
                    {
                        Books = new List<BookEdition> { CreateBookEditionForTests() },
                        Employee = CreateUserForTests(UserType.Employee),
                        Reader = reader,
                        BorrowDate = DateTime.Now,
                        IsReturned = false,
                        ReBorrows = 0,
                        ReturnDate = null
                    };
                    
                    unitOfWork.BookBorrows.Add(bookBorrow1);
                }

                var borrowBook2 = new BookBorrow()
                {
                    Books = new List<BookEdition>() { CreateBookEditionForTests() },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = reader,
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                var result = unitOfWork.BookBorrows.Add(borrowBook2);

                Assert.AreEqual(false, result);
            }
        }
        
        [TestMethod]
        public void AddAvailabilityPercentageBookBorrowTest() // 10% availability
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = CreateBookEditionForTests();
                var borrows = book.Copies * 0.9 - book.CopiesLibrary - 1;
                
                var reader = CreateUserForTests(UserType.Reader);

                for (var i = 0; i < borrows; i++) // 10 books per day in initial case
                {
                    var bookBorrow1 = new BookBorrow
                    {
                        Books = new List<BookEdition> { book },
                        Employee = CreateUserForTests(UserType.Employee),
                        Reader = reader,
                        BorrowDate = DateTime.Now,
                        IsReturned = false,
                        ReBorrows = 0,
                        ReturnDate = null
                    };

                    unitOfWork.BookBorrows.Add(bookBorrow1);
                }

                var borrowBook2 = new BookBorrow()
                {
                    Books = new List<BookEdition>() { book },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = reader,
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                var result = unitOfWork.BookBorrows.Add(borrowBook2);

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void ReturnBookBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var borrow = new BookBorrow
                {
                    Books = new List<BookEdition> { CreateBookEditionForTests() },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = CreateUserForTests(UserType.Reader),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                unitOfWork.BookBorrows.Add(borrow);

                var result = unitOfWork.BookBorrows.ReturnBooks(borrow);
                Assert.AreEqual(true, result);
                Assert.AreEqual(true, borrow.IsReturned);
            }
        }

        [TestMethod]
        public void ReturnNullBookBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var result = unitOfWork.BookBorrows.ReturnBooks(null);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void ReturnEmptyBookBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var borrow = new BookBorrow();
                var result = unitOfWork.BookBorrows.ReturnBooks(borrow);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void ReborrowValidTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var borrow = new BookBorrow
                {
                    Books = new List<BookEdition>() { CreateBookEditionForTests() },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = CreateUserForTests(UserType.Reader),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                unitOfWork.BookBorrows.Add(borrow);
                
                var result = unitOfWork.BookBorrows.ReBorrowBook(borrow);
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void ReborrowInValidTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var borrow = new BookBorrow
                {
                    Books = new List<BookEdition>() { CreateBookEditionForTests() },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = CreateUserForTests(UserType.Reader),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                unitOfWork.BookBorrows.Add(borrow);
                var reborrowLimit = int.Parse(ConfigurationManager.AppSettings["ReBorrowLimit"]); // 3 in initial case
                for (var i = 0; i < reborrowLimit; i++)
                {
                    unitOfWork.BookBorrows.ReBorrowBook(borrow);
                }
                var result = unitOfWork.BookBorrows.ReBorrowBook(borrow);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void ReborrowEmptyTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var borrow = new BookBorrow();

                var result = unitOfWork.BookBorrows.ReBorrowBook(borrow);
                Assert.AreEqual(false, result);
            }
        }
        
        [TestMethod]
        public void ReborrowNullTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var result = unitOfWork.BookBorrows.ReBorrowBook(null);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void DeleteValidTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var borrow = new BookBorrow
                {
                    Books = new List<BookEdition>() { CreateBookEditionForTests() },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = CreateUserForTests(UserType.Reader),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                unitOfWork.BookBorrows.Add(borrow);

                var result = unitOfWork.BookBorrows.Remove(borrow);
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void DeleteInvalidTest() // non-existent
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var borrow = new BookBorrow();

                var result = unitOfWork.BookBorrows.Remove(borrow);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void DeleteNullTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var result = unitOfWork.BookBorrows.Remove(null);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EmptyNullTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var result = unitOfWork.BookBorrows.EditBookBorrow(null);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EmptyEmptyTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var borrow = new BookBorrow();
                var result = unitOfWork.BookBorrows.EditBookBorrow(borrow);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditToOverReborrowLimitTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var reborrowLimit = int.Parse(ConfigurationManager.AppSettings["ReBorrowLimit"]); // 3 in initial case
                var borrow = new BookBorrow
                {
                    Books = new List<BookEdition>() { CreateBookEditionForTests() },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = CreateUserForTests(UserType.Reader),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                unitOfWork.BookBorrows.Add(borrow);
                borrow.ReBorrows = reborrowLimit + 1;
                var result = unitOfWork.BookBorrows.EditBookBorrow(borrow);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditToInvalidReturnTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var borrow = new BookBorrow
                {
                    Books = new List<BookEdition>() { CreateBookEditionForTests() },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = CreateUserForTests(UserType.Reader),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                unitOfWork.BookBorrows.Add(borrow);
                borrow.ReturnDate = DateTime.Now.AddDays(-3);
                var result = unitOfWork.BookBorrows.EditBookBorrow(borrow);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditToInvalidBorrowTest() 
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var borrow = new BookBorrow
                {
                    Books = new List<BookEdition>() { CreateBookEditionForTests() },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = CreateUserForTests(UserType.Reader),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                unitOfWork.BookBorrows.Add(borrow);
                borrow.ReturnDate = DateTime.Now.AddDays(3);
                var result = unitOfWork.BookBorrows.EditBookBorrow(borrow);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditToEmptyBookListTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var borrow = new BookBorrow
                {
                    Books = new List<BookEdition>() { CreateBookEditionForTests() },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = CreateUserForTests(UserType.Reader),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                unitOfWork.BookBorrows.Add(borrow);
                var bookList = new List<BookEdition>();

                borrow.Books = bookList;
                var result = unitOfWork.BookBorrows.EditBookBorrow(borrow);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditToNullBookListTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var borrow = new BookBorrow
                {
                    Books = new List<BookEdition>() { CreateBookEditionForTests() },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = CreateUserForTests(UserType.Reader),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                unitOfWork.BookBorrows.Add(borrow);

                borrow.Books = null;
                var result = unitOfWork.BookBorrows.EditBookBorrow(borrow);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditToNullReaderTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var borrow = new BookBorrow
                {
                    Books = new List<BookEdition>() { CreateBookEditionForTests() },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = CreateUserForTests(UserType.Reader),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                unitOfWork.BookBorrows.Add(borrow);

                borrow.Reader = null;
                var result = unitOfWork.BookBorrows.EditBookBorrow(borrow);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditToEmptyReaderTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var borrow = new BookBorrow
                {
                    Books = new List<BookEdition>() { CreateBookEditionForTests() },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = CreateUserForTests(UserType.Reader),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                unitOfWork.BookBorrows.Add(borrow);

                borrow.Reader = new User();
                var result = unitOfWork.BookBorrows.EditBookBorrow(borrow);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditToNullEmployeeTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var borrow = new BookBorrow
                {
                    Books = new List<BookEdition>() { CreateBookEditionForTests() },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = CreateUserForTests(UserType.Reader),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                unitOfWork.BookBorrows.Add(borrow);

                borrow.Employee = null;
                var result = unitOfWork.BookBorrows.EditBookBorrow(borrow);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditToEmptyEmployeeTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var borrow = new BookBorrow
                {
                    Books = new List<BookEdition>() { CreateBookEditionForTests() },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = CreateUserForTests(UserType.Reader),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                unitOfWork.BookBorrows.Add(borrow);

                borrow.Employee = new User();
                var result = unitOfWork.BookBorrows.EditBookBorrow(borrow);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditToReaderAsEmployeeTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var borrow = new BookBorrow
                {
                    Books = new List<BookEdition>() { CreateBookEditionForTests() },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = CreateUserForTests(UserType.Reader),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                unitOfWork.BookBorrows.Add(borrow);

                borrow.Employee = CreateUserForTests(UserType.Reader);
                var result = unitOfWork.BookBorrows.EditBookBorrow(borrow);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditToRepeatedBookTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = CreateBookEditionForTests();
                var borrow = new BookBorrow
                {
                    Books = new List<BookEdition>() { CreateBookEditionForTests() },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = CreateUserForTests(UserType.Reader),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                unitOfWork.BookBorrows.Add(borrow);
                var bookList = new List<BookEdition>{book, book};

                borrow.Books = bookList;
                var result = unitOfWork.BookBorrows.EditBookBorrow(borrow);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditToOverMaxBooksPerBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var maxBooksPerBorrow = int.Parse(ConfigurationManager.AppSettings["MaxBooksPerBorrow"]);
                var bookList = new List<BookEdition>();

                for (var i = 0; i <= maxBooksPerBorrow; i++)
                {
                    bookList.Add(CreateBookEditionForTests());
                }
                
                var borrow = new BookBorrow
                {
                    Books = new List<BookEdition>() { CreateBookEditionForTests() },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = CreateUserForTests(UserType.Reader),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                unitOfWork.BookBorrows.Add(borrow);

                borrow.Books = bookList;
                var result = unitOfWork.BookBorrows.EditBookBorrow(borrow);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditToOverMaxDomainsPerBooksBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var maxDomainsForBook = int.Parse(ConfigurationManager.AppSettings["MaxDomainsForBook"]);
                var domain = new Domain() { Name = "DomainTestCase" };

                var bookList = new List<BookEdition>();

                for (var i = 0; i <= maxDomainsForBook; i++)
                {
                    bookList.Add(CreateBookEditionForTests());
                }

                foreach (var book in bookList)
                {
                    book.Book.Domains = new List<Domain>() { domain };
                }

                var borrow = new BookBorrow
                {
                    Books = new List<BookEdition>() { CreateBookEditionForTests() },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = CreateUserForTests(UserType.Reader),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                unitOfWork.BookBorrows.Add(borrow);

                borrow.Books = bookList;
                var result = unitOfWork.BookBorrows.EditBookBorrow(borrow);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddOverMaxBooksBorrowEmployeeTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var maxBookPerBorrow = int.Parse(ConfigurationManager.AppSettings["MaxBooksPerBorrow"]);
                var bookList = new List<BookEdition>();
                for (var i = 0; i <= maxBookPerBorrow; i++)
                {
                    bookList.Add(CreateBookEditionForTests());
                }

                var bookBorrow = new BookBorrow
                {
                    Books = bookList,
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = CreateUserForTests(UserType.Employee),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null,
                };

                var result = unitOfWork.BookBorrows.Add(bookBorrow);
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void AddOverTwiceMaxBooksBorrowEmployeeTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var maxBookPerBorrow = int.Parse(ConfigurationManager.AppSettings["MaxBooksPerBorrow"]);
                var bookList = new List<BookEdition>();
                for (var i = 0; i <= maxBookPerBorrow*2; i++)
                {
                    bookList.Add(CreateBookEditionForTests());
                }

                var bookBorrow = new BookBorrow
                {
                    Books = bookList,
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = CreateUserForTests(UserType.Employee),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null,
                };

                var result = unitOfWork.BookBorrows.Add(bookBorrow);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddOverMaxPerPeriodBookBorrowEmployeeTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var maxBooksPerPeriod = int.Parse(ConfigurationManager.AppSettings["MaxBooksPerPeriod"]);
                var periodInDaysForMaxBooksPerPeriod = int.Parse(ConfigurationManager.AppSettings["PeriodInDaysForMaxBooksPerPeriod"]);
                var reader = CreateUserForTests(UserType.Employee);

                for (var i = 0; i < periodInDaysForMaxBooksPerPeriod; i++) // 14 days in this initial case
                {
                    var bookBorrow1 = new BookBorrow()
                    {
                        Books = new List<BookEdition>() { CreateBookEditionForTests(), CreateBookEditionForTests(), CreateBookEditionForTests() },
                        Employee = CreateUserForTests(UserType.Employee),
                        Reader = reader,
                        BorrowDate = DateTime.Now.AddDays(-i), // 3 books per day in this case
                        IsReturned = false,
                        ReBorrows = 0,
                        ReturnDate = null
                    };

                    if ((i + 1) * 3 < maxBooksPerPeriod)
                    {
                        unitOfWork.BookBorrows.Add(bookBorrow1);
                    }
                }

                var borrowBook2 = new BookBorrow()
                {
                    Books = new List<BookEdition>() { CreateBookEditionForTests(), CreateBookEditionForTests(), CreateBookEditionForTests() },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = reader,
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                var result = unitOfWork.BookBorrows.Add(borrowBook2);
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void AddOverTwiceMaxPerPeriodBookBorrowEmployeeTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var maxBooksPerPeriod = int.Parse(ConfigurationManager.AppSettings["MaxBooksPerPeriod"]);
                var periodInDaysForMaxBooksPerPeriod = int.Parse(ConfigurationManager.AppSettings["PeriodInDaysForMaxBooksPerPeriod"]);
                var reader = CreateUserForTests(UserType.Employee);

                for (var i = 0; i < periodInDaysForMaxBooksPerPeriod/2; i++) // 14 days in this initial case
                {
                    var bookList = new List<BookEdition>();
                    for (var j = 0; j < maxBooksPerPeriod * 4 / periodInDaysForMaxBooksPerPeriod; j++)
                    {
                        bookList.Add(CreateBookEditionForTests());
                    }
                    var bookBorrow1 = new BookBorrow()
                    {
                        Books = bookList,
                        Employee = CreateUserForTests(UserType.Employee),
                        Reader = reader,
                        BorrowDate = DateTime.Now.AddDays(-i), // maxBooksPerPeriod * 4 / periodInDaysForMaxBooksPerPeriod books per day in this case
                        IsReturned = false,
                        ReBorrows = 0,
                        ReturnDate = null
                    };

                    if ((i + 1) * maxBooksPerPeriod * 4 / periodInDaysForMaxBooksPerPeriod <= maxBooksPerPeriod * 2)
                    {
                        unitOfWork.BookBorrows.Add(bookBorrow1);
                    }
                    else
                    {
                        break;
                    }
                }

                var bookList2 = new List<BookEdition>();
                for (var j = 0; j < maxBooksPerPeriod * 4 / periodInDaysForMaxBooksPerPeriod + 1; j++)
                {
                    bookList2.Add(CreateBookEditionForTests());
                }
                
                var borrowBook2 = new BookBorrow()
                {
                    Books = bookList2,
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = reader,
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                var result = unitOfWork.BookBorrows.Add(borrowBook2);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddOverMaxBooksPerDayGivenByEmployeeBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var maxBooksPerDayGivenByEmployee = int.Parse(ConfigurationManager.AppSettings["MaxBooksPerDayGivenByEmployee"]);
                var employee = CreateUserForTests(UserType.Employee);

                for (var i = 0; i < maxBooksPerDayGivenByEmployee; i++) // 20 in initial case
                {
                    var bookBorrow1 = new BookBorrow()
                    {
                        Books = new List<BookEdition> { CreateBookEditionForTests() },
                        Employee = employee,
                        Reader = CreateUserForTests(UserType.Reader),
                        BorrowDate = DateTime.Now,
                        IsReturned = false,
                        ReBorrows = 0,
                        ReturnDate = null
                    };
                    unitOfWork.BookBorrows.Add(bookBorrow1);
                }

                var bookBorrow2 = new BookBorrow()
                {
                    Books = new List<BookEdition> { CreateBookEditionForTests() },
                    Employee = employee,
                    Reader = CreateUserForTests(UserType.Reader),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                var result = unitOfWork.BookBorrows.Add(bookBorrow2);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddValidMaxBooksPerDayGivenByEmployeeBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var maxBooksPerDayGivenByEmployee = int.Parse(ConfigurationManager.AppSettings["MaxBooksPerDayGivenByEmployee"]);
                var employee = CreateUserForTests(UserType.Employee);

                for (var i = 0; i < maxBooksPerDayGivenByEmployee - 1; i++) // 20 in initial case
                {
                    var bookBorrow1 = new BookBorrow()
                    {
                        Books = new List<BookEdition> { CreateBookEditionForTests() },
                        Employee = employee,
                        Reader = CreateUserForTests(UserType.Reader),
                        BorrowDate = DateTime.Now,
                        IsReturned = false,
                        ReBorrows = 0,
                        ReturnDate = null
                    };
                    unitOfWork.BookBorrows.Add(bookBorrow1);
                }

                var bookBorrow2 = new BookBorrow()
                {
                    Books = new List<BookEdition> { CreateBookEditionForTests() },
                    Employee = employee,
                    Reader = CreateUserForTests(UserType.Reader),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                var result = unitOfWork.BookBorrows.Add(bookBorrow2);
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void AddOverMaxBooksPerDayBorrowEmployeeTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var maxBooksPerDay = int.Parse(ConfigurationManager.AppSettings["MaxBooksPerDay"]);
                var employee = CreateUserForTests(UserType.Employee);

                for (var i = 0; i < maxBooksPerDay; i++)
                {
                    var bookBorrow1 = new BookBorrow()
                    {
                        Books = new List<BookEdition> { CreateBookEditionForTests() },
                        Employee = CreateUserForTests(UserType.Employee),
                        Reader = employee,
                        BorrowDate = DateTime.Now,
                        IsReturned = false,
                        ReBorrows = 0,
                        ReturnDate = null
                    };
                    unitOfWork.BookBorrows.Add(bookBorrow1);
                }

                var bookBorrow2 = new BookBorrow()
                {
                    Books = new List<BookEdition> { CreateBookEditionForTests() },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = employee,
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                var result = unitOfWork.BookBorrows.Add(bookBorrow2);
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void AddOverMaxBooksPerSameDomainEmployeeTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var maxBooksInSameDomain = int.Parse(ConfigurationManager.AppSettings["MaxBooksInSameDomain"]);
                var reader = CreateUserForTests(UserType.Employee);
                var domain = new Domain() { Name = "DomainTestCase" };

                for (var i = 0; i < maxBooksInSameDomain; i++)
                {
                    var book1 = CreateBookEditionForTests();
                    book1.Book.Domains.Add(domain);
                    var bookBorrow1 = new BookBorrow()
                    {
                        Books = new List<BookEdition>() { book1 },
                        Employee = CreateUserForTests(UserType.Employee),
                        Reader = reader,
                        BorrowDate = DateTime.Now.AddDays(-i),
                        IsReturned = false,
                        ReBorrows = 0,
                        ReturnDate = null
                    };

                    unitOfWork.BookBorrows.Add(bookBorrow1);
                }

                var book2 = CreateBookEditionForTests();
                book2.Book.Domains.Add(domain);
                var borrowBook2 = new BookBorrow()
                {
                    Books = new List<BookEdition>() { book2 },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = reader,
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                var result = unitOfWork.BookBorrows.Add(borrowBook2);

                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void AddOverTwiceMaxBooksPerSameDomainEmployeeTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var maxBooksInSameDomain = int.Parse(ConfigurationManager.AppSettings["MaxBooksInSameDomain"]);
                var reader = CreateUserForTests(UserType.Employee);
                var domain = new Domain() { Name = "DomainTestCase" };

                for (var i = 0; i < maxBooksInSameDomain * 2; i++)
                {
                    var book1 = CreateBookEditionForTests();
                    book1.Book.Domains.Add(domain);
                    var bookBorrow1 = new BookBorrow()
                    {
                        Books = new List<BookEdition>() { book1 },
                        Employee = CreateUserForTests(UserType.Employee),
                        Reader = reader,
                        BorrowDate = DateTime.Now.AddDays(-i),
                        IsReturned = false,
                        ReBorrows = 0,
                        ReturnDate = null
                    };

                    unitOfWork.BookBorrows.Add(bookBorrow1);
                }

                var book2 = CreateBookEditionForTests();
                book2.Book.Domains.Add(domain);
                var borrowBook2 = new BookBorrow()
                {
                    Books = new List<BookEdition>() { book2 },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = reader,
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                var result = unitOfWork.BookBorrows.Add(borrowBook2);

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void ReborrowOverMaxEmployeeTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var reborrowLimit = int.Parse(ConfigurationManager.AppSettings["ReborrowLimit"]);

                var bookBorrow = new BookBorrow
                {
                    Books = new List<BookEdition> { CreateBookEditionForTests() },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = CreateUserForTests(UserType.Employee),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                unitOfWork.BookBorrows.Add(bookBorrow);

                for (var i = 0; i < reborrowLimit; i++)
                {
                    unitOfWork.BookBorrows.ReBorrowBook(bookBorrow);
                }
                var result = unitOfWork.BookBorrows.ReBorrowBook(bookBorrow);

                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void ReborrowOverTwiceMaxEmployeeTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var reborrowLimit = int.Parse(ConfigurationManager.AppSettings["ReborrowLimit"]);

                var bookBorrow = new BookBorrow
                {
                    Books = new List<BookEdition> { CreateBookEditionForTests() },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = CreateUserForTests(UserType.Employee),
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                unitOfWork.BookBorrows.Add(bookBorrow);

                for (var i = 0; i < reborrowLimit * 2; i++)
                {
                    unitOfWork.BookBorrows.ReBorrowBook(bookBorrow);
                }
                var result = unitOfWork.BookBorrows.ReBorrowBook(bookBorrow);

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddOverPeriodForSameBookEmployeeBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var periodInDaysForSameBookBorrowing = int.Parse(ConfigurationManager.AppSettings["PeriodInDaysForSameBookBorrowing"]);
                var reader = CreateUserForTests(UserType.Employee);
                var book = CreateBookEditionForTests();

                var bookBorrow1 = new BookBorrow
                {
                    Books = new List<BookEdition> { book },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = reader,
                    BorrowDate = DateTime.Now.AddDays(- periodInDaysForSameBookBorrowing + 1),
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };
                unitOfWork.BookBorrows.Add(bookBorrow1);


                var bookBorrow2 = new BookBorrow
                {
                    Books = new List<BookEdition> { book },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = reader,
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                var result = unitOfWork.BookBorrows.Add(bookBorrow2);
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void AddOverHalfPeriodForSameBookEmployeeBorrowTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var periodInDaysForSameBookBorrowing = int.Parse(ConfigurationManager.AppSettings["PeriodInDaysForSameBookBorrowing"]);
                var reader = CreateUserForTests(UserType.Employee);

                var book = CreateBookEditionForTests();
                var bookBorrow1 = new BookBorrow
                {
                    Books = new List<BookEdition> { book },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = reader,
                    BorrowDate = DateTime.Now.AddDays(-periodInDaysForSameBookBorrowing/2 + 1),
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };
                unitOfWork.BookBorrows.Add(bookBorrow1);


                var bookBorrow2 = new BookBorrow
                {
                    Books = new List<BookEdition> { book },
                    Employee = CreateUserForTests(UserType.Employee),
                    Reader = reader,
                    BorrowDate = DateTime.Now,
                    IsReturned = false,
                    ReBorrows = 0,
                    ReturnDate = null
                };

                var result = unitOfWork.BookBorrows.Add(bookBorrow2);
                Assert.AreEqual(false, result);
            }
        }


    }
}
