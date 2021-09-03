using System.Data;

namespace FinalMonth.Application.Repository
{
    public interface IFinalMonthDBContext
    {
        IDbConnection DbConnection { get;}
    }
}
