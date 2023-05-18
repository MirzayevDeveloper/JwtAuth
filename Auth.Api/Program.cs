using Auth.Application;
using Auth.Infrastructure;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Auth.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

			var builder = WebApplication.CreateBuilder(args);

			Log.Logger = new LoggerConfiguration()
				.Enrich.FromLogContext()
				.ReadFrom.Configuration(builder.Configuration)
				.CreateLogger();
			try
			{
				Log.Information("Start web host");
				Builder(builder);
			}
			catch (Exception ex)
			{
				string message = "Xatolik ruy berdi";
				Log.Fatal(ex, message);
			}
			finally
			{
				Log.CloseAndFlush();
			}
		}

		private static void Builder(WebApplicationBuilder builder)
		{
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
				}});
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