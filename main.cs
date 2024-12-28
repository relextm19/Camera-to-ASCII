using System.Drawing;
using AForge.Video;
using AForge.Video.DirectShow;

class CamereToASCII{
    private static FilterInfoCollection CaptureDevice;
    private static VideoCaptureDevice ChosenDevice;

    static void Main(string[] args){
        CaptureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        ChosenDevice = new VideoCaptureDevice();

        ChosenDevice = new VideoCaptureDevice(CaptureDevice[0].MonikerString);
        ChosenDevice.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
        ChosenDevice.Start();
    }

    static void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs){
        // string greyScaleSymbols = "@B%8WM#*oahkbdpwmZO0QCJYXzcvunxrjft/|()1{}[]?-_+~<>i!lI;:,\"^`'. ";
        string greyScaleSymbols = "@%#*+=-:. ";

        Bitmap video = (Bitmap)eventArgs.Frame.Clone();
        int videoWidth = video.Width;
        int videoHeight = video.Height;

        Console.Clear();
        for(int i = 0; i < videoHeight; i+=12){
            for(int j = 0; j < videoWidth; j+=12){
                Color pixelColor = video.GetPixel(i, j);
                int red = pixelColor.R;
                int green = pixelColor.G;
                int blue = pixelColor.B;
                int grayScaleColor = (int)((red * 0.3) + (green * 0.59) + (blue * 0.11));

                int symbolIndex = grayScaleColor * greyScaleSymbols.Length / 256;
                System.Console.Write(greyScaleSymbols.ElementAt(symbolIndex));
            }
            System.Console.WriteLine();
        }
    }
}
