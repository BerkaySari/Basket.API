using Data.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Models.Models
{
    public abstract class BaseEntity<TKey> : IBaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        [Key]
        public TKey Id { get; set; }

        public bool IsDeleted { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset? DeleteDate { get; set; }
        public DateTimeOffset? LastModifiedDate { get; set; }
    }

    public class BaseEntity : BaseEntity<string>, IBaseEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString();
            CreateDate = DateTimeOffset.UtcNow;
        }
    }
}
