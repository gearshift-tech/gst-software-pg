using System.Collections.Generic;
using System.Drawing;


namespace GST.Gearshift.Components.Utilities.ReportPrinting
{
  public class ResultsViewerHeaderPrintBlock : PrintBlock
  {
    public override SizeF GetSize( Graphics g, DocumentMetrics metrics )
    {
      return new SizeF( 0, 0);//metrics.PrintAbleWidth, 20 + 2 ); //+2 for spacing with document
    }

    public override void Draw( System.Drawing.Graphics g, Dictionary<CodeEnum, string> codes )
    {
      //GraphicsUnit units = GraphicsUnit.Pixel;
      //RectangleF rec = GST.Gearshift.Components.Properties.Resources.logo.GetBounds(ref units);

      //float scale = imgHeight / rec.Height;
      // g.DrawImage(Properties.Resources.logo, new RectangleF(Rectangle.X, Rectangle.Y, rec.Width * scale, imgHeight));
    }
  }
}
