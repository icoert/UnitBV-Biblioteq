using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UnitBV_Biblioteq.Core.DomainModel
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        public List<Book> Books { get; set; }

    }
}
