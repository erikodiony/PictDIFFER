   M S B A M L   `   `   `  ����   
PictDIFFER/    )PictDIFFER.Views.QualityCheck.PixelLookup   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Z VFirstFloor.ModernUI, Version=1.0.9.0, Culture=neutral, PublicKeyToken=bc9b0c37bf06c6a90mui&http://firstfloorsoftware.com/ModernUI  .���    ContentRoot#��[  .+��� .+��   � ColumnDefinitions  �� $	��160 ��� $
��Auto ��� $
��Auto ��� $	��160 ��� $
��Auto ��� $
��Auto � � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��6��$��3��$��0,0,0,0q�
�������$
��Auto��$
��Auto��$��2�� �� StrokeDashArray$	 4 4R��� $��0��$��0��$��	18,5,10,5q�$��Choose Cover Image :��$��SemiBold"��� -   $��pixel_btn_cover��$��0��$��1��$��	18,0,10,0q�$��30��$	��110��$��Cover Image��$
��Left=�$	��Top=��� $��1��$��1��$��2��$	��185��$	��185��$��20,-15,20,10q�$��1q�
���   �
�������._��� -   $��pixel_img_cover���� $��2��$��3��$��10,10,15,10q�$��3��
��������� $��3��$��0��$��	10,5,10,5q�$��Choose Stego Image :��$��SemiBold"��� -   $��pixel_btn_stego��$��3��$��1��$��	10,0,10,0q�$��30��$	��110��$��Stego Image��$
��Left=�$	��Top=��� $��4��$��1��$��2��$	��185��$	��185��$��20,-15,20,10q�$��1q�
���   �
�������._��� -   $��pixel_img_stego��� $	��265��  �� $
��Auto ��� $
��Auto ��� $��* �.+��� $��0��.+��� $	����$��
15,20,15,0q�$��Segoe MDL2 Assets)�$��Normal"�$��30(��� $��1��$��
15,20,0,20q�$��3��$
��Auto��
��������� $��2��$��15,20,25,20q�$��Disabled=�$
��Auto=�.���� .+�� $��4q�  �� $	��345 ��� $	��345 � �� $
��Auto ��� $	��140 ��� $
��Auto ��� $
��Auto �.+��� -   $��	txt_cover��$��0��$��0��$��Cover Image��  Heading2#��[ �� $��3q�$��0��$��1��$��1q�
���   �._�&   System.Windows.Controls.DataGrid  -   $��dgrid_cover��   AutoGenerateColumns .     
IsReadOnly .    EnableRowVirtualization .     CanUserResizeRows .     SelectionMode$ Single=�   HorizontalGridLinesBrush
 �����	   VerticalGridLinesBrush
	 �����
   GridLinesVisibility$	
 All=�   CanUserReorderColumns .     CanUserResizeColumns .     CanUserSortColumns .     RowDetailsVisibilityMode$ 	Collapsed=�   ClipboardCopyMode$
 None=�$��Center=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ (X,Y)��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 80    CanUserResize .     CanUserSort .  1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *PointXY_Cover+  $ B��$ 55  .   .   �� *	B_Cover+  $ G��$ 55  .   .   �� *	G_Cover+  $ R��$ 55  .   .   �� *	R_Cover+  $ A��$ 55  .   .   �� *	A_Cover+�� $��2��$��2��$��2q� �� Orientation$ 
Horizontal=�.+��� -   $��infoDisplayData_cover��$��1q�$%��Total of Data (Cover Image) : -���� $��	20,1,20,1q�$��|���� -   $��infoDisplayData_stego��$��1q�$%��Total of Data (Stego Image) : -���� $��3��$��2��$��2q�$ 
Horizontal=�.+��� $��1q�$��Show of Data ��$��SemiBold"�$��16(��� -	   $��txt_displayData��$��1q�$��25��$��35��$��80��$
��Auto���� -
   $��btn_displayData��$��1q�$��Execute���� $��	20,1,20,1q�$��|��$��SemiBold"�$��16(��� $��1q�$��Point X ��$��SemiBold"�$��16(��� -   $��txt_displayData_X��$��1q�$��1��$��35��$��80��$
��Auto���� $��1q�$��Point Y ��$��SemiBold"�$��16(��� -   $��txt_displayData_Y��$��1q�$��1��$��35��$��80��$
��Auto���� -   $��btn_displayData_Point��$��1q�$��Search���� -   $��	txt_stego��$��1��$��0��$��Stego Image��#��[ �� $��3q�$��1��$��1��$��1q�
���   �._�  -   $��dgrid_stego�� .   .  .   .  $ Single=�
 �����
	 �����$	
 All=� .   .   .  $ 	Collapsed=�$
 None=�$��Center=�   $ (X,Y)��$ 80  .   .   �� *PointXY_Stego+  $ B��$ 55  .   .   �� *	B_Stego+  $ G��$ 55  .   .   �� *	G_Stego+  $ R��$ 55  .   .   �� *	R_Stego+  $ A��$ 55  .   .   �� *	A_Stego+�   �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��3��$��
0,-8,-8,-8q�
�������$
��Auto��$
��Auto��$��2��$	 4 4R��� $��10,0,0,0q�$��18(�$��SemiBold"�$D��>PictDIFFER (Check & Compare Quality of Picture) | PIXEL LOOKUP���� -   $��pixel_btn_ClearAll��$��	625,0,0,0q�$��0��$��0��$��	Clear All��$��Right=�$��Bottom=��� -   $��pixel_btn_Exec��$��10,0,0,0q�$��1��$��0��$��Execute��$��Right=�$��Bottom=�