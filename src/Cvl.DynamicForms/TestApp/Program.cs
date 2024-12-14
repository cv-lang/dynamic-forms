using Cvl.DynamicForms;
using Cvl.DynamicForms.Core.Importer;
using Cvl.DynamicForms.Core.Models.Base;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata; // Requires NuGet package

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices(services =>
{
    services.AddDyncmicFormsCore();
});

var app = builder.Build();

var parser = app.Services.GetRequiredService<IFormDefinitionImporter>();

var form = parser.LoadFromFile(@"C:\nbac\projects\dynamic-forms\dynamic-form-szablon.xlsx", 0);

Console.WriteLine(form.ToString());


// Opcje serializacji
var options = new JsonSerializerOptions
{
    TypeInfoResolver = new DefaultJsonTypeInfoResolver
    {
        Modifiers =
        {
            typeInfo =>
            {
                if (typeInfo.Type == typeof(Control))
                {
                    typeInfo.PolymorphismOptions.UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor;
                }
            }
        }
    },
    WriteIndented = true
};

// Serializacja
string json = JsonSerializer.Serialize(form, options);

// Zapis do pliku
File.WriteAllText("formatka.json", json);