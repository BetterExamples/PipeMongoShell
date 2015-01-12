using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PipeMongoShell
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

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            txtOutput.Text = string.Empty;

            var psi = new ProcessStartInfo()
            {
                FileName = Environment.ExpandEnvironmentVariables(ConfigurationManager.AppSettings["MongoShell"]),
                Arguments = ConfigurationManager.AppSettings["MongoShellArguments"],
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                ErrorDialog = true
            };

            var process = Process.Start(psi);

            var inputWriter = process.StandardInput;
            var outputReader = process.StandardOutput;

            inputWriter.AutoFlush = true;

            inputWriter.WriteLine("use " + txtDatabase.Text + ";");
            inputWriter.WriteLine(Environment.NewLine + txtInput.Text + ";");

            inputWriter.Close();

            txtOutput.Text = outputReader.ReadToEnd();;
        }
    }
}
