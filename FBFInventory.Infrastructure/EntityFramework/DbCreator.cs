namespace FBFInventory.Infrastructure.EntityFramework
{
    public class DbCreator
    {
        public void Create()
        {
            var connection = new EfSqlServer("SQLServerDb");
            using (var ams = new FBFDBContext(connection))
            {
                if (ams.Database.Exists())
                {
                    ams.Database.Delete();
                }
                ams.Database.Create();
            }
        } 
    }
}