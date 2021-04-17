using Cvl.DynamicForms.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Setup
{
    public static class DyncamicFormsExtensions
    {
        public static IServiceCollection UseDynamicForms(this IServiceCollection services)
        {
            services.AddScoped<GridService, GridService>();
            services.AddScoped<PropretyGridService, PropretyGridService>();
            services.AddScoped<TreeListService, TreeListService>();
            services.AddScoped<ViewService, ViewService>();

            return services;
        }
    }
}
