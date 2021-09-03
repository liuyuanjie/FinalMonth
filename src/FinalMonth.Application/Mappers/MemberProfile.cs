using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FinalMonth.Application.Command;
using FinalMonth.Domain;

namespace FinalMonth.Application.Mappers
{
    public class MemberProfile: Profile
    {
        public MemberProfile()
        {
            CreateMap<AddMemberCommand, Member>().ReverseMap();
        }
    }
}
