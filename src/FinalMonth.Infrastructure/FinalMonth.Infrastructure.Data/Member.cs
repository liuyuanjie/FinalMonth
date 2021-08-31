using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FinalMonth.Infrastructure.Data
{
    public class NotificationMessage
    {
        [Key]
        public string NotificationId { get; set; }
        public string Message { get; set; }
        public string From { get; set; }
    }
}
