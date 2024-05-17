using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Remoting.Channels;

namespace ZonEscape
{
    public partial class Form1 : Form
    {
        public static int estadoJuego = 0;
        List<ObjetoGrafico> listTiles = new List<ObjetoGrafico>();
        List<ObjetoGrafico> listEnemys = new List<ObjetoGrafico>();
        List<Personajes> Enemys = new List<Personajes>();
        List<Impacto> balas = new List<Impacto>();
      
        Audio audio = new Audio();

        Crespo crespo = new Crespo(12, 332);
        ObjetoGrafico helicoptero;
        Impacto impacto;
        Impacto poison;
        Impacto ice;
        Impacto fire;
        Salto salto;
        Tile tileFinal;
        Corazon corazon1;
        Corazon corazon2;
        Corazon corazon3;
        int contaDer = 0;
        int ContaDesactivarTimer = 0;
        int contaMapa = 0;
        int contaImEne = 0;
        bool ban;
        bool End;
        int posEnemigo;
        int puntaje;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            audio.seleccionarAudio(1);
            audio.Reproducir();
            this.BackgroundImage = Properties.Resources.Mapa;
            this.BackgroundImageLayout = ImageLayout.Stretch;

            corazon1 = new Corazon(814, 12);
            this.Controls.Add(corazon1.imagen);

            corazon2 = new Corazon(859, 12);
            this.Controls.Add(corazon2.imagen);

            corazon3 = new Corazon(904, 12);
            this.Controls.Add(corazon3.imagen);

            cargarMapa();

        }
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char Letra = e.KeyChar;
            Letra = char.ToUpper(Letra);

            if (Letra == 'F')
            {
                ContaDesactivarTimer = 0;
                audio.seleccionarAudio(2);
                audio.Reproducir();
                impacto = new Impacto("BulletBall", crespo.direccion);
                impacto.posX = crespo.posX; impacto.posY = crespo.posY;
                this.Controls.Add(impacto.imagen);
                balas.Add(impacto);
                timer1.Enabled = true;
            }

            if (Letra == 'D')
            {
                if (!crespo.EvaluarColision(listTiles) && !crespo.EvaluarColision(listEnemys))
                    crespo.moverDerecha();
                else
                    crespo.Rebote(20);

                if(crespo.EvaluarColision(listEnemys))
                {
                    for (int i = 0; i < listEnemys.Count; i++)
                    {
                        if (crespo.EvaluarColision(listEnemys[i]))
                        {
                            crespo.vida -= 50;
                        }
                    }
                    
                  
                }

            }
            


            if (Letra == 'A')
            {

                if (!crespo.EvaluarColision(listTiles) && !crespo.EvaluarColision(listEnemys))
                    crespo.moverIzquierda();
                else
                    crespo.Rebote(20);


                if (crespo.EvaluarColision(listEnemys))
                {
                    for (int i = 0; i < listEnemys.Count; i++)
                    {
                        if (crespo.EvaluarColision(listEnemys[i]))
                        {
                            crespo.vida -= 50;
                        }
                    }
                }
            }
            

            if (Letra == 'W')
            {
                crespo.direccion = "arriba";

            }

            if (Letra == 'S')
            {
                crespo.direccion = "abajo";
            }

            if (Letra == (int)32)
            {
                salto = new Salto(crespo.posX,crespo.posY, 15, 75);
                Salto.Enabled = true;


            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            ContaDesactivarTimer++;

            for (int i = 0; i < balas.Count; i++)
            {
                ban = crespo.Disparar(balas[i], listEnemys, Enemys, listTiles);

                if (ContaDesactivarTimer == 60 && !ban)
                {
                    this.Controls.Remove(balas[i].imagen);
                    balas.RemoveAt(i);
                }

                if (ban)
                {
                    bool permiso = true;
                    for (int j = 0; j < listEnemys.Count; j++)
                    {
                        if (balas[i].EvaluarColision(listEnemys[j]))
                        {
                            permiso = false;
                            impacto.Bala(Enemys[j]);
                            this.Controls.Remove(balas[i].imagen);
                            balas.RemoveAt(i);
                            break;

                        }
                    }

                    if (permiso)
                    { 
                        for (int j = 0; j < listTiles.Count; j++)
                        {
                            if (balas[i].EvaluarColision(listTiles[j]))
                            {
                                this.Controls.Remove(balas[i].imagen);
                                balas.RemoveAt(i);
                                break;

                            }
                        }
                    }
                }
            }
           
          
         
            
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
             if (contaMapa == 2)
            {
                if (crespo.EvaluarColision(helicoptero))
                {
                    End = true;
                    formMensaje mensaje = new formMensaje();
                    mensaje.cargarMensaje(End, puntaje);
                    mensaje.Show();
                    this.Hide();
                    timer5.Enabled = true;
                    timer2.Enabled = false;
                    timer4.Enabled = false;
                    Salto.Enabled = false;
                    timerCambioMapa.Enabled = false;
                    MovimientoEnemigo.Enabled = false;
                }
            }

            if (crespo.vida == 0)
            {
                End = false;
                formMensaje mensaje = new formMensaje();
                mensaje.cargarMensaje(End, puntaje);
                mensaje.Show();
                this.Hide();
                timer5.Enabled = true; 
                timer2.Enabled = false;
                timer4.Enabled = false;
                Salto.Enabled = false;
                timerCambioMapa.Enabled = false;
                MovimientoEnemigo.Enabled = false;
            }  

           

            switch(crespo.direccion)
            {
                case "derecha":
                    crespo.imagen.Image = Properties.Resources.CrespoAnimadoD;
                    break;
                case "izquierda":
                    crespo.imagen.Image = Properties.Resources.CrespoAnimadoIz;
                    break;
            }    
            
           

            if (crespo.EvaluarColision(tileFinal))
            {
                crespo.posX = 12; crespo.posY = 332; //12, 332
                timerCambioMapa.Enabled = true;
            }

        }
        private void timerCambioMapa_Tick(object sender, EventArgs e)
        {
            BorrarListas();
            contaMapa++;
            cargarMapa();
            timerCambioMapa.Enabled = false;
        }
        public void cargarMapa()
        {
            this.Controls.Add(crespo.imagen);
            impacto = new Impacto("BulletBall", "");

            Tile tileInicio = new Tile("TileInicio", -4, 389);
            listTiles.Add(tileInicio);
            this.Controls.Add(tileInicio.imagen);

            tileFinal = new Tile(1050, 357);
            this.Controls.Add(tileFinal.imagen);                       

            
            if (contaMapa == 0)
            {
                CoordenadasTile(@"C:\Users\juanm\Documents\ZonEscape\ZonEscape\bin\Debug\coordenadasTiles\CoordenadasTiles.txt");
                CoordenadasWarlock(@"C:\Users\juanm\Documents\ZonEscape\ZonEscape\bin\Debug\coordenadasWarlocks\CoordenadasWarlocks.txt");
                CoordenadasMonster(@"C:\Users\juanm\Documents\ZonEscape\ZonEscape\bin\Debug\coordenadasMonsters\CoordenadasMonsters.txt");
                CoordenadasFlyEye(@"C:\Users\juanm\Documents\ZonEscape\ZonEscape\bin\Debug\coordenadasFlyEyes\CoordenadasFlyEyes.txt");
            }

            if (contaMapa == 1)
            {
                CoordenadasTile(@"C:\Users\juanm\Documents\ZonEscape\ZonEscape\bin\Debug\coordenadasTiles\CoordenadasTiles1.txt");
                CoordenadasWarlock(@"C:\Users\juanm\Documents\ZonEscape\ZonEscape\bin\Debug\coordenadasWarlocks\CoordenadasWarlocks1.txt");
                CoordenadasMonster(@"C:\Users\juanm\Documents\ZonEscape\ZonEscape\bin\Debug\coordenadasMonsters\CoordenadasMonsters1.txt");
                CoordenadasFlyEye(@"C:\Users\juanm\Documents\ZonEscape\ZonEscape\bin\Debug\coordenadasFlyEyes\CoordenadasFlyEyes1.txt");
            }

            if (contaMapa == 2)
            {
                helicoptero = new ObjetoGrafico("Helicoptero",789,262,186,126);
                this.Controls.Add(helicoptero.imagen);
                CoordenadasTile(@"C:\Users\juanm\Documents\ZonEscape\ZonEscape\bin\Debug\coordenadasTiles\CoordenadasTiles2.txt");
                CoordenadasWarlock(@"C:\Users\juanm\Documents\ZonEscape\ZonEscape\bin\Debug\coordenadasWarlocks\CoordenadasWarlocks2.txt");
                CoordenadasMonster(@"C:\Users\juanm\Documents\ZonEscape\ZonEscape\bin\Debug\coordenadasMonsters\CoordenadasMonsters2.txt");
                CoordenadasFlyEye(@"C:\Users\juanm\Documents\ZonEscape\ZonEscape\bin\Debug\coordenadasFlyEyes\CoordenadasFlyEyes2.txt");
            }
        }      
        void BorrarListas()
        {
            for (int i = 0; i < listTiles.Count; i++)
            {
                this.Controls.Remove(listTiles[i].imagen);
            }
            for (int i = 0; i < listEnemys.Count; i++)
            {
                this.Controls.Remove(listEnemys[i].imagen);
            }
            for (int i = 0; i < Enemys.Count; i++)
            {
                this.Controls.Remove(Enemys[i].imagen);
            }
            listTiles.Clear();
            listEnemys.Clear();  
            Enemys.Clear();
        }
        void CoordenadasTile(string rutaArchivo)
        {
            StreamReader Read = new StreamReader(rutaArchivo);
            string ArchivoTiles = Read.ReadToEnd();
            var coordenadasTiles = ArchivoTiles.Split('\r');
            for (int i = 0; i < coordenadasTiles.Length; i++)
            {
                string aux = coordenadasTiles[i].Trim();
                var punto = aux.Split(';');
                int x = int.Parse(punto[0]);
                int y = int.Parse(punto[1]);
                Tile tile = new Tile(x, y);
                listTiles.Add(tile);
                this.Controls.Add(tile.imagen);
            }          

        }
        void CoordenadasWarlock(string rutaArchivo)
        {
            StreamReader Read = new StreamReader(rutaArchivo);            
            string Archivo = Read.ReadToEnd();
            var coordenadas = Archivo.Split('\r');
            for (int i = 0; i < coordenadas.Length; i++)
            {
                string aux = coordenadas[i].Trim();
                var punto = aux.Split(';');
                int x = int.Parse(punto[0]);
                int y = int.Parse(punto[1]);
                FireWarlock warlock = new FireWarlock(x, y);
                listEnemys.Add(warlock);
                Enemys.Add(warlock);
                this.Controls.Add(warlock.imagen);
            }
               
        }
        void CoordenadasMonster(string rutaArchivo)
        {
            StreamReader Read = new StreamReader(rutaArchivo);
            string Archivo = Read.ReadToEnd();
            var coordenadas = Archivo.Split('\r');
            for (int i = 0; i < coordenadas.Length; i++)
            {
                string aux = coordenadas[i].Trim();
                var punto = aux.Split(';');
                int x = int.Parse(punto[0]);
                int y = int.Parse(punto[1]);
                PoisionMonster monster = new PoisionMonster(x, y);
                listEnemys.Add(monster);
                Enemys.Add(monster);
                this.Controls.Add(monster.imagen);
            }
        }
        void CoordenadasFlyEye(string rutaArchivo)
        {

            StreamReader Read = new StreamReader(rutaArchivo);
            string Archivo = Read.ReadToEnd();
            var coordenadas = Archivo.Split('\r');
            for (int i = 0; i < coordenadas.Length; i++)
            {
                string aux = coordenadas[i].Trim();
                var punto = aux.Split(';');
                int x = int.Parse(punto[0]);
                int y = int.Parse(punto[1]);
                FlyEye flyEye = new FlyEye(x, y);
                Enemys.Add(flyEye);
                listEnemys.Add(flyEye);
                this.Controls.Add(flyEye.imagen);
            }
            
        }
        private void Salto_Tick(object sender, EventArgs e)
        {

            switch (crespo.direccion)
            {
                case "derecha":
                    if (salto.Posy > 0)
                    {
                        var newPosy = 600 - salto.Posy;
                        crespo.SetPos(Convert.ToInt32(salto.Posx), Convert.ToInt32(newPosy));
                        if (newPosy >= 332)
                        {
                            Salto.Enabled = false;
                        }
                        salto.CalcularPosicion(1);
                    }
                    break;

                case "izquierda":
                    if (salto.Posy > 0)
                    {
                        var newPosx = -salto.Posx;
                        var newPosy = 600 - salto.Posy;
                        crespo.SetPos(Convert.ToInt32(salto.Posx), Convert.ToInt32(newPosy));
                        if (newPosy >= 332)
                        {
                            Salto.Enabled = false;
                        }
                    }

                    salto.CalcularPosicion(2);
                    break;

            }
        
           

            

        }
        private void MovimientoEnemigo_Tick(object sender, EventArgs e)
        {
            contaDer++;
            int Dir;

           

            foreach (Personajes enemys in Enemys)
            {
                enemys.Animacion();
            }

            for (int i = 0; i < Enemys.Count; i++)
            {
                if (Enemys[i].vida == 0)
                {
                    this.Controls.Remove(listEnemys[i].imagen);
                    Enemys.RemoveAt(i);
                    listEnemys.RemoveAt(i);
                }
            }

            if (contaDer <= 25)
            {
                Dir = 0;
                for (int i = 0; i < Enemys.Count; i++)
                {
                    Dir++;
                    if (Enemys[i].Mover(Enemys[i], i, crespo, Dir))
                    {
                        crespo.vida -= 50;
                        Enemys[i].Rebote(6);
                        
                    }
                }
            }

            if (contaDer >= 25 && contaDer <= 50)
            {
                Dir = 0;
                for (int i = 0; i < Enemys.Count; i++)
                {
                    Dir++;
                    if (Enemys[i].Regresar(Enemys[i], i, crespo, Dir))
                    {
                        crespo.vida -= 50;
                        Enemys[i].Rebote(6);
                    }
                }
            }

            if (contaDer == 50)
            {
                contaDer = 0;
            }


        }
        
        private void timer4_Tick(object sender, EventArgs e)
        {
            puntaje++;
            label1.Text = "TIEMPO: "+puntaje.ToString();
            switch (crespo.vida)
            {
                case 300:
                    corazon3.imagen.Image = Properties.Resources.CorazonEntero;
                    break;

                case 250:
                    corazon3.imagen.Image = Properties.Resources.CorazonMedio;
                    break;

                case 200:
                    corazon3.imagen.Image = Properties.Resources.CorazonVacio;
                    break;

                case 150:
                    corazon2.imagen.Image = Properties.Resources.CorazonMedio;
                    break;

                case 100:
                    corazon2.imagen.Image = Properties.Resources.CorazonVacio;
                    break;

                case 50:
                    corazon1.imagen.Image = Properties.Resources.CorazonMedio;
                    break;

                case 0:
                    corazon1.imagen.Image = Properties.Resources.CorazonVacio;
                    break;

            }

          

        }
        private void timer5_Tick(object sender, EventArgs e)
        {
            if (estadoJuego == 1)
            {
                contaMapa = 0;
                crespo.posX = 12; crespo.posY = 332;
                crespo.vida = 300;
                cargarMapa();
                estadoJuego = 0;
            }


            if (estadoJuego == 2)
            {
                this.Close();
            }
        }
    }
}
