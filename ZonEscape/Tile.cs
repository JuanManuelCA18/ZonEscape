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
    class Tile:ObjetoGrafico
    {
        public Tile()
        {

        }

        public Tile(int x, int y):base("TileContinuo", x, y, 80, 80)
        {

        }

        public Tile(string nombre, int x, int y) : base(nombre, x, y, 80, 80)
        {

        }
    }
}
