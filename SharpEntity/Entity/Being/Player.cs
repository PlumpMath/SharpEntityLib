using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpEntity
{
    public class Player : Being
    {
        #region Player Data Members
        protected IItem[] m_equipped = new IItem[12];
        #endregion


        #region Player Constructors
        public Player()
            : base("Default Player", 1)
        {
            m_equipped[0] = new Helm();
            m_equipped[1] = new Shoulders();
            m_equipped[2] = new Chest();
            m_equipped[3] = new Bracers();
            m_equipped[4] = new Gloves();
            m_equipped[5] = new Pants();
            m_equipped[6] = new Boots();
            m_equipped[7] = new Neck();
            m_equipped[8] = new Ring();
            m_equipped[9] = new Ring();
            m_equipped[10] = new OffHand();
            m_equipped[11] = new Weapon();
        }
        #endregion


        #region Player Properties
        public IItem[] Equipped
        {
            get { return m_equipped; }
        }
        #endregion


        #region Player Member Functions
        public override bool Equip(IItem in_item)
        {
            bool wasEquipped = false;

            //for every item or until the item is equipped
            for (int i = 0; i < 12 && !wasEquipped; ++i)
            {
                
                if (m_equipped[i].ToString() == in_item.ToString())
                {
                    if (i == 8)
                    {
                        //shift in the new ring to be equipped & shift out the second ring into inventory
                        m_inventory.Add(m_equipped[9]);
                        m_equipped[9] = m_equipped[8];
                        m_equipped[8] = in_item;
                        m_inventory.Remove(in_item.ID);
                        i++;
                    }
                    else
                    {
                        EquipAndSwapToInventory(ref m_equipped[i], ref in_item);
                        wasEquipped = true;
                    }
                }
                else if (i == 10)
                {
                    if (in_item.ToString() == "SharpEntity.Source" || in_item.ToString() == "SharpEntity.Shield" || in_item.ToString() == "SharpEntity.OffHand")
                    {
                        EquipAndSwapToInventory(ref m_equipped[i], ref in_item);
                        wasEquipped = true;
                    }
                }
                else if (i == 11)
                {
                    if (in_item.ToString() == "SharpEntity.OneHand" || in_item.ToString() == "SharpEntity.TwoHand" || in_item.ToString() == "SharpEntity.Weapon")
                    {
                        EquipAndSwapToInventory(ref m_equipped[i], ref in_item);
                        wasEquipped = true;
                    }
                }
            }

            UpdateStatsFromEquipped();

            return wasEquipped;
        }
        private void EquipAndSwapToInventory(ref IItem eq_item, ref IItem inv_item)
        {
            //put equipped item into inventory
            m_inventory.Add(eq_item);

            //equip new item in it's place
            eq_item = inv_item;

            //remove newly equipped items from inventory
            m_inventory.Remove(inv_item.ID);
        }
        private void UpdateStatsFromEquipped()
        {
            m_specialization.Hitpoints = 0;
            m_specialization.Power = 0;
            m_specialization.Defense = 0;
            m_specialization.Agility = 0;

            for (int i = 0; i < 12; ++i)
            {
                //for every item equipped
                m_specialization.Hitpoints += (int)(m_equipped[i].Vitality * Constants.STAT_HITPOINTS_MULT * m_level);
                m_specialization.Power += (int)(m_equipped[i].Intelligence * Constants.STAT_POWER_MULT * m_level);
                m_specialization.Defense += (int)(m_equipped[i].Strength * Constants.STAT_DEFENSE_MULT * m_level);
                m_specialization.Agility += (int)(m_equipped[i].Dexterity * Constants.STAT_AGILITY_MULT * m_level);

                if (i == 10)
                {
                    //for the offhand
                    if (m_equipped[i].ToString() == "SharpEntity.Source" || m_equipped[i].ToString() == "SharpEntity.Shield" || m_equipped[i].ToString() == "SharpEntity.OffHand")
                    {
                        m_specialization.Defense += (int)(m_equipped[i].Strength * Constants.STAT_ARMOR_MULT * m_level);
                    }
                }
                else if (i == 11)
                {
                    //for the weapon
                    if (m_equipped[i].ToString() == "SharpEntity.OneHand" || m_equipped[i].ToString() == "SharpEntity.TwoHand" || m_equipped[i].ToString() == "SharpEntity.Weapon")
                    {
                        m_specialization.Power += (int)(m_equipped[i].Intelligence * Constants.STAT_WEAPON_MULT * m_level);
                    }
                }
            }
        }
        #endregion
    }
}
