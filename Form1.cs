using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Polygonner
{
    public partial class Form1 : Form
    {
        int borderScaling = 30;
        int xDiff, yDiff;
        bool isMouseDown;

        readonly List<Shape> shapes = new();
        public Form1()
        {
            shapes.Add(new TShape(new Point(100, 100), new Size(300, 300)));
            shapes.Add(new TShape(new Point(500, 500), new Size(300, 300)));
            shapes.Add(new UShape(new Point(1000, 600), new Size(300, 300)));
            shapes.Add(new UShape(new Point(1200, 1000), new Size(300, 300)));
            InitializeComponent();
        }

        private void FormPaintHandler(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            var borderPen = new Pen(Color.Gray, 3);
            borderPen.DashStyle = DashStyle.Dash;
            foreach (var shape in shapes)
            {
                if (shape.IsSelected)
                {
                    g.DrawRectangle(borderPen, new Rectangle(shape.X - (borderScaling / 2), shape.Y - (borderScaling / 2), shape.Size.Width + borderScaling, shape.Size.Height + borderScaling));
                    shape.Thickness = 10;
                }
                else
                {
                    shape.Thickness = 7;
                }
                g.DrawPath(shape.Pen, shape.GetPath());
            }
        }

        private void FormMouseClickHandler(object sender, MouseEventArgs e)
        {
            foreach (var shape in shapes)
                shape.Unselect();
            Invalidate();
            foreach (var shape in shapes)
            {
                if (shape.Contains(new Point(e.X, e.Y)))
                {
                    if (!shape.IsSelected)
                    {
                        shape.Select();
                        shapes.Remove(shape);
                        shapes.Add(shape);
                        xDiff = e.X - shape.X;
                        yDiff = e.Y - shape.Y;
                        Invalidate();
                    }
                    return;
                }
            }
        }

        private void FormMouseMoveHandler(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
                foreach (var shape in shapes)
                {
                    if (shape.IsSelected)
                    {
                        if (shape.Contains(new Point(e.X, e.Y)))
                        {
                            int w = shape.Size.Width, h = shape.Size.Height;
                            shape.Offset = new Point(e.X - xDiff, e.Y - yDiff);
                            shape.Size = new Size(w, h);
                            Invalidate();
                            xDiff = e.X - shape.X;
                            yDiff = e.Y - shape.Y;

                            // Check if its near another object
                            foreach (var anotherShape in shapes)
                            {
                                if (shape.Equals(anotherShape))
                                {
                                    shape.Color = Color.Black;
                                }
                                else if (shape.isNear(anotherShape.Offset) && shape.Accepts(anotherShape.ShapeType))
                                {
                                    shape.X = anotherShape.X;
                                    shape.Y = anotherShape.Y;
                                    shape.Color = Color.BlueViolet;
                                    anotherShape.Color = Color.BlueViolet;
                                    break;
                                }
                            }
                        }
                    }
                }
            UpdateCursor(e.X, e.Y);
        }

        private void FormMouseDownHandler(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
        }

        private void FormMouseUpHandler(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        private void UpdateCursor(int x, int y)
        {
            foreach (var shape in shapes)
            {
                //BorderPointPosition pos = shape.CoordinatesOnBorder(x, y);
                //switch (pos)
                //{
                //    case BorderPointPosition.Up:
                //        Cursor = Cursors.SizeNS;
                //        return;
                //    case BorderPointPosition.Down:
                //        Cursor = Cursors.SizeNS;
                //        return;
                //    case BorderPointPosition.Right:
                //        Cursor = Cursors.SizeWE;
                //        return;
                //    case BorderPointPosition.Left:
                //        Cursor = Cursors.SizeWE;
                //        return;
                //}
                if (shape.Contains(new Point(x, y)))
                {
                    Cursor = shape.IsSelected ? Cursors.NoMove2D : Cursors.Hand;
                    return;
                }
            }
            Cursor = Cursors.Default;
        }

    }
}
