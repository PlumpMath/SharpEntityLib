using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpEntity
{
    public class Specialization
    {
        #region Data Members

        protected int m_hitpoints;
        protected int m_currentHitpoints;
        protected int m_power;
        protected int m_defense;
        protected int m_agility;

        #endregion

        #region Specialization Constructors

        public Specialization()
        {

        }

        #endregion

        #region Getters

        public int Hitpoints
        {
            get { return m_hitpoints; }
            set { m_hitpoints = value; }
        }

        public int CurrentHitpoints
        {
            get { return m_currentHitpoints; }
            set { m_currentHitpoints = value; }
        }

        public int Power
        {
            get { return m_power; }
            set { m_power = value; }
        }
        public int Defense
        {
            get { return m_defense; }
            set { m_defense = value; }
        }
        public int Agility
        {
            get { return m_agility; }
            set { m_agility = value; }
        }

        #endregion

        #region Specialization Member Functions


        #endregion
    }

    public class BlackMage : Specialization
    {

    }

    public class WhiteMage : Specialization
    {

    }

    public class Warrior : Specialization
    {

    }

    public class Rogue : Specialization
    {

    }
}

