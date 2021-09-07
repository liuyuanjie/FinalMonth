using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FinalMonth.Application.Interfaces;
using FinalMonth.Application.Repository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FinalMonth.Application.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ITestingService, TestingOneService>();
            services.AddScoped<ITestingService, TestingTwoService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

        }
    }
}
