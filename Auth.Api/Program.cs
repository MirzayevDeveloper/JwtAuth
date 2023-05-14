using System.Text;
using Auth.Application;
using Auth.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			IConfiguration configuration = builder.Configuration;

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddApplication();
			builder.Services.AddInfrastructure(builder.Configuration);



			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					string key = configuration.GetSection("Jwt").GetValue<string>("Key");
					byte[] convertKeyToBytes = Encoding.UTF8.GetBytes(key);

					options.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(convertKeyToBytes),
						ValidateIssuer = false,
						ValidateAudience = false,
						RequireExpirationTime = true,
						ValidateLifetime = true
					};
				});

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();
			app.UseAuthentication();

			app.MapControllers();

			app.Run();
		}
	}
}