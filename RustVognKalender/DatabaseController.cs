using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using EventLibary;
using System.Threading.Tasks;

namespace RustVognKalender
{
    public class DatabaseController
    {
        private readonly string ConnectionString;

        public DatabaseController()
        {
            //string tempsting = Path.GetFullPath("ConnectionString.txt");
            StreamReader reader = new StreamReader(@"..\..\ConnectionString.txt");
            ConnectionString = reader.ReadLine();
            reader.Close();
        }

        public bool CreateEvent(CalendarEntry events)
        {
             
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                if (events.Hearse == null){ 
                SqlCommand command = new SqlCommand();
                command.CommandText = "EXEC dbo.insert_event2 @START_AT, @END_AT, @AT_ADDRESS, @COMMENT";
                command.Parameters.AddWithValue("@START_AT", events.Start);
                command.Parameters.AddWithValue("@END_AT", events.End);
                command.Parameters.AddWithValue("@AT_ADDRESS", events.Address);
                command.Parameters.AddWithValue("@COMMENT", events.Comment);
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
            }else
            { 
                SqlCommand command = new SqlCommand();
                command.CommandText = "EXEC dbo.insert_event @START_AT, @END_AT, @VEHICLE, @AT_ADDRESS, @COMMENT";
                command.Parameters.AddWithValue("@START_AT", events.Start);
                command.Parameters.AddWithValue("@END_AT", events.End);
                command.Parameters.AddWithValue("@VEHICLE", events.Hearse.Key);
                command.Parameters.AddWithValue("@AT_ADDRESS", events.Address);
                command.Parameters.AddWithValue("@COMMENT", events.Comment);
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
            }
            }
            return true;
        }

        public bool AlterEvent(CalendarEntry events)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "EXEC dbo.update_event @KEY, @START_AT, @END_AT, @VEHICLE, @AT_ADDRESS, @COMMENT";
                command.Parameters.AddWithValue("@KEY", events.Key);
                command.Parameters.AddWithValue("@START_AT", events.Start.ToString());
                command.Parameters.AddWithValue("@END_AT", events.End.ToString());
                command.Parameters.AddWithValue("@VEHICLE", events.Hearse.Key);
                command.Parameters.AddWithValue("@AT_ADDRESS", events.Address);
                command.Parameters.AddWithValue("@COMMENT", events.Comment);
                command.Connection = connection;
                //Console.WriteLine(events.Key + " " + events.Start.ToString() + " " + events.End.ToString() + " " + events.Hearse.Key + " " + events.Address + " " + events.Comment);
                connection.Open();
                command.ExecuteNonQuery();
            }
            return true;
        }

        public bool DeleteEvent(int key)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "EXEC dbo.delete_event @KEY";
                command.Parameters.AddWithValue("@KEY", key);
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
            }
            return true;
        }


        /*
        private string FreeHearse(DateTime start, DateTime end)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("EXEC dbo.GET_HEARSE", connection);
                connection.Open();
                SqlDataReader HearseID = command.ExecuteReader();
                List<int> plateStrings = new List<int>();
                while (HearseID.Read())
                {
                    plateStrings.Add((int)HearseID[0]);
                }
                HearseID.Close();

                command.CommandText = "EXEC dbo.free_at @PRIORITY_";
                SqlDataReader times;

                foreach (int item in plateStrings)
                {
                    command.Parameters.AddWithValue("@PRIORITY_", item);
                    times = command.ExecuteReader();
                    bool isFree = true;
                    while (times.Read())
                    {
                        DateTime startTime = (DateTime)times["START_AT"];
                        DateTime endTime = (DateTime)times["END_AT"];
                        
                        if (!(start > endTime || end < startTime))
                        {
                            isFree = false;
                        }
                    }
                    times.Close();
                    if (isFree)
                    {
                        return item.ToString();
                    }
                    command.Parameters.Clear();
                }
            }
            throw new FileNotFoundException("Ingen rustvogn ledig");
        }
        */

        public bool Update(CalendarEntryRepository eventRepository, HearseRepository rustvognReposetory)
        {
            foreach (Hearse item in rustvognReposetory.GetCopyHearses())
            {

            }
            foreach (CalendarEntry item in eventRepository.GetCopyEvents())
            {
                if (item.Status == status.Changed)
                {
                    AlterEvent(item);
                    item.Status = status.UnChanged;
                }
                else if (item.Status == status.Deleted)
                {
                    DeleteEvent(item.Key);
                }
                else if (item.Status == status.NewlyMade)
                {
                    CreateEvent(item);
                    item.Status = status.UnChanged;
                }
                else if (item.Status == status.UnChanged)
                {
                    continue;
                }
                else
                {
                    throw new InvalidDataException("Ukendt status enum. Fil: 'DatabaseController.Update'");
                }
            }

            return true;
        }

        public List<Tuple<int,int>> StartUpHearse()
        {
            List<Tuple<int, int>> result = new List<Tuple<int, int>>();
            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.GET_ALL_HEARSE", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int pri = (int)reader["PRIORITY_"];
                    int key = (int)reader["SURROGATE_KEY"];
                    Tuple<int, int> tuple = new Tuple<int, int>(key, pri);
                    result.Add(tuple);
                }
            }
            return result;
        }

        public List<Tuple<int,DateTime,DateTime,int, string, string>> StartUpEvents()
        {
            List<Tuple<int, DateTime, DateTime, int, string, string>> result = 
                new List<Tuple<int, DateTime, DateTime, int, string, string>>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.GET_ALL_EVENTS", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int surrogateKey = (int)reader["SURROGATE_KEY"];
                    DateTime start = (DateTime)reader["START_AT"];
                    DateTime end = (DateTime)reader["END_AT"];
                    //int vehicle;
                    //if (int.TryParse(reader["VEHICLE"].ToString(), out vehicle ))
                    //{

                    //}
                    int vehicle = reader["VEHICLE"] == System.DBNull.Value ? default(int) : (int)reader["VEHICLE"];
                    string address = (string)reader["AT_ADDRESS"];
                    string comment = (string)reader["COMMENT"];

                    Tuple<int, DateTime, DateTime, int, string, string> tuple = 
                        new Tuple<int, DateTime, DateTime, int, string, string>
                        (surrogateKey, start, end, vehicle, address, comment);

                    result.Add(tuple);
                }
            }
            return result;
        }
    }
}
