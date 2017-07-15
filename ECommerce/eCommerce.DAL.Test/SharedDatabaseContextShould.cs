using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eCommerce.DAL.Test
{
    [TestClass]
    public class SharedDatabaseContextShould
    {
        public SharedDatabaseContextShould()
        {
            Database.SetInitializer(new VetOfficeInitializer());
            //     AppDomain.CurrentDomain.SetData("DataDirectory", "");
        }

        [TestMethod]
        public void BuildModel()
        {
            var db = new VetOfficeContext();
            db.Database.Initialize(true);
        }
    }
}
