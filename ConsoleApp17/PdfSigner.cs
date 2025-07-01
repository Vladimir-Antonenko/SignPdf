using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

public class PdfSignerLegacy
{
    public static void AddMultipleSignatures()
    {
        // Сертификаты и пароли
        string certPath1 = "signature1.pfx";
        string password1 = "YourPassword123!";
        string certPath2 = "signature2.pfx";
        string password2 = "YourPassword123!";

        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf("2.pdf");
            //create a rectangle for signature location
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(310, 45, 200, 50);

            //create any of the three signature types
            PKCS7 pkcs = new PKCS7(certPath1, password1);

            // sign the PDF file
            pdfSign.Sign(1, true, rect, pkcs);
            //save output PDF file
            pdfSign.Save("signed.pdf");
        }

        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf("signed.pdf");
            //create a rectangle for signature location
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 300, 400);

            //create any of the three signature types
            PKCS7 pkcs = new PKCS7(certPath1, password1);

            // sign the PDF file
            pdfSign.Sign(1, true, rect, pkcs);
            //save output PDF file
            pdfSign.Save("signed2.pdf");
        }
        PdfSignatureValidator.ValidateSignatures("signed2.pdf");       
    }
}

public class PdfSignatureValidator
{
    public static void ValidateSignatures(string filePath)
    {
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            // Загрузка подписанного PDF
            pdfSign.BindPdf(filePath);

            // Получение всех подписей в документе
            var signatureNames = pdfSign.GetSignNames();

            if (signatureNames.Count == 0)
            {
                Console.WriteLine("Документ не содержит подписей");
                return;
            }

            Console.WriteLine($"Проверка документа: {System.IO.Path.GetFileName(filePath)}");
            Console.WriteLine("========================================");

            bool allValid = true;
            int signatureNumber = 1;

            foreach (string name in signatureNames)
            {
                Console.WriteLine($"\nПодпись #{signatureNumber++}");
                Console.WriteLine($"Имя подписи: {name}");

                try
                {
                    // Проверка валидности подписи
                    bool isValid = pdfSign.VerifySignature(name);
                    allValid &= isValid;

                    Console.WriteLine($"Статус: {(isValid ? "VALID" : "INVALID")}");

                    // Дополнительная информация о подписи
                    if (pdfSign.IsContainSignature() && !string.IsNullOrEmpty(name))
                    {
                       // Console.WriteLine($"Дата подписания: {pdfSign.GetSignDate(name)}");
                        Console.WriteLine($"Причина: {pdfSign.GetReason(name)}");
                        Console.WriteLine($"Место: {pdfSign.GetLocation(name)}");
                        Console.WriteLine($"Контакт: {pdfSign.GetContactInfo(name)}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при проверке: {ex.Message}");
                    allValid = false;
                }
            }

            Console.WriteLine("\n========================================");
            Console.WriteLine($"Всего подписей: {signatureNames.Count}");
            Console.WriteLine($"Все подписи валидны: {(allValid ? "ДА" : "НЕТ")}");
        }
    }
}