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
        public static IServiceCollection UseDynamicForms<TDataService,TViewConfiguration>(this IServiceCollection services)
            where TDataService : DataServiceBase
            where TViewConfiguration : ViewConfigurationService
        {
            services.AddScoped<GridService, GridService>();
            services.AddScoped<PropretyGridService, PropretyGridService>();
            services.AddScoped<TreeListService, TreeListService>();
            services.AddScoped<ViewService, ViewService>();
            services.AddScoped<DataServiceBase, TDataService>();
            services.AddScoped<ViewConfigurationService, TViewConfiguration>();

            return services;
        }
    }
}
