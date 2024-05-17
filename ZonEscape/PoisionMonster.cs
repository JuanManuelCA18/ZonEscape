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
    class PoisionMonster:Personajes
    {
        public PoisionMonster(int x, int y) : base("PoisionMonster", x, y, 53, 54)
        {
            this.vida = 200;

        }

        public PoisionMonster()
        {

        }

        public override bool Disparar(Impacto Disparo, List<ObjetoGrafico> listTile, Personajes Crespo, Personajes Enemy)
        {
            switch(this.direccion)
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
    }
}
