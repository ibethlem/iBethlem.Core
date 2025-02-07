namespace iBethlem.Core;

public static class SetupExtensions
{
    public static void AddCoreServices(this IServiceCollection services)
    {
        services.AddControllers();
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();

        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseQueryStrings = true;
            options.LowercaseUrls = true;
        });

    }

    public static void UseCoreServices(this WebApplication app)
    {
        app.MapControllers();
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}
