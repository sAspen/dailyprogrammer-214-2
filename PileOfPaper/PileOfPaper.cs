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
        private List<Rectangle> OriginalRectangles;
        private Stack<Rectangle> ToBeAdded;

        public PileOfPaper(int width, int height)
        {
            Rectangles = new List<Rectangle>();
            OriginalRectangles = new List<Rectangle>();
            ToBeAdded = new Stack<Rectangle>();

            OriginalRectangles.Add(Rectangle.New(0, 0, 0, width, height));
        }

        public void Add(Rectangle r)
        {
            Colors = Math.Max(Colors, r.Color);
            OriginalRectangles.Add(r);
        }

        public void Construct()
        {
            //OriginalRectangles = OriginalRectangles.OrderBy(r => r.Color).ToList();
            //Colors = OriginalRectangles.Last().Color;
            foreach (Rectangle r in OriginalRectangles)
            {
                ToBeAdded.Push(r);
            }

            Rectangles.Add(ToBeAdded.Pop());

            while (ToBeAdded.Count > 0)
            {
                bool isSplit = false;
                Rectangle cur = ToBeAdded.Pop();

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
                                ToBeAdded.Push(complement[i]);
                            }
                        }

                        break;
                    }
                }

                if (!isSplit)
                {
                    Rectangles.Add(cur);
                }
            }

            /*
            int width = OriginalRectangles.First().Width;
            int height = OriginalRectangles.First().Height;
            foreach (Rectangle r in Rectangles)
            {
                if (r.A_X < 0 || r.D_X >= width ||
                    r.A_Y < 0 || r.D_Y >= height)
                {
                    throw new SystemException();
                }
            }
            */
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
