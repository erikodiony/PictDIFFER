   M S B A M L   `   `   `  ����   
PictDIFFER*    $PictDIFFER.Views.Home.GettingStarted   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Z VFirstFloor.ModernUI, Version=1.0.9.0, Culture=neutral, PublicKeyToken=bc9b0c37bf06c6a90mui&http://firstfloorsoftware.com/ModernUI  .���    ContentRoot#��[  .+��� .+��   � RowDefinitions  �� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��4��$��0,0,0,0q�
�������$
��Auto��$
��Auto��$��2�� �� StrokeDashArray$	 4 4R��� $��0��$��0��$	��200��$	��200��$+��%/PictDIFFER;component/Images/Logo.png��$��UniformToFill=��� $��0��$��1��$	��200��$	��200��$+��%/PictDIFFER;component/Images/Logo.png��$��UniformToFill=��� $��0��$��2��$	��200��$	��200��$+��%/PictDIFFER;component/Images/Logo.png��$��UniformToFill=��� $��0��$��3��$	��200��$	��200��$+��%/PictDIFFER;component/Images/Logo.png��$��UniformToFill=��  �� $
��Auto ��� $
��Auto ��� $��* �.+��� $��0��.+��� $	����$��
15,20,15,0q�$��Segoe MDL2 Assets)�$��Normal"�$��30(��� $��1��$��
15,20,0,20q�$��3��$
��Auto��
��������� $��2��$��15,20,25,20q�$	��275��$��Disabled=�$
��Auto=�.���� .+��� $��
15,0,15,10q�$��18(�$
��Wrap=�OMTerdapat 3 + 1 Fitur yang menjadi fungsi dasar dari aplikasi ini antara lain �� $��SemiBold"�.�	Histogram, �� $��SemiBold"�.�Pixel Lookup, �� $��SemiBold"�.�
MSE & PSNR dan �� $��SemiBold"�.�File Checksum Verify.�� $��3��$��75��$��150,10,150,10q�
�������� $��	15,0,15,0q�  �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��$��30(��� $	���$��Segoe MDL2 Assets)� �� $�	Histogram���� $��1��$��18(�$
��Wrap=���Merupakan Fitur yang berfungsi untuk melihat komposisi warna sesuai dengan warna dasar dari Citra Digital berkedalaman 32bit, yakni (BGRA / Blue, Green, Red, Alpha)�� $��2��$��3��$	��150��$��150,25,150,10q�
��������� $��3��$��30(��� $	���$��Segoe MDL2 Assets)� �� $�Pixel Lookup���� $��4��$��18(�$
��Wrap=���Merupakan Fitur yang berfungsi untuk melihat komposisi warna dasar dari Citra Digital berkedalaman 32bit, yakni (BGRA / Blue, Green, Red, Alpha) pada titik-titik Pixel tertentu.�� $��5��$��3��$	��150��$��150,25,150,10q�
��������� $��6��$��30(��� $	���$��Segoe MDL2 Assets)� �� $�
MSE & PSNR���� $��7��$��18(�$
��Wrap=�MSE �� $��SemiBold"�$��Italic$�$�(Mean Square Error)��[Y adalah nilai error kuadrat rata-rata antara Citra Digital hasil Pengolahan Steganografi �� $��SemiBold"�$��Italic$�$�(Stego Image)�� dengan Citra Digital asli �� $��SemiBold"�$��Italic$�$�(Cover Image)��. Adapun PSNR �� $��SemiBold"�$��Italic$�$�(Peak Signal Noise Ratio)���� adalah perbandingan antara nilai maksimum dari sinyal yang diukur dengan besarnya derau yang berpengaruh pada sinyal tersebut. PSNR biasanya diukur dalam satuan �� $��SemiBold"�$��Italic$�$�Decibel / (db)����. MSE & PSNR biasa digunakan untuk mengetahui hasil/kualitas teknik Steganografi yang digunakan. Dengan parameter yang digunakan yakni selisih antara nilai �� $��SemiBold"�$��Italic$�$�Pixel��! Citra Digital asli sebelumnya �� $��SemiBold"�$��Italic$�$�(Cover Image)��$" dan Citra Digital sesudah diolah �� $��SemiBold"�$��Italic$�$�(Stego Image)��.�� $��8��$��3��$	��150��$��150,25,150,10q�
��������� $��9��$��30(��� $	���$��Segoe MDL2 Assets)� �� $�File Checksum Verify���� $��10��$��18(�$
��Wrap=�OMMerupakan Fitur yang berfungsi untuk mengecek maupun membandingkan file asli �� $��SemiBold"�$��Italic$�$�(Original File)��+) dengan file hasil ekstraksi Stego Image �� $��SemiBold"�$��Italic$�$�(Embedded File)��97. Adapun proses membandingkannya menggunakan algoritma �� $��SemiBold"�$��Italic$�$	�MD5��	, dimana �� $��SemiBold"�$��Italic$�$	�MD5��+) sendiri berfungsi sebagai pencari nilai �� $��SemiBold"�$��Italic$�$
�Hash��nl suatu file. Sedangkan manfaat fitur ini adalah untuk menegaskan bahwa dalam hal ekstraksi file Stego Image �� $��SemiBold"�$��Italic$�$�(Embedded File)��CA apakah telah terjadi perubahan struktur file terhadap file asli �� $��SemiBold"�$��Italic$�$�(Original File)�� ataupun tidak.