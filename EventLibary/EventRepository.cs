using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLibary
{
    public class EventRepository
    {
        List<Events> Eventslist = new List<Events>();
        HearseRepository HearseRepo;

        public EventRepository(HearseRepository hr)
        {
            HearseRepo = hr;
        }

        public void AddEvent(Events Event)
        {
            Eventslist.Add(Event);
        }


        public bool CreateEvent(DateTime start, DateTime end, string address, string comment, bool hearseNeded)
        {
            if (hearseNeded)
            {
                bool free = true;
                foreach (Hearse i in HearseRepo.GetCopyHearses())
                {
                    foreach (Events E in Eventslist)
                    {
                        if (E.Hearse == i && ((E.Start > end) || E.End < start))
                        {
                            free = false;
                        }
                    }
                    if (free)
                    {
                        Events Event = new Events(findHighestKey() + 1, start, end, address, comment, status.NewlyMade, i);
                        AddEvent(Event);
                        return true;
                    }
                    else
                    {
                        free = true;
                    }
                }
            }
            else
            {
                Events Event = new Events(findHighestKey() + 1, start, end, address, comment, status.NewlyMade, null);
                AddEvent(Event);
                return true;
            }
            return false;
        }

        public bool AlterEvent(int key, string start, string end, string address, string comment, int hearse)
        {
            Events E = new Events(0,DateTime.Now,DateTime.Now,"","",status.Deleted,null);
            foreach (Events i in Eventslist)
            {
                if (i.Key == key)
                {
                    E = i;
                }
            }
            if (E.Key == 0)
            {
                throw new IndexOutOfRangeException("key not found");
            }
            if (hearse == 0)
            {
                E.Hearse = null;
            }
            else
            {
                E.Hearse = HearseRepo.GetHearse(hearse);
            }
            if (!(start == null))
            {
                DateTime ostart;
                if (DateTime.TryParse(start,out ostart))
                {
                    bool free = true;
                    
                        foreach (Events e in Eventslist)
                        {
                            if (e.Hearse == E.Hearse && !(ostart<e.Start||ostart>e.End) )
                            {
                                free = false;
                            }
                        }
                        if (free)
                        {
                            E.Start = ostart;
                        }
                        else
                        {
                            free = true;
                        }
                    
                    
                }
                if(!(E.Start == ostart))
                {
                    return false;
                }
            }
            if (!(end == null))
            {
                DateTime oend;
                if (DateTime.TryParse(end, out oend))
                {
                    bool free = true;
                    
                        foreach (Events e in Eventslist)
                        {
                            if (e.Hearse == E.Hearse && !(oend > e.Start || oend < e.End))
                            {
                                free = false;
                            }
                        }
                        if (free)
                        {
                            E.Start = oend;
                        }
                        else
                        {
                            free = true;
                        }
                }
                if(E.End == oend)
                {
                    return true;
                }
            }
            if (!(address == null))
            {
                    E.Address = address;
            }
            if (!(comment == null))
            {
                    E.Comment= comment;
            }
            if (!(E.Status == status.NewlyMade))
            {
                E.Status = status.Changed;
            }
            return true;

        }

        public void StartUpEvent(int key, DateTime start, DateTime end, string address, string comment, Hearse hearse = null)
        {
            Events Event = new Events(key, start, end, address, comment, status.UnChanged, hearse);

            AddEvent(Event);
        }

        public int findHighestKey()
        {
            int highest = 0;
            foreach(Events i in Eventslist)
            {
                if(i.Key < highest)
                {
                    highest = i.Key;
                }
            }
            return highest;
        }

        public List<Events> GetCopyEvents()
        {
            List<Events> tempLists = Eventslist.ToList();
            return tempLists;
        }

        public bool DeleteEvent(int key)
        {
            foreach (Events i in Eventslist)
            {
                if (i.Key == key)
                {
                    i.Status = status.Deleted;
                    return true;
                }
            }
            return false;
        }
    }
}
