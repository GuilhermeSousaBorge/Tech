using Tech.Api.Infrastructure.DataAccess;
using TechDto.Request;
using TechDto.Response;

namespace Tech.Api.Service
{
    public class FilterBookService
    {
        private const int PAGE_SIZE = 10;

        public BooksResponseDto Execute(FilterBookRequestDto request)
        {
            var dbContext = new TechDbContext();

            var skip = ((request.PageNumber - 1) * PAGE_SIZE);


            var query = dbContext.Books.AsQueryable();

            if (string.IsNullOrWhiteSpace(request.Title) == false)
            {
                query = query.Where(book => book.Title.Contains(request.Title));
            }

            var books = query
                .OrderBy(book => book.Title).ThenBy(book => book.Author)
                .Skip(skip)
                .Take(PAGE_SIZE)
                .ToList();

            var totalCount = 0;
            if (string.IsNullOrWhiteSpace(request.Title))
                totalCount = dbContext.Books.Count();
            else
                totalCount = dbContext.Books.Count(book => book.Title.Contains(request.Title));

            return new BooksResponseDto
            {
                Pagination = new PaginationResponseDto
                {
                    PageNumber = request.PageNumber,
                    TotalCount = totalCount,
                },
                Books = books.Select(book => new BookResponseDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                }).ToList()
            };
        }
    }
}
