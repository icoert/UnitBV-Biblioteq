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
    }
}
