using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace DataAccessLayer
{
    /// <summary>
    /// Data Access Layer class
    /// </summary>
    class DAL:IDAL
    {
        /// <summary>
        /// Data(rows of tables)
        /// </summary>
        private List<List<object>> Data = new List<List<object>>();

        /// <summary>
        /// Executing given operation with given parameters.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<object> GetData(string code, KeyValuePair<string, object> parameters)
        {
            StreamReader sReader=new StreamReader(@"C:\Users\Mary\Documents\Visual Studio 2017\Projects\DataAccessLayer\DataAccessLayer\orderby.txt");
            string my_code="";
            Data = new List<List<object>>();
            switch (code)
            {
                case "my order by":
                    sReader = new StreamReader(@"C:\Users\Mary\Documents\Visual Studio 2017\Projects\DataAccessLayer\DataAccessLayer\orderby.txt");
                    my_code = sReader.ReadToEnd();
                    my_code += " " + parameters.Key;
                    if (parameters.Value == "descending order")
                    {
                        sReader = new StreamReader(@"C:\Users\Mary\Documents\Visual Studio 2017\Projects\DataAccessLayer\DataAccessLayer\orderbydesc.txt");
                        my_code += " " + sReader.ReadToEnd();
                    }
                    break;
                case "my where":
                    sReader = new StreamReader(@"C:\Users\Mary\Documents\Visual Studio 2017\Projects\DataAccessLayer\DataAccessLayer\where.txt");
                    my_code = sReader.ReadToEnd();
                    my_code += " " + parameters.Key + " < " + parameters.Value;
                    break;
                case "my group by":
                    sReader = new StreamReader(@"C:\Users\Mary\Documents\Visual Studio 2017\Projects\DataAccessLayer\DataAccessLayer\groupby.txt");
                    my_code = sReader.ReadToEnd();
                    my_code += " " + parameters.Key;
                    break;
                case "my right join":
                    sReader = new StreamReader(@"C:\Users\Mary\Documents\Visual Studio 2017\Projects\DataAccessLayer\DataAccessLayer\rightjoin.txt");
                    my_code = sReader.ReadToEnd();
                    my_code += parameters.Key;
                    sReader = new StreamReader(@"C:\Users\Mary\Documents\Visual Studio 2017\Projects\DataAccessLayer\DataAccessLayer\append.txt");
                    my_code += sReader.ReadToEnd();
                    my_code += parameters.Value;
                    Console.WriteLine(my_code);
                    break;
                case "my left join":
                    sReader = new StreamReader(@"C:\Users\Mary\Documents\Visual Studio 2017\Projects\DataAccessLayer\DataAccessLayer\leftjoin.txt");
                    my_code = sReader.ReadToEnd();
                    my_code += parameters.Key;
                    sReader = new StreamReader(@"C:\Users\Mary\Documents\Visual Studio 2017\Projects\DataAccessLayer\DataAccessLayer\append.txt");
                    my_code += sReader.ReadToEnd();
                    my_code += parameters.Value;
                    Console.WriteLine(my_code);
                    break;
            }

            sReader.Close();
            string conStr = "Data Source=(local);Initial Catalog=AdventureWorks2;Integrated Security=True";
            using (var connection = new SqlConnection(conStr))
                {
                    SqlCommand command = new SqlCommand(my_code, connection);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int i = 0;
                            var elems = new List<object>();
                            while (i != reader.FieldCount)
                            {
                                elems.Add(reader[i++]);
                            }
                            Data.Add(elems);
                        }
                    }
                    reader.Close();
                }
            
            return Data;
        }

        /// <summary>
        /// Printng data
        /// </summary>
        public void Print()
        {
            for (int i = 0; i < Data.Count; ++i)
            {
                for (int j = 0; j < Data[0].Count; ++j)
                {
                    Console.Write(Data[i][j] + " ");
                }
                Console.WriteLine();
            }
        }

    }
}


