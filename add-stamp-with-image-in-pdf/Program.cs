
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

    // Position/size (points). Adjust to your layout.
    RectangleF bounds = new RectangleF(250, 162, 100, 100);

    //Create rubber stamp annotation
    PdfRubberStampAnnotation stampAnnotation = new PdfRubberStampAnnotation(bounds, "image stamp");

    //Set author
    stampAnnotation.Author = "John Smith";

    //Get the appearance graphics of the stamp annotation
    PdfGraphics graphics = stampAnnotation.Appearance.Normal.Graphics;

    //Draw the image in the appearance graphics
    PdfBitmap stampImage = new PdfBitmap(new FileStream("../../../../data/stamp.png", FileMode.Open, FileAccess.Read));

    //Draw the image to the stamp appearance
    graphics.DrawImage(stampImage, new RectangleF(0, 0, bounds.Width, bounds.Height));

    //Add annotation to the page
    page.Annotations.Add(stampAnnotation);

    //Save the document
    doc.Save("image-stamp.pdf");
}