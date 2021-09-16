using System;

namespace Data.Models.Interfaces
{
    public interface IBaseEntity : IBaseEntity<string>
    {
    }

    public interface IBaseEntity<TKey>
    {
        TKey Id { get; set; }
        bool IsDeleted { get; set; }
        DateTimeOffset CreateDate { get; set; }
        DateTimeOffset? DeleteDate { get; set; }
        DateTimeOffset? LastModifiedDate { get; set; }
    }
}
