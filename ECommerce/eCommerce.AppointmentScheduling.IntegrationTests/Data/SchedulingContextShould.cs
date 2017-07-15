using System.ComponentModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using eCommerce.AppointmentScheduling.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eCommerce.AppointmentScheduling.IntegrationTests.Data
{
    [TestClass]
    public class SchedulingContextShould
    {
        public SchedulingContextShould()
        {
            Database.SetInitializer<SchedulingContext>(null);
        }

        [TestMethod]
        public void GetClientReferenceList()
        {
            var db = new SchedulingContext();
            Assert.IsNotNull(db.Clients.ToList());
        }

        [TestMethod]
        public void ReturnClientsWithPatients()
        {
            using (var db = new SchedulingContext())
            {
                Assert.IsNotNull(db.Clients.Include(c => c.Patients).ToList());
            }
        }

        [TestMethod]
        public void CanReturnMaterializedAppointments()
        {
            using (var db = new SchedulingContext())
            {
                var appointment = db.Appointments.FirstOrDefault();
                Assert.IsNotNull(appointment?.Title);
                var apptProps = TypeDescriptor.GetProperties(appointment);
                foreach (PropertyDescriptor pd in apptProps)
                {
                    Debug.WriteLine("{0}:{1}", pd.DisplayName, pd.GetValue(appointment));
                }
            }
        }

        [TestMethod]
        public void CanReturnScheduleWithAppointments()
        {
            using (var db = new SchedulingContext())
            {
                var schedule = db.Schedules.Include("_appointments").FirstOrDefault();
                Assert.IsNotNull(schedule?.Appointments);
                Assert.IsTrue(schedule.Appointments.ToList().Count > 0);
            }
        }
    }
}