using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private static string path = "D:/Json.txt";
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string text = "";
            // асинхронное чтение
            using (StreamReader reader = new StreamReader(path))
            {
                text = await reader.ReadToEndAsync();
            }
            JavaScriptSerializer ser = new JavaScriptSerializer();
            var records = ser.Deserialize<List<Client>>(text);
            if (records == null)
            {
                records = new List<Client>();
            }
            records.Add(new Client(textBox1.Text, textBox2.Text));

            string json = JsonSerializer.Serialize <List<Client>>(records);

            using (StreamWriter writer = new StreamWriter(path, false))
            {
                await writer.WriteLineAsync(json);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string text = "";
            using (StreamReader reader = new StreamReader(path))
            {
                text = await reader.ReadToEndAsync();
            }
            JavaScriptSerializer ser = new JavaScriptSerializer();
            var records = ser.Deserialize<List<Client>>(text);
            if ( records == null)
            {
                return;
            }
            foreach (Client client in records)
            {
                listBox1.Items.Add(client.getNameAndYear());
            }
        }
    }

    public class Client
    {
        public String Fio { get; set; }
        public String Year { get; set; }

        public Client()
        {
        }

        public Client(String fio, String year)
        {
            this.Fio = fio;
            this.Year = year;
        }

        public String getNameAndYear() 
        {
            return "Year: " + Year + "; FIO: " + this.Fio;
        }
    }
}
