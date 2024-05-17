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
    class Crespo : Personajes
    {
        public List<ObjetoGrafico> disparoEstado = new List<ObjetoGrafico>();
        public bool ban = true;
        public Crespo(int x, int y) : base("CrespoAnimadoD", x, y, 53, 54)
        {
            this.vida = 300;

        }
        public Crespo()
        {

        }

        public Tuple<int,int> Saltar(int PosX, int PosY, double Velocidad, double Angulo)
        {
            double VelX;
            double VelY;
            double Tiempo = 0.01;

            VelX = Velocidad * Math.Cos(Angulo);
            VelY = Velocidad * Math.Sin(Angulo) - 9.8 * Tiempo;
            PosX += Convert.ToInt32( VelX * Tiempo);
            PosY += Convert.ToInt32(VelY * Tiempo - (0.5 * 9.8) * Math.Pow(Tiempo, 2));
            Velocidad = Math.Sqrt(Math.Pow(VelX, 2) + Math.Pow(VelY, 2));
            Angulo = Math.Atan(VelY / VelX);

            return Tuple.Create(PosX, PosY);
        }

        public void ActivarHabilidad()
        {
        }
        public override bool Disparar(Impacto Disparo, List<ObjetoGrafico> listEnemys, List<Personajes> Enemys, List<ObjetoGrafico> listTile)
        {

            if (!Disparo.EvaluarColision(listEnemys) && !Disparo.EvaluarColision(listTile))
            {
                Disparo.moverImpacto();
            }
            else
                return true;


         /*   switch (this.direccion)
            {
                case "derecha":
                    if (!Disparo.EvaluarColision(listEnemys) && !Disparo.EvaluarColision(listTile))
                    {
                        Disparo.moverDerecha(direccion);
                    }
                    else
                        return true;
                    break;

                case "izquierda":
                    if (!Disparo.EvaluarColision(listEnemys) && !Disparo.EvaluarColision(listTile))
                    {
                        Disparo.moverIzquierda();
                    }
                    else
                        return true;
                    break;

                case "arriba":
                    if (!Disparo.EvaluarColision(listEnemys) && !Disparo.EvaluarColision(listTile))
                    {
                        Disparo.moverArriba();
                    }
                    else
                        return true;
                    break;

                case "abajo":
                    if (!Disparo.EvaluarColision(listEnemys) && !Disparo.EvaluarColision(listTile))
                    {
                        Disparo.moverAbajo();
                    }
                    else
                        return true;
                    break;
            }*/
                                   
            return false;           
        }
    }
}
