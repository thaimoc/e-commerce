using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Threading.Tasks;
using eCommerce.DAL.CustomMigrations.ComplexTypes;
using eCommerce.DAL.Model;

namespace eCommerce.DAL.CustomMigrations
{
    public class ClientConfigurations : EntityConfiguration<Client, int>
    {
        public ClientConfigurations()
        {
            HasOptional(c => c.PreferedDoctor);
            HasMany(c => c.Pattents);
            Property(c => c.BirthDay).IsOptional();

            /*              Start to declare for complextype                   */
            Property(c => c.Contact.FullName.FirstName).IsRequired().HasColumnName("FirstName")
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("ClientFirstName")
                {
                    IsClustered = false,
                    IsUnique = false,
                    Order = 3
                }));
            Property(c => c.Contact.FullName.LastName).IsRequired().HasColumnName("LastName")
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("ClientLastName")
                {
                    IsClustered = false,
                    IsUnique = false,
                    Order = 4
                }));
            Property(c => c.Contact.CompanyName).IsRequired().HasColumnName("CompanyName")
            .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("ClientCompanyName")
            {
                IsClustered = false,
                IsUnique = false,
                Order = 5
            }));
            Property(c => c.Contact.EmailAddress).IsRequired().HasColumnName("Email")
            .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("ClientEmail")
            {
                IsClustered = false,
                IsUnique = false,
                Order = 6
            }));
            Property(c => c.Contact.Phone).IsRequired().HasColumnName("PhoneNumbers");
            /*      Can doing this while Contact is complextype and FullName is also        */
            /*              End to declare for complextype                   */



            Task.Run(() => CreateDefaultInsertUpdateDeleteStoreProcedures(nameof(Client)));
        }
    }
}