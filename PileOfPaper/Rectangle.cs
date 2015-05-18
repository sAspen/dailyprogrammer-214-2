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

        private enum Crossing
        {
            Horizontal,
            Vertical
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
            if (color != 0 &&
                (start_X < 0 || start_X + width > 100 ||
                    start_Y < 0 || start_Y + height > 100))
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
                    _Start_X,
                    subtrahend.A_Y,
                    complementWidth,
                    complementHeight);
            }

            complementWidth = B_X - subtrahend.B_X;
            complementHeight = subtrahend.Height;
            if (complementWidth > 0 && complementHeight > 0)
            {
                complement[3] = Rectangle.New(Color,
                    subtrahend.B_X + 1,
                    subtrahend.B_Y,
                    complementWidth,
                    complementHeight);
            }

            int culW, culH;
            culW = (complement[2] != null ? complement[2].Width : subtrahend.A_X - A_X) +
                (complement[3] != null ? complement[3].Width : B_X - subtrahend.B_X) + 
                subtrahend.Width;
            culH = (complement[0] != null ? complement[0].Height : subtrahend.A_Y - A_Y) +
                (complement[1] != null ? complement[1].Height : C_Y - subtrahend.C_Y) +
                subtrahend.Height;

            if (culW != Width || culH != Height)
            {
                throw new SystemException();
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


            int culW = 0, culH = 0;
            switch (vertice)
            {
                case Vertice.North:
                    {
                        int complementWidth = subtrahend.C_X - C_X;
                        int complementHeight = Height;
                        if (complementWidth > 0 && complementHeight > 0)
                        {
                            complement[0] = Rectangle.New(Color,
                                _Start_X,
                                _Start_Y,
                                complementWidth,
                                complementHeight);
                        }
                        complementWidth = D_X - subtrahend.D_X;
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
                                subtrahend.C_Y + 1,
                                complementWidth,
                                complementHeight);
                        }
                    }

                    culW = (complement[0] != null ? complement[0].Width : 0) + (complement[1] != null ? complement[1].Width : 0) + (complement[2] != null ? complement[2].Width : 0);
                    culH = (complement[2] != null ? complement[2].Height : 0) + subtrahend.C_Y - A_Y + 1;
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
                                _Start_X,
                                subtrahend.A_Y,
                                complementWidth,
                                complementHeight);
                        }
                    }
                    culW = -A_X + B_X + 1;
                    culH = (complement[0] != null ? complement[0].Height : 0) + (complement[1] != null ? complement[1].Height : 0) + (complement[2] != null ? complement[2].Height : 0);
                    break;
                case Vertice.West:
                    {
                        int complementWidth = Width;
                        int complementHeight = subtrahend.B_Y - B_Y;
                        if (complementWidth > 0 && complementHeight > 0)
                        {
                            complement[0] = Rectangle.New(Color,
                                _Start_X,
                                _Start_Y,
                                complementWidth,
                                complementHeight);
                        }
                        complementWidth = Width;
                        complementHeight = D_Y - subtrahend.D_Y;
                        if (complementWidth > 0 && complementHeight > 0)
                        {
                            complement[1] = Rectangle.New(Color,
                                _Start_X,
                                subtrahend.D_Y + 1,
                                complementWidth,
                                complementHeight);
                        }
                        complementWidth = B_X - subtrahend.B_X;
                        complementHeight = subtrahend.Height;
                        if (complementWidth > 0 && complementHeight > 0)
                        {
                            complement[2] = Rectangle.New(Color,
                                _Start_X,
                                subtrahend.B_Y,
                                complementWidth,
                                complementHeight);
                        }
                    }
                    culW = (complement[2] != null ? complement[2].Width : 0) + subtrahend.B_X - A_X + 1;
                    culH = (complement[0] != null ? complement[0].Height : 0) + (complement[1] != null ? complement[1].Height : 0) + (complement[2] != null ? complement[2].Height : 0);
                    break;
                case Vertice.South:
                    {
                        int complementWidth = subtrahend.A_X - A_X;
                        int complementHeight = Height;
                        if (complementWidth > 0 && complementHeight > 0)
                        {
                            complement[0] = Rectangle.New(Color,
                                _Start_X,
                                _Start_Y,
                                complementWidth,
                                complementHeight);
                        }
                        complementWidth = B_X - subtrahend.B_X;
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
                                subtrahend.A_X,
                                _Start_Y,
                                complementWidth,
                                complementHeight);
                        }
                    }
                    culW = (complement[0] != null ? complement[0].Width : 0) + (complement[1] != null ? complement[1].Width : 0) + (complement[2] != null ? complement[2].Width : 0);
                    culH = -A_Y + C_Y + 1;
                    break;
            }

            if (culW != Width || culH != Height)
            {
                throw new SystemException();
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


            int culW = 0, culH = 0;
            switch (corner)
            {
                case Corner.NW:
                    {
                        complement[0] = Rectangle.New(Color,
                            _Start_X,
                            subtrahend.D_Y + 1,
                            subtrahend.D_X - C_X + 1,
                            C_Y - subtrahend.D_Y);
                        complement[1] = Rectangle.New(Color,
                            subtrahend.D_X + 1,
                            _Start_Y,
                            D_X - subtrahend.D_X,
                            Height);
                    }
                    culW = -C_X + 1 + D_X;
                    culH = Height;
                    break;
                case Corner.NE:
                    {
                        complement[0] = Rectangle.New(Color,
                            _Start_X,
                            _Start_Y,
                            subtrahend.C_X - C_X,
                            Height);
                        complement[1] = Rectangle.New(Color,
                            subtrahend.C_X,
                            subtrahend.C_Y + 1,
                            D_X - subtrahend.C_X,
                            D_Y - subtrahend.C_Y);
                    }
                    culW = -C_X + D_X + 1;
                    culH = Height;
                    break;
                case Corner.SW:
                    {
                        complement[0] = Rectangle.New(Color,
                            _Start_X,
                            _Start_Y,
                            subtrahend.B_X - A_X + 1,
                            subtrahend.B_Y - A_Y);
                        complement[1] = Rectangle.New(Color,
                            subtrahend.B_X + 1,
                            _Start_Y,
                            B_X - subtrahend.B_X,
                            Height);
                    }
                    culW = -A_X + B_X + 1;
                    culH = (complement[0] != null ? complement[0].Height : 0) + C_Y - subtrahend.B_Y + 1;
                    break;
                case Corner.SE:
                    {
                        complement[0] = Rectangle.New(Color,
                            _Start_X,
                            _Start_Y,
                            subtrahend.A_X - A_X,
                            Height);
                        complement[1] = Rectangle.New(Color,
                            subtrahend.A_X,
                            _Start_Y,
                            B_X - subtrahend.A_X + 1,
                            subtrahend.A_Y - B_Y);
                    }
                    culW = (complement[0] != null ? complement[0].Width : 0) + (complement[1] != null ? complement[1].Width : B_X - subtrahend.A_X + 1);
                    culH = (complement[1] != null ? complement[1].Height : 0) + D_Y - subtrahend.A_Y + 1;
                    break;
            }

            if (culW != Width || culH != Height)
            {
                throw new SystemException();
            }

            return complement;
        }

        private Rectangle[] _Complement(Rectangle subtrahend, Crossing crossing)
        {
            Rectangle[] complement = new Rectangle[2];
            for (int i = 0; i < complement.Length; i++)
            {
                complement[i] = null;
            }

            int culW = 0, culH = 0;
            switch (crossing)
            {
                case Crossing.Horizontal:
                    {
                        complement[0] = Rectangle.New(Color,
                            _Start_X, _Start_Y,
                            subtrahend.A_X - A_X, Height);
                        complement[1] = Rectangle.New(Color,
                            subtrahend.B_X + 1, _Start_Y,
                            B_X - subtrahend.B_X, Height);
                    }
                    culW = (complement[0] != null ? complement[0].Width : 0) + (complement[1] != null ? complement[1].Width : 0) + subtrahend.Width;
                    culH = Height;
                    break;
                case Crossing.Vertical:
                    {
                        complement[0] = Rectangle.New(Color,
                            _Start_X, _Start_Y,
                            Width, subtrahend.A_Y - A_Y);
                        complement[1] = Rectangle.New(Color,
                            _Start_X, subtrahend.C_Y + 1,
                            Width, C_Y - subtrahend.C_Y);
                    }
                    culW = Width;
                    culH = (complement[0] != null ? complement[0].Height : 0) + (complement[1] != null ? complement[1].Height : 0) + subtrahend.Height;
                    break;
            }

            if (culW != Width || culH != Height)
            {
                throw new SystemException();
            }


            return complement;
        }

        private Rectangle[] _ComplementInverse(Rectangle subtrahend, Vertice vertice)
        {
            Rectangle[] complement = new Rectangle[1];

            int culW = 0, culH = 0;
            switch (vertice)
            {
                case Vertice.North:
                    {
                        complement[0] = Rectangle.New(Color,
                            _Start_X, subtrahend.C_Y + 1,
                            Width, C_Y - subtrahend.C_Y);
                    }
                    culW = complement[0].Width;
                    culH = (complement[0] != null ? complement[0].Height : 0) + subtrahend.C_Y - A_Y + 1;
                    break;
                case Vertice.West:
                    {
                        complement[0] = Rectangle.New(Color,
                            subtrahend.B_X + 1, _Start_Y,
                            B_X - subtrahend.B_X, Height);
                    }
                    culW = B_X - A_X + 1;
                    culH = complement[0].Height;
                    break;
                case Vertice.East:
                    {
                        complement[0] = Rectangle.New(Color,
                            _Start_X, _Start_Y,
                            subtrahend.A_X - A_X, Height);
                    }
                    culW = -A_X + B_X + 1;
                    culH = complement[0].Height;
                    break;
                case Vertice.South:
                    {
                        complement[0] = Rectangle.New(Color,
                            _Start_X, _Start_Y,
                            Width, subtrahend.A_Y - A_Y);
                    }
                    culW = complement[0].Width;
                    culH = (complement[0] != null ? complement[0].Height : 0) + C_Y - subtrahend.A_Y + 1;
                    break;
            }

            if (culW != Width || culH != Height)
            {
                throw new SystemException();
            }

            return complement;
        }

        public Rectangle[] Complement(Rectangle subtrahend)
        {
            if (subtrahend.IsPointInside(A_X, A_Y) &&
                subtrahend.IsPointInside(B_X, B_Y) &&
                subtrahend.IsPointInside(C_X, C_Y) &&
                subtrahend.IsPointInside(D_X, D_Y))
            {
                //This completely overlaps subtrahend
                return new Rectangle[]{null};
            }
            if (IsPointInside(subtrahend.A_X, subtrahend.A_Y) &&
                IsPointInside(subtrahend.B_X, subtrahend.B_Y) &&
                IsPointInside(subtrahend.C_X, subtrahend.C_Y) &&
                IsPointInside(subtrahend.D_X, subtrahend.D_Y))
            {
                //Subtrahend completely overlaps this
                return _Complement(subtrahend);
            }



            else if (A_Y >= subtrahend.A_Y && IsPointInside(subtrahend.A_X, A_Y) &&
                D_Y <= subtrahend.D_Y && IsPointInside(subtrahend.D_X, D_Y))
            {
                return _Complement(subtrahend, Crossing.Horizontal);
            }
            else if (A_X >= subtrahend.A_X && IsPointInside(A_X, subtrahend.A_Y) &&
                D_X <= subtrahend.D_X && IsPointInside(D_X, subtrahend.D_Y))
            {
                return _Complement(subtrahend, Crossing.Vertical);
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
