   M S B A M L   `   `   `  ���� D  @XMLEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:XMLEditor.ViewModelXMLEditor.ViewModel      XMLEditor.SearchPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     3local!clr-namespace:XMLEditor.ViewModel       N� Title$  
SearchPage��   MainViewModel � DataContext# [  .��� .+��� -    ��Name$ searchWindow��.+��� $��
0,10,300,0q�$��30��$	��200��.��%#Bitte Pfad zur XML Datei auswählen��  �� Orientation$ 
Horizontal=�$��Center=�$��0,0,0,10q�.+��� -   $��openFilePathBox��$��25��$	��390�� �� 
IsReadOnly . �� -    ��Name$ openFileButton��$��25��$	��100��$��öffnen��$��10,0,0,0q��� $��10q�$��Center=�.+��� .��&$Bitte gesuchter Term angeben (Value)�� -   $��searchTermBox��$��25��$	��500���� .��42Weitere gesuchter Term angeben (Value2) (Optional)�� -   $��searchTermBox2��$��25��$	��500���� $ 
Horizontal=�$��Center=�$��10q�.+��� -   $ StartButton��$��30��$	��150��$��Start��$��	0,0,100,0q�{�. �� -   $ 
ExitButton��$��30��$	��150��$
��Exit���� -   $��searchTermLabel��$��Center=�&   System.Windows.Controls.DataGrid  -	   $��	dgIdValue��$��10q�$��20��"   VerticalScrollBarVisibility$
 Auto=�$	��300��   AutoGenerateColumns .  ���� *DataGridItems+ �� UpdateSourceTrigger$ PropertyChanged=�	   Columns	 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn
   Header$
 ID��   
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *ID+  $
 Value�� .  �� *Value+  $
 Value2�� .  �� *Value2+