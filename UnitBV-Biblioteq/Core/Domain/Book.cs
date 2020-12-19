using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitBV_Biblioteq.Core.Domain
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public List<Domain> Domains { get; set; }
        public List<Author> Authors { get; set; }
    }
}
