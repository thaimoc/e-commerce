using eCommerce.AppointmentScheduling.Core.Interfaces;
using eCommerce.AppointmentScheduling.Core.Model;
using eCommerce.AppointmentScheduling.Core.Model.Events;
using eCommerce.SharedKernel.Interfaces;

namespace eCommerce.AppointmentScheduling.Core.Services
{
    /// <summary>
    /// Post appointmentscheduledevent to message bus to allow confirmation emails to be sent
    /// </summary>
    public class RelayAppointmentScheduledService : IHandle<AppointmentScheduledEvent>
    {
        private readonly IAppointmentDTORepository _apptRepository;

        private readonly IMessagePublisher _messagePublisher;

        public RelayAppointmentScheduledService(IAppointmentDTORepository apptRepository, IMessagePublisher messagePublisher)
        {
            _apptRepository = apptRepository;
            _messagePublisher = messagePublisher;
        }

        public void Handle(AppointmentScheduledEvent appointmentScheduledEvent)
        {
            AppointmentDTO appointment = _apptRepository.GetFromAppointment(appointmentScheduledEvent.AppointmentScheduled);

            // we are translating from a domain event to an application event here
            var newEvent = new Model.ApplicationEvents.AppointmentScheduledEvent(appointment);

            _messagePublisher.Publish(newEvent);
        }
    }
}