using CleanAuthSystem.Presention.Routes;

namespace CleanAuthSystem.Presentation.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static WebApplication ConfigureApplication(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();
            app.MapAuthRoutes();

            return app;
        }
    }
}
