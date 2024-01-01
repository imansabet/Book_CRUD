using AutoMapper;
using CRUD_API.Model;

namespace CRUD_API.Map
{
    public class Mapper : Profile
    {
        public Mapper() 
        {
            CreateMap<AuthorDTO, Author>().ReverseMap();
            CreateMap<BookDTO, Book >().ReverseMap();
        }
    }
}
