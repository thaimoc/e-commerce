using System;
using System.Data.Entity;
using eCommerce.AppointmentScheduling.Data;
using eCommerce.AppointmentScheduling.Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eCommerce.AppointmentScheduling.IntegrationTests.Data
{
    [TestClass]
    public class ScheduleRepositoryShould
    {
        private int testClinicId = 1;
        private readonly DateTime _testDateWithNoAppointments = new DateTime(2001, 1, 1);
        private readonly DateTime _testDateWithAppointments = new DateTime(2016, 6, 9);

        public ScheduleRepositoryShould()
        {
            Database.SetInitializer<SchedulingContext>(null);
        }

        [TestMethod]
        public void NotReturnNullAppointmentsCollectionInScheduleTypeIfNoAppointmentsFound()
        {
            var repo = new ScheduleRepository(new SchedulingContext());
            Assert.IsNotNull(repo.GetScheduleForDate(testClinicId, _testDateWithNoAppointments).Appointments);
        }

        [TestMethod]
        public void ReturnAppointmentsFromGetScheduledAppointmentsForDate()
        {
            var repo = new ScheduleRepository(new SchedulingContext());
            Assert.IsNotNull(repo.GetScheduleForDate(testClinicId, _testDateWithAppointments).Appointments);
        }
    }
}