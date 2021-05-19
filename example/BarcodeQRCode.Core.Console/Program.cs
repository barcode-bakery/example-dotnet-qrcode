using System.Threading.Tasks;

namespace BarcodeQRCode.Core.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // See each barcode file to see how you can save to a file or a MemoryStream.
            await ExampleQRCode.CreateAsync("barcode_qrcode.png");
        }
    }
}
