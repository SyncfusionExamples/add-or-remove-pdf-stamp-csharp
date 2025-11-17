using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Parsing;

//Register your Syncfusion license key here
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("YOUR LICENSE KEY");

//Load the existing PDF document
using (PdfLoadedDocument doc = new PdfLoadedDocument("../../../../data/input.pdf"))
{
    //Get the first page of the document
    var page = (PdfLoadedPage)doc.Pages[0];

    //Create a rubberstamp annotation
    PdfRubberStampAnnotation rubberStamp = new PdfRubberStampAnnotation(new RectangleF(390, 480, 163, 49), "dynamic-stamp");

    //Draw the dynamic stamp content

    //Get the graphics of the stamp appearance
    PdfGraphics graphics = rubberStamp.Appearance.Normal.Graphics;

    //Create background brush and border pen
    PdfSolidBrush backgroundBrush = new PdfSolidBrush(new PdfColor(229,239,224));
    PdfPen borderPen = new PdfPen(new PdfColor(65, 106, 25));

    DrawRoundedCornerRectangle(new RectangleF(2, 1, 159, 47), graphics, 3, backgroundBrush, borderPen);

    //Create font for stamp text
    PdfStandardFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

    //Create font for date
    PdfStandardFont dateFont = new PdfStandardFont(PdfFontFamily.Helvetica, 10);

    //create brush for text
    PdfSolidBrush textBrush = new PdfSolidBrush(new PdfColor(65, 106, 25));

    //Draw the stamp text
    graphics.DrawString("APPROVED", font, textBrush, new PointF(10, 10));

    //Draw the date text
    graphics.DrawString("on " + System.DateTime.Now.ToString(), dateFont, textBrush, new PointF(10, 33));    

    //Add annotation to the page
    page.Annotations.Add(rubberStamp);

    //Save the document
    doc.Save("dynamic-stamp-pdf.pdf");
}
//Draw the rounded corner rectangle
void DrawRoundedCornerRectangle(RectangleF bounds, PdfGraphics graphics, int cornerRadius, PdfBrush brush, PdfPen borderPen)
{
    //Create the PdfPath 
    int diameter = cornerRadius * 2;
    SizeF size = new SizeF(diameter, diameter);
    RectangleF arc = new RectangleF(bounds.Location, size);
    PdfPath path = new PdfPath();

    path.AddArc(arc, 180, 90);
    arc.X = bounds.Right - diameter;

    path.AddArc(arc, 270, 90);
    arc.Y = bounds.Bottom - diameter;

    path.AddArc(arc, 0, 90);
    arc.X = bounds.Left;

    path.AddArc(arc, 90, 90);
    path.CloseFigure();
    graphics.DrawPath(borderPen, brush, path);    
}
