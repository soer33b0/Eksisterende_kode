using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLibary
{
    public class HearseRepository
    {
        // Create a list of hearses
        private List<Hearse> Hearse = new List<Hearse>();


        // Adds a hearse to the list
        public void AddHearse(Hearse hearse)
        {
            Hearse.Add(hearse);
        }


        // Create a hearse object that takes two parameters
        public void CreateHearse(int prio,status status)
        {

            // For every hearse in the hearse list do ...
            foreach (Hearse i in Hearse)
            {
                if (prio == i.Priority)
                {
                    throw new MemberAccessException();
                }
            }
            Hearse hearse = new Hearse(prio, status);
            Hearse.Add(hearse);
            
        }


        // Alter a hearse object that takes two parameters
        public void AlterHearse(int prio, int cpri)
        {
            foreach (Hearse i in Hearse)
            {
                if (prio == i.Priority)
                {
                    i.Priority = cpri;
                }
            }

        }


        // Creates hearse at startup
        public void StartUpHearse(int prio)
        {
            Hearse hearse = new Hearse(prio, status.UnChanged);
            Hearse.Add(hearse);
        }


        // Deletes a hearse of the prio key parameter
        public void DeleteHearse(int prio)
        {
            foreach(Hearse i in Hearse)
            {
                if (prio == i.Priority)
                {
                    i.Status = status.Deleted;
                }
            }
        }


        // Method for retrieving a hearse.
        public Hearse GetHearse(int key)
        {
            foreach(Hearse i in Hearse)
            {
                if (i.Key == key)
                {
                    return i;
                }
            }
            return null;
        }

        
        // 
        public List<Hearse> GetCopyHearses()
        {
            List<Hearse> result = new List<Hearse>();
            foreach (Hearse item in Hearse)
            {
                result.Add(item);
            }
            return result;
        }
    }
}
