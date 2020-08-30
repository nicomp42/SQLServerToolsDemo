using SQLServerTools;
using SQLServerToolsDemo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Demo
{
    public class Demo
    {
        static void Main(string[] args)
        {
            List<Book> books = ReadBooks();
            Console.WriteLine(books.Count + " books read from database");
/*            foreach (Book book in books)
            {
                Console.WriteLine(book.Author);
            } */
        }
        public Demo() { 
            SQLServerTools.SQLServerTools sqlServerTools = new SQLServerTools.SQLServerTools("IL-Server-002.uccc.uc.edu\\MSSQLServer2019", "nicomp", "nicomp", "Danger42!");
            sqlServerTools.HelloWorld();
            sqlServerTools.ConnectToDatabase();
            SqlDataReader dr = sqlServerTools.submitSQL("SELECT * FROM tFish");
            //check if there are records
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    //display retrieved record (second column only/string value)
                    Console.WriteLine(dr.GetString(1));     // Columns are zero-based
                }
            }
            else
            {
                Console.WriteLine("No data found.");
            }
        }
        /// <summary>
        /// Read books, authors, etc., from the KaggleGoodReadsBookDataset on our server.
        /// Our VPN needs to be running.
        /// </summary>
        /// <returns>A List object containing the collection of Book objects</returns>
        public static List<Book> ReadBooks()
        {
            List<SQLServerToolsDemo.Book> books = new List<Book>();
            string query = "SELECT Book, PagesNumber, Author, Language, Publisher "+
                           "FROM tBook B "+
                           "INNER JOIN tAuthor A ON B.AuthorID = A.AuthorID "+
                           "INNER JOIN tLanguage L ON B.LanguageID = L.LanguageID "+
                           "INNER JOIN tPublisher P ON B.PublisherID = P.PublisherID "+
                           "ORDER BY Book ASC";

            SQLServerTools.SQLServerTools sqlServerTools = new SQLServerTools.SQLServerTools("IL-Server-002.uccc.uc.edu\\MSSQLServer2019", "KaggleGoodReadsBookDataset", "KaggleGoodReadsBookDatasetLogin", "P@ssword1");
            sqlServerTools.HelloWorld();
            sqlServerTools.ConnectToDatabase();
            SqlDataReader dr = sqlServerTools.submitSQL(query);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Book book = new Book();
                    book.BookTitle = dr.GetString(0);
                    book.Author = dr.GetString(2);
                    book.Language = dr.GetString(3);
                    book.Publisher = dr.GetString(4);
                    books.Add(book);
                }
            }
            return books;
        }
    }
}
