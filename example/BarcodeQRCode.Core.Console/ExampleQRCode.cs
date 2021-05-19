﻿using BarcodeBakery.Barcode;
using BarcodeBakery.Common;
using System;
using System.Threading.Tasks;

namespace BarcodeQRCode.Core.Console
{
    public class ExampleQRCode
    {
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

            // Saves the barcode into a file.
            await drawing.FinishAsync(BCGDrawing.ImageFormat.Png, filePath);

            // Saves the barcode into a MemoryStream
            ////var memoryStream = new System.IO.MemoryStream();
            ////await drawing.FinishAsync(BCGDrawing.ImageFormat.Png, memoryStream);
        }
    }
}
