using AutoMapper;
using CRUD_API.Model;

namespace CRUD_API.Map
{
    public class Mapper : Profile
    {
        public Mapper() 
        {
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<Book, BookDTO>().ReverseMap();
        }
    }
}
