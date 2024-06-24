using System.Collections.Generic;
using System.Drawing;

namespace GST.Gearshift.Components.Utilities.ReportPrinting
{
  public class ResultsViewerFooterPrintBlock : PrintBlock
  {
    Font font = new Font( "Tahoma", 9, GraphicsUnit.Point );

    public override SizeF GetSize( Graphics g, DocumentMetrics metrics )
    {
      return g.MeasureString( FooterText + " X Of Y", font );
    }

    public string FooterText = "Page";

    public override void Draw( System.Drawing.Graphics g, Dictionary<CodeEnum, string> codes )
    {
      StringFormat format = new StringFormat();
      format.Trimming = StringTrimming.Word;
      format.FormatFlags = StringFormatFlags.NoWrap;
      format.Alignment = StringAlignment.Far;

      g.DrawString(
          string.Format( FooterText + " {0} Of {1}", codes[CodeEnum.SheetNumber], codes[CodeEnum.SheetsCount] ),
          font,
          new SolidBrush( Color.Black ),
          Rectangle,
          format );
    }
  }
}
