using System;

namespace eCommerce.SharedKernel.Interfaces
{
    public interface IModificationHistory
    {
        DateTime DateModified { get; }
        DateTime DateCreated { get; }
        bool IsDirty { get; }
    }
}