using System.Text;
using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Application.Interfaces.ServiceInterfaces.ManageServiceInterfaces;
using Auth.Application.Interfaces.ServiceInterfaces.ProcessingServices;
using Auth.Application.Interfaces.ServiceInterfaces.ProcessingServicesInterfaces;
using Auth.Application.Interfaces.TokenServiceInterfaces;
using Auth.Application.Services.CoreServices.Permissions;
using Auth.Application.Services.CoreServices.Products;
using Auth.Application.Services.CoreServices.Roles;
using Auth.Application.Services.CoreServices.Security;
using Auth.Application.Services.CoreServices.UserRefreshTokens;
using Auth.Application.Services.CoreServices.Users;
using Auth.Application.Services.ManageServices.Users;
using Auth.Application.Services.ProcessingServices.RefreshTokens;
using Auth.Application.Services.ProcessingServices.Users;
using Auth.Application.Services.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddTransient<IToken, Token>();
			services.AddTransient<IUserService, UserService>();
			services.AddTransient<IRoleService, RoleService>();
			services.AddTransient<IProductService, ProductService>();
			services.AddTransient<ISecurityService, SecurityService>();
			services.AddTransient<IUserManageService, UserManageService>();
			services.AddTransient<IPermissionService, PermissionService>();
			services.AddTransient<IUserProcessingService, UserProcessingService>();
			services.AddTransient<IUserRefreshTokenService, UserRefreshTokenService>();
			services.AddTransient<IRefreshTokenProcessingServiceInterface, RefreshTokenProcessingInterface>();

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					string key = configuration.GetSection("Jwt").GetValue<string>("Key");
					string audience = configuration.GetSection("Jwt").GetValue<string>("Audience");
					string issuer = configuration.GetSection("Jwt").GetValue<string>("Issuer");
					byte[] convertKeyToBytes = Encoding.UTF8.GetBytes(key);
					options.SaveToken = true;

					options.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(convertKeyToBytes),
						ValidateIssuer = true,
						ValidateAudience = true,
						RequireExpirationTime = true,
						ValidateLifetime = true,
						ValidAudience = audience,
						ValidIssuer = issuer,
					};
				});

			return services;
		}
	}
}
