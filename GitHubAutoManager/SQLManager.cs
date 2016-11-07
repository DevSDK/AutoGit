using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
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
        public static SQLiteConnection ConnectionSqliteFile(string file)
        {
            SQLiteConnection connection = null;
            if (!new FileInfo(file).Exists)
            {
                SQLiteConnection.CreateFile(file);
            }
                connection = new SQLiteConnection("Data Source=data.db;Version=3;");
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(
                    @"CREATE TABLE Local_RepoTable (
                     RepoName TEXT(20),
                     RepoID INT(20),
                     Path VARCHAR(40)
                    );", connection))
                {//TODO: 테이블이 없다면 테이블 추가 명령어 추가
                    command.ExecuteNonQuery();
                }
            return connection;
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
