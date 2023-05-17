using Auth.Application;
using Auth.Infrastructure;
using Microsoft.OpenApi.Models;

namespace Auth.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddApplication(builder.Configuration);
			builder.Services.AddInfrastructure(builder.Configuration);
			builder.Services.AddAutoMapper(typeof(Program));

			builder.Services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth", Version = "v1" });

				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "Please enter token",
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					Scheme = "Bearer",
					BearerFormat = "JWT",
				});

				options.AddSecurityRequirement(new OpenApiSecurityRequirement()
				{{
					new OpenApiSecurityScheme()
					{
					   Reference=new OpenApiReference()
					   {
						   Id="Bearer",
						   Type=ReferenceType.SecurityScheme
					   }
					},
					new string[]{}
				} });
			});

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseAuthorization();
			app.MapControllers();
			app.Run();
		}
	}
}