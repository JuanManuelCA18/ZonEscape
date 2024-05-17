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
    class FireWarlock:Personajes
    {
        public FireWarlock(int x, int y) : base("FireWarlock", x, y, 53, 54)
        {
            this.vida = 200;

        }

        public FireWarlock()
        {

        }
    }
}
