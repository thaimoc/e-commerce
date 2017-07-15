using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace eCommerce.DAL.ModelConventions
{
    public class ForgenKeyNameWithKfConvention : IStoreModelConvention<AssociationType>
    {
        public void Apply(AssociationType association, DbModel model)
        {
            //if (association.IsForeignKey && association.Constraint.FromRole.RelationshipMultiplicity != RelationshipMultiplicity.One)
            if (association.IsForeignKey)
            {
                var assnProperty = association.Constraint.ToProperties;
                foreach (var edmProperty in assnProperty)
                {
                    if (edmProperty.Name.EndsWith("Id"))
                    {
                        edmProperty.Name = edmProperty.Name.Substring(0, edmProperty.Name.Length - 3) + "FKId";
                    }
                }
            }
        }
    }
}