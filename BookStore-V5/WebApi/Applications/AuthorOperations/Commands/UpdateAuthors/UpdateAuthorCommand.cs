using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Applications.AuthorOperations.Commands.UpdateAuthors
{
    public class UpdateAuthorCommand
    {
        public UpdateAuthorModel Model { get; set; }
        public int AuthorId { get; set; }
        private readonly BookStoreDBContext _context;
        public UpdateAuthorCommand(BookStoreDBContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (author is null)
                throw new InvalidOperationException("Yazar Bulunamadı");


            if (_context.Authors.Any(x => x.AuthorName.ToLower() + " " + x.AuthorSurname.ToLower() == Model.AuthorName.ToLower() + " " + Model.AuthorSurname.ToLower() && x.Id != AuthorId))
                throw new InvalidOperationException("Aynı isimde yazr mevcut");

            author.AuthorName = string.IsNullOrEmpty(Model.AuthorName.Trim()) ? author.AuthorName : Model.AuthorName;
            author.AuthorSurname = string.IsNullOrEmpty(Model.AuthorSurname.Trim()) ? author.AuthorSurname : Model.AuthorSurname;
            _context.SaveChanges();

        }

    }

    public class UpdateAuthorModel
    {
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public DateTime AuthorBD { get; set; }
        public string Book { get; set; }
    }
}