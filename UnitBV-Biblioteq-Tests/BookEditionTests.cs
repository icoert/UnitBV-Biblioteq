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
    public class BookEditionTests
    {
        [TestMethod]
        public void AddBookEditionMockTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var mock = new Mock<IRepository<BookEdition>>();

                mock.Setup(m => m.Add(It.IsAny<BookEdition>())).Returns(true);

                var bookEdition = new BookEdition();
                var obj = mock.Object;

                var result = obj.Add(bookEdition);

                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void EditBookEditionMockTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var mock = new Mock<IBookEditionRepository>();

                mock.Setup(m => m.EditBookEdition(It.IsAny<BookEdition>())).Returns(true);

                var bookEdition = new BookEdition();
                var obj = mock.Object;

                var result = obj.EditBookEdition(bookEdition);

                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void DeleteBookEditionMockTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var mock = new Mock<IRepository<BookEdition>>();

                mock.Setup(m => m.Remove(It.IsAny<BookEdition>())).Returns(true);

                var bookEdition = new BookEdition();
                var obj = mock.Object;

                var result = obj.Remove(bookEdition);

                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void AddBookEditionTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "TestCaseDomain" } }
                };

                var publisher = new Publisher
                {
                    Name = "PublisherTestCase"
                };

                var edition = new BookEdition
                {
                    Book = book,
                    Publisher = publisher,
                    Copies = 80,
                    CopiesLibrary = 20,
                    Pages = 100,
                    Type = BookType.HardCover,
                    Year = "2000"
                };

                var result = unitOfWork.BookEditions.Add(edition);
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void AddBookEditionZeroPagesTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "TestCaseDomain" } }
                };

                var publisher = new Publisher
                {
                    Name = "PublisherTestCase"
                };

                var edition = new BookEdition
                {
                    Book = book,
                    Publisher = publisher,
                    Copies = 80,
                    CopiesLibrary = 20,
                    Pages = 0,
                    Type = BookType.HardCover,
                    Year = "2000"
                };

                var result = unitOfWork.BookEditions.Add(edition);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddBookEditionNegativeNumberOfPagesTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "TestCaseDomain" } }
                };

                var publisher = new Publisher
                {
                    Name = "PublisherTestCase"
                };

                var edition = new BookEdition
                {
                    Book = book,
                    Publisher = publisher,
                    Copies = 80,
                    CopiesLibrary = 20,
                    Pages = -100,
                    Type = BookType.HardCover,
                    Year = "2000"
                };

                var result = unitOfWork.BookEditions.Add(edition);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddBookEditionNegativeNumberOfCopiesTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "TestCaseDomain" } }
                };

                var publisher = new Publisher
                {
                    Name = "PublisherTestCase"
                };

                var edition = new BookEdition
                {
                    Book = book,
                    Publisher = publisher,
                    Copies = -80,
                    CopiesLibrary = 20,
                    Pages = 100,
                    Type = BookType.HardCover,
                    Year = "2000"
                };

                var result = unitOfWork.BookEditions.Add(edition);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddBookEditionNegativeNumberOfLibraryCopiesTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "TestCaseDomain" } }
                };

                var publisher = new Publisher
                {
                    Name = "PublisherTestCase"
                };

                var edition = new BookEdition
                {
                    Book = book,
                    Publisher = publisher,
                    Copies = 80,
                    CopiesLibrary = -20,
                    Pages = 100,
                    Type = BookType.HardCover,
                    Year = "2000"
                };

                var result = unitOfWork.BookEditions.Add(edition);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddBookOver4DigitsYearTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "TestCaseDomain" } }
                };

                var publisher = new Publisher
                {
                    Name = "PublisherTestCase"
                };

                var edition = new BookEdition
                {
                    Book = book,
                    Publisher = publisher,
                    Copies = 80,
                    CopiesLibrary = 20,
                    Pages = 100,
                    Type = BookType.HardCover,
                    Year = "20000"
                };

                var result = unitOfWork.BookEditions.Add(edition);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddBookUnder4DigitsYearTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "TestCaseDomain" } }
                };

                var publisher = new Publisher
                {
                    Name = "PublisherTestCase"
                };

                var edition = new BookEdition
                {
                    Book = book,
                    Publisher = publisher,
                    Copies = 80,
                    CopiesLibrary = 20,
                    Pages = 100,
                    Type = BookType.HardCover,
                    Year = "200"
                };

                var result = unitOfWork.BookEditions.Add(edition);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddBookCharactersInYearTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "TestCaseDomain" } }
                };

                var publisher = new Publisher
                {
                    Name = "PublisherTestCase"
                };

                var edition = new BookEdition
                {
                    Book = book,
                    Publisher = publisher,
                    Copies = 80,
                    CopiesLibrary = 20,
                    Pages = 100,
                    Type = BookType.HardCover,
                    Year = "2a0a"
                };

                var result = unitOfWork.BookEditions.Add(edition);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddBookEditionWithLibraryCopiesMoreThanCopiesTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "TestCaseDomain" } }
                };

                var publisher = new Publisher
                {
                    Name = "PublisherTestCase"
                };

                var edition = new BookEdition
                {
                    Book = book,
                    Publisher = publisher,
                    Copies = 80,
                    CopiesLibrary = 200,
                    Pages = 100,
                    Type = BookType.HardCover,
                    Year = "2000"
                };

                var result = unitOfWork.BookEditions.Add(edition);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddBookEditionWithEmptyBookTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book();

                var publisher = new Publisher
                {
                    Name = "PublisherTestCase"
                };

                var edition = new BookEdition
                {
                    Book = book,
                    Publisher = publisher,
                    Copies = 80,
                    CopiesLibrary = 20,
                    Pages = 100,
                    Type = BookType.HardCover,
                    Year = "2000"
                };

                var result = unitOfWork.BookEditions.Add(edition);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddBookEditionWithNullBookTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var publisher = new Publisher
                {
                    Name = "PublisherTestCase"
                };

                var edition = new BookEdition
                {
                    Book = null,
                    Publisher = publisher,
                    Copies = 80,
                    CopiesLibrary = 20,
                    Pages = 100,
                    Type = BookType.HardCover,
                    Year = "2000"
                };

                var result = unitOfWork.BookEditions.Add(edition);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddBookEditionWithEmptyPublisherTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "TestCaseDomain" } }
                };

                var publisher = new Publisher();

                var edition = new BookEdition
                {
                    Book = book,
                    Publisher = publisher,
                    Copies = 80,
                    CopiesLibrary = 20,
                    Pages = 100,
                    Type = BookType.HardCover,
                    Year = "2000"
                };

                var result = unitOfWork.BookEditions.Add(edition);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddBookEditionWithNullPublisherTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "TestCaseDomain" } }
                };
                
                var edition = new BookEdition
                {
                    Book = book,
                    Publisher = null,
                    Copies = 80,
                    CopiesLibrary = 20,
                    Pages = 100,
                    Type = BookType.HardCover,
                    Year = "2000"
                };

                var result = unitOfWork.BookEditions.Add(edition);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditBookOfBookEditionTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "TestCaseDomain" } }
                };

                var publisher = new Publisher
                {
                    Name = "PublisherTestCase"
                };

                var edition = new BookEdition
                {
                    Book = book,
                    Publisher = publisher,
                    Copies = 80,
                    CopiesLibrary = 20,
                    Pages = 100,
                    Type = BookType.HardCover,
                    Year = "2000"
                };

                unitOfWork.BookEditions.Add(edition);

                var newBook= new Book
                {
                    Title = "New Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "TestCaseDomain" } }
                };

                edition.Book = newBook;

                var result = unitOfWork.BookEditions.EditBookEdition(edition);
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void EditBookOfBookEditionToEmptyTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "TestCaseDomain" } }
                };

                var publisher = new Publisher
                {
                    Name = "PublisherTestCase"
                };

                var edition = new BookEdition
                {
                    Book = book,
                    Publisher = publisher,
                    Copies = 80,
                    CopiesLibrary = 20,
                    Pages = 100,
                    Type = BookType.HardCover,
                    Year = "2000"
                };

                unitOfWork.BookEditions.Add(edition);

                var newBook = new Book();

                edition.Book = newBook;

                var result = unitOfWork.BookEditions.EditBookEdition(edition);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditBookOfBookEditionToNullTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "TestCaseDomain" } }
                };

                var publisher = new Publisher
                {
                    Name = "PublisherTestCase"
                };

                var edition = new BookEdition
                {
                    Book = book,
                    Publisher = publisher,
                    Copies = 80,
                    CopiesLibrary = 20,
                    Pages = 100,
                    Type = BookType.HardCover,
                    Year = "2000"
                };

                unitOfWork.BookEditions.Add(edition);

                edition.Book = null;

                var result = unitOfWork.BookEditions.EditBookEdition(edition);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditEmptyBookEditionTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var edition = new BookEdition();

                var result = unitOfWork.BookEditions.EditBookEdition(edition);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditNullBookEditionTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var result = unitOfWork.BookEditions.EditBookEdition(null);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditPublisherOfBookEditionTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "TestCaseDomain" } }
                };

                var publisher = new Publisher
                {
                    Name = "PublisherTestCase"
                };

                var edition = new BookEdition
                {
                    Book = book,
                    Publisher = publisher,
                    Copies = 80,
                    CopiesLibrary = 20,
                    Pages = 100,
                    Type = BookType.HardCover,
                    Year = "2000"
                };

                unitOfWork.BookEditions.Add(edition);

                var newPublisher = new Publisher
                {
                    Name = "NewPublisherTestCase"
                };

                edition.Publisher = newPublisher;

                var result = unitOfWork.BookEditions.EditBookEdition(edition);
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void EditPublisherOfBookEditionToEmptyTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "TestCaseDomain" } }
                };

                var publisher = new Publisher
                {
                    Name = "PublisherTestCase"
                };

                var edition = new BookEdition
                {
                    Book = book,
                    Publisher = publisher,
                    Copies = 80,
                    CopiesLibrary = 20,
                    Pages = 100,
                    Type = BookType.HardCover,
                    Year = "2000"
                };

                unitOfWork.BookEditions.Add(edition);

                var newPublisher = new Publisher();
                
                edition.Publisher = newPublisher;

                var result = unitOfWork.BookEditions.EditBookEdition(edition);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditPublisherOfBookEditionToNullTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "TestCaseDomain" } }
                };

                var publisher = new Publisher
                {
                    Name = "PublisherTestCase"
                };

                var edition = new BookEdition
                {
                    Book = book,
                    Publisher = publisher,
                    Copies = 80,
                    CopiesLibrary = 20,
                    Pages = 100,
                    Type = BookType.HardCover,
                    Year = "2000"
                };

                unitOfWork.BookEditions.Add(edition);


                edition.Publisher = null;

                var result = unitOfWork.BookEditions.EditBookEdition(edition);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditYearOfBookEditionTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "TestCaseDomain" } }
                };

                var publisher = new Publisher
                {
                    Name = "PublisherTestCase"
                };

                var edition = new BookEdition
                {
                    Book = book,
                    Publisher = publisher,
                    Copies = 80,
                    CopiesLibrary = 20,
                    Pages = 100,
                    Type = BookType.HardCover,
                    Year = "2000"
                };

                unitOfWork.BookEditions.Add(edition);

                edition.Year = "2001";

                var result = unitOfWork.BookEditions.EditBookEdition(edition);
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void EditYearOfBookEditionToInvalidTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "TestCaseDomain" } }
                };

                var publisher = new Publisher
                {
                    Name = "PublisherTestCase"
                };

                var edition = new BookEdition
                {
                    Book = book,
                    Publisher = publisher,
                    Copies = 80,
                    CopiesLibrary = 20,
                    Pages = 100,
                    Type = BookType.HardCover,
                    Year = "2000"
                };

                unitOfWork.BookEditions.Add(edition);

                edition.Year = "20a1";

                var result = unitOfWork.BookEditions.EditBookEdition(edition);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditCopiesOfBookEditionTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "TestCaseDomain" } }
                };

                var publisher = new Publisher
                {
                    Name = "PublisherTestCase"
                };

                var edition = new BookEdition
                {
                    Book = book,
                    Publisher = publisher,
                    Copies = 80,
                    CopiesLibrary = 20,
                    Pages = 100,
                    Type = BookType.HardCover,
                    Year = "2000"
                };

                unitOfWork.BookEditions.Add(edition);

                edition.Copies = 100;

                var result = unitOfWork.BookEditions.EditBookEdition(edition);
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void EditCopiesOfBookEditionToInvalidTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "TestCaseDomain" } }
                };

                var publisher = new Publisher
                {
                    Name = "PublisherTestCase"
                };

                var edition = new BookEdition
                {
                    Book = book,
                    Publisher = publisher,
                    Copies = 80,
                    CopiesLibrary = 20,
                    Pages = 100,
                    Type = BookType.HardCover,
                    Year = "2000"
                };

                unitOfWork.BookEditions.Add(edition);

                edition.Copies = -80;

                var result = unitOfWork.BookEditions.EditBookEdition(edition);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditPagesOfBookEditionTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "TestCaseDomain" } }
                };

                var publisher = new Publisher
                {
                    Name = "PublisherTestCase"
                };

                var edition = new BookEdition
                {
                    Book = book,
                    Publisher = publisher,
                    Copies = 80,
                    CopiesLibrary = 20,
                    Pages = 100,
                    Type = BookType.HardCover,
                    Year = "2000"
                };

                unitOfWork.BookEditions.Add(edition);

                edition.Pages = 150;

                var result = unitOfWork.BookEditions.EditBookEdition(edition);
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void EditPagesOfBookEditionToInvalidTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "TestCaseDomain" } }
                };

                var publisher = new Publisher
                {
                    Name = "PublisherTestCase"
                };

                var edition = new BookEdition
                {
                    Book = book,
                    Publisher = publisher,
                    Copies = 80,
                    CopiesLibrary = 20,
                    Pages = 100,
                    Type = BookType.HardCover,
                    Year = "2000"
                };

                unitOfWork.BookEditions.Add(edition);

                edition.Pages = -100;

                var result = unitOfWork.BookEditions.EditBookEdition(edition);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditLibraryCopiesOfBookEditionTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "TestCaseDomain" } }
                };

                var publisher = new Publisher
                {
                    Name = "PublisherTestCase"
                };

                var edition = new BookEdition
                {
                    Book = book,
                    Publisher = publisher,
                    Copies = 80,
                    CopiesLibrary = 20,
                    Pages = 100,
                    Type = BookType.HardCover,
                    Year = "2000"
                };

                unitOfWork.BookEditions.Add(edition);

                edition.CopiesLibrary = 25;

                var result = unitOfWork.BookEditions.EditBookEdition(edition);
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void EditLibraryCopiesOfBookEditionToInvalidTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "TestCaseDomain" } }
                };

                var publisher = new Publisher
                {
                    Name = "PublisherTestCase"
                };

                var edition = new BookEdition
                {
                    Book = book,
                    Publisher = publisher,
                    Copies = 80,
                    CopiesLibrary = 20,
                    Pages = 100,
                    Type = BookType.HardCover,
                    Year = "2000"
                };

                unitOfWork.BookEditions.Add(edition);

                edition.CopiesLibrary = -80;

                var result = unitOfWork.BookEditions.EditBookEdition(edition);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditLibraryCopiesOfBookEditionToMoreThanCopiesTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "TestCaseDomain" } }
                };

                var publisher = new Publisher
                {
                    Name = "PublisherTestCase"
                };

                var edition = new BookEdition
                {
                    Book = book,
                    Publisher = publisher,
                    Copies = 80,
                    CopiesLibrary = 20,
                    Pages = 100,
                    Type = BookType.HardCover,
                    Year = "2000"
                };

                unitOfWork.BookEditions.Add(edition);

                edition.CopiesLibrary = 100;

                var result = unitOfWork.BookEditions.EditBookEdition(edition);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void DeleteBookEditionTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "TestCaseDomain" } }
                };

                var publisher = new Publisher
                {
                    Name = "PublisherTestCase"
                };

                var edition = new BookEdition
                {
                    Book = book,
                    Publisher = publisher,
                    Copies = 80,
                    CopiesLibrary = 20,
                    Pages = 100,
                    Type = BookType.HardCover,
                    Year = "2000"
                };

                unitOfWork.BookEditions.Add(edition);

                var result = unitOfWork.BookEditions.Remove(edition);
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void DeleteEmptyBookEditionTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var edition = new BookEdition();

                var result = unitOfWork.BookEditions.Remove(edition);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void DeleteNullBookEditionTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var result = unitOfWork.BookEditions.Remove(null);
                Assert.AreEqual(false, result);
            }
        }

    }
}
