using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PileOfPaper
{
    class PileOfPaper
    {
        private int Colors;
        List<Rectangle> Rectangles;
        private Stack<Rectangle> toBeAdded;

        public PileOfPaper(int width, int height)
        {
            Rectangles = new List<Rectangle>();
            toBeAdded = new Stack<Rectangle>();

            toBeAdded.Push(new Rectangle(0, 0, 0, width, height));
        }

        public void Add(Rectangle r)
        {
            Colors = Math.Max(Colors, r.Color);
            toBeAdded.Push(r);
        }

        public void Construct()
        {
            Rectangles.Add(toBeAdded.Pop());

            while (toBeAdded.Count > 0)
            {
                bool isSplit = false;
                Rectangle cur = toBeAdded.Pop();

                foreach (Rectangle r in Rectangles)
                {
                    Rectangle[] complement = cur.Complement(r);
                    if (complement != null)
                    {
                        isSplit = true;

                        for (int i = 0; i < complement.Length; i++)
                        {
                            if (complement[i] != null)
                            {
                                toBeAdded.Push(complement[i]);
                            }
                        }

                        break;
                    }


                    /*if (r.IsPointInside(cur.A_X, cur.A_Y)) 
                    {
                        int split_W = cur.Width - (r.D_X - cur.A_X);
                        int split_H = cur.Height - (r.D_Y - cur.A_Y);
                        
                        if (split_W > 0)
                        {
                            toBeAdded.Push(new Rectangle(cur.Color, 
                                r.D_X + 1, cur.A_Y, split_W, cur.Height));
                        }
                        if (split_H > 0)
                        {
                            toBeAdded.Push(new Rectangle(cur.Color, 
                                cur.A_X, r.D_Y + 1, cur.Width, split_H));
                        }

                        isSplit = true;
                        break;
                    } 
                    if (r.IsPointInside(cur.B_X, cur.B_Y))
                    {
                        int split_W = cur.Width - (r.C_X - cur.B_X);
                        int split_H = cur.Height - (r.C_Y - cur.B_Y);

                        if (split_W > 0)
                        {
                            toBeAdded.Push(new Rectangle(cur.Color,
                                r.C_X + 1, cur.B_Y, split_W, cur.Height));
                        }
                        if (split_H > 0)
                        {
                            toBeAdded.Push(new Rectangle(cur.Color,
                                cur.B_X, r.C_Y + 1, cur.Width, split_H));
                        }

                        isSplit = true;
                        break;
                    }
                    if (r.IsPointInside(cur.C_X, cur.C_Y))
                    {
                        int split_W = cur.Width - (r.B_X - cur.C_X);
                        int split_H = cur.Height - (r.B_Y - cur.C_Y);

                        if (split_W > 0)
                        {
                            toBeAdded.Push(new Rectangle(cur.Color,
                                r.B_X + 1, cur.C_Y, split_W, cur.Height));
                        }
                        if (split_H > 0)
                        {
                            toBeAdded.Push(new Rectangle(cur.Color,
                                cur.C_X, r.B_Y + 1, cur.Width, split_H));
                        }

                        isSplit = true;
                        break;
                    }
                    if (r.IsPointInside(cur.D_X, cur.D_Y))
                    {
                        int split_W = cur.Width - (r.A_X - cur.D_X);
                        int split_H = cur.Height - (r.A_Y - cur.D_Y);

                        if (split_W > 0)
                        {
                            toBeAdded.Push(new Rectangle(cur.Color,
                                r.A_X + 1, cur.D_Y, split_W, cur.Height));
                        }
                        if (split_H > 0)
                        {
                            toBeAdded.Push(new Rectangle(cur.Color,
                                cur.D_X, r.A_Y + 1, cur.Width, split_H));
                        }

                        isSplit = true;
                        break;
                    }*/
                }

                if (!isSplit)
                {
                    Rectangles.Add(cur);
                }
            }
        }

        public override string ToString()
        {
            String s = "";

            int[] colorAreas = new int[Colors + 1];
            for (int i = 0; i < colorAreas.Length; i++)
			{
                colorAreas[i] = 0;
			}

            foreach (Rectangle r in Rectangles)
            {
                colorAreas[r.Color] += r.Width * r.Height;
            }

            for (int i = 0; i < colorAreas.Length; i++)
            {
                s += i + " " + colorAreas[i] + "\n";
            }

            s = s.Substring(0, s.Length - 1);

            return s;
        }
    }
}
