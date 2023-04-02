using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Proiect
{
    public partial class Main : Form
    {
        private Service srv;
        private List<Excursie> lista2 = new List<Excursie>();
        private Excursie excursieSelectata;
        public Main(Service srv)
        {
            this.srv = srv;
            InitializeComponent();
            initializeTable1();
            initializeTable2();
            loadTable1();
        }

        private void initializeTable1()
        {
            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "Nume obiectiv";
            dataGridView1.Columns[1].Name = "Nume firma transport";
            dataGridView1.Columns[2].Name = "Ora plecarii";
            dataGridView1.Columns[3].Name = "Pret";
            dataGridView1.Columns[4].Name = "Numar locuri disponibile";
        }

        private void initializeTable2()
        {
            dataGridView2.ColumnCount = 4;
            dataGridView2.Columns[0].Name = "Nume firma transport";
            dataGridView2.Columns[1].Name = "Ora plecarii";
            dataGridView2.Columns[2].Name = "Pret";
            dataGridView2.Columns[3].Name = "Numar locuri disponibile";
        }

        private void loadTable1()
        {
            dataGridView1.Rows.Clear();
            var excursii = srv.getAllExcursii();
            for(var i = 0;i<excursii.Count;i++) 
            {
                string numeObiectiv = srv.getObiectivById(excursii[i].idObiectiv).nume;
                string numeFirmaTransport = srv.getFirmaTransportById(excursii[i].idFirmaTransport).nume;
                string oraPlecarii = excursii[i].ora;
                string pret = excursii[i].pret.ToString();
                string numarLocuriDisponibile = srv.getNrLocuriDisponibile(excursii[i]).ToString();
                string[] row = { numeObiectiv, numeFirmaTransport, oraPlecarii, pret, numarLocuriDisponibile };
                dataGridView1.Rows.Add(row);
                if(numarLocuriDisponibile == "0")
                {
                    DataGridViewRow r = dataGridView1.Rows[i];
                    r.DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }

        private void loadTable2(string nume, int oraMinima, int oraMaxima)
        {
            dataGridView2.Rows.Clear();
            var excursii = srv.getAllExcursiiByNumeAndInterval(nume,oraMinima,oraMaxima);
            lista2 = excursii;
            for (var i = 0; i < excursii.Count; i++)
            {
                string numeFirmaTransport = srv.getFirmaTransportById(excursii[i].idFirmaTransport).nume;
                string oraPlecarii = excursii[i].ora;
                string pret = excursii[i].pret.ToString();
                string numarLocuriDisponibile = srv.getNrLocuriDisponibile(excursii[i]).ToString();
                string[] row = { numeFirmaTransport, oraPlecarii, pret, numarLocuriDisponibile };
                dataGridView2.Rows.Add(row);
                if (numarLocuriDisponibile == "0")
                {
                    DataGridViewRow r = dataGridView2.Rows[i];
                    r.DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nume = textBox1.Text;
            int oraMinima = int.Parse(textBox2.Text);
            int oraMaxima = int.Parse(textBox3.Text);
            loadTable2(nume,oraMinima,oraMaxima);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string numeCLient = textBox4.Text;
            string numarTelefon = textBox5.Text;
            int bileteDorite = int.Parse(textBox6.Text);
            int selectedRowCount = dataGridView2.Rows.GetRowCount(DataGridViewElementStates.Selected) - 1;
            excursieSelectata = lista2[selectedRowCount];
            srv.rezervaLocuri(numeCLient,numarTelefon,bileteDorite, excursieSelectata);
            loadTable1();
            reloadTable2();
        }

        private void reloadTable2()
        {
            dataGridView2.Rows.Clear();
            var excursii = lista2;
            lista2 = excursii;
            for (var i = 0; i < excursii.Count; i++)
            {
                string numeFirmaTransport = srv.getFirmaTransportById(excursii[i].idFirmaTransport).nume;
                string oraPlecarii = excursii[i].ora;
                string pret = excursii[i].pret.ToString();
                string numarLocuriDisponibile = srv.getNrLocuriDisponibile(excursii[i]).ToString();
                string[] row = { numeFirmaTransport, oraPlecarii, pret, numarLocuriDisponibile };
                dataGridView2.Rows.Add(row);
                if (numarLocuriDisponibile == "0")
                {
                    DataGridViewRow r = dataGridView2.Rows[i];
                    r.DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
