using Cvl.DynamicForms;
using Cvl.DynamicForms.Core.Importer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting; // Requires NuGet package

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices(services =>
{
    services.AddDyncmicFormsCore();
});

var app = builder.Build();

var parser = app.Services.GetRequiredService<IFormDefinitionImporter>();

var form = parser.LoadFromFile(@"C:\nbac\projects\dynamic-forms\dynamic-form-szablon.xlsx", 0);

Console.WriteLine(form.ToString());

