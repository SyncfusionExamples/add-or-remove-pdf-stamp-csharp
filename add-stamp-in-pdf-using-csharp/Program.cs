using Syncfusion.Drawing;
using Syncfusion.Pdf;
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
    PdfRubberStampAnnotation rubberStamp = new PdfRubberStampAnnotation(new RectangleF(390, 480, 163, 49), "APPROVED");

    //Set the standard stamp icon
    rubberStamp.Icon = PdfRubberStampAnnotationIcon.Approved;

    //Set the appearance as true to view the stamp icon across PDF viewers
    rubberStamp.SetAppearance(true);

    //Add annotation to the page
    page.Annotations.Add(rubberStamp);

    //Save the document
    doc.Save("stamp-pdf.pdf");
}