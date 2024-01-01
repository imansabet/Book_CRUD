namespace CRUD_API.Model
{
    public class BookDTO
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public AuthorDTO Author { get; set; }
    }
}