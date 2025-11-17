

//Register your Syncfusion license key here
using Syncfusion.Pdf;
using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Parsing;

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("YOUR LICENSE KEY");

//This code example demonstrates how to flatten stamp annotations in a PDF document using Syncfusion PDF library.

//Load the existing PDF document
using (PdfLoadedDocument doc = new PdfLoadedDocument("../../../../data/customize-stamp.pdf"))
{
    //Get the first page of the document
    var page = (PdfLoadedPage)doc.Pages[0];

    //Iterate the annotations and get stamp annotations
    foreach (PdfLoadedAnnotation annotation in page.Annotations)
    {
        if (annotation is PdfLoadedRubberStampAnnotation stampAnnotation)
        {
            //Flatten the stamp annotation
            stampAnnotation.Flatten = true;
        }
    }
    //Save the document
    doc.Save("flattened-samp_-nnotation.pdf");
}

// This code example demonstrates how to remove stamp annotations in a PDF document using Syncfusion PDF library.
//Load the existing PDF document
using (PdfLoadedDocument doc = new PdfLoadedDocument("../../../../data/customize-stamp.pdf"))
{
    //Get the first page of the document
    var page = (PdfLoadedPage)doc.Pages[0];
    //Create a list to hold annotations to be removed
    List<PdfLoadedAnnotation> annotationsToRemove = new List<PdfLoadedAnnotation>();
    //Iterate the annotations and get stamp annotations
    foreach (PdfLoadedAnnotation annotation in page.Annotations)
    {
        if (annotation is PdfLoadedRubberStampAnnotation)
        {
            //Add the stamp annotation to the list for removal
            annotationsToRemove.Add(annotation);
        }
    }
    //Remove the stamp annotations from the page
    foreach (var annotation in annotationsToRemove)
    {
        page.Annotations.Remove(annotation);
    }
    //Save the document
    doc.Save("removed-stamp-annotation.pdf");
}

