using System.Linq;
using eCommece.Core.Interfaces;
using eCommerce.Model.ApplicationEvents;
using eCommerce.SharedKernel.Interfaces;

namespace eCommece.Core.Services
{
    public class EmailConfirmationHandler : IHandle<AppointmentConfirmedEvent>
    {
        private readonly IScheduleRepository _scheduleRepository;

        private readonly IApplicationSettings _settings;

        public EmailConfirmationHandler(IScheduleRepository scheduleRepository, IApplicationSettings settings)
        {
            _scheduleRepository = scheduleRepository;
            _settings = settings;
        }
        
        public void Handle(AppointmentConfirmedEvent appointmentConfirmedEvent)
        {
            // Note: In this demo this only works for appointments scheduled on TestDate
            var schedule = _scheduleRepository.GetScheduleForDate(_settings.ClinicId, _settings.TestDate);

            var appointmentToConfirm = schedule.Appointments.FirstOrDefault(a => a.Id == appointmentConfirmedEvent.Id);
            
            appointmentToConfirm?.Confirm(appointmentConfirmedEvent.DateTimeEventOccurred);

            _scheduleRepository.Update(schedule);
        }
    }
}