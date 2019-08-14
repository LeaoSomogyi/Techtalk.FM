using System;

namespace Techtalk.FM.Domain.Contracts
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
