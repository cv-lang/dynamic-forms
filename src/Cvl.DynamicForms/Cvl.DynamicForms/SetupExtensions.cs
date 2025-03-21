﻿using Cvl.DynamicForms.Core.Importer;
using Cvl.DynamicForms.Infrastructure.Importers.Excel;
using Cvl.DynamicForms.Infrastructure.Importers.Excel.Tools;
using Microsoft.Extensions.DependencyInjection;

namespace Cvl.DynamicForms
{
    public static class SetupExtensions
    {
        public static IServiceCollection AddDyncmicFormsCore(this IServiceCollection services)
        {
            services.AddTransient<IWorksheetValidator, WorksheetValidator>();
            services.AddTransient<IExcelReader, ExcelReader>();
            services.AddTransient<IFormDefinitionImporter, ExcelFormDefinitionImporter>();
            services.AddTransient<IFormSectionTypeParser, FormSectionTypeParser>();

            return services;
        }
    }
}
