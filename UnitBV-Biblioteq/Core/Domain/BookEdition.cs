using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitBV_Biblioteq.Core.Domain
{
    public class BookEdition
        {
            public int Id { get; set; }
            [Required] 
            public virtual Publisher Publisher { get; set; }

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



        }
}
