using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace UnitBV_Biblioteq.Core.DomainModel
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public List<DomainModel.Domain> Domains { get; set; }
        public List<Author> Authors { get; set; }

        public bool DomainStructure()
        {
            var maxDomains = int.Parse(ConfigurationManager.AppSettings["MaxDomainsForBook"]);
            if (this.Domains.Count > maxDomains)
            {
                return false;
            }

            foreach (var domain in this.Domains)
            {
                var parent = domain.Parent;
                while (parent != null)
                {
                    // check parent domain in the list
                    if (this.Domains.Contains(parent))
                    {
                        return false;
                    }

                    parent = parent.Parent;
                }
            }

            return true;
        }

        public bool IsInDomain(DomainModel.Domain domain)
        {
            foreach (var dom in this.Domains)
            {
                var parent = dom;
                while (parent != null)
                {
                    if (parent == domain)
                    {
                        return true;
                    }

                    parent = parent.Parent;
                }
            }

            return false;
        }
    }
}
