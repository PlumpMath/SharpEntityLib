using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpEntity
{
    public class Source : OffHand
    {
        public Source()
            : base()
        {
            m_entity.Name = "Default Source";
        }

        public Source(String in_name, int in_level, int in_class_index)
            : base(in_name, in_level, in_class_index)
        {
            // TODO add unique Source attributes
        }
    }
}
