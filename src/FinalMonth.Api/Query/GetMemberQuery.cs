using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FinalMonth.Infrastructure.Data;
using FinalMonth.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinalMonth.Api.Query
{
    public class GetMemberQuery : IRequest<IList<Member>>
    {

    }

    public class GetMemberQueryHandler : IRequestHandler<GetMemberQuery, IList<Member>>
    {
        private readonly IGenericRepository<Member> _repository;

        public GetMemberQueryHandler(IGenericRepository<Member> repository)
        {
            _repository = repository;
        }
        public async Task<IList<Member>> Handle(GetMemberQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
