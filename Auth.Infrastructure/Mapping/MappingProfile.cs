using Auth.Application.Abstractions;
using Auth.Application.DTOs.Users;
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
		}

		public MappingProfile(IApplicationDbContext context)
		{
			_context = context;
		}
	}
}
