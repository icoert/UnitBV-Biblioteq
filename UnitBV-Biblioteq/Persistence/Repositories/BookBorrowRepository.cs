// ***********************************************************************
// Assembly         : UnitBV-Biblioteq
// Author           : silvi
// Created          : 12-20-2020
//
// Last Modified By : silvi
// Last Modified On : 12-29-2020
// ***********************************************************************
// <copyright file="BookBorrowRepository.cs" company="Transilvanya University of Brasov">
//     Copyright © Silviu-Daniel Vijiala 2020
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace UnitBV_Biblioteq.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using log4net;
    using UnitBV_Biblioteq.Core.DomainModel;
    using UnitBV_Biblioteq.Core.Repositories;

    /// <summary>
    /// Class BookBorrowRepository.
    /// Implements the <see cref="UnitBV_Biblioteq.Persistence.Repositories.Repository{UnitBV_Biblioteq.Core.DomainModel.BookBorrow}" />
    /// Implements the <see cref="UnitBV_Biblioteq.Core.Repositories.IBookBorrowRepository" />
    /// </summary>
    /// <seealso cref="UnitBV_Biblioteq.Persistence.Repositories.Repository{UnitBV_Biblioteq.Core.DomainModel.BookBorrow}" />
    /// <seealso cref="UnitBV_Biblioteq.Core.Repositories.IBookBorrowRepository" />
    public class BookBorrowRepository : Repository<BookBorrow>, IBookBorrowRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookBorrowRepository" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public BookBorrowRepository(AppDbContext context) 
            : base(context)
        {
        }

        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(typeof(BookBorrowRepository));

        /// <summary>
        /// Gets the application database context.
        /// </summary>
        /// <value>The application database context.</value>
        private AppDbContext AppDbContext => Context as AppDbContext;

        /// <summary>
        /// Gets the book borrows.
        /// </summary>
        /// <value>The book borrows.</value>
        public IEnumerable<BookBorrow> BookBorrows => AppDbContext.BookBorrows;

        /// <summary>
        /// Adds the specified borrow.
        /// </summary>
        /// <param name="borrow">The borrow.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public new bool Add(BookBorrow borrow)
        {
            try
            {
                if (borrow == null)
                {
                    Logger.Info("Failed to add null book borrow.");
                    return false;
                }
                if (!this.IsValidObject(borrow))
                {
                    Logger.Info("Failed to add book borrow.");
                    return false;
                }

                borrow.LastReBorrowDate = borrow.BorrowDate;
                AppDbContext.BookBorrows.Add(borrow);
                AppDbContext.SaveChanges();
                Logger.Info($"New book borrow was added(id={borrow.Id}).");
            }
            catch (Exception ex)
            {
                Logger.Info("Failed to add book borrow.");
                Logger.Error(ex.Message, ex);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Edits the book borrow.
        /// </summary>
        /// <param name="borrow">The borrow.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool EditBookBorrow(BookBorrow borrow)
        {
            try
            {
                if (borrow == null)
                {
                    Logger.Info("Failed to add null book borrow.");
                    return false;
                }
                var existing = AppDbContext.BookBorrows.FirstOrDefault(a => a.Id == borrow.Id);
                if (existing != null)
                {
                    if (!this.IsValidObject(borrow))
                    {
                        Logger.Info("Failed to edit book borrow.");
                        return false;
                    }

                    existing.Books = borrow.Books;
                    existing.BorrowDate = borrow.BorrowDate;
                    existing.Employee = borrow.Employee;
                    existing.IsReturned = borrow.IsReturned;
                    existing.LastReBorrowDate = borrow.LastReBorrowDate;
                    existing.ReBorrows = borrow.ReBorrows;
                    existing.Reader = borrow.Reader;
                    existing.ReturnDate = borrow.ReturnDate;

                    AppDbContext.SaveChanges();
                    Logger.Info($"Book borrow with id={borrow.Id} was updated.");
                }
                else
                {
                    Logger.Info($"Failed to update book borrow with id={borrow.Id}.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Info(borrow != null
                    ? $"Failed to update book borrow with id={borrow.Id}."
                    : $"Failed to update book borrow.");
                Logger.Error(ex.Message, ex);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Res the borrow book.
        /// </summary>
        /// <param name="borrow">The borrow.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool ReBorrowBook(BookBorrow borrow)
        {
            try
            {
                if (borrow == null)
                {
                    Logger.Info("Failed to reborrow null book borrow.");
                    return false;
                }

                var reborrowLimit = int.Parse(ConfigurationManager.AppSettings["ReBorrowLimit"]);

                var existing = AppDbContext.BookBorrows.FirstOrDefault(a => a.Id == borrow.Id);
                if (existing != null)
                {
                    if (existing.Reader.UserType == UserType.Employee)
                    {
                        reborrowLimit *= 2;
                    }

                    if (this.IsValidReBorrow(borrow, reborrowLimit))
                    {
                        existing.ReBorrows++;
                    }
                    else
                    {
                        Logger.Info($"Failed to update book borrow with id={borrow.Id}.");
                        return false;
                    }

                    AppDbContext.SaveChanges();

                    Logger.Info($"Book borrow with id={borrow.Id} was updated.");
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Info(borrow != null
                    ? $"Failed to update book borrow with id={borrow.Id}."
                    : $"Failed to update book borrow.");
                Logger.Error(ex.Message, ex);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns the books.
        /// </summary>
        /// <param name="borrow">The borrow.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool ReturnBooks(BookBorrow borrow)
        {
            try
            {
                if (borrow == null)
                {
                    Logger.Info("Failed to return null book borrow.");
                    return false;
                }

                var existing = AppDbContext.BookBorrows.FirstOrDefault(a => a.Id == borrow.Id);
                if (existing != null)
                {
                    existing.IsReturned = true;
                    existing.ReturnDate = DateTime.Now;

                    AppDbContext.SaveChanges();

                    Logger.Info($"Book borrow with id={borrow.Id} was updated.");
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Info(borrow != null
                    ? $"Failed to update book borrow with id={borrow.Id}."
                    : $"Failed to update book borrow.");
                Logger.Error(ex.Message, ex);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Determines whether [is valid object] [the specified borrow].
        /// </summary>
        /// <param name="borrow">The borrow.</param>
        /// <returns><c>true</c> if [is valid object] [the specified borrow]; otherwise, <c>false</c>.</returns>
        private bool IsValidObject(BookBorrow borrow)
        {
            var maxBooksPerBorrow = int.Parse(ConfigurationManager.AppSettings["MaxBooksPerBorrow"]);
            var maxBooksPerPeriod = int.Parse(ConfigurationManager.AppSettings["MaxBooksPerPeriod"]);
            var periodInDaysMaxBookPerPeriod = int.Parse(ConfigurationManager.AppSettings["PeriodInDaysForMaxBooksPerPeriod"]);
            var maxBooksInSameDomain = int.Parse(ConfigurationManager.AppSettings["MaxBooksInSameDomain"]);
            var periodInMonthsForSameDomain = int.Parse(ConfigurationManager.AppSettings["PeriodInMonthsForMaxBooksInSameDomain"]);
            var periodInDaysForSameBookBorrow = int.Parse(ConfigurationManager.AppSettings["PeriodInDaysForSameBookBorrowing"]);
            var maxBooksPerDay = int.Parse(ConfigurationManager.AppSettings["MaxBooksPerDay"]);
            var bookLimitPerEmployee = int.Parse(ConfigurationManager.AppSettings["MaxBooksPerDayGivenByEmployee"]);
            var reborrowLimit = int.Parse(ConfigurationManager.AppSettings["ReBorrowLimit"]);

            if (borrow.Books == null || borrow.Employee == null || borrow.Reader == null)
            {
                return false;
            }

            if (borrow.Employee.UserType != UserType.Employee)
            {
                return false;
            }

            if (borrow.BorrowDate > DateTime.Now)
            {
                return false;
            }

            if (borrow.ReturnDate.HasValue && borrow.ReturnDate.Value < borrow.BorrowDate)
            {
                return false;
            }

            if (borrow.Books.Count != borrow.Books.Distinct().Count())
            {
                return false;
            }

            if (borrow.Reader.UserType == UserType.Employee)
            {
                maxBooksPerPeriod *= 2;
                maxBooksPerBorrow *= 2;
                maxBooksInSameDomain *= 2;
                periodInDaysForSameBookBorrow /= 2;
                periodInDaysMaxBookPerPeriod /= 2;
            }

            if (!this.IsValidReBorrow(borrow, reborrowLimit))
            {
                return false;
            }

            if (borrow.Books.Count > maxBooksPerBorrow || borrow.Books.Count == 0)
            {
                return false;
            }

            if (borrow.Books.Count >= 3 && !this.HasTwoDistinctDomains(borrow.Books))
            {
                return false;
            }

            if (!this.AreBooksAvailable(borrow.Books))
            {
                return false;
            }

            if (borrow.Reader.UserType != UserType.Employee && this.HasReachedMaxBooksPerDay(borrow, maxBooksPerDay))
            {
                return false;
            }

            if (this.HasReachedBookLimitPerPeriod(borrow, maxBooksPerPeriod, periodInDaysMaxBookPerPeriod))
            {
                return false;
            }

            if (this.HasReachedBookLimitPerSameDomain(borrow, maxBooksInSameDomain, periodInMonthsForSameDomain))
            {
                return false;
            }

            if (this.HasReachedBookLimitPerEmployee(borrow, bookLimitPerEmployee))
            {
                return false;
            }

            if (!this.IsValidSameBookBorrow(borrow, periodInDaysForSameBookBorrow))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Determines whether [is valid re borrow] [the specified borrow].
        /// </summary>
        /// <param name="borrow">The borrow.</param>
        /// <param name="reborrowLimit">The reborrow limit.</param>
        /// <returns><c>true</c> if [is valid re borrow] [the specified borrow]; otherwise, <c>false</c>.</returns>
        private bool IsValidReBorrow(BookBorrow borrow, int reborrowLimit)
        {
            var limitDate = DateTime.Now.AddMonths(-3);
            var last3MonthsBooks = AppDbContext.BookBorrows.Where(b => b.Reader.Id == borrow.Reader.Id && b.LastReBorrowDate >= limitDate);
            var reborrowsNr = 0;

            foreach (var bookBorrow in last3MonthsBooks)
            {
                reborrowsNr += bookBorrow.ReBorrows;
            }

            if (reborrowsNr + borrow.ReBorrows > reborrowLimit)
            {
                return false;
            }

            if (reborrowsNr < reborrowLimit)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether [has two distinct domains] [the specified books].
        /// </summary>
        /// <param name="books">The books.</param>
        /// <returns><c>true</c> if [has two distinct domains] [the specified books]; otherwise, <c>false</c>.</returns>
        private bool HasTwoDistinctDomains(List<BookEdition> books)
        {
            var nrDomain = 0;
            var domains = new List<Domain>();

            foreach (var book in books)
            {
                domains.AddRange(book.Book.Domains);
            }

            foreach (var domain in domains.Distinct())
            {
                var parent = domain.Parent;
                var isDistinct = true;
                while (parent != null)
                {
                    if (domains.Contains(parent))
                    {
                        isDistinct = false;
                        break;
                    }

                    parent = parent.Parent;
                }

                if (isDistinct)
                {
                    nrDomain++;
                }
            }

            return nrDomain >= 2;
        }

        /// <summary>
        /// Determines whether [has reached book limit per period] [the specified borrow].
        /// </summary>
        /// <param name="borrow">The borrow.</param>
        /// <param name="maxBooksPerPeriod">The maximum books per period.</param>
        /// <param name="periodInDaysMaxBookPerPeriod">The period in days maximum book per period.</param>
        /// <returns><c>true</c> if [has reached book limit per period] [the specified borrow]; otherwise, <c>false</c>.</returns>
        private bool HasReachedBookLimitPerPeriod(BookBorrow borrow, int maxBooksPerPeriod, int periodInDaysMaxBookPerPeriod)
        {
            var limitDate = DateTime.Now.AddDays(-periodInDaysMaxBookPerPeriod);
            var borrowsInPeriod = AppDbContext.BookBorrows.Where(b => b.Reader.Id == borrow.Reader.Id && b.BorrowDate >= limitDate);
            var numberOfBooks = 0;
            foreach (var borrowInPer in borrowsInPeriod)
            {
                numberOfBooks += borrowInPer.Books.Count();
            }

            if (numberOfBooks > maxBooksPerPeriod)
            {
                return true;
            }

            return numberOfBooks + borrow.Books.Count > maxBooksPerPeriod;
        }

        /// <summary>
        /// Determines whether [has reached book limit per same domain] [the specified borrow].
        /// </summary>
        /// <param name="borrow">The borrow.</param>
        /// <param name="maxBooksInSameDomain">The maximum books in same domain.</param>
        /// <param name="periodInMonthsForSameDomain">The period in months for same domain.</param>
        /// <returns><c>true</c> if [has reached book limit per same domain] [the specified borrow]; otherwise, <c>false</c>.</returns>
        private bool HasReachedBookLimitPerSameDomain(BookBorrow borrow, int maxBooksInSameDomain, int periodInMonthsForSameDomain)
        {
            var limitDate = DateTime.Now.AddMonths(-periodInMonthsForSameDomain);
            var borrowsInPeriod = AppDbContext.BookBorrows.Where(b => b.Reader.Id == borrow.Reader.Id && b.BorrowDate >= limitDate);
            foreach (var book in borrow.Books)
            {
                foreach (var domain in book.Book.Domains)
                {
                    var booksInDomain = 0;
                    foreach (var borrowBook in borrowsInPeriod)
                    {
                        booksInDomain += borrowBook.BooksInDomain(domain);
                    }

                    if (booksInDomain > maxBooksInSameDomain)
                    {
                        return true;
                    }
                    else
                    {
                        var booksInBorrowInDomain = borrow.BooksInDomain(domain);
                        if (booksInDomain + booksInBorrowInDomain > maxBooksInSameDomain)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Determines whether [has reached book limit per employee] [the specified borrow].
        /// </summary>
        /// <param name="borrow">The borrow.</param>
        /// <param name="maxBooksPerEmployee">The maximum books per employee.</param>
        /// <returns><c>true</c> if [has reached book limit per employee] [the specified borrow]; otherwise, <c>false</c>.</returns>
        private bool HasReachedBookLimitPerEmployee(BookBorrow borrow, int maxBooksPerEmployee)
        {
            var borrowsInPeriod = AppDbContext.BookBorrows.Where(b => b.Employee.Id == borrow.Employee.Id
                                                    && b.BorrowDate.Year == borrow.BorrowDate.Year
                                                    && b.BorrowDate.Month == borrow.BorrowDate.Month
                                                    && b.BorrowDate.Day == borrow.BorrowDate.Day);
            var numberOfBooks = 0;
            foreach (var borrowInPer in borrowsInPeriod)
            {
                numberOfBooks += borrowInPer.Books.Count();
            }

            if (numberOfBooks >= maxBooksPerEmployee)
            {
                return true;
            }

            if (numberOfBooks + borrow.Books.Count > maxBooksPerEmployee)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether [is valid same book borrow] [the specified borrow].
        /// </summary>
        /// <param name="borrow">The borrow.</param>
        /// <param name="periodInDaysForSameBookBorrow">The period in days for same book borrow.</param>
        /// <returns><c>true</c> if [is valid same book borrow] [the specified borrow]; otherwise, <c>false</c>.</returns>
        private bool IsValidSameBookBorrow(BookBorrow borrow, int periodInDaysForSameBookBorrow)
        {
            var limitDate = DateTime.Now.AddDays(-periodInDaysForSameBookBorrow);
            var borrowsInPeriod = AppDbContext.BookBorrows.Where(b => b.Reader.Id == borrow.Reader.Id && b.BorrowDate >= limitDate);
            foreach (var book in borrow.Books)
            {
                var sameBooksNr = 0;
                foreach (var borrowInPer in borrowsInPeriod)
                {
                    if (borrowInPer.Books.Contains(book))
                    {
                        sameBooksNr++;
                    }
                }

                if (sameBooksNr > 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether [has reached maximum books per day] [the specified borrow].
        /// </summary>
        /// <param name="borrow">The borrow.</param>
        /// <param name="maxBooksPerDay">The maximum books per day.</param>
        /// <returns><c>true</c> if [has reached maximum books per day] [the specified borrow]; otherwise, <c>false</c>.</returns>
        private bool HasReachedMaxBooksPerDay(BookBorrow borrow, int maxBooksPerDay)
        {
            var borrowsOnDay = AppDbContext.BookBorrows.Where(b => b.Reader.Id == borrow.Reader.Id
                                                    && b.BorrowDate.Year == borrow.BorrowDate.Year
                                                    && b.BorrowDate.Month == borrow.BorrowDate.Month
                                                    && b.BorrowDate.Day == borrow.BorrowDate.Day);
            var booksNr = 0;
            foreach (var borrowDay in borrowsOnDay)
            {
                booksNr += borrowDay.Books.Count();
            }

            if (booksNr >= maxBooksPerDay)
            {
                return true;
            }

            if (booksNr + borrow.Books.Count > maxBooksPerDay)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Ares the books available.
        /// </summary>
        /// <param name="books">The books.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool AreBooksAvailable(List<BookEdition> books)
        {
            foreach (var book in books)
            {
                var borrows = AppDbContext.BookBorrows
                    .Where(b => b.IsReturned == false);
                var borrowedCopies = 0;
                foreach (var borrow in borrows)
                {
                    if (borrow.Books.Contains(book))
                    {
                        borrowedCopies++;
                    }
                }

                var availableBooks = book.Copies - book.CopiesLibrary - borrowedCopies;
                var tenPercentOfInitialBooks = book.Copies * 0.1;
                if (availableBooks < tenPercentOfInitialBooks)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
