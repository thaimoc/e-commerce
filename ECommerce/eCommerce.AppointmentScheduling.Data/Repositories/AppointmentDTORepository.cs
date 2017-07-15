using System.Linq;
using eCommerce.AppointmentScheduling.Core.Interfaces;
using eCommerce.AppointmentScheduling.Core.Model;
using eCommerce.AppointmentScheduling.Core.Model.ScheduleAggregate;

namespace eCommerce.AppointmentScheduling.Data.Repositories
{
	public class AppointmentDTORepository : IAppointmentDTORepository
	{
		private readonly SchedulingContext _context;

		public AppointmentDTORepository(SchedulingContext context)
		{
			_context = context;
		}

		public AppointmentDTO GetFromAppointment(Appointment appointment)
		{
			string query = @"select @p0 as AppointmentId, 
									(c.FirstName  + ' ' + c.LastName) as 'ClientName',
									c.Email as 'ClientEmailAddress',
									p.Name as 'PatientName',
									at.Name as 'AppointmentType',
									@p1 as 'Start',
									@p2 as 'End'
							from VetOffice_dbo.Clients c
								inner join VetOffice_dbo.Patients p on p.Id = @p3
								inner join VetOffice_dbo.Doctors d on d.Id = @p4
								inner join VetOffice_dbo.AppointmentTypes at on at.Id = @p5
							where c.Id = @p6 ";

			return _context.Database.SqlQuery<AppointmentDTO>(query, appointment.Id, appointment.TimeRange.Start, appointment.TimeRange.End, appointment.PatientId, appointment.DoctorId, appointment.AppointmentTypeId, appointment.ClientId)
				.FirstOrDefault();
		}
	}
}