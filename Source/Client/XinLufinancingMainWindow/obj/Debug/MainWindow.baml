   M S B A M L   `   `   `  ����D  @FoxBaseUi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullL4clr-namespace:FoxBaseUi.ControlEx;assembly=FoxBaseUiFoxBaseUi.ControlEx  5   	   S OXinLufinancingMainWindow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullY1clr-namespace:XinLufinancingMainWindow.ViewModels#XinLufinancingMainWindow.ViewModels 5   	   Q-clr-namespace:XinLufinancingMainWindow.ModelsXinLufinancingMainWindow.Models 5   	   )   #XinLufinancingMainWindow.MainWindow   5   	   B	ControlEx4clr-namespace:FoxBaseUi.ControlEx;assembly=FoxBaseUi  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @VM1clr-namespace:XinLufinancingMainWindow.ViewModels     5   	   ?Model-clr-namespace:XinLufinancingMainWindow.Models     5   	     � Title$  
MainWindow��5   	   $	��900��6   $
��1920��6)    � 
ResizeMode$ NoResize=�66    � WindowStartupLocation$ Manual=�6L    � WindowStyle$
 None=�6k    � 	Resources 5       �� DataType3  -XinLufinancingMainWindow.Models.FirstLevelBtn%   FoxBaseUi.ControlEx.ImageButton   CornerRadius   MouseOverBackground   MousePressedBackground	   
ButtonType
   BtnSelectedBackground   Icon   IsBtnSelected �� Mode4  .XinLufinancingMainWindow.Models.SecondLevelBtn��%�  (��       5	      *, +)(�� �    5      *, +)�� 5	   
   "  6   .R�5
        $��Stretch=�6$   $	��2,4q�6@   $ 1��6M    �	#7F797979 �	#7F1B1B1C
�������$	 TopIcon=�5   $   $��14(�6O   
 �	#FF797979$��Stretch=�5   k   $��2q�5   $   $��Center=�60   $��Center=�6T   ��5   Z   �� *BtnModel.Content+ 5   9   �� *Icon+ 6]   �� *BtnModel.IsSelected+��5   F   �� *BtnModel.Command+��5   $   �� $ OneWay=�5      �� 5   
   "  6   .R�5        $��Center=�6$   $
��2,10q�6?   $ 1��6M    �	#CCF0F0F0 �	#7F1B1B1C���	#FFB8B8B8$	 LeftIcon=�5   $   $��14(�6:   
 �	#FFE6E6E6$3 -pack://siteoforigin:,,,/Resources/sanjiao.png��5   $   $��Stretch=�5   $   $��32��6B   ��5   ^   �� *BtnModel.Content+ 5   H   �� *BtnModel.IsSelected+��5   F   �� *BtnModel.Command+��5   Z   �� $ OneWay=�5   B   5      5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��80 �6   �� 5      $	��160 �6   �� 5      $��* �6    � RowDefinitions 5!   
   �� 5"      $��80 �6   �� 5#      $��* �6   .+�5%   
   � $��3��6   ��5&      �� 5'       �� ImageSource$2 ,pack://siteoforigin:,,,/Resources/top_bg.png��6    j� Stretch$ UniformToFill=�6X   .+�5)      �� $
��Left=�6   $��65,4,0,4q�6/   $	��509��6A   $1��+pack://siteoforigin:,,,/Resources/title.png��6M   $��UniformToFill=�6�    �� StretchDirection$ DownOnly=�6�     5*      -   5        $��退出系统��5*   $   $��Right=�63   $��0,0,30,0q�6O   $��Center=�6a   $	 LeftIcon=�6|   $2 ,pack://siteoforigin:,,,/Resources/tuichu.png��6�   
������� �	#481F2F6E$ 2��6    5+      $��ImageButton��6$   $��Right=�6:   $��Center=�6V   $��	0,0,260,0q�6q   
�������$��Italic$�6�    �	#331F2F6E �	#4C1F2F6E6�   �� 5,      $!��，欢迎使用本系统！��6   $��Right=�6(   $��	0,0,150,0q�6D   $��Center=�6W   
�������$��0q�6�   5.      � 5/   
   $��1��6   ���	#FF575757.+�50      �� ��6   �� *FirstMenuBtns+��51      �� 52      .R�53      ��  �� Orientation$ Vertical=�6%   $��0,30,0,0q�6<   54      56      58      � 59   
   $��1��6   $	��2,4q�6   $��1��6,   ���	#FFF6F6F6 5:      �� 5;      $	��120 �6    �� 5<      $��* �6    .+�5>      � $��0q�6    5?      �� 5@      $��* �6$   �� 5A      $��* �6$   .+�5C      �� $
��Left=�6L   $��8,0,0,0q�6g   $��Bottom=�6x   $��24(�6�   ��6   �� *!SelectedFirstBtn.Intro.MenuName+6�   �� 5D      $��0q�6   $��1��6%    �� TextWrapping$
 Wrap=�62   ���	#FFE0E0E0���	#FFB8B8B8$��0,0,0,1q�5F       J� 	AllowDrop .   �� 
IsReadOnly . {�.  ��5D   F   �� *" SelectedFirstBtn.Intro.MenuIntro+ �� SelectionBrush 5E      Z� ��64   Z� 5F   X   5H      � 5I      $��1��6   .+�5J      �� ��6   �� *" SelectedFirstBtn.SecondLevelBtns+��5K      �� 5L      .R�5M      �� $ Vertical=�6)   $��0,10,0,0q�6@   5N      5P      5Q      5S      � 5T   
   -   5        $��ctlPanel��5T   >   $��2��6   $��0,10,0,0q�6   $��1��61   6>   � 5U   
   $��2��6   $��3��6    ���	#7F000000y�6K   �� *MsgBoxVisibility+.+�5W      � $��2��6   $��Center=�6'   $��0q�6D   $��2��6O   $��Center=�6`    5X      �� 5Y      $
��Auto �6$   �� 5Z       5\      �� 5]      �� 5^      $
��Auto �6'   .+�5`        $��40��6(   $��Right=�63   $	��Top=�6O   $��40��6g   $2 ,pack://siteoforigin:,,,/Resources/guanbi.png��5a   (   $	 OnlyIcon=�6\    �	#99000000$ 30��5b   (   
�������$��1q�6N   $��1��6b   ��6r   �� *CloseMsgBoxCmd+�� 5c      -   5         ��Name$ messageBorder��5c   7   $��1��6   $��0,5,5,0q�6&    �� CornerRadius$ 4��6N   5d      5e      5f      5g      