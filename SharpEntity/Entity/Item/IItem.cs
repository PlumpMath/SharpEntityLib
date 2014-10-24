using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpEntity
{
    public interface IItem
    {
        #region Property Signatures
        int ID
        {
            get;
        }
        String Name
        {
            get;
        }
        float Value
        {
            get;
        }
        int Level
        {
            get;
        }
        int Strength
        {
            get;
        }
        int Dexterity
        {
            get;
        }
        int Intelligence
        {
            get;
        }
        int Vitality
        {
            get;
        }
        float Damage
        {
            get;
        }
        float Defense
        {
            get;
        }
        int ClassIndex
        {
            get;
        }
        int Cost
        {
            get;
        }
        float Rarity
        {
            get;
        }
        #endregion


        #region Member Function Signatures
        //int GenerateStat();
        //float RandomDistribution(int x);
        //float CalcRarity();
        #endregion
    }
}
