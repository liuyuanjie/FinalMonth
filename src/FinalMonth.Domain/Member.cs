using System;

namespace FinalMonth.Domain
{
    public class Member
    {
        public string MemberId { get; set; }
        public string UserId { get; set; }
        public int Age { get; set; }
        public string JobTitle { get; set; }
        public DateTime JoinDate { get; set; }

        public ShinetechUser User { get; set; }

        public static Member Create(string userId, int age, string jobTitle)
        {
            return new Member
            {
                UserId = userId,
                Age = age,
                JobTitle = jobTitle
            };
        }
    }
}
