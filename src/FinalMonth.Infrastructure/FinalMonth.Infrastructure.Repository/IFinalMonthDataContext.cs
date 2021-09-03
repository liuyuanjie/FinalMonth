using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace FinalMonth.Infrastructure.Data
{
    public interface IFinalMonthDBContext
    {
        IDbConnection DbConnection { get;}
    }
}
