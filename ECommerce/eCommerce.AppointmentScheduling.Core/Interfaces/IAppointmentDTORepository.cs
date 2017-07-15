using eCommerce.AppointmentScheduling.Core.Model;
using eCommerce.AppointmentScheduling.Core.Model.ScheduleAggregate;

namespace eCommerce.AppointmentScheduling.Core.Interfaces
{
    public interface IAppointmentDTORepository
    {
        AppointmentDTO GetFromAppointment(Appointment appointment);
    }
}