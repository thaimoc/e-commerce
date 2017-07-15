using eCommerce.Model;
using eCommerce.Model.ScheduleAggregate;

namespace eCommece.Core.Interfaces
{
    public interface IAppointmentDTORepository
    {
        AppointmentDTO GetFromAppointment(Appointment appointment);
    }
}