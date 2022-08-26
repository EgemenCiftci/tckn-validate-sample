using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using TcknValidateSample.KPSPublicServiceReference;

namespace TcknValidateSample
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateRemoteCertificate);
        }

        private async void ValidateButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            long tckn = Convert.ToInt64(tbTckn.Text);
            string name = tbName.Text.Trim();
            string surname = tbSurname.Text.Trim();
            int birthYear = Convert.ToInt32(tbBirthYear.Text);

            KPSPublicSoapClient client = new KPSPublicSoapClient();
            TCKimlikNoDogrulaResponse response = await client.TCKimlikNoDogrulaAsync(tckn, name, surname, birthYear);

            _ = MessageBox.Show(response.Body.TCKimlikNoDogrulaResult ? "VALID" : "NOT VALID");
        }

        private static bool ValidateRemoteCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
        {
            _ = certificate.Issuer;
            _ = certificate.Subject;
            //_ = MessageBox.Show(string.Format("Issuer Name: {0}" + Environment.NewLine + "Subject Name: {1}", issuerName, subjectName));
            return true;
        }
    }
}
