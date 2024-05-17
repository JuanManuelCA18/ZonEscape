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
    class FlyEye:Personajes
    {
        public FlyEye()
        {

        }

        public FlyEye(int x, int y): base("FlyEye", x, y, 53, 54)
        {
            this.vida = 200;
        }
      
    }
}
