using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _737Connector
{
    class Db
    {
        private SQLiteConnection connection = new SQLiteConnection("Data Source=db.sqlite");
      private Connector _connector;
        public Db(Connector connector)
        {
          _connector = connector;
            connection.Open();
        }

        public List<RotaryEncoder> InitializeEncodersFromDb()
        {
            var encoders = new List<RotaryEncoder>();
            using (SQLiteCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM rotary_encoders";
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (DBNull.Value.Equals(reader["BoardId"])|| DBNull.Value.Equals(reader["Pin1"]) || DBNull.Value.Equals(reader["Pin2"]))
                    {
                        continue;
                    }
                        var pin1 = PinToRegisterAndPosition(Convert.ToInt32(reader["Pin1"]));
                        var pin2 = PinToRegisterAndPosition(Convert.ToInt32(reader["Pin2"]));
                        bool invert = Convert.ToBoolean(reader["InvertPins"]);
                    encoders.Add(!invert
                        ? new RotaryEncoder(Convert.ToInt32(reader["EventId"]), _connector, pin1.Key, pin2.Key,
                            pin1.Value, pin2.Value)
                        : new RotaryEncoder(Convert.ToInt32(reader["EventId"]), _connector, pin2.Key, pin1.Key,
                            pin2.Value, pin1.Value));
                }
            }

            return encoders;
        }

        public KeyValuePair<int, int> PinToRegisterAndPosition(int pin)
        {
            var pair = new KeyValuePair<int,int>();

            using (SQLiteCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = "SELECT RegId,Bit FROM board_pins where Id = @id";
                cmd.Parameters.Add(new SQLiteParameter("@id", pin));
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                   pair = new KeyValuePair<int, int>(Convert.ToInt32(reader["RegId"]),Convert.ToInt32(reader["Bit"]));
                }
            }

            return pair;
        }

        public Dictionary<int, KeyValuePair<int, int>> GetRegistersAndPins()
        {
            var dict = new Dictionary<int,KeyValuePair<int,int>>();

            using (SQLiteCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = "SELECT RegId,Bit FROM board_pins";               
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {                    
                    dict.Add(reader.StepCount,new KeyValuePair<int, int>(Convert.ToInt32(reader["RegId"]), Convert.ToInt32(reader["Bit"])));
                }
            }

            return dict;
        }

        public void InitializeGlobalPinArray()
        {
            using (SQLiteCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = "SELECT Id,RegId,Bit FROM board_pins";
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Globals.Registers[Convert.ToInt32(reader["RegId"]), Convert.ToInt32(reader["Bit"])] =
                        Convert.ToInt32(reader["Id"]);
                }
            }
        }
    }
}
