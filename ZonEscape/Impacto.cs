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
    class Impacto:Personajes
    {
        
        int daño;

        public Impacto(string nombre, string direccion):base(nombre,-20,-10,19,19)//La pos x y pos y debe de ser la que tiene el que disparara
        {
            this.direccion = direccion;
        }

        public Impacto()
        { 
           
        }

        public void Veneno(Personajes Crespo)
        {
            this.imagen.Image = Properties.Resources.PoisionBall;
            daño = 50;
            Crespo.velocidad = 1;
            Crespo.vida -= daño;

        }

        public void Hielo()
        {
            this.imagen.Image = Properties.Resources.IceBall;

        }

        public void Fuego()
        {
            this.imagen.Image = Properties.Resources.FireBall;

        }

        public void Bala(Personajes Recibe)
        {
            this.imagen.Image = Properties.Resources.BulletBall;
            daño = 100;
            Recibe.vida -= daño;           

        }

        public override void moverDerecha()
        {
            direccion = "derecha";
            posX += velocidad;
            SetPos(posX, posY);
        }

        public override void moverArriba()
        {
            direccion = "arriba";
            posY -= velocidad;
            SetPos(posX, posY);
        }

        public override void moverAbajo()
        {
            direccion = "abajo";
            posY += velocidad;
            SetPos(posX, posY);
        }

        public override void moverIzquierda()
        {
            direccion = "izquierda";
            posX -= velocidad;
            SetPos(posX, posY);
        }       

        public void moverImpacto()
        {
            switch(this.direccion)
            {
                case "derecha":
                    posX += velocidad;
                    SetPos(posX, posY);
                    break;

                case "izquierda":
                    posX -= velocidad;
                    SetPos(posX, posY);
                    break;

                case "arriba":
                    posY -= velocidad;
                    SetPos(posX, posY);
                    break;

                case "abajo":
                    posY += velocidad;
                    SetPos(posX, posY);
                    break;
            }
        }
    }
}
