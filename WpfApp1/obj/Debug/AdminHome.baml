   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5          WpfApp1.AdminHome   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:WpfApp1     5      -   5          N� Title$  	AdminHome��5      $	��534��5	      $	��708��6)   .��5      �  � ColumnDefinitions 5   
   �� 5      $��20 �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $��20 �6    � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $��20 �6   .+�5   
   �� -   5        $��WelcomeLabel��5   A   $��2��6   $��2��6    $��	Welcome: ��6-   6A   �� 5    
   $��3��6   $��2��6    $��Groups: ��6-   6B   �� 5"   
   -   5         ��Name$ messageListbox��5"   @   $��2��6   $��3��6"   $��2��6/   $	��400��6X   $	��500��6i   $��	0 0 12 12q�6x   ��5#      �� 5$       �� 
TargetType" ��6   .�5%      ��  �� Property � �� �� Value$
 Left=�6C   �� 5&       � ��$ Center=�6A   5'      5)      �� 5*   
   -   5        $��GroupListBox��5*   0   $��3��6   $��3��6"   $	��400��6E   $	��150��6U   $��0 0 0 12q�6d   ��5+      �� 5,      " ��6   .�5-      ��  � ��$
 Left=�6C   �� 5.       � ��$ Center=�6A   5/      51      �� 53   
   -   5        $��AdminPageBtn��53   @   $��3��6   $��4��6!   $��0 0 0 12q�6.   $��	AdminPage��6U   6i   �� 55   
   -   5        $��ChatInputTextBox��55   /   $��2��6   $��5��6"   $��0 0 12 0q�6G   6\   �� 56   
   -   5        $��
SendButton��56   .   $��3��6   $��5��6!   $
��Send��6A   6P   58      5:      