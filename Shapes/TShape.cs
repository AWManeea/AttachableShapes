using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Polygonner
{
    public class TShape : Shape
    {
        private Point offset;
        private Size size;
        private readonly Pen pen;
        private const ShapeType shapeType = ShapeType.TShape;

        public override ShapeType ShapeType { get { return shapeType; } }
        public override Point Offset { get { return offset; } set { offset = value; } }
        public override int X { get { return offset.X; } set { offset.X = value; } }
        public override int Y { get { return offset.Y; } set { offset.Y = value; } }
        public override Size Size { get { return size; } set { size = value; } }
        public override Pen Pen { get { return pen; } }
        public override Color Color { get { return pen.Color; } set { pen.Color = value; } }
        public override float Thickness { get { return pen.Width; } set { pen.Width = value; } }
        public override DashStyle DashStyle { get { return pen.DashStyle; } set { pen.DashStyle = value; } }
        public override int Width { get => size.Width; set => size = new Size(value, Height); }
        public override int Height { get => size.Height; set => size=new Size(Width, value); }

        public TShape(Point offset, Size size)
        {
            this.offset = offset;
            this.size = size;
            pen = new Pen(Color.Black, 7);
        }

        public override bool Accepts(ShapeType type)
        {
            return type== ShapeType.UShape;
        }


        public override GraphicsPath GetPath()
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddLine(new Point(X, Y+Height/3), new Point(X+Width/3, Y + Height / 3));
            gp.AddLine(new Point(X + Width / 3, Y + Height / 3), new Point(X + Width / 3, Y + 2*Height / 3));
            gp.AddLine(new Point(X + Width / 3, Y + 2*Height / 3), new Point(X + 2 * Width / 3, Y + 2*Height / 3));
            gp.AddLine(new Point(X + 2 * Width / 3, Y + 2*Height / 3), new Point(X + 2 * Width / 3, Y + Height / 3));
            gp.AddLine(new Point(X + 2 * Width / 3, Y + Height / 3), new Point(X + Width, Y + Height / 3));
            gp.AddLine(new Point(X + Width, Y + Height / 3), new Point(X + Width, Y + Height));
            gp.AddLine(new Point(X + Width, Y +Height), new Point(X, Y +Height));
            gp.AddLine(new Point(X, Y +  Height ), new Point(X, Y + Height / 3));
            return gp;
        }
    }
}