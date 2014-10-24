using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpEntity
{
    public class Being : Entity
    {
        #region Data Members

        protected Specialization m_specialization;
        protected Inventory m_inventory;
        protected Wallet m_wallet;

        #endregion


        #region Being Constructors

        public Being()
            : base("Default Being", 1)
        {
            m_specialization = new Specialization();
            m_inventory = new Inventory();
            m_wallet = new Wallet();
        }

        public Being(String in_name, int in_level)
            : base(in_name, in_level)
        {
            m_specialization = new Specialization();
            m_inventory = new Inventory();
            m_wallet = new Wallet();
            m_specialization.Hitpoints = (in_level * Constants.HITPOINTS_MULT);
            m_specialization.Power = (in_level * Constants.POWER_MULT);
            m_specialization.Defense = (in_level * Constants.DEFENSE_MULT);
            m_specialization.Agility = (in_level * Constants.AGILITY_MULT);
        }

        #endregion


        #region Getters

        public int getHitpoints
        {
            get { return m_specialization.Hitpoints; }
        }
        public int getPower
        {
            get { return m_specialization.Power; }
        }
        public int getDefense
        {
            get { return m_specialization.Defense; }
        }
        public int getAgility
        {
            get { return m_specialization.Agility; }
        }
        public Inventory getInventory
        {
            get { return m_inventory; }
        }
        public int Gold
        {
            get { return m_wallet.Gold; }
            set { m_wallet.Gold = value; }
        }
        #endregion


        #region Being Member Functions
        public virtual bool Equip(IItem in_item)
        {
            throw new Exception("Being can not equip items! Use an inheriting class.");
        }
        public int AddGold(int in_gold)
        {
            return m_wallet.Add(in_gold);
        }
        public int RemoveGold(int in_gold)
        {
            return m_wallet.Remove(in_gold);
        }
        public String WalletValue()
        {
            return m_wallet.ToString();
        }
        #endregion
    }
}
