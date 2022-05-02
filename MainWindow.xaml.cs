using NReco.VideoConverter;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace mp4tomp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string output = _output.Text;
                string input = _input.Text;
                convert(input, output);

            }
            catch  (Exception ex){ MessageBox.Show(ex.ToString()); }
        }

        private void ImagePanel_Drop(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                // Assuming you have one file that you care about, pass it off to whatever
                // handling code you have defined.

                try {
                    convert(files[0], files[0].Replace(".mp4", "") + ".mp3");
                }
                catch { }
            }
        }

        private void convert(string input, string output)
        {

            //ConvertSettings cs = new ConvertSettings();
            //FFMpegConverter converter = new FFMpegConverter();
            //converter.ConvertMedia(input, output, ".mp3"); //".mp4", cs

            var mp3out = "";
            var ffmpegProcess = new Process();
            ffmpegProcess.StartInfo.UseShellExecute = false;
            ffmpegProcess.StartInfo.RedirectStandardInput = true;
            ffmpegProcess.StartInfo.RedirectStandardOutput = true;
            ffmpegProcess.StartInfo.RedirectStandardError = true;
            ffmpegProcess.StartInfo.CreateNoWindow = true;
            ffmpegProcess.StartInfo.FileName = "ffmpeg.exe";
            ffmpegProcess.StartInfo.Arguments = " -i \"" + input + "\" \"" + output + "\" "; //-vn -f mp3 -ab 320k output 
            ffmpegProcess.Start();
            ffmpegProcess.StandardOutput.ReadToEnd();
            mp3out = ffmpegProcess.StandardError.ReadToEnd();
            ffmpegProcess.WaitForExit();
            if (!ffmpegProcess.HasExited)
            {
                ffmpegProcess.Kill();
            }
        }


    }
}
