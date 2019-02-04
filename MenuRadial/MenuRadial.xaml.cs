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

        private void Dibujar() {
            CanvasMR.Children.Clear();
            if (ItemsSource is null || ItemsSource.Count() == 0){
                return;
            }

            int iteracion = 0;
            int numItems = ItemsSource.Count();

            //Perímero de la esfera
            double perimetro = 2 * Math.PI * Radio;
            //Perímetro que corresponde a cada item del menú
            double perimItem = perimetro / numItems;

            //Margen con los bordes izquierdo y superior
            double margen = 70;
            //Compensación requerida para que la esfera no se pierda a la izquierda
            double compensacion = Radio + margen;

            //punto central del componente
            Point centro = new Point(Width/2, Height/2);
            double sizeBola = 1;

            Ellipse bolacentro = new Ellipse
            {
                Fill = new SolidColorBrush(Colors.Yellow),
                Height = sizeBola,
                Width = sizeBola,
            };

            Canvas.SetTop(bolacentro, centro.Y - sizeBola / 2);
            Canvas.SetLeft(bolacentro, centro.X - sizeBola / 2);
            CanvasMR.Children.Add(bolacentro);
            
            foreach (MenuItem item in ItemsSource)
            {
                float angulo = 360 / numItems * iteracion;
                PointCollection points = new PointCollection
                {
                    new Point(0, 0),
                    new Point(0, Radio)
                };
                Polyline linea = new Polyline
                {
                    Points = points,
                    CenterPoint = new Vector3(0, 0, 0),
                    Stroke = item.Color,
                    StrokeThickness = 4,
                };
                linea.Rotation = angulo;

                Canvas.SetTop(linea, centro.Y);
                Canvas.SetLeft(linea, centro.X);
                CanvasMR.Children.Add(linea);

                //x = centro.X + radio*cos(angulo)
                //y = centro.Y + radio*sin(angulo)
                float anguloAnterior = gradosEnRadianes(angulo - 360 / numItems);
                float anguloActual = gradosEnRadianes(angulo);
                Point anterior = new Point(Radio * Math.Cos(anguloAnterior), Radio * Math.Sin(anguloAnterior));
                Point este = new Point(Radio * Math.Cos(anguloActual), Radio * Math.Sin(anguloActual));
                PointCollection points2 = new PointCollection
                {
                    anterior,
                    este
                };
                Polyline linea2 = new Polyline
                {
                    Points = points2,
                    Stroke = item.Color,
                    StrokeThickness = 4,
                };

                Canvas.SetTop(linea2, centro.Y);
                Canvas.SetLeft(linea2, centro.X);
                CanvasMR.Children.Add(linea2);

                /*
                Polygon pizza = new Polygon
                {
                    Fill = item.Color
                };
                PointCollection puntos = new PointCollection
                {
                    new Point(margen+Radio+Radio/numItems, margen),
                    new Point(margen-Radio-Radio/numItems, margen),
                    centro,
                };
                pizza.Points = puntos;
                pizza.CenterPoint = new System.Numerics.Vector3((float)centro.X, (float)centro.Y, 0);
                pizza.Rotation = 360 / numItems * iteracion;
                CanvasMR.Children.Add(pizza);
                Canvas.SetTop(pizza, 0);
                Canvas.SetLeft(pizza, 0);
                */
                iteracion++;
            }
        }

        public float gradosEnRadianes(double angulo)
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
