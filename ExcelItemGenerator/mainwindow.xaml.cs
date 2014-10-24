using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Excel = Microsoft.Office.Interop.Excel;
using SharpEntity;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace ExcelItemGenerator
{
    public partial class MainWindow : System.Windows.Window
    {
        //Global
        Random m_rand = new Random((int)DateTime.Now.Ticks);
        int m_level = 1;

        //Character Grid
        Player m_character = new Player();

        //Excel Grid
        private bool m_isClosing = false;

        //Item Grid
        private IItem m_gen_item;

        //Inventory Grid
        //use the character grid's inventory
        //List<Item> m_character.getInventory = new List<Item>();

        public MainWindow()
        {
            InitializeComponent();

            //disable the loading animation for excel panel
            this.LoadingBlock.Visibility = Visibility.Hidden;

            //init loading text to blank
            this.ExcelLoadingPercentageBlock.Text = "";

            UpdateCharacterEquippedItemInfo();
            UpdateInventoryListBox();
            UpdateCharacterIDListBox();

            //DISABLE UNEQUIP BUTTON FOR NOW
            //this.Unequip_Item.Visibility = Visibility.Hidden;
        }


        #region Excel Grid Controls

        private void GenerateExcelItems()
        {
            //Open Excel application
            Excel.Application excelApp = new Excel.Application();

            //list to store items in
            List<IItem> items = new List<IItem>();

            //other variables
            int times = 0;
            int level = 1;

            //Get the number from NumItemsExcelBox
            try
            {
                //Communicate with another thread
                this.Dispatcher.Invoke((System.Action)(() =>
                {
                    times = Int32.Parse(this.NumItemsExcelBox.Text.ToString());
                }));
            }
            catch (Exception)
            {
                times = 100;
            }

            //Randomly generate variety of items
            for (int i = 0; i < times; ++i)
            {
                switch (m_rand.Next() % 13)
                {
                    case  0: { items.Add(new OneHand("OneHand", level, 0)); } break;
                    case  1: { items.Add(new TwoHand("TwoHand", level, 0)); } break;
                    case  2: { items.Add(new Helm("Helm", level, 0)); } break;
                    case  3: { items.Add(new Shoulders("Shoulders", level, 0)); } break;
                    case  4: { items.Add(new Chest("Chest", level, 0)); } break;
                    case  5: { items.Add(new Bracers("Bracers", level, 0)); } break;
                    case  6: { items.Add(new Gloves("Gloves", level, 0)); } break;
                    case  7: { items.Add(new Pants("Pants", level, 0)); } break;
                    case  8: { items.Add(new Boots("Boots", level, 0)); } break;
                    case  9: { items.Add(new Neck("Neck", level, 0)); } break;
                    case 10: { items.Add(new Ring("Ring", level, 0)); } break;
                    case 11: { items.Add(new Shield("Shield", level, 0)); } break;
                    case 12: { items.Add(new Source("Source", level, 0));  } break;
                    default: { } break;
                }
            }

            //Add and name the workbook
            excelApp.Workbooks.Add();
            Excel._Worksheet workSheet = (Excel.Worksheet)excelApp.ActiveSheet;

            //Set column width
            workSheet.Columns.ColumnWidth = 13;

            //freeze chart area
            workSheet.Application.ActiveWindow.SplitColumn = 10;
            workSheet.Application.ActiveWindow.SplitRow= 1;
            workSheet.Application.ActiveWindow.FreezePanes = true;

            //Insert the accounts into the spreadsheet
            if (!InsertItemsExcel(workSheet, items, times))
            {
                //Make Excel visible
                excelApp.Visible = true;

                //Create Chart that displays the rarity distribution
                Excel.ChartObjects xlCharts = (Excel.ChartObjects)workSheet.ChartObjects(Type.Missing);
                Excel.ChartObject myChart = (Excel.ChartObject)xlCharts.Add(750, 20, 450, 550);
                Excel.Chart chartPage = myChart.Chart;
                myChart.Select();
                chartPage.ChartType = Excel.XlChartType.xlLine;
                Excel.SeriesCollection seriesCollection = chartPage.SeriesCollection();

                //set up the series and it's range
                Excel.Series series1 = seriesCollection.NewSeries();
                series1.Values = workSheet.get_Range("C1", "C" + (times + 1));
                Excel.Range allDataRange = workSheet.UsedRange;
                allDataRange.Sort(allDataRange.Columns[3], Excel.XlSortOrder.xlAscending);

                try
                {
                    //Set the column labels
                    workSheet.Cells[1, "A"] = "ID";
                    workSheet.Cells[1, "B"] = "Name";
                    workSheet.Cells[1, "C"] = "Rarity";
                    workSheet.Cells[1, "D"] = "Value";
                    workSheet.Cells[1, "E"] = "Damage";
                    workSheet.Cells[1, "F"] = "Armor";
                    workSheet.Cells[1, "G"] = "Strength";
                    workSheet.Cells[1, "H"] = "Dexterity";
                    workSheet.Cells[1, "I"] = "Intelligence";
                    workSheet.Cells[1, "J"] = "Vitality";

                    //Communicate with another thread
                    this.Dispatcher.Invoke((System.Action)(() =>
                    {
                        // turn off loading indicator in GUI
                        this.LoadingBlock.Visibility = Visibility.Hidden;

                        // clear loading percentage box
                        this.ExcelLoadingPercentageBlock.Text = "";
                    }));
                }
                catch (TaskCanceledException)
                {   }
                catch (Exception)
                {   }
            }
        }
        private void GenerateExcelButton_Click(object sender, RoutedEventArgs e)
        {
            // Turn on loading indicator in GUI
            this.LoadingBlock.Visibility = Visibility.Visible;

            //Run the Excel generator in a new thread
            new Thread(() => GenerateExcelItems()).Start();
        }
        private bool InsertItemsExcel(Excel._Worksheet work_sheet, List<IItem> items, int num_items)
        {
            var row = 0;
            bool isClosing = false;
            IItem item = items[row];

            while(row < num_items && isClosing == false)
            {

                item = items[row++];

                //for all types of items
                work_sheet.Cells[row + 1, "A"] = item.ID;
                work_sheet.Cells[row + 1, "B"] = item.Name;
                work_sheet.Cells[row + 1, "C"] = item.Rarity;
                work_sheet.Cells[row + 1, "D"] = item.Value.ToString();
                work_sheet.Cells[row + 1, "G"] = item.Strength.ToString();
                work_sheet.Cells[row + 1, "H"] = item.Dexterity.ToString();
                work_sheet.Cells[row + 1, "I"] = item.Intelligence.ToString();
                work_sheet.Cells[row + 1, "J"] = item.Vitality.ToString();

                //for weapons
                if (item is Weapon)
                {
                    work_sheet.Cells[row + 1, "E"] = ((Weapon)item).Damage.ToString();
                }

                //for Armor
                if (item is Armor)
                {
                    work_sheet.Cells[row + 1, "F"] = ((Armor)item).Defense.ToString();
                }

                try
                {
                    //Communicate with another thread
                    this.Dispatcher.Invoke( (System.Action) (() =>
                        {
                            if (m_isClosing)
                            {
                                isClosing = true;
                            }
                            else
                            {
                                //Update ExcelLoadingPrecentageBlock
                                ExcelLoadingPercentageBlock.Text = ((int)(((float)row / (float)num_items) * 100.0f)).ToString() + "%";
                            }
                        }));
                }
                catch (TaskCanceledException)
                {
                    isClosing = true;
                }
                catch (Exception)
                {
                    isClosing = true;
                }
            }
            return isClosing;
        }
        private void LoadingBlock_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void NumItemsExcelBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void NumItemsExcelBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //check for the enter key while the focus is in the box for number
            if (e.Key == Key.Enter)
            {
                GenerateExcelButton_Click(sender, e);
            }
        }

        #endregion


        #region Item Grid Controls

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                m_level = Int32.Parse(this.LevelTextBox.Text.ToString());
            }
            catch (Exception)
            {
                m_level = 1;
            }

            if (m_level < 1)
            {
                m_level = 1;
            }
            else if (m_level > 100)
            {
                m_level = 100;
            }

            LevelTextBox.Text = m_level.ToString();
        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            //check for the enter key while the focus is in the box for number
            if (e.Key == Key.Enter)
            {
                try
                {
                    m_level = Int32.Parse(this.LevelTextBox.Text.ToString());
                }
                catch (Exception)
                {
                    m_level = 1;
                }
                GenerateItemButton_Click(sender, e);
                LevelTextBox.Text = m_level.ToString();
            }
        }
        private void RandomItemCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ItemTypeListBox.Visibility = Visibility.Hidden;
            ClassTypeListBox.Visibility = Visibility.Hidden;
        }
        private void RandomItemCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ItemTypeListBox.Visibility = Visibility.Visible;
            ClassTypeListBox.Visibility = Visibility.Visible;
        }
        private void GenerateItemButton_Click(object sender, RoutedEventArgs e)
        {
            //Generate a completely random item
            if (RandomItemCheckBox.IsChecked == true)
            {
                GenerateRandomItem();

                if (LegendaryRadioButton.IsChecked == true)
                {
                    while (m_gen_item.Rarity < 90f)
                    {
                        GenerateRandomItem();
                    }
                }
                else if (RareRadioButton.IsChecked == true)
                {
                    while (m_gen_item.Rarity < 75f)
                    {
                        GenerateRandomItem();
                    }
                }
                else if (MagicRadioButton.IsChecked == true)
                {
                    while (m_gen_item.Rarity < 50f)
                    {
                        GenerateRandomItem();
                    }
                }
            }
            
            //generate an item from the given criteria
            else 
            {
                GenerateCustomItem();

                if (LegendaryRadioButton.IsChecked == true)
                {
                    while (m_gen_item.Rarity < 90f)
                    {
                        GenerateCustomItem();
                    }
                }
                else if (RareRadioButton.IsChecked == true)
                {
                    while (m_gen_item.Rarity < 75f)
                    {
                        GenerateCustomItem();
                    }
                }
                else if (MagicRadioButton.IsChecked == true)
                {
                    while (m_gen_item.Rarity < 50f)
                    {
                        GenerateCustomItem();
                    }
                }
            }

            UpdateGeneratedItemInfo();
        }
        private void GenerateRandomItem()
        {
            int class_num = (m_rand.Next() % 5) - 1;
            switch (m_rand.Next() % 13)
            {
                case 0: { m_gen_item = new OneHand("OneHand", m_level, class_num); } break;
                case 1: { m_gen_item = new TwoHand("TwoHand", m_level, class_num); } break;
                case 2: { m_gen_item = new Helm("Helm", m_level, class_num); } break;
                case 3: { m_gen_item = new Shoulders("Shoulders", m_level, class_num); } break;
                case 4: { m_gen_item = new Chest("Chest", m_level, class_num); } break;
                case 5: { m_gen_item = new Bracers("Bracers", m_level, class_num); } break;
                case 6: { m_gen_item = new Gloves("Gloves", m_level, class_num); } break;
                case 7: { m_gen_item = new Pants("Pants", m_level, class_num); } break;
                case 8: { m_gen_item = new Boots("Boots", m_level, class_num); } break;
                case 9: { m_gen_item = new Neck("Neck", m_level, class_num); } break;
                case 10: { m_gen_item = new Ring("Ring", m_level, class_num); } break;
                case 11: { m_gen_item = new Shield("Shield", m_level, class_num); } break;
                case 12: { m_gen_item = new Source("Source", m_level, class_num); } break;
                default: { } break;
            }
        }
        private void GenerateCustomItem()
        {
            String temp = ItemTypeListBox.SelectedItem.ToString();
            switch (temp.Substring(temp.LastIndexOf(':') + 2))
            {
                
                case "All": 
                    {
                        switch (m_rand.Next() % 13)
                        {
                            case 0: { m_gen_item = new OneHand("OneHand", m_level, ClassTypeListBox.SelectedIndex - 1); } break;
                            case 1: { m_gen_item = new TwoHand("TwoHand", m_level, ClassTypeListBox.SelectedIndex - 1); } break;
                            case 2: { m_gen_item = new Helm("Helm", m_level, ClassTypeListBox.SelectedIndex - 1); } break;
                            case 3: { m_gen_item = new Shoulders("Shoulders", m_level, ClassTypeListBox.SelectedIndex - 1); } break;
                            case 4: { m_gen_item = new Chest("Chest", m_level, ClassTypeListBox.SelectedIndex - 1); } break;
                            case 5: { m_gen_item = new Bracers("Bracers", m_level, ClassTypeListBox.SelectedIndex - 1); } break;
                            case 6: { m_gen_item = new Gloves("Gloves", m_level, ClassTypeListBox.SelectedIndex - 1); } break;
                            case 7: { m_gen_item = new Pants("Pants", m_level, ClassTypeListBox.SelectedIndex - 1); } break;
                            case 8: { m_gen_item = new Boots("Boots", m_level, ClassTypeListBox.SelectedIndex - 1); } break;
                            case 9: { m_gen_item = new Neck("Neck", m_level, ClassTypeListBox.SelectedIndex - 1); } break;
                            case 10: { m_gen_item = new Ring("Ring", m_level, ClassTypeListBox.SelectedIndex - 1); } break;
                            case 11: { m_gen_item = new Shield("Shield", m_level, ClassTypeListBox.SelectedIndex - 1); } break;
                            case 12: { m_gen_item = new Source("Source", m_level, ClassTypeListBox.SelectedIndex - 1); } break;
                            default: { } break;
                        }
                    } 
                    break;
                case "AllWeapon":
                    {
                        switch (m_rand.Next() % 2)
                        {
                            case 0: { m_gen_item = new OneHand("OneHand", m_level, ClassTypeListBox.SelectedIndex); } break;
                            case 1: { m_gen_item = new TwoHand("TwoHand", m_level, ClassTypeListBox.SelectedIndex); } break;
                            default: { } break;
                        }
                    }
                    break;
                case "AllArmor":
                    {
                        switch (m_rand.Next() % 11)
                        {
                            case 0: { m_gen_item = new Helm("Helm", m_level, ClassTypeListBox.SelectedIndex); } break;
                            case 1: { m_gen_item = new Shoulders("Shoulders", m_level, ClassTypeListBox.SelectedIndex); } break;
                            case 2: { m_gen_item = new Chest("Chest", m_level, ClassTypeListBox.SelectedIndex); } break;
                            case 3: { m_gen_item = new Bracers("Bracers", m_level, ClassTypeListBox.SelectedIndex); } break;
                            case 4: { m_gen_item = new Gloves("Gloves", m_level, ClassTypeListBox.SelectedIndex); } break;
                            case 5: { m_gen_item = new Pants("Pants", m_level, ClassTypeListBox.SelectedIndex); } break;
                            case 6: { m_gen_item = new Boots("Boots", m_level, ClassTypeListBox.SelectedIndex); } break;
                            case 7: { m_gen_item = new Neck("Neck", m_level, ClassTypeListBox.SelectedIndex); } break;
                            case 8: { m_gen_item = new Ring("Ring", m_level, ClassTypeListBox.SelectedIndex); } break;
                            case 9: { m_gen_item = new Shield("Shield", m_level, ClassTypeListBox.SelectedIndex); } break;
                            case 10: { m_gen_item = new Source("Source", m_level, ClassTypeListBox.SelectedIndex); } break;
                            default: { } break;
                        }
                    }
                    break;
                case "OneHand": { m_gen_item = new OneHand("OneHand", m_level, ClassTypeListBox.SelectedIndex); } break;
                case "TwoHand": { m_gen_item = new TwoHand("TwoHand", m_level, ClassTypeListBox.SelectedIndex); } break;
                case "Helm": { m_gen_item = new Helm("Helm", m_level, ClassTypeListBox.SelectedIndex); } break;
                case "Shoulders": { m_gen_item = new Shoulders("Shoulders", m_level, ClassTypeListBox.SelectedIndex); } break;
                case "Chest": { m_gen_item = new Chest("Chest", m_level, ClassTypeListBox.SelectedIndex); } break;
                case "Bracers": { m_gen_item = new Bracers("Bracers", m_level, ClassTypeListBox.SelectedIndex); } break;
                case "Gloves": { m_gen_item = new Gloves("Gloves", m_level, ClassTypeListBox.SelectedIndex); } break;
                case "Pants": { m_gen_item = new Pants("Pants", m_level, ClassTypeListBox.SelectedIndex); } break;
                case "Boots": { m_gen_item = new Boots("Boots", m_level, ClassTypeListBox.SelectedIndex); } break;
                case "Neck": { m_gen_item = new Neck("Neck", m_level, ClassTypeListBox.SelectedIndex); } break;
                case "Ring": { m_gen_item = new Ring("Ring", m_level, ClassTypeListBox.SelectedIndex); } break;
                case "Shield": { m_gen_item = new Shield("Shield", m_level, ClassTypeListBox.SelectedIndex); } break;
                case "Source": { m_gen_item = new Source("Source", m_level, ClassTypeListBox.SelectedIndex); } break;
                default: { } break;
            }
        }
        private void UpdateGeneratedItemInfo()
        {
            //Set all the String in the item panel to the item's attributes
            IDString.Content = m_gen_item.ID;
            LevelString.Content = m_gen_item.Level;
            RarityString.Content = m_gen_item.Rarity.ToString("##0.#0") + " %";
            NameString.Content = m_gen_item.Name;
            ClassIndexString.Content = m_gen_item.ClassIndex;
            ValueString.Content = m_gen_item.Value.ToString("##0.#0") + " %";
            StrengthString.Content = m_gen_item.Strength;
            DexterityString.Content = m_gen_item.Dexterity;
            IntelligenceString.Content = m_gen_item.Intelligence;
            VitalityString.Content = m_gen_item.Vitality;

            //Modify color of the rarity string to match item's rarity
            if (m_gen_item.Rarity > 90.0f)
            {
                RarityString.Foreground = LegendaryRadioButton.Foreground;
            }
            else if (m_gen_item.Rarity > 75.0f)
            {
                RarityString.Foreground = RareRadioButton.Foreground;
            }
            else if (m_gen_item.Rarity > 50.0f)
            {
                RarityString.Foreground = MagicRadioButton.Foreground;
            }
            else
            {
                RarityString.Foreground = NormalRadioButton.Foreground;
            }
        }

        #endregion


        #region Global Controls

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void ItemGenerator_SharpItemLib_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            m_isClosing = true;
        }

        #endregion


        #region Inventory Grid Controls

        private void EquipItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (InventoryListBox.SelectedIndex != -1)
            {
                //finds the item based on the ID parsed out of the list box's selected item string
                IItem t_item = m_character.getInventory.GetItem(Int32.Parse(Regex.Match(InventoryListBox.Items[InventoryListBox.SelectedIndex].ToString(), @"\d+$").Value));

                //equip the item
                m_character.Equip(t_item);

                //remove the item from inventory
                //m_character.getInventory.Remove(t_item.getID);
                InventoryListBox.Items.Remove(t_item.Name + " " + t_item.ID);

                //reset the selected index to the top item
                InventoryListBox.SelectedIndex = 0;

                //update Character Grid information
                UpdateCharacterEquippedItemInfo();

                UpdateInventoryListBox();
                UpdateCharacterIDListBox();
            }
        }
        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (m_gen_item != null)
            {
                m_character.getInventory.Add(m_gen_item);
                //InventoryListBox.Items.Add(m_gen_item.getName + " " + m_gen_item.getID);
                UpdateInventoryListBox();
            }
            GenerateItemButton_Click(sender, e);
        }
        private void UpdateInventoryListBox()
        {
            InventoryListBox.Items.Clear();

            foreach (IItem t_item in m_character.getInventory.getInventoryItemList)
            {
                if (t_item != null)
                {
                    InventoryListBox.Items.Add(t_item.Name + " " + t_item.ID);
                }
            }
        }
        private void RemoveItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (InventoryListBox.SelectedIndex != -1)
            {
                //finds the item based on the ID parsed out of the list box's selected item string
                IItem t_item = m_character.getInventory.GetItem(Int32.Parse(Regex.Match(InventoryListBox.Items[InventoryListBox.SelectedIndex].ToString(), @"\d+$").Value));

                //remove the item from inventory
                m_character.getInventory.Remove(t_item.ID);
                InventoryListBox.Items.Remove(t_item.Name + " " + t_item.ID);

                //reset the selected index to the top item
                InventoryListBox.SelectedIndex = 0;
            }
        }
        private void InventoryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InventoryListBox.SelectedIndex != -1)
            {
                IItem t_item = m_character.getInventory.GetItem(Int32.Parse(Regex.Match(InventoryListBox.Items[InventoryListBox.SelectedIndex].ToString(), @"\d+$").Value));

                if (t_item != null)
                {
                    UpdateSelectedInventoryItemInfo(t_item);
                }
            }
        }
        private void UpdateSelectedInventoryItemInfo(IItem t_item)
        {
            //Set all the String in the item panel to the item's attributes
            InventoryIDString.Content = t_item.ID;
            InventoryLevelString.Content = t_item.Level;
            InventoryRarityString.Content = t_item.Rarity.ToString("##0.#0") + " %";
            InventoryNameString.Content = t_item.Name;
            InventoryClassIndexString.Content = t_item.ClassIndex;
            InventoryValueString.Content = t_item.Value.ToString("##0.#0") + " %";
            InventoryStrengthString.Content = t_item.Strength;
            InventoryDexterityString.Content = t_item.Dexterity;
            InventoryIntelligenceString.Content = t_item.Intelligence;
            InventoryVitalityString.Content = t_item.Vitality;

            //Modify color of the rarity string to match item's rarity
            if (t_item.Rarity > 90.0f)
            {
                InventoryRarityString.Foreground = LegendaryRadioButton.Foreground;
            }
            else if (t_item.Rarity > 75.0f)
            {
                InventoryRarityString.Foreground = RareRadioButton.Foreground;
            }
            else if (t_item.Rarity > 50.0f)
            {
                InventoryRarityString.Foreground = MagicRadioButton.Foreground;
            }
            else
            {
                InventoryRarityString.Foreground = NormalRadioButton.Foreground;
            }
        }

        #endregion



        #region Character Grid Controls
        
        private void Unequip_Item_Click(object sender, RoutedEventArgs e)
        {
            UpdateCharacterEquippedItemInfo();
            UpdateInventoryListBox();
            UpdateCharacterIDListBox();
        }

        private void UpdateCharacterIDListBox()
        {
            CharacterIDListBox.Items.Clear();
            CharacterIDListBox.FontSize = 14.2;

            foreach (IItem t_item in m_character.Equipped)
            {
                //if (t_item != null)
                {
                    CharacterIDListBox.Items.Add(t_item.ID);
                }
            }
        }

        private void UpdateCharacterEquippedItemInfo()
        {
            UpdateCharacterHeadItemInfo();
            UpdateCharacterShouldersItemInfo();
            UpdateCharacterChestItemInfo();
            UpdateCharacterBracersItemInfo();
            UpdateCharacterGlovesItemInfo();
            UpdateCharacterPantsItemInfo();
            UpdateCharacterBootsItemInfo();
            UpdateCharacterNeckItemInfo();
            UpdateCharacterRing1ItemInfo();
            UpdateCharacterRing2ItemInfo();
            UpdateCharacterOffHandItemInfo();
            UpdateCharacterWeaponItemInfo();

            UpdateCharacterStatsInfo();
        }
        private void UpdateCharacterStatsInfo()
        {
            CharacterLevelString.Content = m_character.Level.ToString();
            CharacterPowerString.Content = m_character.getPower.ToString();
            CharacterAgilityString.Content = m_character.getAgility.ToString();
            CharacterDefenseString.Content = m_character.getDefense.ToString();
            CharacterHitPointsString.Content = m_character.getHitpoints.ToString();
        }
        private void UpdateCharacterHeadItemInfo()
        {
            CharacterHeadRarityBlock.Content = (m_character.Equipped[0].Rarity.ToString("#0.0") + " %");
            CharacterHeadValueBlock.Content = (m_character.Equipped[0].Value.ToString("#0.0") + " %");
            CharacterHeadDamageBlock.Content = "";
            CharacterHeadArmorBlock.Content = (((Helm)m_character.Equipped[0]).Defense.ToString("#0.0") + " %");
            CharacterHeadStrBlock.Content = m_character.Equipped[0].Strength;
            CharacterHeadDexBlock.Content = m_character.Equipped[0].Dexterity;
            CharacterHeadIntBlock.Content = m_character.Equipped[0].Intelligence;
            CharacterHeadVitBlock.Content = m_character.Equipped[0].Vitality;
            CharacterHeadLevelBlock.Content = m_character.Equipped[0].Level;
        }
        private void UpdateCharacterShouldersItemInfo()
        {
            CharacterShouldersRarityBlock.Content = (m_character.Equipped[1].Rarity.ToString("#0.0") + " %");
            CharacterShouldersValueBlock.Content = (m_character.Equipped[1].Value.ToString("#0.0") + " %");
            CharacterShouldersDamageBlock.Content = "";
            CharacterShouldersArmorBlock.Content = (((Shoulders)m_character.Equipped[1]).Defense.ToString("#0.0") + " %");
            CharacterShouldersStrBlock.Content = m_character.Equipped[1].Strength;
            CharacterShouldersDexBlock.Content = m_character.Equipped[1].Dexterity;
            CharacterShouldersIntBlock.Content = m_character.Equipped[1].Intelligence;
            CharacterShouldersVitBlock.Content = m_character.Equipped[1].Vitality;
            CharacterShouldersLevelBlock.Content = m_character.Equipped[1].Level;
        }
        private void UpdateCharacterChestItemInfo()
        {
            CharacterChestRarityBlock.Content = (m_character.Equipped[2].Rarity.ToString("#0.0") + " %");
            CharacterChestValueBlock.Content = (m_character.Equipped[2].Value.ToString("#0.0") + " %");
            CharacterChestDamageBlock.Content = "";
            CharacterChestArmorBlock.Content = (((Chest)m_character.Equipped[2]).Defense.ToString("#0.0") + " %");
            CharacterChestStrBlock.Content = m_character.Equipped[2].Strength;
            CharacterChestDexBlock.Content = m_character.Equipped[2].Dexterity;
            CharacterChestIntBlock.Content = m_character.Equipped[2].Intelligence;
            CharacterChestVitBlock.Content = m_character.Equipped[2].Vitality;
            CharacterChestLevelBlock.Content = m_character.Equipped[2].Level;
        }
        private void UpdateCharacterBracersItemInfo()
        {
            CharacterBracersRarityBlock.Content = (m_character.Equipped[3].Rarity.ToString("#0.0") + " %");
            CharacterBracersValueBlock.Content = (m_character.Equipped[3].Value.ToString("#0.0") + " %");
            CharacterBracersDamageBlock.Content = "";
            CharacterBracersArmorBlock.Content = (((Bracers)m_character.Equipped[3]).Defense.ToString("#0.0") + " %");
            CharacterBracersStrBlock.Content = m_character.Equipped[3].Strength;
            CharacterBracersDexBlock.Content = m_character.Equipped[3].Dexterity;
            CharacterBracersIntBlock.Content = m_character.Equipped[3].Intelligence;
            CharacterBracersVitBlock.Content = m_character.Equipped[3].Vitality;
            CharacterBracersLevelBlock.Content = m_character.Equipped[3].Level;
        }
        private void UpdateCharacterGlovesItemInfo()
        {
            CharacterGlovesRarityBlock.Content = (m_character.Equipped[4].Rarity.ToString("#0.0") + " %");
            CharacterGlovesValueBlock.Content = (m_character.Equipped[4].Value.ToString("#0.0") + " %");
            CharacterGlovesDamageBlock.Content = "";
            CharacterGlovesArmorBlock.Content = (((Gloves)m_character.Equipped[4]).Defense.ToString("#0.0") + " %");
            CharacterGlovesStrBlock.Content = m_character.Equipped[4].Strength;
            CharacterGlovesDexBlock.Content = m_character.Equipped[4].Dexterity;
            CharacterGlovesIntBlock.Content = m_character.Equipped[4].Intelligence;
            CharacterGlovesVitBlock.Content = m_character.Equipped[4].Vitality;
            CharacterGlovesLevelBlock.Content = m_character.Equipped[4].Level;
        }
        private void UpdateCharacterPantsItemInfo()
        {
            CharacterPantsRarityBlock.Content = (m_character.Equipped[5].Rarity.ToString("#0.0") + " %");
            CharacterPantsValueBlock.Content = (m_character.Equipped[5].Value.ToString("#0.0") + " %");
            CharacterPantsDamageBlock.Content = "";
            CharacterPantsArmorBlock.Content = (((Pants)m_character.Equipped[5]).Defense.ToString("#0.0") + " %");
            CharacterPantsStrBlock.Content = m_character.Equipped[5].Strength;
            CharacterPantsDexBlock.Content = m_character.Equipped[5].Dexterity;
            CharacterPantsIntBlock.Content = m_character.Equipped[5].Intelligence;
            CharacterPantsVitBlock.Content = m_character.Equipped[5].Vitality;
            CharacterPantsLevelBlock.Content = m_character.Equipped[5].Level;
        }
        private void UpdateCharacterBootsItemInfo()
        {
            CharacterBootsRarityBlock.Content = (m_character.Equipped[6].Rarity.ToString("#0.0") + " %");
            CharacterBootsValueBlock.Content = (m_character.Equipped[6].Value.ToString("#0.0") + " %");
            CharacterBootsDamageBlock.Content = "";
            CharacterBootsArmorBlock.Content = (((Boots)m_character.Equipped[6]).Defense.ToString("#0.0") + " %");
            CharacterBootsStrBlock.Content = m_character.Equipped[6].Strength;
            CharacterBootsDexBlock.Content = m_character.Equipped[6].Dexterity;
            CharacterBootsIntBlock.Content = m_character.Equipped[6].Intelligence;
            CharacterBootsVitBlock.Content = m_character.Equipped[6].Vitality;
            CharacterBootsLevelBlock.Content = m_character.Equipped[6].Level;
        }
        private void UpdateCharacterNeckItemInfo()
        {
            CharacterNeckRarityBlock.Content = (m_character.Equipped[7].Rarity.ToString("#0.0") + " %");
            CharacterNeckValueBlock.Content = (m_character.Equipped[7].Value.ToString("#0.0") + " %");
            CharacterNeckDamageBlock.Content = "";
            CharacterNeckArmorBlock.Content = (((Neck)m_character.Equipped[7]).Defense.ToString("#0.0") + " %");
            CharacterNeckStrBlock.Content = m_character.Equipped[7].Strength;
            CharacterNeckDexBlock.Content = m_character.Equipped[7].Dexterity;
            CharacterNeckIntBlock.Content = m_character.Equipped[7].Intelligence;
            CharacterNeckVitBlock.Content = m_character.Equipped[7].Vitality;
            CharacterNeckLevelBlock.Content = m_character.Equipped[7].Level;
        }
        private void UpdateCharacterRing1ItemInfo()
        {
            CharacterRing1RarityBlock.Content = (m_character.Equipped[8].Rarity.ToString("#0.0") + " %");
            CharacterRing1ValueBlock.Content = (m_character.Equipped[8].Value.ToString("#0.0") + " %");
            CharacterRing1DamageBlock.Content = "";
            CharacterRing1ArmorBlock.Content = (((Ring)m_character.Equipped[8]).Defense.ToString("#0.0") + " %");
            CharacterRing1StrBlock.Content = m_character.Equipped[8].Strength;
            CharacterRing1DexBlock.Content = m_character.Equipped[8].Dexterity;
            CharacterRing1IntBlock.Content = m_character.Equipped[8].Intelligence;
            CharacterRing1VitBlock.Content = m_character.Equipped[8].Vitality;
            CharacterRing1LevelBlock.Content = m_character.Equipped[8].Level;
        }
        private void UpdateCharacterRing2ItemInfo()
        {
            CharacterRing2RarityBlock.Content = (m_character.Equipped[9].Rarity.ToString("#0.0") + " %");
            CharacterRing2ValueBlock.Content = (m_character.Equipped[9].Value.ToString("#0.0") + " %");
            CharacterRing2DamageBlock.Content = "";
            CharacterRing2ArmorBlock.Content = (((Ring)m_character.Equipped[9]).Defense.ToString("#0.0") + " %");
            CharacterRing2StrBlock.Content = m_character.Equipped[9].Strength;
            CharacterRing2DexBlock.Content = m_character.Equipped[9].Dexterity;
            CharacterRing2IntBlock.Content = m_character.Equipped[9].Intelligence;
            CharacterRing2VitBlock.Content = m_character.Equipped[9].Vitality;
            CharacterRing2LevelBlock.Content = m_character.Equipped[9].Level;
        }
        private void UpdateCharacterOffHandItemInfo()
        {
            CharacterOffHandRarityBlock.Content = (m_character.Equipped[10].Rarity.ToString("#0.0") + " %");
            CharacterOffHandValueBlock.Content = (m_character.Equipped[10].Value.ToString("#0.0") + " %");
            CharacterOffHandDamageBlock.Content = "";
            CharacterOffHandArmorBlock.Content = (((OffHand)m_character.Equipped[10]).Defense.ToString("#0.0") + " %");
            CharacterOffHandStrBlock.Content = m_character.Equipped[10].Strength;
            CharacterOffHandDexBlock.Content = m_character.Equipped[10].Dexterity;
            CharacterOffHandIntBlock.Content = m_character.Equipped[10].Intelligence;
            CharacterOffHandVitBlock.Content = m_character.Equipped[10].Vitality;
            CharacterOffHandLevelBlock.Content = m_character.Equipped[10].Level;
        }
        private void UpdateCharacterWeaponItemInfo()
        {
            CharacterWeaponRarityBlock.Content = (m_character.Equipped[11].Rarity.ToString("#0.0") + " %");
            CharacterWeaponValueBlock.Content = (m_character.Equipped[11].Value.ToString("#0.0") + " %");
            CharacterWeaponDamageBlock.Content = (((Weapon)m_character.Equipped[11]).Damage.ToString("#0.0") + " %");
            CharacterWeaponArmorBlock.Content = "";
            CharacterWeaponStrBlock.Content = m_character.Equipped[11].Strength;
            CharacterWeaponDexBlock.Content = m_character.Equipped[11].Dexterity;
            CharacterWeaponIntBlock.Content = m_character.Equipped[11].Intelligence;
            CharacterWeaponVitBlock.Content = m_character.Equipped[11].Vitality;
            CharacterWeaponLevelBlock.Content = m_character.Equipped[11].Level;
        }

        #endregion
        
    }
}
