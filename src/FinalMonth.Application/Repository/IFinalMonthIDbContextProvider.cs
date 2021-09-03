using System.Data;

namespace FinalMonth.Application.Repository
{
    public interface IFinalMonthIDbContextProvider
    {
        IDbConnection DbConnection { get;}
    }
}
