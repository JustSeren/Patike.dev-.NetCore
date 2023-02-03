using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public DateTime AuthorBirthday { get; set; }

        public ICollection<Book> Book { get; set; }
    }
}