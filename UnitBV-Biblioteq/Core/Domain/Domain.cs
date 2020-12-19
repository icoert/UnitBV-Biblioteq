using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitBV_Biblioteq.Core.Domain
{
    public class Domain
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public Domain ParentDomain { get; set; }
        public List<Book> Books { get; set; }
    }
}
