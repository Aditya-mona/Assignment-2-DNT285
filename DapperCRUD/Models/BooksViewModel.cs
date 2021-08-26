using System;

namespace DapperCRUD.Models
{
    public class BookViewModel
    {
        public Guid BookId { get; set; }

        public string BookTitle { get; set; }

        public string BookAuthor { get; set; }

        public Guid BookLanguageId { get; set; }

        public string BookLangugeName { get; set; }

    }
}
