using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpEntity
{
    //This is the wallet class
    public class Wallet
    {
        #region Wallet Data Members
        int m_gold;
        #endregion


        #region Wallet Constructors
        public Wallet()
        {
            m_gold = 0;
        }
        public Wallet(int in_gold)
        {
            m_gold = in_gold;
        }
        #endregion


        #region Wallet Properties
        public int Gold
        {
            get { return m_gold; }
            set { m_gold = value; }
        }
        #endregion


        #region Wallet Member Functions
        public int Add(int in_gold)
        {
            return (m_gold += in_gold);
        }
        public int Remove(int in_gold)
        {
            return (m_gold -= in_gold);
        }
        public override string ToString()
        {
            return m_gold.ToString("N0");
        }
        #endregion
    }
}
