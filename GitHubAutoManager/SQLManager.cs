using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubAutoManager
{
    static class SQLManager
    {
        public static bool isHaveTable(string TableName, SQLiteConnection connection)
        {

            bool state = false;
 
            using (SQLiteCommand command = connection.CreateCommand())
            {
                
                command.CommandText  = @"SELECT COUNT(*) FROM sqlite_master WHERE name='Local_RepoTable'";
                command.CommandType  = System.Data.CommandType.Text;
                SQLiteDataReader rdr = command.ExecuteReader();
                while(rdr.Read())
                {
                    Console.WriteLine(rdr["Count(*)"]);
                    state = rdr.GetInt32(0) == 1;
                }
            }
            return state;
         }

        public static void NoReturnCommand( string stringCommand, SQLiteConnection connection)
        {
            using (SQLiteCommand command = new SQLiteCommand(stringCommand, connection))
            {
                command.ExecuteNonQuery();
            }
        }


    }

}
