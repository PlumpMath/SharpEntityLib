using System;

namespace SharpEntity
{
    public class Entity
    {
        #region Data Members
        //Globally tracks number of items that have been instantiated
        protected static int m_static_ID = 1;

        //the current item's ID copied from static_ID on instantiation
        protected int m_ID;

        //used for random generation
        protected static Random m_rand = new Random();

        //the item's unique name
        protected String m_name;

        //value is the entity's randomly generated data member
        protected float m_value;
        
        //stores the rarity once it's been calculated
        protected float m_rarity;

        //the item's level
        protected int m_level;

        //the item's morality percentage
        protected float m_morality;

        //used to skip rarity generation if it's already been generated
        protected bool m_isRarityCalculated;
        #endregion


        #region Entity Constructors
        public Entity()
        {
            //set the default morality
            m_morality = Constants.MORALITY_DEFAULT;

            //default the rarity calculation status to true for default item
            m_isRarityCalculated = true;

            //sets this item's ID based on the the static static_ID for all items
            m_ID = m_static_ID++;

            //set to default falues
            m_level = 1;
            m_name = "Default Entity";
            m_value = 0;
            m_rarity = 0;
        }

        public Entity(String in_name, int in_level)
        {
            //set the default morality
            m_morality = Constants.MORALITY_DEFAULT;

            //default the rarity calculation status to false
            m_isRarityCalculated = false;

            //sets this item's ID based on the the static static_ID for all items
            m_ID = m_static_ID++;

            //set the level
            m_level = in_level;

            //sets the name
            m_name = in_name;

            //get value from function
            m_value = ParabolicDistribution(m_rand.Next());
        }
        #endregion


        #region Getters
        public int ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }
        public Random Rand
        {
            get { return m_rand; }
            set { m_rand = value; }
        }
        public String Name
        {
            get { return m_name; }
            set { m_name = value; }
        }
        public float Value
        {
            get { return m_value; }
            set { m_value = value; }
        }
        public float Rarity
        {
            get { return m_rarity; }
            set { m_rarity = value; }
        }
        public int Level
        {
            get { return m_level; }
            set { m_level = value; }
        }
        public float Morality
        {
            get { return m_morality; }
            set { m_morality = value; }
        }
        public bool isRarityCalculated
        {
            get { return m_isRarityCalculated; }
            set { m_isRarityCalculated = value; }
        }
        #endregion


        #region Random Distribution Functions
        protected float SquareDistribution(int x)
        {
            float result = ((float)x / (float)int.MaxValue) * 100.0f;
            return (float)Math.Pow(((float)result / 10f), 2f);
        }

        protected float FourthPowerDistribution(int x)
        {
            float result = ((float)x / (float)int.MaxValue) * 100.0f;
            return (float)Math.Pow(((float)result / 31.6227766f), 4f);
        }

        protected float ParabolicDistribution(int x)
        {
            float result = ((float)x / (float)int.MaxValue) * 100.0f;
            return (float)Math.Pow((((float)result - 50f) / 5f), 2f);
        }
        #endregion


        #region Member Functions
        //This function calculates the item rarity using Value.
        //the rarity is stored in calculated_rarity and returned
        //public virtual float Rarity()
        //{
        //    if (!m_isRarityCalculated)
        //    {
        //        //this is the number of data points factored in for rarity
        //        //this function serves as a model for inheriting objects
        //        //in the case of objects with more than one randomly rolled data member,
        //        //more than one integer would be averaged into the final rarity
        //        int num_data = 1;

        //        //initialize rarity to 0
        //        m_rarity = 0;

        //        //sum the values
        //        m_rarity += m_value;

        //        //average the value
        //        m_rarity = (m_rarity / num_data);

        //        //set bool
        //        m_isRarityCalculated = true;
        //    }
        //    return m_rarity;
        //}
        #endregion
    }
}
