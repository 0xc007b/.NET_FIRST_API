namespace WebApplication1.Helpers;

using Models.Professions;
using AutoMapper;
using Entities;
using Models.Users;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // CreateRequest -> User
        CreateMap<CreateUserRequest, User>();
        // UpdateRequest -> User
        CreateMap<UpdateUserRequest, User>()
            .ForAllMembers(x => x.Condition((src, dest, prop) =>
                {
                    // ignore both null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) &&
                        string.IsNullOrEmpty((string)prop)) return false;
                    // ignore null role
                    if (x.DestinationMember.Name == "Role" && src.Role ==
                        null) return false;
                    return true;
                }
            ));

        CreateMap<CreateProfessionRequest, Profession>();
        CreateMap<UpdateProfessionRequest, Profession>()
            .ForAllMembers(x => x.Condition((src, dest, prop) =>
                {
                    // ignore both null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) &&
                        string.IsNullOrEmpty((string)prop)) return false;
                    return true;
                }
            ));
    }
}