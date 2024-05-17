using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZonEscape
{
    public partial class formMensaje : Form
    {
        public formMensaje()
        {
            InitializeComponent();
        }

        private void formMensaje_Load(object sender, EventArgs e)
        {
            /*this.BackgroundImage = Properties.Resources.Mapa;
            this.BackgroundImageLayout = ImageLayout.Stretch;*/
            button1.Text = "Retry";
            button2.Text = "Close";
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public void cargarMensaje(bool estado, int puntaje)
        {
            if (estado) 
            {
                pictureBox1.Image = Properties.Resources.Win;
                label1.Text = "Tu puntaje:" + puntaje.ToString();
               
            } 
            else
            {
                pictureBox1.Image = Properties.Resources.Lose;
                label1.Text = "";

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.estadoJuego = 1;
            Form1 form1= new Form1();

            form1.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1.estadoJuego = 2;
        }
    }
}
