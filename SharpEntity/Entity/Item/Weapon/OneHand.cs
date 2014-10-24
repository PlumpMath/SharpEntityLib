using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpEntity
{
    public class OneHand : Weapon
    {
        public OneHand()
            : base()
        {
            m_entity.Name = "Default OneHand";
        }

        public OneHand(String in_name, int in_level, int in_class_index)
            : base(in_name, in_level, in_class_index)
        {
            // TODO add unique OneHand Weapon attributes
            //faster attack speed
            //less damage
        }
    }
}
