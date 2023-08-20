using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Wardrobe.CrossCutting.Registrations;

public static class AzureRegistrations
{
    public static void UseAzure(this IServiceCollection services, IConfiguration configuration)
    {
        var azureConfiguration = configuration.GetSection("Azure");

        services.AddAzureClients(builder =>
        {
            builder.AddBlobServiceClient(azureConfiguration["Storage:ConnectionStrings"]);
        });
    }
}