using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpEntity
{
    public static class Constants
    {
        #region Interaction Enumeration
        //all interaction types 
        public enum InteractionType
        {
            Speak,
            Give,
            Sell,
            Take,
            Buy,
            Battle,
            Attack,
            Defend
        }
        #endregion


        #region ItemType Enumeration
        //all item types seperated by inheritance groups
        public enum ItemType
        {
            //Item.Weapon
            OneHand = 0,
            TwoHand,

            //Item.Armor
            Helm,
            Shoulders,
            Chest,
            Bracers,
            Gloves,
            Pants,
            Boots,
            Neck,
            Ring,

            //Item.Armor.Offhand
            Shield,
            Source
        }
        #endregion


        #region BeingType
        //all types for beings
        public enum BeingType
        {
            NPC,
            Player,
            Monster
        }

        //all sub-types for beings
        public enum BeingClassType
        {
            BlackMage,  //damage & intelligence
            WhiteMage,  //healing & intelligence
            Warrior,    //defense & strength
            Rogue       //damage & dexterity
        }
        #endregion


        #region Entity Constants
        public const float MORALITY_DEFAULT = 5.0f;
        #endregion


        #region Inventory Constants
        public const int INVENTORY_SIZE_DEFAULT = 32;
        #endregion


        #region Being Constants
        public const int HITPOINTS_MULT = 10;
        public const int POWER_MULT = 2;
        public const int DEFENSE_MULT = 2;
        public const int AGILITY_MULT = 2;

        #region Player Constants
        public const float STAT_HITPOINTS_MULT = 5.0f;
        public const float STAT_POWER_MULT = 1.0f;
        public const float STAT_DEFENSE_MULT = 1.0f;
        public const float STAT_AGILITY_MULT = 1.0f;

        public const float STAT_ARMOR_MULT = 2.0f;
        public const float STAT_WEAPON_MULT = 7.0f;
        #endregion
        #endregion
    }
}
