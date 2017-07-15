using System;
using System.Collections.Generic;
using eCommerce.DAL.Model.ComplexTypes;
using eCommerce.DAL.Model.Interfaces;

namespace eCommerce.DAL.Model
{
    public class Client : IEntity<int>, IModificationHistory
    {
        public int Id { get; set; }
        public Contact Contact { get; set; }
        public string PreferredName { get; set; }
        public DateTime BirthDay { get; set; }
        public string Salutation { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDirty { get; set; }

        public Doctor PreferedDoctor { get; set; }

        public IList<Patient> Pattents { get; set; }

        public Client()
        {
            Pattents = new List<Patient>(); // EF required
        }
    }
}