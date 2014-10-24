using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpEntity;

namespace SharpEntity
{
    #region Item Factory
    public class ItemFactory
    {
        public static IItem MakeItem(Constants.ItemType item_type)
        {
            IItem t_item = null;

            switch (item_type)
            {
                case Constants.ItemType.OneHand:
                    {
                        t_item = new OneHand("Generic OneHand", 1, 0);
                    }
                    break;
                case Constants.ItemType.TwoHand:
                    {
                        t_item = new TwoHand("Generic TwoHand", 1, 0);
                    }
                    break;
                case Constants.ItemType.Helm:
                    {
                        t_item = new Helm("Generic Helm", 1, 0);
                    }
                    break;
                case Constants.ItemType.Shoulders:
                    {
                        t_item = new Shoulders("Generic Shoulders", 1, 0);
                    }
                    break;
                case Constants.ItemType.Chest:
                    {
                        t_item = new Chest("Generic Chest", 1, 0);
                    }
                    break;
                case Constants.ItemType.Bracers:
                    {
                        t_item = new Bracers("Generic Bracers", 1, 0);
                    }
                    break;
                case Constants.ItemType.Gloves:
                    {
                        t_item = new Gloves("Generic Gloves", 1, 0);
                    }
                    break;
                case Constants.ItemType.Pants:
                    {
                        t_item = new Pants("Generic Pants", 1, 0);
                    }
                    break;
                case Constants.ItemType.Boots:
                    {
                        t_item = new Boots("Generic Boots", 1, 0);
                    }
                    break;
                case Constants.ItemType.Neck:
                    {
                        t_item = new Neck("Generic Neck", 1, 0);
                    }
                    break;
                case Constants.ItemType.Ring:
                    {
                        t_item = new Ring("Generic Ring", 1, 0);
                    }
                    break;
                case Constants.ItemType.Shield:
                    {
                        t_item = new Shield("Generic Shield", 1, 0);
                    }
                    break;
                case Constants.ItemType.Source:
                    {
                        t_item = new Source("Generic Source", 1, 0);
                    }
                    break;
            }

            return t_item;
        }
        public static IItem MakeItem(Constants.ItemType item_type, String in_name, int in_level, Constants.BeingClassType class_type)
        {
            IItem t_item = null;

            switch (item_type)
            {
                case Constants.ItemType.OneHand:
                    {
                        t_item = new OneHand(in_name, in_level, (int)class_type);
                    }
                    break;
                case Constants.ItemType.TwoHand:
                    {
                        t_item = new TwoHand(in_name, in_level, (int)class_type);
                    }
                    break;
                case Constants.ItemType.Helm:
                    {
                        t_item = new Helm(in_name, in_level, (int)class_type);
                    }
                    break;
                case Constants.ItemType.Shoulders:
                    {
                        t_item = new Shoulders(in_name, in_level, (int)class_type);
                    }
                    break;
                case Constants.ItemType.Chest:
                    {
                        t_item = new Chest(in_name, in_level, (int)class_type);
                    }
                    break;
                case Constants.ItemType.Bracers:
                    {
                        t_item = new Bracers(in_name, in_level, (int)class_type);
                    }
                    break;
                case Constants.ItemType.Gloves:
                    {
                        t_item = new Gloves(in_name, in_level, (int)class_type);
                    }
                    break;
                case Constants.ItemType.Pants:
                    {
                        t_item = new Pants(in_name, in_level, (int)class_type);
                    }
                    break;
                case Constants.ItemType.Boots:
                    {
                        t_item = new Boots(in_name, in_level, (int)class_type);
                    }
                    break;
                case Constants.ItemType.Neck:
                    {
                        t_item = new Neck(in_name, in_level, (int)class_type);
                    }
                    break;
                case Constants.ItemType.Ring:
                    {
                        t_item = new Ring(in_name, in_level, (int)class_type);
                    }
                    break;
                case Constants.ItemType.Shield:
                    {
                        t_item = new Shield(in_name, in_level, (int)class_type);
                    }
                    break;
                case Constants.ItemType.Source:
                    {
                        t_item = new Source(in_name, in_level, (int)class_type);
                    }
                    break;
            }

            return t_item;
        }
    }
    #endregion
}
