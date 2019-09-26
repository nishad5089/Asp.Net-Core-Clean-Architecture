

namespace Domain.Entities
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string Headline { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; }
        public int ReviewerId { get; set; }
        public virtual Reviewer Reviewer { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}