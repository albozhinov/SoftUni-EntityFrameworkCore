namespace ADONetTest
{
    using System;
    using System.Data.SqlClient;

    class Program
    {
        static void Main(string[] args)
        {
            var connection = new SqlConnection("Server=.;Database=TelerikAcademy;Integrated Security=True;");

            connection.Open();

            var something = new SomeClass();
            using (something)
            {

                // After this scope the connection will be closed! Here "using" call Dispose() to close the connection!
            }

            
            using (connection)
            {

                // Here we use ExecuteScalar() method who returns a single value (empoyees count)!
                var firstCommand = new SqlCommand("SELECT COUNT(*) FROM Employees", connection);

                var result = (int)firstCommand.ExecuteScalar();

                Console.WriteLine(result);
                

                // Here we got all employees and print there firstname and lastname!
                var secondCommand = new SqlCommand("SELECT * FROM Employees", connection);

                var reader = secondCommand.ExecuteReader();
                                
                using (reader)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Firstname: " + reader[1] + " <===> Lastname: " + reader[2]);

                        // Another syntax
                        var firstName = reader["FirstName"];
                        var secondName = reader["LastName"];

                        var print = $"Firstname: {firstName} <<<>>> LastName: {secondName}";
                        Console.WriteLine(print);
                    }
                }

                // This method will be Insert,Update or Delete some rows and return number of update rows! 
                firstCommand.ExecuteNonQuery();


                // SQL Injection!!!
                var name = "Kevin";
                var thirdCommand = new SqlCommand($"SELECT * FROM Employees WHERE FirstName = @name", connection);

                // This line protect us from SQL Injection!
                thirdCommand.Parameters.AddWithValue("@name", name);

                var injectionReader = thirdCommand.ExecuteReader();

                using (thirdCommand)
                {
                    while (injectionReader.Read())
                    {
                        for (int i = 0; i < injectionReader.FieldCount; i++)
                        {
                            Console.Write(injectionReader[i] + " " );
                        }

                        Console.WriteLine();
                    }
                }


            }





        }
    }
}
