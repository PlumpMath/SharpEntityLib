using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpEntity
{
    public class ItemStats
    {
        int m_strength;
        int m_dexterity;
        int m_intelligence;
        int m_vitality;
        float m_damage;
        float m_defense;
        int m_class_index;
        int m_cost;

        public ItemStats()
        {
            m_strength = 0;
            m_dexterity = 0;
            m_intelligence = 0;
            m_vitality = 0;
            m_damage = 0;
            m_defense = 0;
            m_class_index = 0;
            m_cost = 0;
        }

        #region ItemStats Properties
        public int Strength
        {
            get { return m_strength; }
            set { m_strength = value; }
        }
        public int Dexterity
        {
            get { return m_dexterity; }
            set { m_dexterity = value; }
        }
        public int Intelligence
        {
            get { return m_intelligence; }
            set { m_intelligence = value; }
        }
        public int Vitality
        {
            get { return m_vitality; }
            set { m_vitality = value; }
        }
        public float Damage
        {
            get { return m_damage; }
            set { m_damage = value; }
        }
        public float Defense
        {
            get { return m_defense; }
            set { m_defense = value; }
        }
        public int ClassIndex
        {
            get { return m_class_index; }
            set { m_class_index = value; }
        }
        public int Cost
        {
            get { return m_cost; }
            set { m_cost = value; }
        }
        #endregion

        #region Misc Functions
        public float SquareDistribution(int x)
        {
            float result = ((float)x / (float)int.MaxValue) * 100.0f;
            return (float)Math.Pow(((float)result / 10f), 2f);
        }
        public float FourthPowerDistribution(int x)
        {
            float result = ((float)x / (float)int.MaxValue) * 100.0f;
            return (float)Math.Pow(((float)result / 31.6227766f), 4f);
        }
        public float ParabolicDistribution(int x)
        {
            float result = ((float)x / (float)int.MaxValue) * 100.0f;
            return (float)Math.Pow((((float)result - 50f) / 5f), 2f);
        }
        #endregion
    }
}
