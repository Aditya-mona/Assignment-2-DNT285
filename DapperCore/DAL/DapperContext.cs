using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Data;
using DapperCore.Model;
using Dapper;

namespace DapperCore.DAL
{
    public class DapperContext
    {

        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DbConnection");
        }
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public List<Book> GetAllBooks()
        {
            var query = "SELECT * FROM Book";
            using (var connection = CreateConnection())
            {
                var books = connection.Query<Book>(query);
                return books.ToList();
            }
        }
        public List<Language> GetLanguage()
        {
            var query = "SELECT * FROM Language";
            using (var connection = CreateConnection())
            {
                var language = connection.Query<Language>(query);
                return language.ToList();
            }
        }
        public Language GetLanguageById(Guid? id)
        {
            var query = "SELECT * FROM Language WHERE LanguageId=@languageId";
            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@languageId", id == Guid.Empty ? (object)DBNull.Value : id, DbType.Guid);
                //var values = new { langugaeId = id };
                var language = connection.Query<Language>(query, parameters).FirstOrDefault();
                return language;
            }
        }

        public Book GetBookById(Guid? Id)
        {
            using (var connection = CreateConnection())
            {
                var query = "exec [usp_GetBook] @bookId";
                var parameters = new DynamicParameters();
                parameters.Add("@bookId", Id == Guid.Empty ? (object)DBNull.Value : Id, DbType.Guid);
                var book = connection.Query<Book>(query, parameters).FirstOrDefault();
                return book;
            }

        }
        public void SaveBook(Book book)
        {
            try
            {
                Book books = book;
                using (var connection = CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@bookId", book.BookId == Guid.Empty ? (object)DBNull.Value : book.BookId, DbType.Guid);
                    parameters.Add("@bookTitle", book.BookTitle);
                    parameters.Add("@bookAuthor", book.BookAuthor);
                    parameters.Add("@bookLanguageId", book.BookLanguageId == Guid.Empty ? (object)DBNull.Value : book.BookLanguageId, DbType.Guid);
                    var query = "exec [usp_SaveBook] @bookId, @bookTitle,@bookAuthor,@bookLanguageId";
                    //var values = new { bookId = (books.BookId==Guid.Empty?(object)DBNull.Value:book.BookId), bookTitle = books.BookTitle, bookAuthor = books.BookAuthor, bookLanguageId = books.BookLanguageId };
                    var data = connection.Query<Book>(query, parameters).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteBook(Guid? Id)
        {
            try
            {
                var query = "DELETE  FROM Book WHERE BookId=@bookId";
                using (var connection = CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@bookId", Id == Guid.Empty ? (object)DBNull.Value : Id, DbType.Guid);
                    connection.Query<Book>(query, parameters);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

    }
}
