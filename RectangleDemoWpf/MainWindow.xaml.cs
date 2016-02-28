using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RectangleDemoWpf.ExtensionHelpers;
namespace RectangleDemoWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
 
    public partial class MainWindow : Window
    {
        private bool isDragging;
        Rectangle r2;
        public MainWindow()
        {
            InitializeComponent();
            //create a rectangle that is not able to move by mouse
            CreateARectangle();
          
        }
        public void CreateARectangle()
        {
            // Create a blue and a black Brush
            SolidColorBrush blueBrush = new SolidColorBrush();
            blueBrush.Color = Colors.Blue;
            SolidColorBrush blackBrush = new SolidColorBrush();
            blackBrush.Color = Colors.Black;
            //second rectangle
            r2 = new Rectangle();
            r2.Height = 300;
            r2.Width = 200;

            r2.StrokeThickness = 1;
            r2.Stroke = blueBrush;
         
            myCanv.Children.Add(r2);
            Canvas.SetLeft(r2, 400);
            Canvas.SetTop(r2, 0);
        }
        #region detect collision function
        private void CollisionDetection()
        {
            string testResultText = "";
            if (r2.IntersectsWithW(r1))
            {
                testResultText = "Intersection";
                if (r2.ContainsW(r1))
                {

                    testResultText = "Containment";
                }
            }
            if (r2.IsAdjacentTo(r1))
            {

                if (!string.IsNullOrEmpty(testResultText))
                {
                    testResultText += " and Adjacent";
                }
                else
                {
                    testResultText = "Adjacent";
                }
            }
            label.Content = testResultText;

        }
        #endregion
        #region make the rectangle move follow the mouse and detect collision when the mouse is up
        private void r1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            r1.ReleaseMouseCapture();
            isDragging = false;
            CollisionDetection();
        }

        private void r1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point canvPosToWindow = myCanv.TransformToAncestor(this).Transform(new Point(0, 0));

                Rectangle r = sender as Rectangle;
                var upperlimit = canvPosToWindow.Y + (r.Height / 2);
                var lowerlimit = canvPosToWindow.Y + myCanv.ActualHeight - (r.Height / 2);

                var leftlimit = canvPosToWindow.X + (r.Width / 2);
                var rightlimit = canvPosToWindow.X + myCanv.ActualWidth - (r.Width / 2);


                var absmouseXpos = e.GetPosition(this).X;
                var absmouseYpos = e.GetPosition(this).Y;
                mouseLocationLabel.Content = "Mouse location:" + absmouseXpos + "," + absmouseYpos;
                if ((absmouseXpos > leftlimit && absmouseXpos < rightlimit)
                    && (absmouseYpos > upperlimit && absmouseYpos < lowerlimit))
                {
                    r.SetValue(Canvas.LeftProperty, e.GetPosition(myCanv).X - (r.Width / 2));
                    r.SetValue(Canvas.TopProperty, e.GetPosition(myCanv).Y - (r.Height / 2));
                }
            }
        }

        private void r1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            r1.CaptureMouse();
            isDragging = true;
        }
        #endregion
    }
}
