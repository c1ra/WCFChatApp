   M S B A M L   `   `   `  ����F  BlocalClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:localClientlocalClient  5          localClient.LogInPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:localClient     5        N� Title$  	LogInPage��5	      .��5      �  � ColumnDefinitions 5   
   �� 5      $��20 �6   �� 5      $��* �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $��20 �6    � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $��* �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $��20 �6   .+�5   
   �� $��Welcome��6   $��2��6"   $��2��6/   $��2��6?   $��32(�6S   $��Center=�6a   #��� ��6~   $��Segoe Print)�6�   ���	#00000000 J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5       5"      �� 5#   
   $��
Username: ��6   $��2��6%   $��3��65   �� 5$   
   -   5         ��Name$ usrNameTextBox��5$      $��3��6*   $��3��6:   $	��120��6G   $��27��6V   $��0 0 0 12q�6e   �� 5&   
   $��
Password: ��6   $��2��6%   $��4��65   �� 5'   
   -   5        $ passTextBox��5'      $��3��6'   $��4��67   $	��120��6D   $��27��6U   $��0 0 0 12q�6d   �� 5)   
   -   5         ��Name$ 	LogInBttn��5)      $��Log In��6$   $��2��65   $��5��6E   $��2��6S   $��90��6g   6u   �� 5+   
   -   5        $��msgLabel��5+      $�� ��6   $��2��6+   $��6��6;   5-      5.      