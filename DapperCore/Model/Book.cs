using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
