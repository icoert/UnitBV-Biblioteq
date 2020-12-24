﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UnitBV_Biblioteq.Core.DomainModel
{
    public class Domain
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public Domain Parent { get; set; }
        public List<Book> Books { get; set; }
    }
}