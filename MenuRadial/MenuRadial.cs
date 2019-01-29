using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MenuRadial
{
    class MenuItem : INotifyPropertyChanged
    {
        private IconElement icono;
        public IconElement Icono
        {
            get
            {
                return icono;
            }

            set
            {
                icono = value;
                NotifyPropertyChanged("IconElement");
            }
        }

        private string texto;
        public string Texto
        {
            get
            {
                return texto;
            }

            set
            {
                texto = value;
                NotifyPropertyChanged("Texto");
            }
        }

        private Type enlace;
        public Type Enlace
        {
            get
            {
                return enlace;
            }

            set
            {
                enlace = value;
                NotifyPropertyChanged("Enlace");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    class MenuRadial : Control
    {
        public MenuRadial()
        {
            
        }



        public IEnumerable<MenuItem> ItemsSource
        {
            get { return (IEnumerable<MenuItem>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable<MenuItem>), typeof(MenuRadial), new PropertyMetadata(0));


    }
}
