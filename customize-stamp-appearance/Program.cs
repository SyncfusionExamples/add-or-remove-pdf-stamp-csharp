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
    PdfRubberStampAnnotation draftStamp = new PdfRubberStampAnnotation(new RectangleF(85, 567, 150, 49))
    {
        //Set the author name
        Author = "John Smith",
        //Set the subject
        Subject = "Document Approval",
        //Set the opacity
        Opacity = 0.75f,
        //Set the stamp text
        Text = "Draft By John",
        //Set the stamp icon
        Icon = PdfRubberStampAnnotationIcon.Draft
    };

    //Set the appearance as true to view the stamp icon across PDF viewers
    draftStamp.SetAppearance(true);

    //Add annotation to the page
    page.Annotations.Add(draftStamp);

    //Create a rubberstamp annotation
    PdfRubberStampAnnotation rejectedStamp = new PdfRubberStampAnnotation(new RectangleF(296, 567, 200, 49))
    {
        //Set the author name
        Author = "Milton",
        //Set the subject
        Subject = "Document Approval",      
        //Set the stamp text
        Text = "Rejected By Milton",
        //Set the stamp icon
        Icon = PdfRubberStampAnnotationIcon.NotApproved
    };

    //Set the appearance as true to view the stamp icon across PDF viewers
    rejectedStamp.SetAppearance(true);

    //Add annotation to the page
    page.Annotations.Add(rejectedStamp);

    //Rotate the stamp annotation
    PdfRubberStampAnnotation confidentialStamp = new PdfRubberStampAnnotation(new RectangleF(202, 260, 260, 260))
    {
        //Set the author name
        Author = "Sarah",
        //Set the subject
        Subject = "Confidential",
        //Set the stamp text
        Text = "Confidential By Sarah",       
    };

    //Get stamp appearance graphics
    PdfGraphics graphics = confidentialStamp.Appearance.Normal.Graphics;

    //Create a font
    PdfFont font = new PdfTrueTypeFont("../../../../data/MomoTrustDisplay-Regular.ttf", 38, PdfFontStyle.Regular);

    //Create a brush
    PdfBrush brush = new PdfSolidBrush(Color.Red);

    //Create a pen
    PdfPen pen = new PdfPen(Color.DarkBlue, 2);

    graphics.Save();

    //Move the graphics origin to center of the stamp
    graphics.TranslateTransform(confidentialStamp.Bounds.Width/2, confidentialStamp.Bounds.Height/2);  

    //Rotate the graphics
    graphics.RotateTransform(-45);
   
    //Measure the text size
    SizeF textSize = font.MeasureString("CONFIDENTIAL");

    PointF location = new PointF(-textSize.Width / 2, -textSize.Height / 2);

    //Draw the text
    graphics.DrawString("CONFIDENTIAL", font, pen, brush, location);

    //Draw rounded corner rectangle
    DrawRoundedCornerRectangle(new RectangleF(location.X - 5, location.Y -5, textSize.Width + 10, textSize.Height + 10), graphics, 5, pen);


    graphics.Restore();

    //Add annotation to the page
    page.Annotations.Add(confidentialStamp);

    //Save the document
    doc.Save("customize-stamp.pdf");
}

void DrawRoundedCornerRectangle(RectangleF bounds, PdfGraphics graphics, int cornerRadius, PdfPen borderPen)
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
    graphics.DrawPath(borderPen, path);
}
