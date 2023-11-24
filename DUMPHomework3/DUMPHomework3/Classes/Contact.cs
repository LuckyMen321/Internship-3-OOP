using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DUMPHomework3.Classes
{
    public class Contact
    {
        public string NameOfContact { get; set; } = "";
        public int NumberOfContact { get; set; }
        public string PreferenceOfContact { get; set; } = "";
        
        public Contact(int number, string name, string preference)
        {
            NameOfContact = name;
            NumberOfContact = number;
            PreferenceOfContact = preference;
        }
        public override int GetHashCode()
        {
            return NumberOfContact.GetHashCode() ^ NameOfContact.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Contact other = (Contact)obj;
            return NumberOfContact == other.NumberOfContact && NameOfContact == other.NameOfContact;
        }
    }
}
