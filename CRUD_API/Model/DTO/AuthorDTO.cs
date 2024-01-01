namespace CRUD_API.Model
{
    public class AuthorDTO
    {
        internal readonly object Books;

        public int AuthorId { get; set; }
        public string Name { get; set; }
    }
}