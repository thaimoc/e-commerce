using System;

namespace eCommerce.AppointmentScheduling.Core.Interfaces
{
    public interface IApplicationSettings
    {
        int ClinicId { get; }
        DateTime TestDate { get; }
    }
}