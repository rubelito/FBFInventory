using System.Data.Common;

namespace FBFInventory.Infrastructure.EntityFramework
{
    public interface IDatabaseType
    {
        DbConnection Connectionstring();
    }
}