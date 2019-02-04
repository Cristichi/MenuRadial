using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
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
using Windows.UI.Xaml.Shapes;

// La plantilla de elemento Control de usuario está documentada en https://go.microsoft.com/fwlink/?LinkId=234236

namespace MenuRadial
{
    public sealed partial class MenuRadial : ContentControl
    {
        public MenuRadial()
        {
            this.InitializeComponent();
        }


        private void GridSizeChanged(object sender, RoutedEventArgs e)
        {
            Dibujar();
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Dibujar();
            GridMR.SizeChanged += GridSizeChanged;
        }

        public void ElementoPinchado(object sender, RoutedEventArgs e)
        {

        }

        private void Dibujar() {
            CanvasMR.Children.Clear();
            if (ItemsSource is null || ItemsSource.Count() == 0){
                return;
            }

            int numItems = ItemsSource.Count();
            Point centro = new Point(Width/2, Height/2);
            int iteracion = 0;
            foreach (MenuItem item in ItemsSource)
            {
                float angulo = 360 / numItems * iteracion;
                
                //x = centro.X + radio*cos(angulo)
                //y = centro.Y + radio*sin(angulo)
                float anguloAnterior = GradosEnRadianes(angulo - 360 / numItems);
                float anguloActual = GradosEnRadianes(angulo);
                Point anterior = new Point(centro.X + Radio * Math.Cos(anguloAnterior), centro.Y + Radio * Math.Sin(anguloAnterior));
                Point este = new Point(centro.X + Radio * Math.Cos(anguloActual), centro.Y + Radio * Math.Sin(anguloActual));
                PointCollection points2 = new PointCollection
                {
                    centro,
                    anterior,
                    este
                };
                Polygon poligono = new Polygon
                {
                    Points = points2,
                    Fill = item.Color,
                };

                Canvas.SetTop(poligono, 0);
                Canvas.SetLeft(poligono, 0);
                CanvasMR.Children.Add(poligono);

                poligono.PointerPressed += ElementoPinchado;

                iteracion++;
            }
        }

        public float GradosEnRadianes(double angulo)
        {
            return (float) (Math.PI / 180 * angulo);
        }

        public double Radio
        {
            get { return (double)GetValue(RadioProperty); }
            set
            {
                double real = value;
                if (real < 0)
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
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable<MenuItem>), typeof(MenuRadial), new PropertyMetadata(0));


    }

    public class MenuItem : INotifyPropertyChanged
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
}
