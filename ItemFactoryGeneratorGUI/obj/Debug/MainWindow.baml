   M S B A M L   `   `   `  ����R  NItemFactoryGeneratorGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:ItemFactoryGeneratorGUIItemFactoryGeneratorGUI  5   	   (    "ItemFactoryGeneratorGUI.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   7local%clr-namespace:ItemFactoryGeneratorGUI     5   	     � Title$  
MainWindow��5   	   $	��593��6   $	��843��6)   $	��843��65   $	��300��6D   .��5      � .+�5   
   � -   5         �Name$ ItemDisplayGrid��5      $��	0,0,0,133q�6(   ���	#FFB8B8B8.+�5      +   %ItemFactoryGeneratorGUI.SharpItemInfo  -   5          Name$ ItemDisplayInfo��5   "   $
��Auto��6;   $
��Auto��6H   $��0,0,0,10q�6V   5	      � 5
   
   -   5        $ 
ButtonGrid��5
      $	��133��6#   $��Bottom=�60   ���	#FF636363.+�5      �� -   5         ��Name$ MakeItemButton��5      $��	Make Item��6-   $��	0,10,10,0q�6A   $	��Top=�6T   $��34��6l   $��Right=�6x   $��95��6�   6�   �� 5      -   5         ��Name$ ItemNameTextBox��5      $��23��6/   $��	10,27,0,0q�6;    �� TextWrapping$
 Wrap=�6N   $��	Item Name��6b   $	��Top=�6s   $
��Left=�6�   $	��148��6�   �� 5      -   5         ��Name$ ItemNameLabel��5      $��
Item Name:��6+   $
��Left=�6@   $��10,1,0,0q�6[   $	��Top=�6m   �� 5      -   5         ��Name$ TypeListBox��5      $
��Left=�6+   $	��117��6F   $��	236,6,0,0q�6S   $	��Top=�6f   $	��100��6~    �� SelectedIndex$ 0��6�   .@�5      �� $��OneHand��6   �� 5      $��TwoHand��6   �� 5      $
��Helm��6   �� 5      $��	Shoulders��6   �� 5      $��Chest��6   �� 5      $��Bracers��6   �� 5      $��Gloves��6   �� 5      $��Pants��6   �� 5      $��Boots��6   �� 5      $
��Neck��6   �� 5      $
��Ring��6   �� 5      $��Shield��6   �� 5      $��Source��6   5      �� 5      -   5        $ ItemType��5      $��
Item Type:��6&   $
��Left=�6;   $��	166,2,0,0q�6V   $	��Top=�6i   �� 5      -	   5        $ 	BeingType��5      $��Being Type:��6'   $
��Left=�6=   $��	349,2,0,0q�6X   $	��Top=�6k   �� 5      -
   5        $ BeingTypeListBox��5      $
��Left=�60   $	��117��6K   $��	425,6,0,0q�6X   $	��Top=�6k   $	��100��6�   $ 0��6�   .@�5       �� $��	BlackMage��6   �� 5!      $��	WhiteMage��6   �� 5"      $��Warrior��6   �� 5#      $��Rogue��6   5$      �� 5%      -   5        $ ItemLevelTextBox��5%      $��23��60   $��	10,81,0,0q�6<   $
 Wrap=�6O   $��
Item Level��6c   $	��Top=�6u   $
��Left=�6�   $	��148��6�   �� 5&      -   5        $ 
LevelLabel��5&      $��Item Level:��6(   $
��Left=�6>   $��	10,55,0,0q�6Y   $	��Top=�6l   5'      5(      5)      