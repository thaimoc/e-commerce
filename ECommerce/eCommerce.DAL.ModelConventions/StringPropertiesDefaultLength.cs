using System;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace eCommerce.DAL.ModelConventions
{
    public class StringPropertiesDefaultLength : Convention
    {
        public StringPropertiesDefaultLength()
        {
            Properties<String>().Configure(s=>s.HasMaxLength(255));
        }
        
    }
}