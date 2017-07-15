using eCommerce.SharedKernel.Interfaces;

namespace eCommece.Core.Interfaces
{
    public interface IMessagePublisher
    {
        void Publish(IApplicationEvent applicationEvent);
    }
}