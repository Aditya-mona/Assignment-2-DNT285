using System;

namespace DapperCore.Model
{
    public class Book
    {
        public Guid BookId { get; set; }

        public string BookTitle { get; set; }

        public string BookAuthor { get; set; }

        public Guid BookLanguageId { get; set; }

    }
}
