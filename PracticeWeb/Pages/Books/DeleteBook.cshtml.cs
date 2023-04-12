using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace PracticeWeb.Pages.Books
{
    public class DeleteBookModel : PageModel
    {
        public Books book = new Books();
        public string message = "";
        public string BookCode = "";
        public void OnGet()
        {
            try
            {
                BookCode = Request.Query["BookCode"];

                SqlConnection connection = new SqlConnection("Data Source=5CG9441HWP;Initial Catalog=Library;Integrated Security=True;Encrypt=False;");
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();

                cmd.CommandText = $"SELECT BOOK_CODE,BOOK_TITLE,AUTHOR,CATEGORY,PUBLICATION," +
                    $"PUBLIC_DATE,BOOK_EDITION,PRICE FROM LMS_BOOK_DETAILS WHERE BOOK_CODE = '{BookCode}';";



                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    book.BookTitle = (string)reader["BOOK_TITLE"];
                    book.Author = (string)reader["AUTHOR"];
                    book.Category = (string)reader["CATEGORY"];
                    book.Publication = (string)reader["PUBLICATION"];
                    book.PublicDate = (DateTime)reader["PUBLIC_DATE"];
                    book.BookEdition = (int)reader["BOOK_EDITION"];
                    book.Price = (int)reader["PRICE"];

                }

            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }

        public void OnPost()
        {
            message = "";
            BookCode = Request.Query["BookCode"];
            SqlConnection connection = new SqlConnection("Data Source=5CG9441HWP;Initial Catalog=Library;Integrated Security=True;Encrypt=False;");
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            try
            {
                cmd.CommandText = $"DELETE FROM LMS_BOOK_DETAILS WHERE BOOK_CODE = '{BookCode}'";

                int affectedRow = cmd.ExecuteNonQuery();

                message = "Book Deleted Successfully";

                if(affectedRow > 0)
                {
                    Response.Redirect("/Books/IndexBook/");
                }

            }
            catch (Exception ex)
            {
                message = ex.Message;
                Console.WriteLine(ex.Message);
            }
        }
    }
}
