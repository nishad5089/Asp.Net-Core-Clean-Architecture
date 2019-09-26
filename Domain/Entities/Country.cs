using System.Collections.Generic;

namespace Domain.Entities
{
    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
    }
}