using System;
using eCommerce.AppointmentScheduling.Core.Model.ScheduleAggregate;

namespace eCommerce.AppointmentScheduling.Core.Interfaces
{
    public interface IScheduleRepository
    {
        Schedule GetScheduleForDate(int clinicId, DateTime date);
        void Update(Schedule schedule);
    }
}