using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpEntity
{
    public class Inventory
    {
        #region Inventory Data Members
        //The list of items in the inventory
        protected IItem[] m_item_array;
        
        //The maximum number of items
        protected int m_size;
        #endregion


        #region Inventory Constructors
        public Inventory()
        {
            m_item_array = new IItem[Constants.INVENTORY_SIZE_DEFAULT];
            m_size = Constants.INVENTORY_SIZE_DEFAULT;
        }

        public Inventory(int in_size)
        {
            m_item_array = new IItem[in_size];
            m_size = in_size;
        }
        #endregion


        #region Inventory Properties
        public int getMaxSize
        {
            get { return m_size; }
        }

        //Mostly for testing
        public IItem[] getInventoryItemList
        {
            get { return m_item_array; }
        }
        #endregion


        #region Inventory Member Functions
        public String[] ListNames()
        {
            //Create a temporary list of strings which are the names of all items within the inventory
            String[] t_name_list = new String[m_size];

            //for every item in the inventory, copy the name into the name list
            for (int i = 0; i < m_size; ++i)
            {
                t_name_list[i] = m_item_array[i].Name;
            }

            //return the list of names
            return t_name_list;
        }

        public bool Add(IItem in_item)
        {
            bool wasAdded = false;

            //insert item into the first empty slot (null)
            for (int i = 0; i < m_size && !wasAdded; ++i)
            {
                if (m_item_array[i] == null)
                {
                    m_item_array[i] = in_item;
                    wasAdded = true;
                }
            }

            return wasAdded;
        }

        public bool Remove(int in_ID)
        {
            bool wasRemoved = false;

            //compare with each item until it's found
            for (int i = 0; i < m_size && !wasRemoved; ++i)
            {
                if (m_item_array[i] != null)
                {
                    if (m_item_array[i].ID == in_ID)
                    {
                        m_item_array[i] = null;
                        wasRemoved = true;
                    }
                }
            }

            // if the item isn't found, this will return null
            return wasRemoved;
        }

        public IItem GetItem(int in_ID)
        {
            IItem t_item = null;

            //compare with each item until it's found
            for (int i = 0; i < m_size; ++i)
            {
                if (m_item_array[i] != null)
                {
                    if (m_item_array[i].ID == in_ID)
                    {
                        t_item = m_item_array[i];
                    }
                }
            }

            // if the item isn't found, this will return null
            return t_item;
        }
        #endregion
    }
}
