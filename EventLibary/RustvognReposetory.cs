using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLibary
{
    public class RustvognReposetory
    {
        private List<Hearse> Rustvognen;

        public void AddRustvogn(Hearse rustvogn)
        {
            Rustvognen.Add(rustvogn);
        }


        public void CreateRustvogn(int pri,status status)
        {
            foreach (Hearse i in Rustvognen)
            {
                if (pri == i.Priority)
                {
                    throw new MemberAccessException();
                }
            }
            Hearse rustvogn = new Hearse(pri, status);
            Rustvognen.Add(rustvogn);
            
        }
        public void alterRustvogn(int pri, int cpri)
        {
            foreach (Hearse i in Rustvognen)
            {
                if (pri == i.Priority)
                {
                    i.Priority = cpri;
                }
            }

        }

        public void getRustvogn(int pri)
        {
            Hearse rustvogn = new Hearse(pri, status.UnChanged);
            Rustvognen.Add(rustvogn);
        }
        public void DeleteRustvogn(int pri)
        {
            foreach(Hearse i in Rustvognen)
            {
                if (pri == i.Priority)
                {
                    i.Status = status.Deleted;
                }
            }
        }

        public List<Hearse> GetCopyHearses()
        {
            List<Hearse> result = new List<Hearse>();
            foreach (Hearse item in Rustvognen)
            {
                result.Add(item);
            }
            return result;
        }
    }
}
