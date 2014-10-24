using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpEntity
{
    public class Misc : IItem
    {
        protected Entity m_entity;
        protected ItemStats m_stats;

        #region IItem Properties
        public int ID
        {
            get { return m_entity.ID; }
            set { m_entity.ID = value; }
        }
        public String Name
        {
            get { return m_entity.Name; }
            set { m_entity.Name = value; }
        }
        public float Value
        {
            get { return m_entity.Value; }
            set { m_entity.Value = value; }
        }
        public int Level
        {
            get { return m_entity.Level; }
            set { m_entity.Level = value; }
        }
        public int Strength
        {
            get { return m_stats.Strength; }
            set { m_stats.Strength = value; }
        }
        public int Dexterity
        {
            get { return m_stats.Dexterity; }
            set { m_stats.Dexterity = value; }
        }
        public int Intelligence
        {
            get { return m_stats.Intelligence; }
            set { m_stats.Intelligence = value; }
        }
        public int Vitality
        {
            get { return m_stats.Vitality; }
            set { m_stats.Vitality = value; }
        }
        public float Damage
        {
            get { return m_stats.Damage; }
            set { m_stats.Damage = value; }
        }
        public float Defense
        {
            get { return m_stats.Defense; }
            set { m_stats.Defense = value; }
        }
        public int ClassIndex
        {
            get { return m_stats.ClassIndex; }
            set { m_stats.ClassIndex = value; }
        }
        public int Cost
        {
            get { return m_stats.Cost; }
            set { m_stats.Cost = value; }
        }
        public float Rarity
        {
            get { return CalcRarity(); }
        }
        #endregion

        public Misc()
        {
            m_entity = new Entity("Default Weapon", 1);
            m_stats = new ItemStats();
        }
        public Misc(String in_name, int in_level)
        {
            m_entity = new Entity(in_name, in_level);
            m_stats = new ItemStats();
            m_stats.Damage = m_stats.SquareDistribution(m_entity.Rand.Next());
        }
        public Misc(String in_name, int in_level, int in_class)
        {
            m_entity = new Entity(in_name, in_level);
            m_stats = new ItemStats();
            m_stats.ClassIndex = in_class;
            m_stats.Damage = m_stats.SquareDistribution(m_entity.Rand.Next());
        }

        private int GenerateStat()
        {
            float t_stat_value = 0;
            float rand_avg = 0;

            //average 2 rolls between 0 and 10
            rand_avg = (m_entity.Rand.Next() % 10) + (m_entity.Rand.Next() % 10);
            rand_avg = rand_avg / 2;

            //scale to item's level
            t_stat_value = rand_avg * m_entity.Level;

            //scale to item's rarity
            t_stat_value *= (m_entity.Value / 25);

            return (int)t_stat_value;
        }
        public float CalcRarity()
        {
            if (!m_entity.isRarityCalculated)
            {
                // this is the number of data points factored in for rarity
                int num_data = 1;
                m_entity.Rarity = 0;

                //sum the values
                m_entity.Rarity += m_entity.Value;

                //average the value
                m_entity.Rarity = (m_entity.Rarity / num_data);

                //set bool
                m_entity.isRarityCalculated = true;
            }
            return m_entity.Rarity;
        }
    }
}
