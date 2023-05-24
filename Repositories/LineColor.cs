using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bintronic_Inspect.Repositories
{
    internal class LineColor
    {
        public Color getColor(Color nowClolr)
        {
            if (nowClolr == Color.FromArgb(255, 150, 150, 150)) //灰
            {
                return Color.FromArgb(255, 200, 30, 30); //紅
            }
            else if (nowClolr == Color.FromArgb(255, 200, 30, 30))
            {
                return Color.FromArgb(255, 200, 100, 30); //橙
            }
            else if (nowClolr == Color.FromArgb(255, 200, 100, 30))
            {
                return Color.FromArgb(255, 200, 200, 30); //黃
            }
            else if (nowClolr == Color.FromArgb(255, 200, 200, 30))
            {
                return Color.FromArgb(255, 100, 200, 30); //綠
            }
            else if (nowClolr == Color.FromArgb(255, 100, 200, 30))
            {
                return Color.FromArgb(255, 30, 200, 30); //藍
            }
            else if (nowClolr == Color.FromArgb(255, 30, 200, 30))
            {
                return Color.FromArgb(255, 30, 200, 100); //藍
            }
            else if (nowClolr == Color.FromArgb(255, 30, 200, 100))
            {
                return Color.FromArgb(255, 30, 200, 200); //
            }
            else if (nowClolr == Color.FromArgb(255, 30, 200, 200))
            {
                return Color.FromArgb(255, 30, 100, 200); //
            }
            else if (nowClolr == Color.FromArgb(255, 30, 100, 200))
            {
                return Color.FromArgb(255, 30, 30, 200); //
            }
            else if (nowClolr == Color.FromArgb(255, 30, 30, 200))
            {
                return Color.FromArgb(255, 100, 30, 200); //
            }
            else if (nowClolr == Color.FromArgb(255, 100, 30, 200))
            {
                return Color.FromArgb(255, 200, 30, 200); //
            }
            else if (nowClolr == Color.FromArgb(255, 200, 30, 200))
            {
                return Color.FromArgb(255, 200, 30, 100); //
            }
            else if (nowClolr == Color.FromArgb(255, 200, 30, 100))
            {
                return Color.FromArgb(255, 0, 0, 0); //
            }
            else if (nowClolr == Color.FromArgb(255, 0, 0, 0))
            {
                return Color.FromArgb(255, 255, 255, 255); //
            }
            else if (nowClolr == Color.FromArgb(255, 255, 255, 255))
            {
                return Color.FromArgb(255, 150, 150, 150); //灰
            }
            else
            {
                return Color.FromArgb(255, 150, 150, 150); //灰
            }
        }
    }
}
