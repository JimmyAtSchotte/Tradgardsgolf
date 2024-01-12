using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace Tradgardsgolf.Api.Startup;

public static class ApplicationPipline
{
    public static void ConfigureApplicationPipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
            app.UseDeveloperExceptionPage();
            
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tradgardsgolf.Api v1");
            c.RoutePrefix = string.Empty;
        });

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}