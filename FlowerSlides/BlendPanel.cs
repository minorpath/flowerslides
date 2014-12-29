using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

/// <summary>
/// Equivalent to PictureBoxSizeMode
/// </summary>
public enum BlendPanelSizeMode
{
    //
    // Summary:
    //     The size of the image is increased or decreased maintaining the size ratio.
    Zoom = 4
}
// Based on:
// http://stackoverflow.com/questions/3270919/transition-of-images-in-windows-forms-picture-box
public class BlendPanel : Panel
{
    private Image mImg1;
    private Image mImg2;
    private float mBlend;
    public BlendPanelSizeMode SizeMode { get; set; }
    public BlendPanel()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
        SizeMode = BlendPanelSizeMode.Zoom;
    }
    public Image Image1
    {
        get { return mImg1; }
        set { mImg1 = value; Invalidate(); }
    }
    public Image Image2
    {
        get { return mImg2; }
        set { mImg2 = value; Invalidate(); }
    }
    public float Blend
    {
        get { return mBlend; }
        set { mBlend = value; Invalidate(); }
    }
    protected override void OnPaint(PaintEventArgs e)
    {
        if (mImg1 == null && mImg2 == null)
        {
            e.Graphics.FillRectangle(new SolidBrush(this.BackColor), new Rectangle(0, 0, this.Width, this.Height));
        }
        else if (mImg2 == null)
        {
            var rc1 = ResizedRect(mImg1);
            e.Graphics.DrawImage(mImg1, rc1, 0, 0, mImg1.Width, mImg1.Height, GraphicsUnit.Pixel);
        }
        else
        {
            var rc1 = ResizedRect(mImg1);
            var rc2 = ResizedRect(mImg2);
            var cm = new ColorMatrix() { Matrix33 = mBlend };
            var ia = new ImageAttributes();
            ia.SetColorMatrix(cm);
            e.Graphics.DrawImage(mImg2, rc2, 0, 0, mImg2.Width, mImg2.Height, GraphicsUnit.Pixel, ia);
            cm.Matrix33 = 1F - mBlend;
            ia.SetColorMatrix(cm);
            e.Graphics.DrawImage(mImg1, rc1, 0, 0, mImg1.Width, mImg1.Height, GraphicsUnit.Pixel, ia);
        }
        base.OnPaint(e);
    }

    private Rectangle ResizedRect(Image img)
    {
        float f = (float)this.Height / img.Height;
        int width = (int)(img.Width * f);
        int x = width < this.Width ?
            (this.Width - width) / 2 : 0;

        return new Rectangle(x, 0, width, this.Height);
    }
}