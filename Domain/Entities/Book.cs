using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public string Isbn { get; set; }
        public string Title { get; set; }

        public DateTime? DatePublished { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
        public virtual ICollection<BookCategory> BookCategories { get; set; }
    }
}