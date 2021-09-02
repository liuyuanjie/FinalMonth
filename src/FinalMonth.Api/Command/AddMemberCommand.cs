using System.Threading;
using System.Threading.Tasks;
using FinalMonth.Infrastructure.Data;
using FinalMonth.Infrastructure.Repository;
using FluentValidation;
using MediatR;

namespace FinalMonth.Api.Command
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

        public AddMemberCommandHandler(IGenericRepository<Member> rep)
        {
            _rep = rep;
        }

        public async Task<bool> Handle(AddMemberCommand request, CancellationToken cancellationToken)
        {
            _rep.Create(new Member
            {
                UserId = request.UserId,
                Age = request.Age,
                JobTitle = request.JobTitle
            });

            var result = await _rep.UnitOfWOrk.CommitAsync(cancellationToken);

            return result > 0;
        }
    }
}
