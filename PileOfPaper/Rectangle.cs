using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PileOfPaper
{
    class Rectangle
    {
        private enum Vertice
        {
            North,
            West,
            East,
            South
        }

        private enum Corner
        {
            NW,
            NE,
            SE,
            SW
        }

        public int Color;
        public int Width, Height;
        private int _Start_X, _Start_Y;

        private Rectangle(int color, int start_X, int start_Y, int width, int height)
        {
            if (!(width > 0 && height > 0))
            {
                throw new ArgumentException();
            }

            Color = color;
            _Start_X = start_X;
            _Start_Y = start_Y;
            Width = width;
            Height = height;
        }

        public static Rectangle New(int color, int start_X, int start_Y, int width, int height)
        {
            if (!(width > 0 && height > 0))
            {
                return null;
            }

            return new Rectangle(color, start_X, start_Y, width, height);
        }

        public int A_X
        {
            get
            {
                return _Start_X;
            }
        }

        public int A_Y
        {
            get
            {
                return _Start_Y;
            }
        }

        public int B_X
        {
            get
            {
                return A_X + Width - 1;
            }
        }

        public int B_Y
        {
            get
            {
                return A_Y;
            }
        }

        public int C_X
        {
            get
            {
                return A_X;
            }
        }

        public int C_Y
        {
            get
            {
                return A_Y + Height - 1;
            }
        }

        public int D_X
        {
            get
            {
                return B_X;
            }
        }

        public int D_Y
        {
            get
            {
                return C_Y;
            }
        }

        public bool IsPointInside(int x, int y)
        {
            return x >= A_X && x <= B_X && y >= A_Y && y <= C_Y;
        }

        private Rectangle[] _Complement(Rectangle subtrahend)
        {
            Rectangle[] complement = new Rectangle[4];
            for (int i = 0; i < complement.Length; i++)
            {
                complement[i] = null;
            }

            int complementWidth = Width;
            int complementHeight = subtrahend.A_Y - A_Y;
            if (complementWidth > 0 && complementHeight > 0)
            {
                complement[0] = Rectangle.New(Color, _Start_X, _Start_Y,
                    complementWidth, complementHeight);
            }

            complementWidth = Width;
            complementHeight = C_Y - subtrahend.C_Y;
            if (complementWidth > 0 && complementHeight > 0)
            {
                complement[1] = Rectangle.New(Color, _Start_X,
                    subtrahend.C_Y + 1,
                    complementWidth, complementHeight);
            }

            complementWidth = subtrahend.A_X - A_X;
            complementHeight = subtrahend.Height;
            if (complementWidth > 0 && complementHeight > 0)
            {
                complement[2] = Rectangle.New(Color, _Start_X,
                    subtrahend.A_Y,
                    complementWidth, complementHeight);
            }

            complementWidth = B_X - subtrahend.B_X;
            complementHeight = subtrahend.Height;
            if (complementWidth > 0 && complementHeight > 0)
            {
                complement[3] = Rectangle.New(Color,
                    subtrahend.B_X + 1,
                    subtrahend.B_Y,
                    complementWidth, complementHeight);
            }

            return complement;
        }

        private Rectangle[] _Complement(Rectangle subtrahend, Vertice vertice)
        {
            Rectangle[] complement = new Rectangle[3];
            for (int i = 0; i < complement.Length; i++)
            {
                complement[i] = null;
            }

            switch (vertice)
            {
                case Vertice.North:
                    {
                        int complementWidth = subtrahend.C_X - A_X;
                        int complementHeight = Height;
                        if (complementWidth > 0 && complementHeight > 0)
                        {
                            complement[0] = Rectangle.New(Color,
                                _Start_X,
                                _Start_Y,
                                complementWidth,
                                complementHeight);
                        }
                        complementWidth = B_X - subtrahend.D_X;
                        complementHeight = Height;
                        if (complementWidth > 0 && complementHeight > 0)
                        {
                            complement[1] = Rectangle.New(Color,
                                subtrahend.D_X + 1,
                                _Start_Y,
                                complementWidth,
                                complementHeight);
                        }
                        complementWidth = subtrahend.Width;
                        complementHeight = C_Y - subtrahend.C_Y;
                        if (complementWidth > 0 && complementHeight > 0)
                        {
                            complement[2] = Rectangle.New(Color,
                                subtrahend.C_X,
                                subtrahend.C_Y,
                                complementWidth,
                                complementHeight);
                        }
                    }
                    break;
                case Vertice.East:
                    {
                        int complementWidth = Width;
                        int complementHeight = subtrahend.A_Y - A_Y;
                        if (complementWidth > 0 && complementHeight > 0)
                        {
                            complement[0] = Rectangle.New(Color,
                                _Start_X,
                                _Start_Y,
                                complementWidth,
                                complementHeight);
                        }
                        complementWidth = Width;
                        complementHeight = C_Y - subtrahend.C_Y;
                        if (complementWidth > 0 && complementHeight > 0)
                        {
                            complement[1] = Rectangle.New(Color,
                                _Start_X,
                                subtrahend.C_Y + 1,
                                complementWidth,
                                complementHeight);
                        }
                        complementWidth = subtrahend.A_X - A_X;
                        complementHeight = subtrahend.Height;
                        if (complementWidth > 0 && complementHeight > 0)
                        {
                            complement[2] = Rectangle.New(Color,
                                subtrahend.B_X + 1,
                                subtrahend.A_Y,
                                complementWidth,
                                complementHeight);
                        }
                    }
                    break;
                case Vertice.West:
                    {
                        int complementWidth = Width;
                        int complementHeight = subtrahend.A_Y - C_Y;
                        if (complementWidth > 0 && complementHeight > 0)
                        {
                            complement[0] = Rectangle.New(Color,
                                _Start_X,
                                _Start_Y,
                                complementWidth,
                                complementHeight);
                        }
                        complementWidth = Width;
                        complementHeight = C_Y - subtrahend.B_Y;
                        if (complementWidth > 0 && complementHeight > 0)
                        {
                            complement[1] = Rectangle.New(Color,
                                _Start_X,
                                subtrahend.C_Y + 1,
                                complementWidth,
                                complementHeight);
                        }
                        complementWidth = subtrahend.A_X - A_X;
                        complementHeight = subtrahend.Height;
                        if (complementWidth > 0 && complementHeight > 0)
                        {
                            complement[2] = Rectangle.New(Color,
                                _Start_X,
                                subtrahend.A_Y,
                                complementWidth,
                                complementHeight);
                        }
                    }
                    break;
                case Vertice.South:
                    {
                        int complementWidth = subtrahend.A_X - C_X;
                        int complementHeight = Height;
                        if (complementWidth > 0 && complementHeight > 0)
                        {
                            complement[0] = Rectangle.New(Color,
                                _Start_X,
                                _Start_Y,
                                complementWidth,
                                complementHeight);
                        }
                        complementWidth = D_X - subtrahend.B_X;
                        complementHeight = Height;
                        if (complementWidth > 0 && complementHeight > 0)
                        {
                            complement[1] = Rectangle.New(Color,
                                subtrahend.B_X + 1,
                                _Start_Y,
                                complementWidth,
                                complementHeight);
                        }
                        complementWidth = subtrahend.Width;
                        complementHeight = subtrahend.A_Y - A_Y;
                        if (complementWidth > 0 && complementHeight > 0)
                        {
                            complement[2] = Rectangle.New(Color,
                                subtrahend.C_X,
                                _Start_Y,
                                complementWidth,
                                complementHeight);
                        }
                    }
                    break;
            }


            return complement;
        }

        private Rectangle[] _Complement(Rectangle subtrahend, Corner corner)
        {
            Rectangle[] complement = new Rectangle[2];
            for (int i = 0; i < complement.Length; i++)
            {
                complement[i] = null;
            }

            switch (corner)
            {
                case Corner.NW:
                    {
                        int intersectionWidth = subtrahend.D_X - A_X + 1;
                        int intersectionHeight = subtrahend.D_Y - A_Y + 1;

                        complement[0] = Rectangle.New(Color,
                            _Start_X,
                            subtrahend.D_Y + 1,
                            Width,
                            Height - intersectionHeight);
                        complement[1] = Rectangle.New(Color,
                            subtrahend.D_X + 1,
                            _Start_Y,
                            Width - intersectionWidth,
                            intersectionHeight);
                    }
                    break;
                case Corner.NE:
                    {
                        int intersectionWidth = B_X - subtrahend.C_X + 1;
                        int intersectionHeight = subtrahend.C_Y - B_Y + 1;

                        complement[0] = Rectangle.New(Color,
                            _Start_X,
                            subtrahend.C_Y + 1,
                            Width,
                            Height - intersectionHeight);
                        complement[1] = Rectangle.New(Color,
                            _Start_X,
                            _Start_Y,
                            Width - intersectionWidth,
                            intersectionHeight);
                    }
                    break;
                case Corner.SW:
                    {
                        int intersectionWidth = subtrahend.B_X - C_X + 1;
                        int intersectionHeight = C_Y - subtrahend.B_Y + 1;

                        complement[0] = Rectangle.New(Color,
                            _Start_X,
                            _Start_Y,
                            Width,
                            Height - intersectionHeight);
                        complement[1] = Rectangle.New(Color,
                            subtrahend.B_X + 1,
                            subtrahend.B_Y,
                            Width - intersectionWidth,
                            intersectionHeight);
                    }
                    break;
                case Corner.SE:
                    {
                        int intersectionWidth = D_X - subtrahend.A_X + 1;
                        int intersectionHeight = D_Y - subtrahend.A_Y + 1;

                        complement[0] = Rectangle.New(Color,
                            _Start_X,
                            _Start_Y,
                            Width,
                            Height - intersectionHeight);
                        complement[1] = Rectangle.New(Color,
                            _Start_X,
                            subtrahend.B_Y,
                            Width - intersectionWidth,
                            intersectionHeight);
                    }
                    break;
            }

            return complement;
        }

        private Rectangle[] _ComplementInverse(Rectangle subtrahend, Vertice vertice)
        {
            Rectangle[] complement = new Rectangle[1];

            switch (vertice)
            {
                case Vertice.North:
                    {
                        complement[0] = Rectangle.New(Color,
                            _Start_X, subtrahend.C_Y + 1,
                            Width, C_Y - subtrahend.C_Y);
                    }
                    break;
                case Vertice.West:
                    {
                        complement[0] = Rectangle.New(Color,
                            subtrahend.B_X + 1, _Start_Y,
                            B_X - subtrahend.B_X , Height);
                    }
                    break;
                case Vertice.East:
                    {
                        complement[0] = Rectangle.New(Color,
                            _Start_X, _Start_Y,
                            subtrahend.A_X - A_X, Height);
                    }
                    break;
                case Vertice.South:
                    {
                        complement[0] = Rectangle.New(Color,
                            _Start_X, _Start_Y,
                            Width, subtrahend.A_Y - A_Y );
                    }
                    break;
            }


            return complement;
        }

        public Rectangle[] Complement(Rectangle subtrahend)
        {
            if (IsPointInside(subtrahend.A_X, subtrahend.A_Y) &&
                IsPointInside(subtrahend.B_X, subtrahend.B_Y) &&
                IsPointInside(subtrahend.C_X, subtrahend.C_Y) &&
                IsPointInside(subtrahend.D_X, subtrahend.D_Y))
            {
                //Subtrahend completely overlaps this
                return _Complement(subtrahend);
            }
            else if (IsPointInside(subtrahend.A_X, subtrahend.A_Y))
            {
                if (IsPointInside(subtrahend.B_X, subtrahend.B_Y))
                {
                    return _Complement(subtrahend, Vertice.South);
                }
                else if (IsPointInside(subtrahend.C_X, subtrahend.C_Y))
                {
                    return _Complement(subtrahend, Vertice.East);
                }
                else
                {
                    return _Complement(subtrahend, Corner.SE);
                }
            }
            else if (IsPointInside(subtrahend.B_X, subtrahend.B_Y))
            {
                if (IsPointInside(subtrahend.D_X, subtrahend.D_Y))
                {
                    return _Complement(subtrahend, Vertice.West);
                }
                else
                {
                    return _Complement(subtrahend, Corner.SW);
                }
            }
            else if (IsPointInside(subtrahend.C_X, subtrahend.C_Y))
            {
                if (IsPointInside(subtrahend.D_X, subtrahend.D_Y))
                {
                    return _Complement(subtrahend, Vertice.North);
                }
                else
                {
                    return _Complement(subtrahend, Corner.NE);
                }
            }
            else if (IsPointInside(subtrahend.D_X, subtrahend.D_Y))
            {
                return _Complement(subtrahend, Corner.NW);
            }


            else if (subtrahend.IsPointInside(A_X, A_Y))
            {
                if (subtrahend.IsPointInside(B_X, B_Y))
                {
                    return _ComplementInverse(subtrahend, Vertice.North);
                }
                else if (subtrahend.IsPointInside(C_X, C_Y))
                {
                    return _ComplementInverse(subtrahend, Vertice.West);
                }
            }
            else if (subtrahend.IsPointInside(B_X, B_Y))
            {
                if (subtrahend.IsPointInside(D_X, D_Y))
                {
                    return _ComplementInverse(subtrahend, Vertice.East);
                }
            }
            else if (subtrahend.IsPointInside(C_X, C_Y))
            {
                if (subtrahend.IsPointInside(D_X, D_Y))
                {
                    return _ComplementInverse(subtrahend, Vertice.South);
                }
            }

            return null;
        }
    }
}
