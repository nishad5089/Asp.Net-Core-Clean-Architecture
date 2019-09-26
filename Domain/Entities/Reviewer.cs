using System.Collections.Generic;

namespace Domain.Entities
{
    public class Reviewer
    {
        public int ReviewerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}