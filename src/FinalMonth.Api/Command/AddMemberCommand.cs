using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FinalMonth.Infrastructure.Data;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.SwaggerGen;

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
        private readonly IFinalMonthDataContext _dataContext;

        public AddMemberCommandHandler(IFinalMonthDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> Handle(AddMemberCommand request, CancellationToken cancellationToken)
        {
            _dataContext.Members.Add(new Member
            {
                UserId = request.UserId,
                Age = request.Age,
                JobTitle = request.JobTitle
            });

            var result = await _dataContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
