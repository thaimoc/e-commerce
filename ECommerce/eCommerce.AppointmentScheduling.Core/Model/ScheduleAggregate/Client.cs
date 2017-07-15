using System;
using System.Collections.Generic;
using eCommerce.SharedKernel;
using eCommerce.SharedKernel.Interfaces;

namespace eCommerce.AppointmentScheduling.Core.Model.ScheduleAggregate
{
    
    public class Client : Entity<int>, IModificationHistory
    {
        private IList<Patient> _patients;
        public Contact Contact { get; private set; }
        public string PreferredName { get; private set; }
        public DateTime BirthDay { get; private set; }
        public string Salutation { get; private set; }
        public DateTime DateModified { get; protected set; }
        public DateTime DateCreated { get; protected set; }
        public bool IsDirty { get; protected set; }
        

        public IList<Patient> Patients
        {
            get { return _patients; }
            protected set { _patients = value; }
        }


        public Client(Contact contact, string preferredName, DateTime birthDay, string salutation) : this()
        {
            Contact = contact;
            PreferredName = preferredName;
            BirthDay = birthDay;
            Salutation = salutation;
        }

        private Client()
        {
            Patients = new List<Patient>();
        }
        
    }
}