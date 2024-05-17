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
    class Personajes:ObjetoGrafico
    {
        public int vida;
        public int velocidad = 3;
        public string direccion = "quieto";
        int estado;


        public Personajes()
        {

        }

        public Personajes(string nombre, int x, int y, int w, int h) : 
         base(nombre, x, y, w, h)
        {
            estado = 1;
        }

        public virtual bool Disparar(Impacto disparo, List<ObjetoGrafico> listEnemys, List<Personajes> Enemys, List<ObjetoGrafico> listTile)
        {
            return true;
        }

        public virtual bool Disparar(Impacto Disparo, List<ObjetoGrafico> listTile, Personajes Crespo, Personajes Enemy)
        {

            switch (Enemy.direccion)
            {
                case "derecha":
                    if (!Disparo.EvaluarColision(Crespo) && !Disparo.EvaluarColision(listTile))
                    {
                        Disparo.moverDerecha();
                    }
                    else
                        return true;
                    break;

                case "izquierda":
                    if (!Disparo.EvaluarColision(Crespo) && !Disparo.EvaluarColision(listTile))
                    {
                        Disparo.moverIzquierda();
                    }
                    else
                        return true;
                    break;
            }

            return false;
        }
        public virtual void moverDerecha()
        {
            direccion = "derecha";
            posX += velocidad;
            SetPos(posX, posY);
        }

        public virtual void moverIzquierda()
        {
            direccion = "izquierda";
            posX -= velocidad;
            SetPos(posX, posY); 
        }

        public virtual void moverArriba()
        {
            
        }

        public virtual void moverAbajo()
        {
            
        }

        public bool Mover(Personajes Enemy, int posEnemy, Personajes crespo, int dir)
        {
            if (Enemy.EvaluarColision(crespo))
            {
                return true;
            }
            if (dir % 2 == 0 )
            {
                Enemy.moverDerecha();               
            }
            else
            {
                Enemy.moverIzquierda();               
            }

            return false;
        }

        public bool Regresar(Personajes Enemy, int posEnemy, Personajes crespo, int dir)
        {

            if (Enemy.EvaluarColision(crespo))
            {
                return true;
            }

            if (dir % 2 == 0 )
            {
                Enemy.moverIzquierda();              
            }
            else
            {
                Enemy.moverDerecha();               
            }

            return false;
        }
       

        public void Rebote(int velocidad)
        {           
            switch (direccion)
            {
                case "arriba":
                    this.posX -= velocidad;
                    direccion = "izquierda";
                    break;

                case "abajo":
                    this.posX -= velocidad;
                    direccion = "izquierda";
                    break;

                case "izquierda":
                    this.posX += velocidad;
                    direccion = "derecha";
                    break;

                case "derecha":
                    this.posX -= velocidad;
                    direccion = "izquierda";
                    break;
                default: break;
            }
            SetPos(posX, posY);
            velocidad = 3;
        }

        public void Animacion()
        {
            string recurso = "";
            switch (estado)
            {
                case 1:
                    recurso = this.NombrePersonaje + "_" + estado + "_" + direccion;
                    this.imagen.Image=
                    (Image)Properties.Resources.ResourceManager.GetObject(recurso);
                    estado = 2;
                    break;
                case 2:
                    recurso = this.NombrePersonaje + "_" + estado + "_" + direccion;
                    imagen.Image =
                    (Image)Properties.Resources.ResourceManager.GetObject(recurso);
                    estado = 3;
                    break;
                case 3:
                    recurso = this.NombrePersonaje + "_" + estado + "_" + direccion;
                    imagen.Image =
                    (Image)Properties.Resources.ResourceManager.GetObject(recurso);
                    estado = 4;
                    break;
                case 4:
                    recurso = this.NombrePersonaje + "_" + estado + "_" + direccion;
                    imagen.Image =
                    (Image)Properties.Resources.ResourceManager.GetObject(recurso);
                    estado = 1;
                    break;
                default:
                    break;

            }

        }



    }
}
