using AutoMapper;
using GAMMHomeAssignmentAPI.Modal.UserModal;

namespace GAMMHomeAssignmentAPI.Mapper
{
	public class RegisterUserMapper : Profile
	{
		public RegisterUserMapper() {
			CreateMap<RegisterUserDTO, User>();
		}
	}

}
