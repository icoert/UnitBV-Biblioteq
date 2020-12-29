using System;
using System.Collections.Generic;
using System.Configuration;
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

        [TestMethod]
        public void AddBookTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "DomainTestCase" } }
                };

                var result = unitOfWork.Books.Add(book);
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void AddEmptyBookTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book();

                var result = unitOfWork.Books.Add(book);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddNullBookTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var result = unitOfWork.Books.Add(null);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddBookJustWithTitleTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",

                };

                var result = unitOfWork.Books.Add(book);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddBookWithEmptyAuthorsTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> (),
                    Domains = new List<Domain> { new Domain { Name = "DomainTestCase" } }
                };

                var result = unitOfWork.Books.Add(book);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddBookWithNullAuthorsTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = null,
                    Domains = new List<Domain> { new Domain { Name = "DomainTestCase" } }
                };

                var result = unitOfWork.Books.Add(book);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddBookWithEmptyDomainsTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain>()
                };

                var result = unitOfWork.Books.Add(book);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddBookWithNullDomainsTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = null
                };

                var result = unitOfWork.Books.Add(book);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddBookWithOverMaxDomainsTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var maxDomains = int.Parse(ConfigurationManager.AppSettings["MaxDomainsForBook"]);

                var domainsList = new List<Domain>();
                for (var i = 0; i < maxDomains + 1; i++)
                {
                    domainsList.Add(new Domain { Name = "DomainTestCase" + i } );
                }
                
                var book = new Book
                    {
                        Title = "Fake Title For Test",
                        Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                        Domains = domainsList
                    };

                    var result = unitOfWork.Books.Add(book);
                    Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddBookWithInvalidDomainStructureTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var maxDomains = int.Parse(ConfigurationManager.AppSettings["MaxDomainsForBook"]);

                var domainsList = new List<Domain>();
                var firstDomain = new Domain {Name = "DomainTestCase11"};
                var secondDomain = new Domain {Name = "DomainTestCase12", Parent = firstDomain}; // this one is a domain of firstDomain, thus it is not a correct structure
                
                domainsList.Add(firstDomain);
                domainsList.Add(secondDomain);

                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = domainsList
                };

                var result = unitOfWork.Books.Add(book);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditTitleOfBookTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "DomainTestCase" } }
                };

                unitOfWork.Books.Add(book);

                book.Title = "New Face Title For Test";

                var result = unitOfWork.Books.EditBook(book);
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void EditEmptyBookTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book();

                var result = unitOfWork.Books.EditBook(book);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditNullBookTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var result = unitOfWork.Books.EditBook(null);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditTitleOfBookToEmptyTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "DomainTestCase" } }
                };

                unitOfWork.Books.Add(book);

                book.Title = "";

                var result = unitOfWork.Books.EditBook(book);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditAuthorsOfBookToEmptyTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "DomainTestCase" } }
                };

                unitOfWork.Books.Add(book);

                book.Authors = new List<Author>();
                
                var result = unitOfWork.Books.EditBook(book);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditAuthorsOfBookToNullTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "DomainTestCase" } }
                };

                unitOfWork.Books.Add(book);

                book.Authors = null;

                var result = unitOfWork.Books.EditBook(book);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditDomainsOfBookToEmptyTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "DomainTestCase" } }
                };

                unitOfWork.Books.Add(book);

                book.Domains = new List<Domain>();

                var result = unitOfWork.Books.EditBook(book);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditDomainsOfBookToNullTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "DomainTestCase" } }
                };

                unitOfWork.Books.Add(book);

                book.Domains = null;

                var result = unitOfWork.Books.EditBook(book);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditDomainsOfBookToOverMaxDomainsTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var maxDomains = int.Parse(ConfigurationManager.AppSettings["MaxDomainsForBook"]);

                var domainsList = new List<Domain>();
                for (var i = 0; i < maxDomains; i++)
                {
                    domainsList.Add(new Domain { Name = "DomainTestCase" + i });
                }
                
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = domainsList
                };

                unitOfWork.Books.Add(book);

                domainsList.Add(new Domain() { Name = "DomainTestCase" + maxDomains });
                book.Domains = domainsList;

                var result = unitOfWork.Books.EditBook(book);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void DeleteBookTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book
                {
                    Title = "Fake Title For Test",
                    Authors = new List<Author> { new Author { Firstname = "AuthorFNTest", Lastname = "AuthorLNTest" } },
                    Domains = new List<Domain> { new Domain { Name = "DomainTestCase" } }
                };

                unitOfWork.Books.Add(book);
                
                var result = unitOfWork.Books.Remove(book);
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void DeleteEmptyBookTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var book = new Book();

                var result = unitOfWork.Books.Remove(book);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void DeleteNullBookTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var result = unitOfWork.Books.Remove(null);
                Assert.AreEqual(false, result);
            }
        }
    }
}
