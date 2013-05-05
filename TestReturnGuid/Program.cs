namespace TestReturnGuid
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data;
    using System.Data.SqlClient;

    public class Program
    {
        static public Guid AddBook(string title, string strConnection)
        {
            Guid ID = Guid.Empty;

            string query = "INSERT INTO books(title) output inserted.ID values (@title);";

            using (SqlConnection conn = new SqlConnection(strConnection))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@title", SqlDbType.NVarChar);
                cmd.Parameters["@title"].Value = title;
                try
                {
                    conn.Open();
                    ID = (Guid)cmd.ExecuteScalar();
                    Console.WriteLine("Added book " + title + " with ID = " + ID.ToString("D"));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return (Guid)ID;
        }

        static void Main(string[] args)
        {
            string strConnection = @"Data Source=(local)\SQLExpress2005; Initial Catalog=Tests; Integrated Security=SSPI";

            for (int i = 0; i < 10; i++)
            {
                string title = "Enciclopedia Volumen " + i;
                Guid id = AddBook(title, strConnection);
            }

            Console.ReadLine();
        }
    }
}
