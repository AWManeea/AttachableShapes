using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;

namespace Polygonner
{
    public abstract class Shape
    {
        public static int MAGNET_TOLERANCE=100;
        private bool isSelected;

        public abstract ShapeType ShapeType { get;  }
        public bool IsSelected { get { return isSelected; } }
        public abstract Point Offset { get; set; }
        public abstract int X { get; set; }
        public abstract int Y { get; set; }
        public abstract int Width { get; set; }
        public abstract int Height { get; set; }
        public abstract Size Size { get; set; }
        public abstract Pen Pen { get; }
        public abstract Color Color { get; set; }
        public abstract float Thickness { get; set; }
        public abstract DashStyle DashStyle { get; set; } 
        public abstract GraphicsPath GetPath();
        public abstract bool Accepts(ShapeType type);
        public bool Contains(Point p)
        {
            return (p.X > Offset.X &&
                    p.X < Width + Offset.X &&
                    p.Y > Offset.Y &&
                    p.Y < Offset.Y + Height);
        }
        public void Select()
        {
            isSelected = true;
        }
        public void Unselect()
        {
            isSelected = false;
        }
        public bool isNear(Point point)
        {
            return ((Math.Abs(point.X - X) < MAGNET_TOLERANCE ||
                Math.Abs(point.X + X) < MAGNET_TOLERANCE) && (
                Math.Abs(point.Y - Y) < MAGNET_TOLERANCE ||
                Math.Abs(point.Y - Y) < MAGNET_TOLERANCE));
        }
    }

    public enum ShapeType
    {
        TShape,
        UShape,
    }
}