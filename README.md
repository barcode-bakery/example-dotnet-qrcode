<p align="center"><a href="https://www.barcodebakery.com" target="_blank">
    <img src="https://www.barcodebakery.com/images/BCG-Logo-SQ-GitHub.svg">
</a></p>

This example repository will allow you to generate QRCode barcodes. You can find more information on our [Barcode Bakery website][1].

This is based on the [barcode QRCode][2] library.

The library is not free, you must [purchase a license][3] in order to obtain it.

Installation
------------
There are two ways to install our library:

* With the command line, get the file from GitHub, run the following command.
```
PM> dotnet add PROJECT package BarcodeBakery.BarcodeQRCode --version 3.0.1
```
* Or, download the library on our [website][3], and follow our [developer's guide][4].

Requirements
------------
* .NET Standard 2.0
* .NET Core 2.0+
* .NET Framework 4.6.1+

Example usages
--------------
For a full example of how to use each symbology type, visit our [API page][5].

### Saving a QRCode to a `MemoryStream`
```csharp
public static async Task CreateAsync(string filePath, string? text = null)
{
    // Loading Font
    var font = new BCGFont("Arial", 18);

    // Don't forget to sanitize user inputs
    text = text?.Length > 0 ? text : "QRCode";

    // Label, this part is optional
    var label = new BCGLabel();
    label.SetFont(font);
    label.SetPosition(BCGLabel.Position.Bottom);
    label.SetAlignment(BCGLabel.Alignment.Center);
    label.SetText(text);

    // The arguments are R, G, B for color.
    var colorBlack = new BCGColor(0, 0, 0);
    var colorWhite = new BCGColor(255, 255, 255);

    Exception? drawException = null;
    BCGBarcode? barcode = null;
    try
    {
        var code = new BCGqrcode();
        code.SetScale(3);
        code.SetSize(BCGqrcode.Size.Full);
        code.SetErrorLevel('M');
        code.SetMirror(false);
        code.SetQuietZone(true);
        code.SetForegroundColor(colorBlack); // Color of bars
        code.SetBackgroundColor(colorWhite); // Color of spaces

        code.AddLabel(label);

        code.Parse(text);
        barcode = code;
    }
    catch (Exception exception)
    {
        drawException = exception;
    }

    var drawing = new BCGDrawing(barcode, colorWhite);
    if (drawException != null)
    {
        drawing.DrawException(drawException);
    }

    // Saves the barcode into a MemoryStream
    var memoryStream = new System.IO.MemoryStream();
    await drawing.FinishAsync(BCGDrawing.ImageFormat.Png, memoryStream);
}
```

### Saving the image to a file
Replace the last line of the previous code with the following:
```csharp
// Saves the barcode into a file.
await drawing.FinishAsync(BCGDrawing.ImageFormat.Png, filePath);
```

This will generate the following:
<br />
<img src="https://www.barcodebakery.com/images/qrcode-github.png">


[1]: https://www.barcodebakery.com
[2]: https://www.barcodebakery.com/en/docs/dotnet/barcode/qrcode/api
[3]: https://www.barcodebakery.com/en/purchase
[4]: https://www.barcodebakery.com/en/docs/dotnet/barcode/qrcode/download
[5]: https://www.barcodebakery.com/en/docs/dotnet/guide
