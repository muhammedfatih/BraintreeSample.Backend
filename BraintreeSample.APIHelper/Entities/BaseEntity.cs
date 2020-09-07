using System;

namespace BraintreeSample.APIHelper.Entities
{
    public abstract class BaseEntity
    {
        public int ID { get; set; }
        public string Guid { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
