using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Applications.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }
        private readonly BookStoreDBContext _contex;

        public DeleteAuthorCommand(BookStoreDBContext contex)
        {
            _contex = contex;
        }

        public void Handle()
        {
            var author = _contex.Authors.SingleOrDefault(z => z.Id == AuthorId);

            if (author is null)
                throw new InvalidOperationException("Yazar Bulunamadı");

            _contex.Authors.Remove(author);
            _contex.SaveChanges();
        }

    }

}