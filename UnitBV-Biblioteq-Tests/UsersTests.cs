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
    public class UsersTests
    {
        [TestMethod]
        public void AddUserMockTest()
        {
            var mock = new Mock<IUserRepository>();

                mock.Setup(m => m.Add(It.IsAny<User>())).Returns(true);

                var user = new User();
                var obj = mock.Object;

                var result = obj.Add(user);

                Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void EditUserMockTest()
        {
            var mock = new Mock<IUserRepository>();

                mock.Setup(m => m.EditUser(It.IsAny<User>())).Returns(true);

                var user = new User();
                var obj = mock.Object;

                var result = obj.EditUser(user);

                Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void DeleteUserMockTest()
        {
            var mock = new Mock<IUserRepository>();

                mock.Setup(m => m.Remove(It.IsAny<User>())).Returns(true);

                var user = new User();
                var obj = mock.Object;

                var result = obj.Remove(user);

                Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void AddUserWithEmailTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var user = new User()
                {
                    Firstname = "UserFNTestCase",
                    Lastname = "UseLNTestCase",
                    Email = "testcase@mail.com"
                };
                var result = unitOfWork.Users.Add(user);
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void AddUserWithAllFieldsTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var user = new User()
                {
                    Firstname = "UserFNTestCase",
                    Lastname = "UseLNTestCase",
                    Email = "testcase@mail.com",
                    PhoneNumber = "+40755567454",
                    Address = "Street FarAway, number 999",
                };
                var result = unitOfWork.Users.Add(user);
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void AddUserWithInvalidEmailTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var user = new User()
                {
                    Firstname = "UserFNTestCase",
                    Lastname = "UseLNTestCase",
                    Email = "testCase"
                };
                var result = unitOfWork.Users.Add(user);
                Assert.AreEqual(false, result);
            }

        }

        [TestMethod]
        public void AddUserWithPhoneNumberTest()
        {

            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var user = new User()
                {
                    Firstname = "UserFNTestCase",
                    Lastname = "UseLNTestCase",
                    PhoneNumber = "+40792696111",
                };
                var result = unitOfWork.Users.Add(user);
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void AddUserWithInvalidPhoneNumberTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var user = new User()
                {
                    Firstname = "UserFNTestCase",
                    Lastname = "UseLNTestCase",
                    PhoneNumber = "07WRONG"
                };
                var result = unitOfWork.Users.Add(user);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddUserWithAddressTest()
        {

            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var user = new User()
                {
                    Firstname = "UserFNTestCase",
                    Lastname = "UseLNTestCase",
                    Address = "St OneStreet, Number 1"
                };
                var result = unitOfWork.Users.Add(user);
                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void AddUserWithInvalidAddressTest()
        {

            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var user = new User()
                {
                    Firstname = "UserFNTestCase",
                    Lastname = "UseLNTestCase",
                    Address = "OneStreet 2"
                };
                var result = unitOfWork.Users.Add(user);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddUserWithInvalidEmailAndAddressTest()
        {

            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var user = new User()
                {
                    Firstname = "UserFNTestCase",
                    Lastname = "UseLNTestCase",
                    Email = "noEmail",
                    Address = "OneStreet 2"
                };
                var result = unitOfWork.Users.Add(user);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddUserWithInvalidEmailAndPhoneNumberTest()
        {

            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var user = new User()
                {
                    Firstname = "UserFNTestCase",
                    Lastname = "UseLNTestCase",
                    Email = "noEmail",
                    PhoneNumber = "07number"
                };
                var result = unitOfWork.Users.Add(user);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddUserWithInvalidPhoneNumberAndAddressTest()
        {

            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var user = new User()
                {
                    Firstname = "UserFNTestCase",
                    Lastname = "UseLNTestCase",
                    PhoneNumber = "07noidoi",
                    Address = "OneStreet 2"
                };
                var result = unitOfWork.Users.Add(user);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddUserWithInvalidEmailAndPhoneNumberAndAddressTest()
        {

            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var user = new User()
                {
                    Firstname = "UserFNTestCase",
                    Lastname = "UseLNTestCase",
                    Email = "noEmail",
                    PhoneNumber = "07noidoi",
                    Address = "OneStreet 2"
                };
                var result = unitOfWork.Users.Add(user);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddUserWithNoEmailPhoneOrAddressTest()
        {

            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var user = new User()
                {
                    Firstname = "UserFNTestCase",
                    Lastname = "UseLNTestCase",
                };
                var result = unitOfWork.Users.Add(user);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddNullUserTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var result = unitOfWork.Users.Add(null);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddUserWithNullFirstNameTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var user = new User()
                {
                    Firstname = null,
                    Lastname = "UseLNTestCase",
                    Email = "testcase@mail.com",
                };
                var result = unitOfWork.Users.Add(user);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddUserWithNullLastNameTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var user = new User()
                {
                    Firstname = "UseFNTestCase",
                    Lastname = null,
                    Email = "testcase@mail.com",
                };
                var result = unitOfWork.Users.Add(user);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddUserWithNullNameTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var user = new User()
                {
                    Firstname = null,
                    Lastname = null,
                    Email = "testcase@mail.com",
                };
                var result = unitOfWork.Users.Add(user);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddUserWithEmptyFirstNameTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var user = new User()
                {
                    Firstname = "",
                    Lastname = "UseLNTestCase",
                    Email = "testcase@mail.com",
                };
                var result = unitOfWork.Users.Add(user);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddUserWithEmptyLastNameTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var user = new User()
                {
                    Firstname = "UseFNTestCase",
                    Lastname = "",
                    Email = "testcase@mail.com",
                };
                var result = unitOfWork.Users.Add(user);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddUserWithEmptyNameTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var user = new User()
                {
                    Firstname = "",
                    Lastname = "",
                    Email = "testcase@mail.com",
                };
                var result = unitOfWork.Users.Add(user);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddUserWithOnlyFirstNameTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var user = new User()
                {
                    Firstname = "UseFNTestCase",
                    Email = "testcase@mail.com",
                };
                var result = unitOfWork.Users.Add(user);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void AddUserWithOnlyLastNameTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var user = new User()
                {
                    Lastname = "UseLNTestCase",
                    Email = "testcase@mail.com",
                };
                var result = unitOfWork.Users.Add(user);
                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditUserToFirstNameEmptyTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var user = new User
                {
                    Firstname = "UserFNTestCase",
                    Lastname = "UserLNTestCase",
                    Email = "testCase@email.com"
                };

                unitOfWork.Users.Add(user);

                user.Firstname = "";
                
                var result = unitOfWork.Users.EditUser(user);

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditUserToFirstNameNullTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var user = new User
                {
                    Firstname = "UserFNTestCase",
                    Lastname = "UserLNTestCase",
                    Email = "testCase@email.com"
                };

                unitOfWork.Users.Add(user);

                user.Firstname = null;

                var result = unitOfWork.Users.EditUser(user);

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditUserToLastNameEmptyTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var user = new User
                {
                    Firstname = "UserFNTestCase",
                    Lastname = "UserLNTestCase",
                    Email = "testCase@email.com"
                };

                unitOfWork.Users.Add(user);

                user.Lastname = "";

                var result = unitOfWork.Users.EditUser(user);

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditUserToLastNameNullTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var user = new User
                {
                    Firstname = "UserFNTestCase",
                    Lastname = "UserLNTestCase",
                    Email = "testCase@email.com"
                };

                unitOfWork.Users.Add(user);

                user.Lastname = null;

                var result = unitOfWork.Users.EditUser(user);

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditNullUserTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var result = unitOfWork.Users.EditUser(null);

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditEmptyUserTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var user = new User();
                var result = unitOfWork.Users.EditUser(user);

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditUserToInvalidEmailTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var user = new User
                {
                    Firstname = "UserFNTestCase",
                    Lastname = "UserLNTestCase",
                    Email = "testCase@email.com"
                };

                unitOfWork.Users.Add(user);

                user.Email = "noEmail";

                var result = unitOfWork.Users.EditUser(user);

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditUserToInvalidAddressTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var user = new User
                {
                    Firstname = "UserFNTestCase",
                    Lastname = "UserLNTestCase",
                    Email = "testCase@email.com",
                    Address = "Street OneStreet, number 69"
                };

                unitOfWork.Users.Add(user);

                user.Address = "street oneStreet";

                var result = unitOfWork.Users.EditUser(user);

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditUserToInvalidPhoneNumberTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var user = new User
                {
                    Firstname = "UserFNTestCase",
                    Lastname = "UserLNTestCase",
                    Email = "testCase@email.com",
                    Address = "Street OneStreet, number 69",
                    PhoneNumber = "+40745234493",
                };

                unitOfWork.Users.Add(user);

                user.PhoneNumber = "32131";

                var result = unitOfWork.Users.EditUser(user);

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void EditUserPhoneNumberTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var user = new User
                {
                    Firstname = "UserFNTestCase",
                    Lastname = "UserLNTestCase",
                    Email = "testCase@email.com",
                    Address = "Street OneStreet, number 69",
                    PhoneNumber = "+40745234493",
                };

                unitOfWork.Users.Add(user);

                user.PhoneNumber = "+40745234492";

                var result = unitOfWork.Users.EditUser(user);

                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void EditUserAddressTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var user = new User
                {
                    Firstname = "UserFNTestCase",
                    Lastname = "UserLNTestCase",
                    Email = "testCase@email.com",
                    Address = "Street OneStreet, number 69",
                    PhoneNumber = "+40745234493",
                };

                unitOfWork.Users.Add(user);

                user.Address = "Street OneStreet, number 99";

                var result = unitOfWork.Users.EditUser(user);

                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void DeleteUserTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var user = new User { 
                    Firstname = "UserFNTestCase", 
                    Lastname = "UserLNTestCase",
                    Email = "testCase@email.com"
                };

                unitOfWork.Users.Add(user);


                var result = unitOfWork.Users.Remove(user);

                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void DeleteNullUserTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var result = unitOfWork.Users.Remove(null);

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void DeleteEmptyUserTest()
        {
            using (var unitOfWork = new UnitOfWork(new AppDbContext()))
            {
                var user = new User();
                var result = unitOfWork.Users.Remove(user);

                Assert.AreEqual(false, result);
            }
        }
    }
}
