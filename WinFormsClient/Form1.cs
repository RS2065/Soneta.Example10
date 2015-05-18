using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WinFormsClient.ServiceReference1;

namespace WinFormsClient {
    public partial class Form1 : Form {

        private const string DbName = "konferencja2015";
        private const string ServiceName = "Soneta.Examples.Example10.Extender.ICennikSerwis, Soneta.Examples";

        public Form1() {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e) {
            var client = new BusinessProviderClient();
            var result = await client.InvokeServiceMethodAsync(new ServiceMethodInvokerParams {
                DatabaseHandle = DbName,
                Operator = "Administrator",
                Password = "",
                ServiceName = ServiceName,
                MethodName = "ImportujCennik",
                MethodArgs = new Dictionary<string, object>()
            });
            if (result.IsException) {
                textBox1.ForeColor = Color.DarkRed;
                textBox1.Text = "-- Błąd wywołania metody serwisu --";
            }
            else {
                textBox1.ForeColor = Color.DarkGreen;
                textBox1.Text = result.ResultInstance.ToString();
                button2.Enabled = true;
            }
        }

        private async void button2_Click(object sender, EventArgs e) {
            var client = new BusinessProviderClient();
            var result = await client.InvokeServiceMethodAsync(new ServiceMethodInvokerParams {
                DatabaseHandle = DbName,
                Operator = "Administrator",
                Password = "",
                ServiceName = ServiceName,
                MethodName = "EksportujCennik",
                MethodArgs = new Dictionary<string, object> {
                    {"tsvContent", textBox1.Text }
                }
            });

            textBox1.Text = "";
            if (result.IsException) {
                MessageBox.Show(this, result.ExceptionMessage, "Eksport cennika", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else {
                MessageBox.Show(this, "Wysyłanie cennika do serwisu zakończone", "Eksport cennika", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        //void PobierzCennik_MethodCompleted(object sender, InvokeServiceMethodCompletedEventArgs e) {
        //    textBox1.Text = e.Result.ResultInstance.ToString();
        //    textBox2.Text = e.Result.ResultInstance.ToString();
        //    button2.Enabled = true;
        //}

        //void WyslijCennik_MethodCompleted(object sender, InvokeServiceMethodCompletedEventArgs e) {
        //    MessageBox.Show(this, "Wysyłanie cennika do serwisu zakończone", "Eksport cennika", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //}

        private void textBox2_TextChanged(object sender, EventArgs e) {
            button2.Enabled = true;
        }
    }
}
