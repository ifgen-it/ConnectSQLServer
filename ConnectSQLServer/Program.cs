using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;



namespace ConnectSQLServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("begin");
            // Data Source = sql;
            // Initial Catalog = evgenDb;
            // Integrated Security = True

            // string connectionString = @"Data Source=sql;
            // Initial Catalog=evgenDb;
            // Integrated Security=True";

            // получаем строку подключения
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            Console.WriteLine(connectionString);

            // Создание подключения
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                // Открываем подключение
                connection.Open();
                Console.WriteLine("Подключение открыто");

                // Вывод информации о подключении
                Console.WriteLine("Свойства подключения:");
                Console.WriteLine("\tСтрока подключения: {0}", connection.ConnectionString);
                Console.WriteLine("\tБаза данных: {0}", connection.Database);
                Console.WriteLine("\tСервер: {0}", connection.DataSource);
                Console.WriteLine("\tВерсия сервера: {0}", connection.ServerVersion);
                Console.WriteLine("\tСостояние: {0}", connection.State);
                Console.WriteLine("\tWorkstationld: {0}", connection.WorkstationId);

               
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM messages";
                command.Connection = connection;


                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    // выводим названия столбцов
                    Console.WriteLine();
                    Console.WriteLine("{0}\t{1}\t\t{2}\t{3}", reader.GetName(0), reader.GetName(3), reader.GetName(1), reader.GetName(2));

                    while (reader.Read()) // построчно считываем данные
                    {
                        object id = reader.GetValue(0);
                        object user_name = reader.GetValue(1);
                        object text = reader.GetValue(2);
                        object date_time = reader.GetValue(3);

                        Console.WriteLine("{0} \t{1} \t{2} \t\t{3}", id, date_time, user_name, text);
                    }
                    Console.WriteLine();
                }
                reader.Close();
                
                string sqlExpression1 = "INSERT INTO messages (id, user_name, text, date_time) VALUES (15, 'Katusha', 'Hello Evgen, it is Kate!', GETDATE())";
                // string sqlExpression1 = "insert into messages (id) values(4);";
               // string sqlExpression1 = "update messages set user_name = 'evg' where id = 1;";


                SqlCommand command1 = new SqlCommand(sqlExpression1, connection);
                int number = command1.ExecuteNonQuery();
                Console.WriteLine("Добавлено объектов: {0}", number);
                


            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // закрываем подключение
                connection.Close();
                Console.WriteLine("Подключение закрыто...");
            }

            Console.Read();

        }
    
    }
}
