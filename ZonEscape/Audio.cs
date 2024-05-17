using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace ZonEscape
{
    internal class Audio
    {
        SoundPlayer sound;

        public Audio()
        {

        }

        public void seleccionarAudio(int tipo)
        {
            if(tipo == 1) 
            {
                sound = new SoundPlayer(Properties.Resources.Inicio);
            }
            else if(tipo == 2) 
            {
                sound = new SoundPlayer(Properties.Resources.Shot);
            }
        }

        public void Reproducir()
        {
            sound.Play();
        }
    }
}
