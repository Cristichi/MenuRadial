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
            int iteracion = 0;
            int numItems = ItemsSource.Count();

            //Perímero de la esfera
            double perimetro = 2 * Math.PI * Radio;
            //Perímetro que corresponde a cada item del menú
            double perimItem = perimetro / numItems;

            //Margen con los bordes izquierdo y superior
            double margen = 10;
            //Compensación requerida para que la esfera no se pierda a la izquierda
            double compensacion = Radio + margen;
            foreach (MenuItem item in ItemsSource) {
                //ArcSegment arco = new ArcSegment();

                Polygon pizza = new Polygon
                {
                    Fill = item.Color
                };
                Windows.Foundation.Point centro = new Windows.Foundation.Point(Radio+margen, Radio+margen);
                PointCollection puntos = new PointCollection
                {
                    new Windows.Foundation.Point(margen, margen),
                    new Windows.Foundation.Point(margen+(Radio*2), margen),
                    centro,
                };
                pizza.Points = puntos;
                pizza.CenterPoint = new System.Numerics.Vector3((float)centro.X, (float)centro.Y, 0);
                pizza.Rotation = 360/numItems*iteracion;
                Children.Add(pizza);
                Canvas.SetTop(pizza, 0);
                Canvas.SetLeft(pizza, 0);

                iteracion++;
            }
        }

        public double Radio
        {
            get { return (double)GetValue(RadioProperty); }
            set {
                double real = value;
                if (real<0)
                {
                    real *= -1;
                }
                SetValue(RadioProperty, real);
            }
        }

        // Using a DependencyProperty as the backing store for Radio.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RadioProperty =
            DependencyProperty.Register("Radio", typeof(double), typeof(MenuRadial), new PropertyMetadata(0));

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
