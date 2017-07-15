using System;
using eCommerce.Model.ScheduleAggregate;

namespace eCommece.Core.Interfaces
{
    public interface IScheduleRepository
    {
        Schedule GetScheduleForDate(int clinicId, DateTime date);
        void Update(Schedule schedule);
    }
}