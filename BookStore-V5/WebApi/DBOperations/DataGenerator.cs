using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DBOperations
{

    public class DataGenerator
    {
        public static void Initilaze(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDBContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDBContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }

                context.Authors.AddRange(
                    new Author
                    {
                        AuthorName = "Madeline",
                        AuthorSurname = "Miller",
                        AuthorBirthday = new DateTime(1995, 06, 12)
                    },
                     new Author
                     {
                         AuthorName = "Patrick",
                         AuthorSurname = "Rothfuss",
                         AuthorBirthday = new DateTime(1968, 02, 15)
                     },
                     new Author
                     {
                         AuthorName = "Ahmet",
                         AuthorSurname = "Batman",
                         AuthorBirthday = new DateTime(1945, 08, 14)
                     }
                );

                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Personal Growth"
                    },
                    new Genre
                    {
                        Name = "Science Fiction"
                    },
                    new Genre
                    {
                        Name = "Noval"
                    }
                );

                context.Books.AddRange(new Book
                {
                    Title = "Lean Startup",
                    GenreID = 1, //Personal Growth,
                    PageCount = 200,
                    PublishDate = new DateTime(2001, 06, 12)
                },
                    new Book
                    {
                        Title = "Herland",
                        GenreID = 2, //Science Fiction,
                        PageCount = 250,
                        PublishDate = new DateTime(2010, 05, 23)
                    },
                    new Book
                    {
                        Title = "Dune",
                        GenreID = 2, //Personal Growth,
                        PageCount = 540,
                        PublishDate = new DateTime(2002, 12, 21)
                    }

                );
                context.SaveChanges();
            }

        }
    }
}