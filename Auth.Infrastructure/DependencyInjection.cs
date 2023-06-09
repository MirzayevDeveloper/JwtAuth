﻿using Auth.Application.Abstractions;
using Auth.Infrastructure.Mapping;
using Auth.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
			{
				options.UseNpgsql(connectionString: configuration.GetConnectionString("DefaultConnection"));
			});

			services.AddAutoMapper(typeof(MappingProfile));

			return services;
		}
	}
}
