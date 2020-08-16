using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Neko
{
    class Database
    {
        private static MySqlConnection _connection;
        List<string> colums = new List<string>()
        {
            "neko",
            "nekololi",
            "nekogif",
            "nekochibi",
            "nekoplus",
            "nekololiplus",
            "nekovideo"
        };

        public Database(string connection)
        {
            _connection = new MySqlConnection(connection);
        }

        public List<List<string>> ReadData(string sql, int count)
        {
            _connection.Open();
            // объект для выполнения SQL-запроса
            MySqlCommand command = new MySqlCommand(sql, _connection);
            // объект для чтения ответа сервера
            MySqlDataReader reader = command.ExecuteReader();
            var query = new List<List<string>>();
            int i = 0;
            while (reader.Read())
            {
                query.Add(new List<string>());
                for (int j = 0; j < count; j++)
                {
                    query[i].Add(reader[j].ToString());
                }
                i++;
            }
            _connection.Close();
            return query;  
        }

        public void IntializeUser(long id)
        {
            var b = ReadData("Select* from user where user.id = " + id, 8);
            if (b.Count == 0)//проверка count !=1
            {
                string te = "Insert into user values (" + id +",0,0,0,0,0,0,0)";
                ReadData(te, 0);
            }
        }

        public void InsertUser(int id)
        {
            string te = "Insert into user (`id`, `neko`, `nekololi`, `nekogif`, `nekochibi`, `nekoplus`, `nekololiplus`, `nekovideo`) " +
                "value (" + id + ", 0, 0, 0, 0, 0, 0, 0)";
            ReadData(te, 0);
        }

        //так как эта штука писалась меньше чем за вечер, то бд спроектирована одной таблицей и захардены места ответов, они анологичны порядку кнопок.
        //не меняй просто так индексы.
        //TODO: создать пару таблиц пользователь и на каждую кнопку, где хранятся id альбома, количество у пользователя, максимум в альбоме.
        public ulong GetArtCount(long id, int type)
        {
            var answer = ReadData("Select* from user where id = " + id, 8);
            ulong count = (ulong)Convert.ToInt64(answer[0][type]);
            return count;
        }
        //
        public void InsertCurrentCount(long id, ulong count, int type)
        {
            type = type - 1;
            ReadData("Update user set "+ colums[type] +"=" + count + " where id =" + id, 0);
        }


    }
}
