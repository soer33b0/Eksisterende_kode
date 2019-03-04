using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLibary
{
    // Hearse class to make our hearse objects used in the calendar to check availabilty.
    public class Hearse
    {
        // Variables 
        public int Key;
        public int Priority;
        public status Status;


        // Hearse constructor with two parameters.
        public Hearse(int prio, status sta)
        {
            
            Priority = prio;
            Status = sta;
        }


        // Hearse constructor with three parameters.
        public Hearse(int key,int prio, status sta) : this(prio,sta)
        {
            Key = key;
        }


        // A simple override for the Equals function.
        public override bool Equals(object obj)
        {
            if (obj is Hearse)
            {
                return this.Priority == (obj as Hearse).Priority;
                /*
                if (this.Priority == (obj as Hearse).Priority)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                */
            }
            else
            {
                return base.Equals(obj);
            }
        }
    }
}
