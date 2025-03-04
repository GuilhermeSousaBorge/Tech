﻿using Tech.Api.Infrastructure.DataAccess;
using Tech.Api.Service.LoggedUser;
using Tech.Exception;

namespace Tech.Api.Service
{
    public class RegisterBookCheckoutService
    {
        private const int MAX_LOAN_DAYS = 7;

        private readonly LoggedUserService _loggedUser;
        public RegisterBookCheckoutService(LoggedUserService loggedUser)
        {
            _loggedUser = loggedUser;
        }

        public void Execute(Guid bookId)
        {
            var dbContext = new TechDbContext();

            Validate(dbContext, bookId);

            var user = _loggedUser.User(dbContext);

            var entity = new Model.Checkout
            {
                UserId = user.Id,
                BookId = bookId,
                ExpectedReturnDate = DateTime.UtcNow.AddDays(MAX_LOAN_DAYS)
            };

            dbContext.Checkouts.Add(entity);

            dbContext.SaveChanges();
        }
        private void Validate(TechDbContext dbContext, Guid bookId)
        {
            var book = dbContext.Books.FirstOrDefault(book => book.Id == bookId);
            if (book is null)
                throw new NotFoundException("Livro não encontrado!");

            var amountBookNotReturned = dbContext.Checkouts.Count(checkout => checkout.BookId == bookId && checkout.ReturnedDate == null);

            if (amountBookNotReturned == book.Amount)
                throw new ConflictException("Livro não está disponível para empréstimo!");
        }
    }
}
