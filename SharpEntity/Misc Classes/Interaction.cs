using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpEntity
{
    public class Interaction
    {
        #region Interaction Data Members
        protected Being m_source;
        protected Being m_dest;
        protected Queue<String> m_words;
        protected bool m_isSpeaking;
        #endregion


        #region Interaction Constructors
        public Interaction(Being in_source, Being in_dest)
        {
            m_source = in_source;
            m_dest = in_dest;
            m_words = new Queue<String>();
            m_isSpeaking = false;
        }
        #endregion


        #region Interaction Member Functions
        public String Speak()
        {
            String t_string = null;

            //the end of speaking
            if (m_words.Count == 0 && m_isSpeaking)
            {
                m_isSpeaking = false;
            }

            //while speaking is in progress
            else if (m_isSpeaking)
            {
                t_string = m_words.Dequeue();
            }

            //the start of speaking
            else if (!m_isSpeaking)
            {
                m_words = ((NPC)m_dest).Speech;
                t_string = m_words.Dequeue();
                m_isSpeaking = true;
            }

            return t_string;
        }
        public bool Give(IItem item)
        {
            bool wasGiven = false;

            //if the item can be added & the item can be removed
            if (m_dest.getInventory.Add(item))
            {
                if (m_source.getInventory.Remove(item.ID))
                {
                    wasGiven = true;
                }
                else
                {
                    m_dest.getInventory.Remove(item.ID);
                }
            }

            return wasGiven;
        }
        public bool Sell(IItem item)
        {
            bool wasSold = false;

            if (m_dest.Gold >= item.Cost)
            {
                if (m_dest.getInventory.Add(item))
                {
                    if (m_source.getInventory.Remove(item.ID))
                    {
                        m_dest.RemoveGold(item.Cost);
                        m_source.AddGold(item.Cost);
                        wasSold = true;
                    }
                    else
                    {
                        m_dest.getInventory.Remove(item.ID);
                    }
                }
            }

            return wasSold;
        }
        public bool Take(IItem item)
        {
            bool wasTaken = false;

            //if the item can be added & the item can be removed
            if (m_source.getInventory.Add(item))
            {
                if (m_dest.getInventory.Remove(item.ID))
                {
                    wasTaken = true;
                }
                else
                {
                    m_source.getInventory.Remove(item.ID);
                }
            }

            return wasTaken;
        }
        public bool Buy(IItem item)
        {
            bool wasBought = false;

            if (m_source.Gold >= item.Cost)
            {
                if (m_source.getInventory.Add(item))
                {
                    if (m_dest.getInventory.Remove(item.ID))
                    {
                        m_source.RemoveGold(item.Cost);
                        m_dest.AddGold(item.Cost);
                        wasBought = true;
                    }
                    else
                    {
                        m_source.getInventory.Remove(item.ID);
                    }
                }
            }

            return wasBought;
        }
        public void Battle()
        {
            throw new NotImplementedException();
        }
        public void Attack()
        {
            throw new NotImplementedException();
        }
        public void Defend()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
