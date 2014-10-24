using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpEntity
{
    public class NPC : Being
    {
        #region NPC Data Members
        Queue<String> m_speech;
        #endregion


        #region NPC Constructors
        public NPC()
            : base("Default NPC", 1)
        {
            m_speech = new Queue<String>();
            m_speech.Enqueue("Message 1");
            m_speech.Enqueue("Message 2");
            m_speech.Enqueue("Message 3");
        }
        #endregion


        #region NPC Properties
        public Queue<String> Speech
        {
            get { return m_speech; }
            set { m_speech = value; }
        }
        #endregion
    }
}
