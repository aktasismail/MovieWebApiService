using AutoMapper;
using EntitiesLayer;
using EntitiesLayer.DTO;

namespace MovieReviewAPP.Utilities.Mapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Movies,MovieDTO>();
            CreateMap<MovieUpdateDto,Movies>().ReverseMap();
            CreateMap<MovieCreatedDto, Movies>().ReverseMap();
            CreateMap<RegisterDto, User>();
        }
    }
}
