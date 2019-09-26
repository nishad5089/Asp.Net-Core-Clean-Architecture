using System;

namespace Domain.Infrastructure
{
    public class BaseEntity
    {
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}