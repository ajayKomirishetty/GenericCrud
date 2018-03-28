using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace GenericCrudOperations
{
    class GenericCrud
    {
        public void Select<T>(T tablename)
        {
            using (SqlConnection conn = new SqlConnection("data source=GGKU4MPC62;database =ajay;integrated security=true"))
            {

                string temp = tablename.GetType().Name;
                string _tableName = temp.Remove(temp.Length - 3);
                Console.WriteLine(_tableName);
                DataTable table = new DataTable();
                SqlCommand cm = new SqlCommand($"select * from {_tableName}", conn);
                SqlDataAdapter da = new SqlDataAdapter(cm);
                da.Fill(table);
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow dar in table.Rows)
                    {
                        for (int i = 0; i < table.Columns.Count; i++)
                            Console.Write(dar[i] + "\t");
                        Console.WriteLine();
                    }
                }
            }
        }
        public void Update<T>(T tablename)
        {

            using (SqlConnection conn = new SqlConnection("data source=GGKU4MPC62;database =ajay;integrated security=true"))
            {
                //retrieving tablename
                string temp = tablename.GetType().Name;
                string _tableName = temp.Remove(temp.Length - 3);

                //extracting primary key
                DataTable table = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter($"select * from {_tableName}", conn);
                da.Fill(table);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                string[] pk = GetPrimaryKeys(conn, _tableName);
              

                //asking user to enter primary key value and fetch records based on it
                var columnType = pk[0].GetType().Name;
                Console.WriteLine("enter value of" + pk[0] + " which is of type " + columnType);
                int _pkValue = Convert.ToInt32(Console.ReadLine());


                DataRow row = table.Rows[0];
                int i = 0;
                //asking users to update the value
                foreach (var column in table.Columns)
                {
                    Console.WriteLine("enter value of " + column/*+"  Type: "+column.GetType().Name*/);
                    row[i++] = Console.ReadLine();
                }
                da.Update(table);
                //  Select(tablename);
                Console.ReadLine();
            }
        }
        public void Delete<T>(T tablename)
        {
            using (SqlConnection conn = new SqlConnection("data source=GGKU4MPC62;database =ajay;integrated security=true"))
            {
                //retrieving tablename
                string name = tablename.GetType().Name;
                string _tableName = name.Remove(name.Length - 3);

                //extracting primary key
                string[] pk = GetPrimaryKeys(conn, _tableName);

                //asking user to enter primary key value and fetch records based on it
                var columnType = pk[0].GetType().Name;
                Console.WriteLine("enter value of" + pk[0] + " to be deleted ");
                int _pkValue = Convert.ToInt32(Console.ReadLine());

                DataTable table = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter($"delete  from {_tableName} where {pk[0]}={_pkValue}", conn);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                table.Clear();
                da.Fill(table);
                da.Update(table);
                Select(tablename);
                Console.ReadLine();
            }
        }
        public void Insert<T>(T tablename)
        {
            using (SqlConnection conn = new SqlConnection("data source=GGKU4MPC62;database =ajay;integrated security=true"))
            {
                try
                {
                    //retrieving tablename
                    string temp = tablename.GetType().Name;
                    string _tableName = temp.Remove(temp.Length - 3);
                    Console.WriteLine(_tableName);

                    //extracting primary key
                    DataTable table = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter($"select * from {_tableName}", conn);
                    SqlCommandBuilder command = new SqlCommandBuilder(da);
                    da.Fill(table);
                    PropertyInfo[] properties = tablename.GetType().GetProperties();
                    DataRow row = table.NewRow();
                    foreach (var property in properties)
                    {

                        var propertyName = property.Name;
                        Console.WriteLine("enter " + propertyName);
                        row[propertyName] = Console.ReadLine();
                    }
                    table.Rows.Add(row);
                    da.Update(table);
                  
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
        public string[] GetPrimaryKeys(SqlConnection connection, string tableName)
        {
            using (SqlDataAdapter adapter = new SqlDataAdapter("select * from " + tableName, connection))
            using (DataTable table = new DataTable(tableName))
            {
                return adapter
                    .FillSchema(table, SchemaType.Mapped)
                    .PrimaryKey.Select(c => c.ColumnName)
                    .ToArray();
            }
        }
    }
}
