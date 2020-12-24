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
                var obj = mock.Object;

                var result = obj.Add(author);

                if (result)
                {
                    result = unitOfWork.Complete();
                }

                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void AddValidAuthorTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var author = new Author() {Firstname = "Test11", Lastname = "Test12"};
                var result = unitOfWork.Authors.Add(author);

                if (result)
                {
                    result = unitOfWork.Complete();
                }

                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void AddAuthorWithEmptyNamesTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var author = new Author() { Firstname = "", Lastname = "" };
                var result = unitOfWork.Authors.Add(author);

                if (result)
                {
                    result = unitOfWork.Complete();
                }

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddAuthorWithNullNamesTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var author = new Author() { Firstname = null, Lastname = null };
                var result = unitOfWork.Authors.Add(author);

                if (result)
                {
                    result = unitOfWork.Complete();
                }

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddAuthorWithOnlyFirstnameTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var author = new Author() { Firstname = "Test21"};
                var result = unitOfWork.Authors.Add(author);

                if (result)
                {
                    result = unitOfWork.Complete();
                }

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddAuthorWithOnlyLastnameTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var author = new Author() { Lastname = "Test22" };
                var result = unitOfWork.Authors.Add(author);

                if (result)
                {
                    result = unitOfWork.Complete();
                }

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditAuthorMockTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var mock = new Mock<IAuthorsRepository>();

                mock.Setup(m => m.EditAuthor(It.IsAny<Author>())).Returns(true);

                var author = new Author();
                var obj = mock.Object;

                var result = obj.EditAuthor(author);

                if (result)
                {
                    result = unitOfWork.Complete();
                }

                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void EditAuthorFullNameTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var author = new Author() {Firstname = "Test31", Lastname = "Test32"};

                unitOfWork.Authors.Add(author);
                unitOfWork.Complete();

                author.Firstname = "Edited";
                author.Lastname = "Edited";

                var result = unitOfWork.Authors.EditAuthor(author);
                if (result)
                {
                    result = unitOfWork.Complete();
                }

                var editedAuthor = unitOfWork.Authors.Get(author.Id);

                Assert.AreEqual(true, result);
                Assert.AreEqual(author.Firstname, editedAuthor.Firstname);
                Assert.AreEqual(author.Lastname, editedAuthor.Lastname);
            }
        }

        [TestMethod]
        public void EditNoAuthorTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var author = new Author();

                var result = unitOfWork.Authors.EditAuthor(author);

                if (result)
                {
                    result = unitOfWork.Complete();
                }

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditNullAuthorTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var result = unitOfWork.Authors.EditAuthor(null);

                if (result)
                {
                    result = unitOfWork.Complete();
                }

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditAuthorFirstnameTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var author = new Author() {Firstname = "Test41", Lastname = "Test42"};

                unitOfWork.Authors.Add(author);
                unitOfWork.Complete();

                author.Firstname = "Edited";

                var result = unitOfWork.Authors.EditAuthor(author);

                if (result)
                {
                    result = unitOfWork.Complete();
                }

                var editedAuthor = unitOfWork.Authors.Get(author.Id);
                Assert.AreEqual(true, result);
                Assert.AreEqual(author.Firstname, editedAuthor.Firstname);
            }
        }

        [TestMethod]
        public void EditAuthorFirstnameEmptyTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var author = new Author() { Firstname = "Test41", Lastname = "Test42" };

                unitOfWork.Authors.Add(author);
                unitOfWork.Complete();

                author.Firstname = "";

                var result = unitOfWork.Authors.EditAuthor(author);

                if (result)
                {
                    result = unitOfWork.Complete();
                }

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditAuthorFirstnameNullTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var author = new Author() { Firstname = "Test41", Lastname = "Test42" };

                unitOfWork.Authors.Add(author);
                unitOfWork.Complete();

                author.Firstname = null;

                var result = unitOfWork.Authors.EditAuthor(author);

                if (result)
                {
                    result = unitOfWork.Complete();
                }

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditAuthorLastnameTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var author = new Author() { Firstname = "Test51", Lastname = "Test52" };

                unitOfWork.Authors.Add(author);
                unitOfWork.Complete();

                author.Lastname = "Edited";

                var result = unitOfWork.Authors.EditAuthor(author);

                if (result)
                {
                    result = unitOfWork.Complete();
                }

                var editedAuthor = unitOfWork.Authors.Get(author.Id);
                Assert.AreEqual(true, result);
                Assert.AreEqual(author.Lastname, editedAuthor.Lastname);
            }
        }

        [TestMethod]
        public void EditAuthorLastnameEmptyTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var author = new Author() { Firstname = "Test61", Lastname = "Test62" };

                unitOfWork.Authors.Add(author);
                unitOfWork.Complete();

                author.Lastname = "";

                var result = unitOfWork.Authors.EditAuthor(author);

                if (result)
                {
                    result = unitOfWork.Complete();
                }

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditAuthorLastnameNullTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var author = new Author() { Firstname = "Test71", Lastname = "Test72" };

                unitOfWork.Authors.Add(author);
                unitOfWork.Complete();

                author.Lastname = null;

                var result = unitOfWork.Authors.EditAuthor(author);

                if (result)
                {
                    result = unitOfWork.Complete();
                }

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void DeleteAuthorMockTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var mock = new Mock<IRepository<Author>>();

                mock.Setup(m => m.Remove(It.IsAny<Author>())).Returns(true);

                var author = new Author();
                var obj = mock.Object;

                var result = obj.Remove(author);

                if (result)
                {
                    result = unitOfWork.Complete();
                }

                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void DeleteAuthorTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var author = new Author() { Firstname = "Test81", Lastname = "Test82" };

                unitOfWork.Authors.Add(author);
                unitOfWork.Complete();


                var result = unitOfWork.Authors.Remove(author);
                if (result)
                {
                    result = unitOfWork.Complete();
                }

                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void DeleteNoAuthorTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var author = new Author();


                var result = unitOfWork.Authors.Remove(author);
                if (result)
                {
                    result = unitOfWork.Complete();
                }

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void DeleteAuthorNullTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var result = unitOfWork.Authors.Remove(null);
                if (result)
                {
                    result = unitOfWork.Complete();
                }

                Assert.AreEqual(false, result);
            }
        }
    }



}
