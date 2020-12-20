﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using log4net;
using UnitBV_Biblioteq.Core.DomainModel;
using UnitBV_Biblioteq.Core.Repositories;

namespace UnitBV_Biblioteq.Persistence.Repositories
{
    public class BookBorrowRepository : Repository<BookBorrow>, IBookBorrowRepository
    {
        public BookBorrowRepository(AppDbContext context) : base(context)
        {
            
        }

        private static readonly ILog Logger = LogManager.GetLogger(typeof(BookBorrowRepository));
        public AppDbContext AppDbContext => Context as AppDbContext;

        public IEnumerable<BookBorrow> BookBorrows => Context.Set<BookBorrow>();

        public bool AddBookBorrow(BookBorrow borrow)
        {
            try
            {
                if (!this.IsValidObject(borrow))
                {
                    Logger.Info("Failed to add book borrow.");
                    return false;
                }

                borrow.LastReBorrowDate = borrow.BorrowDate;
                Context.Set<BookBorrow>().Add(borrow);
                Context.SaveChanges();
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

        public bool EditBookBorrow(BookBorrow borrow)
        {
            try
            {
                var existing = Context.Set<BookBorrow>().FirstOrDefault(a => a.Id == borrow.Id);
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

                    Context.SaveChanges();
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
                Logger.Info($"Failed to update book borrow with id={borrow.Id}.");
                Logger.Error(ex.Message, ex);
                return false;
            }

            return true;
        }

        public bool DeleteBookBorrow(BookBorrow borrow)
        {
            try
            {
                Context.Set<BookBorrow>().Remove(borrow);
                Context.SaveChanges();
                Logger.Info($"Book borrow was deleted (id={borrow.Id}).");
            }
            catch (Exception ex)
            {
                Logger.Info("Failed to delete book borrow.");
                Logger.Error(ex.Message, ex);
                return false;
            }

            return true;
        }

        public bool ReBorrowBook(BookBorrow borrow)
        {
            try
            {
                var reborrowLimit = int.Parse(ConfigurationManager.AppSettings["ReBorrowLimit"]);

                var existing = Context.Set<BookBorrow>().FirstOrDefault(a => a.Id == borrow.Id);
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

                    Context.SaveChanges();
                    Logger.Info($"Book borrow with id={borrow.Id} was updated.");
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Info($"Failed to update book borrow with id={borrow.Id}.");
                Logger.Error(ex.Message, ex);
                return false;
            }

            return true;
        }

        public bool ReturnBooks(BookBorrow borrow)
        {
            try
            {
                var existing = Context.Set<BookBorrow>().FirstOrDefault(a => a.Id == borrow.Id);
                if (existing != null)
                {
                    existing.IsReturned = true;
                    existing.ReturnDate = DateTime.Now;

                    Context.SaveChanges();
                    Logger.Info($"Book borrow with id={borrow.Id} was updated.");
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Info($"Failed to update book borrow with id={borrow.Id}.");
                Logger.Error(ex.Message, ex);
                return false;
            }

            return true;
        }

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

            if (!this.AreBooksAvailable(borrow.Books))
            {
                return false;
            }

            if (borrow.Books.Count >= 3 && !this.HasTwoDistinctDomains(borrow.Books))
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

        private bool IsValidReBorrow(BookBorrow borrow, int reborrowLimit)
        {
            var limitDate = DateTime.Now.AddMonths(-3);
            var last3MonthsBooks = Context.Set<BookBorrow>().Where(b => b.Reader.Id == borrow.Reader.Id && b.LastReBorrowDate >= limitDate);
            var nrreborrows = 0;
            foreach (var bookBorrow in last3MonthsBooks)
            {
                nrreborrows += bookBorrow.ReBorrows;
            }

            if (nrreborrows + borrow.ReBorrows > reborrowLimit)
            {
                return false;
            }

            if (nrreborrows < reborrowLimit)
            {
                return true;
            }

            return false;
        }

        private bool HasTwoDistinctDomains(List<BookEdition> books)
        {
            var domainnr = 0;
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
                    // check if list contains parent domain
                    if (domains.Contains(parent))
                    {
                        isDistinct = false;
                        break;
                    }

                    parent = parent.Parent;
                }

                if (isDistinct)
                {
                    domainnr++;
                }
            }

            return domainnr >= 2;
        }

        private bool HasReachedBookLimitPerPeriod(BookBorrow borrow, int maxBooksPerPeriod, int periodInDaysMaxBookPerPeriod)
        {
            var limitDate = DateTime.Now.AddDays(-periodInDaysMaxBookPerPeriod);
            var borrowsInPeriod = Context.Set<BookBorrow>().Where(b => b.Reader.Id == borrow.Reader.Id && b.BorrowDate >= limitDate);
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

        private bool HasReachedBookLimitPerSameDomain(BookBorrow borrow, int maxBooksInSameDomain, int periodInMonthsForSameDomain)
        {
            var limitDate = DateTime.Now.AddMonths(-periodInMonthsForSameDomain);
            var borrowsInPeriod = Context.Set<BookBorrow>().Where(b => b.Reader.Id == borrow.Reader.Id && b.BorrowDate >= limitDate);
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

        private bool HasReachedBookLimitPerEmployee(BookBorrow borrow, int maxBooksPerEmployee)
        {
            var borrowsInPeriod = Context.Set<BookBorrow>().Where(b => b.Employee.Id == borrow.Employee.Id
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

        private bool IsValidSameBookBorrow(BookBorrow borrow, int periodInDaysForSameBookBorrow)
        {
            var limitDate = DateTime.Now.AddDays(-periodInDaysForSameBookBorrow);
            var borrowsInPeriod = Context.Set<BookBorrow>().Where(b => b.Reader.Id == borrow.Reader.Id && b.BorrowDate >= limitDate);
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

        private bool HasReachedMaxBooksPerDay(BookBorrow borrow, int maxBooksPerDay)
        {
            var borrowsOnDay = Context.Set<BookBorrow>().Where(b => b.Reader.Id == borrow.Reader.Id
                                                    && b.BorrowDate.Year == borrow.BorrowDate.Year
                                                    && b.BorrowDate.Month == borrow.BorrowDate.Month
                                                    && b.BorrowDate.Day == borrow.BorrowDate.Day);
            var nrbooks = 0;
            foreach (var borrowDay in borrowsOnDay)
            {
                nrbooks += borrowDay.Books.Count();
            }

            if (nrbooks >= maxBooksPerDay)
            {
                return true;
            }

            if (nrbooks + borrow.Books.Count > maxBooksPerDay)
            {
                return true;
            }

            return false;
        }

        private bool AreBooksAvailable(IEnumerable<BookEdition> books)
        {
            foreach (var book in books)
            {
                var borrows = Context.Set<BookBorrow>()
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
