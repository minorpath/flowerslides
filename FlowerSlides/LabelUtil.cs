using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowerSlides
{
    class LabelUtil
    {
        public static Label CreateTitle(string text)
        {
            return CreateLabel(text, 30F, Globals.LightText);
        }

        internal static Label CreateSubTitle(string text)
        {
            return CreateLabel(text, 16F, Globals.LightText, FontStyle.Bold);
        }

        internal static Label CreateLabel(string text, float emSize, Color foreColor, FontStyle fontStyle = FontStyle.Regular)
        {
            return new Label
            {
                AutoSize = true,
                Text = text,
                Font = new Font("Segoe UI Light", emSize, fontStyle, GraphicsUnit.Point),
                ForeColor = foreColor
            };
        }
    }
}
