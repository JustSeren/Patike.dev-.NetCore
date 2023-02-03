using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Applications.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        public CreateAuthorModel Model { get; set; }
        private readonly BookStoreDBContext _context;

        public CreateAuthorCommand(BookStoreDBContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var aouthor = _context.Authors.SingleOrDefault(x => x.AuthorName + " " + x.AuthorSurname == Model.Name + " " + Model.Surname);
            if (aouthor is not null)
                throw new InvalidOperationException("Kitap yazarı zaten mevcut");

            aouthor = new Author();
            aouthor.AuthorName = Model.Name;
            _context.Authors.Add(aouthor);
            _context.SaveChanges();
        }
    }

    public class CreateAuthorModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime AouthorBD { get; set; }
    }
}