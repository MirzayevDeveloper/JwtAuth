using Auth.Application.Abstractions;
using Auth.Application.DTOs.Permissions;
using Auth.Application.DTOs.Products;
using Auth.Application.DTOs.RolePermissions;
using Auth.Application.DTOs.Roles;
using Auth.Application.DTOs.UserRoles;
using Auth.Application.DTOs.Users;
using Auth.Domain.Entities.Permissions;
using Auth.Domain.Entities.Products;
using Auth.Domain.Entities.RolePermissions;
using Auth.Domain.Entities.Roles;
using Auth.Domain.Entities.UserRoles;
using Auth.Domain.Entities.Users;
using AutoMapper;

namespace Auth.Infrastructure.Mapping
{
	public class MappingProfile : Profile
	{
		private readonly IApplicationDbContext _context;
		public MappingProfile()
		{
			CreateMap<User, PostUserDto>().ReverseMap();
			CreateMap<User, GetUserDto>().ReverseMap();
			CreateMap<User, UpdateUserDto>().ReverseMap();
			CreateMap<User, DeleteUserDto>().ReverseMap();

			CreateMap<Product, PostProductDto>().ReverseMap();
			CreateMap<Product, GetProductDto>().ReverseMap();
			CreateMap<Product, UpdateProductDto>().ReverseMap();
			CreateMap<Product, DeleteProductDto>().ReverseMap();

			CreateMap<Role, PostRoleDto>().ReverseMap();
			CreateMap<Role, GetRoleDto>().ReverseMap();
			CreateMap<Role, UpdateRoleDto>().ReverseMap();
			CreateMap<Role, DeleteRoleDto>().ReverseMap();

			CreateMap<UserRole, PostUserDto>().ReverseMap();
			CreateMap<UserRole, GetUserRoleDto>().ReverseMap();
			CreateMap<UserRole, UpdateUserDto>().ReverseMap();
			CreateMap<UserRole, DeleteUserDto>().ReverseMap();

			CreateMap<RolePermission, PostRolePermissionDto>().ReverseMap();
			CreateMap<RolePermission, GetRolePermissionDto>().ReverseMap();
			CreateMap<RolePermission, UpdateRolePermissionDto>().ReverseMap();
			CreateMap<RolePermission, DeleteRolePermissionDto>().ReverseMap();

			CreateMap<Permission, PostPermissionDto>().ReverseMap();
			CreateMap<Permission, GetPermissionDto>().ReverseMap();
			CreateMap<Permission, UpdatePermissionDto>().ReverseMap();
			CreateMap<Permission, DeletePermissionDto>().ReverseMap();
		}

		public MappingProfile(IApplicationDbContext context)
		{
			_context = context;
		}
	}
}
