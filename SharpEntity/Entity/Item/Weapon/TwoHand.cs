using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpEntity
{
    public class TwoHand : Weapon
    {
        public TwoHand()
            : base()
        {
            m_entity.Name = "Default TwoHand";
        }

        public TwoHand(String in_name, int in_level, int in_class_index)
            : base(in_name, in_level, in_class_index)
        {
            // TODO add unique TwoHand Weapon attributes 
            //slower attack speed
            //more damage
        }
    }
}
