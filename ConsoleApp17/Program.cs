using Aspose.Pdf;
using Aspose.Pdf.Forms;
// преодолеваем лицуху
LicenseHelper.ModifyInMemory.ActivateMemoryPatching();

// ставим подписи
PdfSignerLegacy.AddMultipleSignatures();

//using (Document document = new Document("2.pdf"))
//{

//    // Инкрементное сохранение (ключевой момент!)
//    document.Save("3.pdf", new PdfSaveOptions {  });
//}
