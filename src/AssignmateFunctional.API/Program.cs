using AssignmateFunctional.API.Auth.Jwt;
using AssignmateFunctional.API.Business;
using AssignmateFunctional.API.DAL.Data.EfCore;
using AssignmateFunctional.API.DAL.Dependency;
using AssignmateFunctional.Common.Dependency;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();
builder.Services.AddOpenApi(); // keeps OpenAPI document generation
builder.Services.AddEndpointsApiExplorer(); // REQUIRED for Swagger
builder.Services.AddSwaggerGen(); // Swagger generator

builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowFrontend", policy =>
	{
		_ = policy.WithOrigins("http://localhost:3000")
		.AllowAnyHeader()
		.AllowAnyMethod()
		.AllowCredentials();
	});
});


builder.Services.AddJwtAuth(builder.Configuration);

builder.Services.RegisterDbContext(builder.Configuration);

builder.Services.AddScoped<IUserManagementService, UserManagementService>();
builder.Services.RegisterCommonPackage();

WebApplication app = builder.Build();
app.UsePathBase("/api");
// Pipeline
if (app.Environment.IsDevelopment())
{
	_ = app.MapOpenApi(); // your existing endpoint

	_ = app.UseSwagger(); // generates /swagger/v1/swagger.json
	_ = app.UseSwaggerUI(options =>
	{
		options.SwaggerEndpoint("/swagger/v1/swagger.json", "Assignmate API v1");
		options.RoutePrefix = string.Empty; // opens Swagger at root (http://localhost:xxxx/)
	});
}

using (IServiceScope scope = app.Services.CreateScope())
{
	ServiceDbContext serviceDbContext = scope.ServiceProvider.GetRequiredService<ServiceDbContext>();
	await serviceDbContext.Database.MigrateAsync();
}

app.UseCors("AllowFrontend");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();