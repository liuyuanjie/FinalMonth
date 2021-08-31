using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace FinalMonth.Infrastructure.Data
{
    public interface IFinalMonthDataContext
    {
        DbSet<Member> Members { get; set; }
        DbSet<NotificationMessage> NotificationMessages { get; set; }
        Task<int> SaveChangesAsync();

        DatabaseFacade Database { get;}
    }
}
