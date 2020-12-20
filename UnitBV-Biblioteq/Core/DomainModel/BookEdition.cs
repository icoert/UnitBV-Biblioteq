using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace UnitBV_Biblioteq.Core.DomainModel
{
    public class BookEdition
        {
            public int Id { get; set; }
            [Required] 
            public virtual Publisher Publisher { get; set; }

            [Required]
            public virtual Book Book { get; set; }

            [MaxLength(4)]
            [MinLength(4)]
            public string Year { get; set; }

            [IntegerValidator(MinValue = 1)]
            public int Pages { get; set; }

            public BookType Type { get; set; }

            [IntegerValidator(MinValue =  0)]
            public int Copies { get; set; }

            [IntegerValidator(MinValue = 0)]
            public int CopiesLibrary { get; set; }

            public virtual List<BookBorrow> BookBorrows { get; set; }

            public bool IsValid()
            {
            if (!int.TryParse(this.Year, out _))
            {
                return false;
            }

            if (this.Copies < 0 || this.CopiesLibrary< 0 || this.Pages < 1)
                return false;

            return this.CopiesLibrary <= this.Copies;
            }

    }
}
