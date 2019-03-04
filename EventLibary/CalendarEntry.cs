using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLibary
{
    // Enumerate values made to indicate what state an event object is in.
    public enum status : byte
    {
         NewlyMade,
         Changed,
         Deleted,
         UnChanged
    }


    // Creation of variables used by our program to make new Calendar entries (also known as events).
    public class CalendarEntry
    {
        public int Key;
        public DateTime Start;
        public DateTime End;
        public Hearse Hearse;
        public string Address;
        public string Comment;
        public status Status;


        // Constructor for our calendar entry.
        public CalendarEntry(int key, DateTime start, DateTime end, string address, string comment, status status, Hearse hearse = null)
        {
            Key = key;
            Start = start;
            End = end;
            Address = address;
            Comment = comment;
            Status = status;
            Hearse = hearse;
        }
        



         
    }
}
