﻿using System.Text;
using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Application.Interfaces.ServiceInterfaces.ManageServiceInterfaces;
using Auth.Application.Interfaces.ServiceInterfaces.ProcessingServices;
using Auth.Application.Interfaces.TokenServiceInterfaces;
using Auth.Application.Services.CoreServices.Permissions;
using Auth.Application.Services.CoreServices.Products;
using Auth.Application.Services.CoreServices.Roles;
using Auth.Application.Services.CoreServices.Security;
using Auth.Application.Services.CoreServices.Users;
using Auth.Application.Services.ManageServices.Users;
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
			services.AddTransient<IPermissionService, PermissionService>();
			services.AddTransient<IRoleService, RoleService>();
			services.AddTransient<IUserService, UserService>();
			services.AddTransient<IToken, Token>();
			services.AddTransient<IProductService, ProductService>();
			services.AddTransient<ISecurityService, SecurityService>();
			services.AddTransient<IUserManageService, UserManageService>();
			services.AddTransient<IUserProcessingService, UserProcessingService>();

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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

			return services;
		}
	}
}
