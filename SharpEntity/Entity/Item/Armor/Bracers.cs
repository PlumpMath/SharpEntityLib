using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpEntity
{
    public class Bracers : Armor
    {
        public Bracers()
            : base()
        {
            m_entity.Name = "Default Bracers";
        }

        public Bracers(String in_name, int in_level, int in_class_index)
            : base(in_name, in_level, in_class_index)
        {
            // TODO add unique Bracers Armor attributes
        }
    }
}
