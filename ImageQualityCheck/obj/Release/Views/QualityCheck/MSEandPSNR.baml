   M S B A M L   `   `   `  ����   
PictDIFFER.    (PictDIFFER.Views.QualityCheck.MSEandPSNR   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Z VFirstFloor.ModernUI, Version=1.0.9.0, Culture=neutral, PublicKeyToken=bc9b0c37bf06c6a90mui&http://firstfloorsoftware.com/ModernUI  .���    ContentRoot#��[  .+��� .+��   � ColumnDefinitions  �� $	��160 ��� $
��Auto ��� $
��Auto ��� $	��160 ��� $
��Auto ��� $
��Auto � � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��6��$��3��$��0,0,0,0q�
�������$
��Auto��$
��Auto��$��2�� �� StrokeDashArray$	 4 4R��� $��0��$��0��$��	18,5,10,5q�$��Choose Cover Image :��$��SemiBold"��� -   $��MSE_btn_cover��$��0��$��1��$��	18,0,10,0q�$��30��$	��110��$��Cover Image��$
��Left=�$	��Top=��� $��1��$��1��$��2��$	��185��$	��185��$��20,-15,20,10q�$��1q�
���   �
�������._��� -   $��mse_img_cover���� $��2��$��3��$��10,10,15,10q�$��3��
��������� $��3��$��0��$��	10,5,10,5q�$��Choose Stego Image :��$��SemiBold"��� -   $��MSE_btn_stego��$��3��$��1��$��	10,0,10,0q�$��30��$	��110��$��Stego Image��$
��Left=�$	��Top=��� $��4��$��1��$��2��$	��185��$	��185��$��20,-15,20,10q�$��1q�
���   �
�������._��� -   $��mse_img_stego��� $	��265��  �� $
��Auto ��� $
��Auto ��� $��* �.+��� $��0��.+��� $	����$��
15,20,15,0q�$��Segoe MDL2 Assets)�$��Normal"�$��30(��� $��1��$��
15,20,0,20q�$��3��$
��Auto��
��������� $��2��$��15,20,25,20q�$	��275��$��Disabled=�$
��Auto=�.���� .+�� $��4q�  �� $	��345 ��� $	��345 � �� $
��Auto ��� $	��180 �.+��� -   $��	txt_cover��$��0��$��0��$��Cover Image��  Heading2#��[ �� -   $��	txt_stego��$��1��$��0��$��Stego Image��#��[ �� $��5q�$��2��$��1��$��
MSE & PSNR��$��1q�.���   �� $	��340 ��� $	��340 �.+��� $��0�� �� Orientation$ Vertical=�$��2q�.+��� -   $��mse_B��$��4q�$��MSE | Blue: -���� -   $��mse_G��$��4q�$��MSE | Green: -���� -	   $��mse_R��$��4q�$��MSE | Red: -���� -
   $��mse_A��$��4q�$��MSE | Alpha: -���� $��4q�
�������$��2���� -   $��mse_All��$��4q�$��MSE | All: -���� $��1��$ Vertical=�$��2q�.+��� -   $��psnr_B��$��4q�$��PSNR | Blue: -���� -   $��psnr_G��$��4q�$��PSNR | Green: -���� -   $��psnr_R��$��4q�$��PSNR | Red: -���� -   $��psnr_A��$��4q�$��PSNR | Alpha: -���� $��4q�
�������$��2���� -   $��psnr_All��$��4q�$��PSNR | All: -���   �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��3��$��
0,-8,-8,-8q�
�������$
��Auto��$
��Auto��$��2��$	 4 4R��� $��10,0,0,0q�$��18(�$��SemiBold"�$B��<PictDIFFER (Check & Compare Quality of Picture) | MSE & PSNR���� -   $��MSE_btn_ClearAll��$��	625,0,0,0q�$��0��$��0��$��	Clear All��$��Right=�$��Bottom=��� -   $��MSE_btn_Exec��$��10,0,0,0q�$��1��$��0��$��Execute��$��Right=�$��Bottom=�