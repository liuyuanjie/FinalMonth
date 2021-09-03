using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace FinalMonth.Application.Repository
{
    public interface IMSSqlConnection
    {
        IDbConnection OpenConnectionAsync();
    }
}
