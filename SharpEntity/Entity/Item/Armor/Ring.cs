using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpEntity
{
    public class Ring : Armor
    {
        public Ring()
            : base()
        {
            m_entity.Name = "Default Ring";
        }

        public Ring(String in_name, int in_level, int in_class_index)
            : base(in_name, in_level, in_class_index)
        {
            // TODO add unique Ring Armor attributes
        }
    }
}
