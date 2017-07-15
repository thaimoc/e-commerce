using System;

namespace eCommece.Core.Interfaces
{
    public interface IApplicationSettings
    {
        int ClinicId { get; }
        DateTime TestDate { get; }
    }
}