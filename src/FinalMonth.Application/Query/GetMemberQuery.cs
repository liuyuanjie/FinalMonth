using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FinalMonth.Domain;
using FinalMonth.Infrastructure.Repository;
using MediatR;

namespace FinalMonth.Application.Query
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
