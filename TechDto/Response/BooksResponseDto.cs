namespace TechDto.Response
{
    public class BooksResponseDto
    {
        public PaginationResponseDto Pagination { get; set; } = default!;
        public List<BookResponseDto> Books { get; set; } = [];
    }
}
