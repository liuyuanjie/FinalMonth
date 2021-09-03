using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinalMonth.Application.Repository;
using FinalMonth.Domain;
using FluentValidation;
using MediatR;

namespace FinalMonth.Application.Command
{
    public class AddMemberCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public int Age { get; set; }
        public string JobTitle { get; set; }
    }

    public class AddMemberCommandValidator : AbstractValidator<AddMemberCommand>
    {
        public AddMemberCommandValidator()
        {
            RuleFor(c => c.Age).NotEmpty().GreaterThan(18);
            RuleFor(c => c.JobTitle).NotEmpty().MaximumLength(10);
        }
    }

    public class AddMemberCommandHandler : IRequestHandler<AddMemberCommand, bool>
    {
        private readonly IGenericRepository<Member> _rep;
        private readonly IMapper _mapper;

        public AddMemberCommandHandler(IGenericRepository<Member> rep, IMapper mapper)
        {
            _rep = rep;
            _mapper = mapper;
        }

        public async Task<bool> Handle(AddMemberCommand request, CancellationToken cancellationToken)
        {
            var member = _mapper.Map<Member>(request);
            _rep.Create(member);

            var result = await _rep.UnitOfWOrk.CommitAsync(cancellationToken);

            return result > 0;
        }
    }
}
