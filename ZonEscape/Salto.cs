using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZonEscape
{
    internal class Salto
    {
        const double t = 0.1;
        const double g = 9.8;
        double posx, posy, ang, vel, vx, vy;

        public double Posx { get => posx; set => posx = value; }
        public double Posy { get => posy; set => posy = value; }

        public Salto()
        {

        }

        public Salto(double x, double y, double v, double a)
        {
            
            posx = x;
            posy = y;
            ang = (a * Math.PI)/180;
            vel = v;
        }

        public void CalcularPosicion(int dir)
        {
            CalcularVelocidad();
            if(dir == 1)
                posx = posx + vx * t;
            else
                posx = posx - vx * t;


            posy = posy + vy * t - (1 / 2 * g * t * t);

        }
        public void CalcularVelocidad()
        {
            vx = vel * Math.Cos(ang);
            vy = vel * Math.Sin(ang) - g * t;
            vel = Math.Sqrt(vx * vx + vy * vy);
            ang = Math.Atan(vy / vx);
        }
    }
}
