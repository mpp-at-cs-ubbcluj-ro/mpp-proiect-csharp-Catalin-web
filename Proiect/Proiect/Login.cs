using Proiect.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect
{
    public partial class Login : Form
    {
        private string email;
        private string parola;
        private ITripClient client;

        public Login(ITripClient client)
        {
            InitializeComponent();
            this.client = client;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            email = t.Text; 
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            parola = t.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            client.login(email, parola);
            showMainForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            client.authentificare(email, parola);
            showMainForm();
        }

        private void showMainForm()
        {
            Main main = new Main(client);
            main.Show();
            Close();
        }
    }
}
