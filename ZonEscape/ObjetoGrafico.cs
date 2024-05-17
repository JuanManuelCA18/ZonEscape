using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ZonEscape
{
    class ObjetoGrafico
    {
        public PictureBox imagen;
        public int posX;
        public int posY;
        public int w;
        public int h;
        string nombrePersonaje;

        public string NombrePersonaje { get => nombrePersonaje; set => nombrePersonaje = value; }

        public ObjetoGrafico()
        {

        }

        public ObjetoGrafico(string nombre, int x, int y, int w, int h)
        {
            this.w = w;
            this.h = h;
            nombrePersonaje = nombre;
            imagen = new PictureBox();
            imagen.Location = new Point(x, y);
            imagen.Size = new Size(w, h);
            imagen.Image = (Image)Properties.Resources.
                ResourceManager.GetObject(nombre);
            imagen.SizeMode = PictureBoxSizeMode.StretchImage;
            imagen.BackColor = Color.Transparent;
            SetPos(x, y);
        }

        public void SetPos(int x, int y)
        {
            this.posX = x;
            this.posY = y;
            this.imagen.Location = new Point(x,y);
        }

        public virtual Rectangle ObtenerLimite()
        {
            return imagen.Bounds;
        }

        public bool EvaluarColision(List<ObjetoGrafico> objetos)
        {
            for (int i = 0; i < objetos.Count; i++)
            {
                if (this.ObtenerLimite().IntersectsWith(objetos[i].ObtenerLimite()))
                {
                    return true;
                }
            }

            return false;
        }
        public bool EvaluarColision(ObjetoGrafico objeto)
        {
            if (this.ObtenerLimite().IntersectsWith(objeto.ObtenerLimite()))
            {
                return true;
            }


            return false;
        }

    }
}
