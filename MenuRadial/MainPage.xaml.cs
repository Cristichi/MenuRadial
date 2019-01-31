using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace MenuRadial
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ObservableCollection<MenuItem> ItemsMenu;

        public MainPage()
        {
            this.InitializeComponent();
            ItemsMenu = new ObservableCollection<MenuItem>()
            {
                new MenuItem(){ Texto = "Entrada 1", Color = new SolidColorBrush(Colors.Yellow), Enlace = typeof(MainPage)},
                new MenuItem(){ Texto = "Entrada 2", Color = new SolidColorBrush(Colors.Red), Enlace = typeof(MainPage)},
                new MenuItem(){ Texto = "Entrada 3", Color = new SolidColorBrush(Colors.White), Enlace = typeof(MainPage)},
                new MenuItem(){ Texto = "Entrada 4", Color = new SolidColorBrush(Colors.Blue), Enlace = typeof(MainPage)},
            };
        }
    }
}
