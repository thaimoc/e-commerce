using eCommerce.SharedKernel.Interfaces;

namespace eCommerce.AppointmentScheduling.Core.Interfaces
{
    public interface IMessagePublisher
    {
        void Publish(IApplicationEvent applicationEvent);
    }
}