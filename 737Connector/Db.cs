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
        private readonly SQLiteConnection _connection = new SQLiteConnection("Data Source=db.sqlite");
        private readonly Connector _connector;
        public Db(Connector connector)
        {
          _connector = connector;
            _connection.Open();
        }

        public List<RotaryEncoder> InitializeEncodersFromDb()
        {
            var encoders = new List<RotaryEncoder>();
            using (SQLiteCommand cmd = _connection.CreateCommand())
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

            using (SQLiteCommand cmd = _connection.CreateCommand())
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

            using (SQLiteCommand cmd = _connection.CreateCommand())
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

        public void InitializeGlobalPinArray()//run once, on initiialize
        {
            using (SQLiteCommand cmd = _connection.CreateCommand())
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

        public Dictionary<int, HashSet<int>> GetRegistersWithBitsForActionbyType(string type)//to setup
        {
            var dict = new Dictionary<int, HashSet<int>>();

            using (SQLiteCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT RegId,GROUP_CONCAT(Bit) as GH from board_pins where Id IN(SELECT BoardPin FROM events WHERE Type = @type ) GROUP By RegId";
                cmd.Parameters.Add(new SQLiteParameter("@type", type));
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var bits = new HashSet<int>(reader["GH"].ToString().Split(',').Select(Int32.Parse));
                    dict.Add(Convert.ToInt32(reader["RegID"]),bits);
                }
            }

            return dict;
        }

        public void GenerateEventsMap()//to setup
        { 
            using (SQLiteCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText =
                    "SELECT events.Id,board_pins.RegId,board_pins.Bit FROM events INNER JOIN board_pins ON events.BoardPin = board_pins.Id;";
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Globals.EventsMap[Convert.ToInt32(reader["RegId"]), Convert.ToInt32(reader["Bit"])] =
                        Convert.ToInt32(reader["Id"]);
                }
            }
        }

    }
}
