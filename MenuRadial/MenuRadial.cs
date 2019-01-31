using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

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

        private Brush color;
        public Brush Color
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
                NotifyPropertyChanged("Color");
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

    class MenuRadial : Canvas
    {

        public MenuRadial()
        {

        }

        private void OnSourceChanged()
        {
            int iteracion = 0, numItems = ItemsSource.Count();
            foreach (MenuItem item in ItemsSource) { 
                Polygon pizza = new Polygon
                {
                    Fill = item.Color
                };
                PointCollection puntos = new PointCollection
                {
                    new Windows.Foundation.Point(10, 10),
                    new Windows.Foundation.Point(300, 10),
                    new Windows.Foundation.Point(155, 400)
                };
                pizza.Points = puntos;
                pizza.CenterPoint = new System.Numerics.Vector3(155, 400, 0);
                pizza.Rotation = 360/numItems*iteracion;
                Children.Add(pizza);
                Canvas.SetTop(pizza, 0);
                Canvas.SetLeft(pizza, 0);

                iteracion++;
            }
        }

        public IEnumerable<MenuItem> ItemsSource
        {
            get { return (IEnumerable<MenuItem>)GetValue(ItemsSourceProperty); }
            set {
                SetValue(ItemsSourceProperty, value);
                OnSourceChanged();
            }
        }
        
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable<MenuItem>), typeof(MenuRadial), new PropertyMetadata(0));


    }
}
