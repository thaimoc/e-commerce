using System;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace eCommerce.DAL.ModelConventions
{
    public class NamePropertiesDefaultLength : Convention
    {
        public NamePropertiesDefaultLength()
        {
            Properties<String>().Where(n=>n.Name == "Name").Configure(n=> 
            {
                n.HasMaxLength(128);
                n.IsRequired();
            });
        }
    }
}