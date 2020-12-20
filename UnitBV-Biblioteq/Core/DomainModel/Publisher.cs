using System.ComponentModel.DataAnnotations;

namespace UnitBV_Biblioteq.Core.DomainModel
{
    public class Publisher
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

    }
}
