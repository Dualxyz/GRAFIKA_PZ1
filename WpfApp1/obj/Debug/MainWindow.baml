   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	       WpfApp1.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	     � Title$  PZ1_RACUNARSKA_GRAFIKA��5   	   $	��750��6(   $	��750��65   .��5
      ]� -   5        
�������.+�5   
   o� -   5         o�Name$
 menu��5      $��25��6   $	��Top=�6)   .@�5      m� $��Add Objects...��6   $��15(�6/   .@�5      m� -   5         m�Name$ EllipseChecked��5      $��Draw Ellipse��63    m� IsCheckable . 6I   m� 5      -   5        $ TextChecked��5      $��Add Text��60    . 6B   m� 5      -   5        $ PolygonChecked��5      $��Draw Polygon��63    . 6I   5      5      �� 5   
   -   5         ��Name$ slider��5       � Minimum$	 0.3Q�6!    � Maximum$ 15Q�6/    � Value$ 1Q�6<   $	��Top=�6F    � 	Resources 5      ��%    6!   5      �� 5   
   $	��Top=�6   
��� ���	 �� Orientation$	 
Horizontal=�6?   $	��Top=�6X   $��10,10q�6p   $��50��6   .+�5      �� $	 
Horizontal=�6   
���iii�$
��10,0q�6G   .+�5      �� -   5        
 ��Name$
 
UndoButton��5      $
��Undo��6-   $��50��6O   
��� ���$��10,10q�6r    � Cursor$
 Hand��6�   �� 5      -   5        $
 ClearButton��5      $��Clear��6.   $��50��6R   
��� E��$��10,10q�6t   $
 Hand��6�   �� 5      -	   5        $
 
RedoButton��5      $
��Redo��6-   $��50��6O   
��� ���$��10,10q�6r   $
 Hand��6�   5      �� 5      -
   5        $
 
LoadButton��5      $��LOAD THE MAP��6)   $��30��6S   $	��470��6_   
����� �$��10,10q�6~   $
 Hand��6�   5      �� 5   
   $
��Auto=�6   $
��Auto=�6<   .��5      �� -   5         ��Name$ canvas��5      $��Bottom=�6%   $	��600��6=   $	��750��6J   
�������|�.  � LayoutTransform 5       �� 5!      -   5        SkaliranjeTransform�� �� ScaleX 5!   B   ��  �� ElementName$ slider�� �� Path$ Value� �� ScaleY 5"   !   �� $ slider��$ Value�5$      5%      5&      5'      