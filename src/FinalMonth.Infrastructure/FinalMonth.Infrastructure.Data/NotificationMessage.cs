using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FinalMonth.Infrastructure.Data
{
    public class Member
    {
        [Key]
        public string MemberId { get; set; }
        public string UserId { get; set; }
        public int Age { get; set; }
        public string JobTitle { get; set; }
        public DateTime JoinDate { get; set; }

        public ShinetechUser User { get; set; }
    }
}
