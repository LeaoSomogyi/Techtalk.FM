using System;
using Techtalk.FM.Domain.Contracts;

namespace Techtalk.FM.Domain.Entities
{
    public class BaseModel : IEntity
    {
        public virtual Guid Id { get; set; }

        public virtual DateTime CreatedAt { get; set; }

        public virtual DateTime UpdatedAt { get; set; }

        public BaseModel()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}
