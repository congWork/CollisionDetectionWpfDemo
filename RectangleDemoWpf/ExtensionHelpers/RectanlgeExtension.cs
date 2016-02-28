using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace RectangleDemoWpf.ExtensionHelpers
{
    //basic informations about x-axis and y-axis
    public class RectangleCordinationInfo
    {
        public double MinX { get; set; }
        public double MaxX { get; set; }
        public double MinY { get; set; }
        public double MaxY { get; set; }
    }
    
    public static class RectangleExtension
    {
        #region Get basic information for this rectangle
        public static RectangleCordinationInfo GetRectangleCoordinationInfo(Rectangle re)
        {
            double left = Canvas.GetLeft(re);
            double top = Canvas.GetTop(re);
            double r1X1 = left;
            double r1X2 = r1X1 + re.Width;
            double r1Y1 = top;
            double r1Y2 = r1Y1 + re.Height;
            return new RectangleCordinationInfo
            {
                MinX = r1X1,
                MaxX = r1X2,
                MinY = r1Y1,
                MaxY = r1Y2
            };
        }
        #endregion
        #region detect containment
        public static bool ContainsW(this Rectangle r1, Rectangle r2)
        {
            int i = 0;
            var r1Cord = GetRectangleCoordinationInfo(r1);
            var r2Cord = GetRectangleCoordinationInfo(r2);
            if (r2Cord.MinX >= r1Cord.MinX && r2Cord.MinX <= r1Cord.MaxX && r2Cord.MaxX >= r1Cord.MinX && r2Cord.MaxX <= r1Cord.MaxX)
            {
                i++;
            }
            if (r2Cord.MinY >= r1Cord.MinY && r2Cord.MinY <= r1Cord.MaxY && r2Cord.MaxY >= r1Cord.MinY && r2Cord.MaxY <= r1Cord.MaxY)
            {
                i++;
            }
            if (i == 2)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region detect adjacency
        public static bool IsAdjacentTo(this Rectangle r1, Rectangle r2)
        {
            var r1Cord = GetRectangleCoordinationInfo(r1);
            var r2Cord = GetRectangleCoordinationInfo(r2);

            if (r1Cord.MaxX == r2Cord.MinX)
            {
                //on the same x, might share the same side
                if (r2Cord.MinY >= r1Cord.MinY && r2Cord.MaxY <= r1Cord.MaxY)
                {
                    return true;
                }
            }

            if (r1Cord.MaxX == r2Cord.MaxX)
            {
                if (r2Cord.MinX >= r1Cord.MinX)
                {
                    return true;
                }
            }

            if (r1Cord.MinX == r2Cord.MaxX)
            {
                if (r2Cord.MinY >= r1Cord.MinY && r2Cord.MaxY <= r1Cord.MaxY)
                {
                    return true;
                }
            }
            if (r1Cord.MinX == r2Cord.MinX)
            {
                if (r2Cord.MaxX <= r1Cord.MaxX)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
        #region detect intersection
        public static bool IntersectsWithW(this Rectangle r1, Rectangle r2)
        {
            var r1Cord = GetRectangleCoordinationInfo(r1);
            var r2Cord = GetRectangleCoordinationInfo(r2);

            double res1 = Math.Abs((r1Cord.MinX + r1Cord.MaxX) / 2 - (r2Cord.MinX + r2Cord.MaxX) / 2);
            double res2 = (r1Cord.MaxX + r2Cord.MaxX - r1Cord.MinX - r2Cord.MinX) / 2;

            double res3 = Math.Abs((r1Cord.MinY + r1Cord.MaxY) / 2 - (r2Cord.MinY + r2Cord.MaxY) / 2);
            double res4 = (r1Cord.MaxY + r2Cord.MaxY - r1Cord.MinY - r2Cord.MinY) / 2;

            if (res1 < res2 && res3 < res4)
                return true;
            else
                return false;
        }
        #endregion
    }
}
