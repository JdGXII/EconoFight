using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Graphics.Model;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
using FlatRedBall.Localization;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Microsoft.Xna.Framework.Media;
#endif

namespace TesisEconoFight.Screens
{
	public partial class GameScreen
	{
        bool pausa;
        double gameover;
        bool murio;
        bool flag;
        Song song;
		void CustomInitialize()
		{
            IniciarMundo();
            IniciarP1();
            IniciarP2();
            pausa = false;
            murio = false;
            this.PauseTextInstance.SpriteVisible = false;
            this.P1WinTextInstance.SpriteVisible = false;
            this.P2WinTextInstance.SpriteVisible = false;
            this.DoubleKOTextInstance.SpriteVisible = false;
            flag = false;
            this.LimitesMundo.Visible = false;
            
		}

		void CustomActivity(bool firstTimeCalled)
		{
            ListaJugadores[0].ActividadNormal(InputManager.Xbox360GamePads[0], ListaJugadores[1].getPosicion());
            ListaJugadores[1].ActividadFlip(GlobalData.getControl2(), ListaJugadores[0].getPosicion());            
            ColisionEntreJugadores();
            ColisionMundo();
            ColisionBloqueo1();
            ColisionBloqueo2();
            ColisionPhys();
            ColisionAtaqueJugador1();
            ColisionAtaqueJugador2();
            ColisionEntreAtaquesEspeciales();
            ColisionSuperAtaques();
            ColisionSuperConEspecial1();
            ColisionSuperConEspecial2();
            Pausar();
            NuevaPantalla();
            
		}

		void CustomDestroy()
		{

            foreach (Entities.Player i in ListaJugadores)
            {
                i.Destroy();
            }
		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        private void IniciarMundo()
        {
            if (GlobalData.mundo == "Hayek")
            {
                CurrentState = VariableState.MundoHayek;
                Microsoft.Xna.Framework.Media.MediaPlayer.IsRepeating = true;
                song =
      FlatRedBallServices.Load<Song>(@"Content/cancion 1", ContentManagerName);
                Microsoft.Xna.Framework.Media.MediaPlayer.Play(song);
            }
            else if (GlobalData.mundo == "Keynes")
            {
                CurrentState = VariableState.MundoKeynes;
                Microsoft.Xna.Framework.Media.MediaPlayer.IsRepeating = true;
                song =
      FlatRedBallServices.Load<Song>(@"Content/Cancion pelea", ContentManagerName);
                Microsoft.Xna.Framework.Media.MediaPlayer.Play(song);
            }
            else if (GlobalData.mundo == "Malthus")
            {
                Microsoft.Xna.Framework.Media.MediaPlayer.IsRepeating = true;
                song =
      FlatRedBallServices.Load<Song>(@"Content/boss battle", ContentManagerName);
                Microsoft.Xna.Framework.Media.MediaPlayer.Play(song);
                CurrentState = VariableState.MundoMalthus;
            }
            else if (GlobalData.mundo == "Smith")
            {
                Microsoft.Xna.Framework.Media.MediaPlayer.IsRepeating = true;
                song =
      FlatRedBallServices.Load<Song>(@"Content/boss intro", ContentManagerName);
                Microsoft.Xna.Framework.Media.MediaPlayer.Play(song);
                CurrentState = VariableState.MundoSmith;
            }
        }

        //Falta posicionar las barras
        private void IniciarP1()
        {
            if (GlobalData.Personaje1 == "Hayek")
            {
                Entities.Hayek hayek = new Entities.Hayek(ContentManagerName);
                hayek.setPosicion(-230,-120);
                hayek.getSensorial().setPosicion(-240, 200);
                hayek.getHP().SetPosicion(210, 240);
                
                
                if (GlobalData.bancaP1 == "Completa")
                {
                    Entities.SuperBarra barra = new Entities.SuperBarra(ContentManagerName);
                    barra.SetPosicion(hayek.X, -250-20);
                    hayek.LlenarBarra(barra);
                   // barra.Destroy();
                    
                }
                else if (GlobalData.bancaP1 == "Fraccionaria")
                {
                    Entities.BarraFraccionaria barra = new Entities.BarraFraccionaria(ContentManagerName);
                    barra.SetPosicion(hayek.X, -250-20);
                    hayek.LlenarBarra(barra);
                    //barra.Destroy();

                }

                if (GlobalData.proyectilP1 == "Oro")
                {
                    hayek.setEstado("Oro");
                }
                else if (GlobalData.proyectilP1 == "Bajo")
                {
                    hayek.setEstado("Bajo");
                }

                ListaJugadores.Add(hayek);
                //hayek.Destroy();
            }
            else if (GlobalData.Personaje1 == "Keynes")
            {
                Entities.Keynes keynes = new Entities.Keynes(ContentManagerName);
                keynes.setPosicion(-230, -120);
                keynes.getBarra().SetPosicion(-230, -250 - 20);
                keynes.getHP().SetPosicion(210, 240);
                keynes.getInteres().setPosicion(-240, 200);
                
                ListaJugadores.Add(keynes);
                //keynes.Destroy();
            }
            else if (GlobalData.Personaje1 == "Malthus")
            {
                Entities.Malthus malthus = new Entities.Malthus(ContentManagerName);
                malthus.setPosicion(-230, -120);
                malthus.getBarra().SetPosicion(-230, -250 -20);
                malthus.getHP().SetPosicion(210, 240);
                malthus.getPoblacion().SetPosicionFlip(240, 200);
                ListaJugadores.Add(malthus);
               // malthus.Destroy();
                
            }
            else if (GlobalData.Personaje1 == "Smith")
            {
                Entities.Smith smith = new Entities.Smith(ContentManagerName);
                smith.setPosicion(-230, -120);
                smith.getBarra().SetPosicion(smith.X, -250 -20);
                smith.getHP().SetPosicion(210, 240);
                ListaJugadores.Add(smith);
               // smith.Destroy();
            }

        }

        //Falta posicionar las barras
        // OJO!!! Los jugadores en la izquierda tienen las barras del lado negativo porque se voltean en el Setposicion
        private void IniciarP2()
        {
            if (GlobalData.Personaje2 == "Hayek")
            {
                Entities.Hayek hayek = new Entities.Hayek(ContentManagerName);
                hayek.setPosicion(230, -120);
                hayek.getSensorial().setPosicion(240, 200);
                hayek.getHP().SetPosicionFlip(-210, 240);
                hayek.VoltearPersonaje();

                if (GlobalData.bancaP2 == "Completa")
                {
                    Entities.SuperBarra barra = new Entities.SuperBarra(ContentManagerName);
                    barra.SetPosicionFlip(hayek.X, -250-20);
                    hayek.LlenarBarra(barra);
                   // barra.Destroy();

                }
                else if (GlobalData.bancaP2 == "Fraccionaria")
                {
                    Entities.BarraFraccionaria barra = new Entities.BarraFraccionaria(ContentManagerName);
                    barra.SetPosicionFlip(hayek.X, -250 -20);
                    hayek.LlenarBarra(barra);
                   // barra.Destroy();

                }

                if (GlobalData.proyectilP2 == "Oro")
                {
                    hayek.setEstado("Oro");
                }
                else if (GlobalData.proyectilP2 == "Bajo")
                {
                    hayek.setEstado("Bajo");
                }

                ListaJugadores.Add(hayek);
                //hayek.Destroy();
            }
            else if (GlobalData.Personaje2 == "Keynes")
            {
                Entities.Keynes keynes = new Entities.Keynes(ContentManagerName);
                keynes.setPosicion(230, -120);
                keynes.getBarra().SetPosicionFlip(keynes.X, -250 -20);
                keynes.getHP().SetPosicionFlip(-210, 240);
                keynes.getInteres().setPosicion(240, 200);
                keynes.VoltearPersonaje();
                ListaJugadores.Add(keynes);
              //  keynes.Destroy();
            }
            else if (GlobalData.Personaje2 == "Malthus")
            {
                Entities.Malthus malthus = new Entities.Malthus(ContentManagerName);
                malthus.setPosicion(230, -120);
                malthus.getBarra().SetPosicionFlip(malthus.X, -250 -20);
                malthus.getHP().SetPosicionFlip(-210, 240);
                malthus.getPoblacion().SetPosicionFlip(-240, 200);
                malthus.VoltearPersonaje();
                ListaJugadores.Add(malthus);
                //malthus.Destroy();

            }
            else if (GlobalData.Personaje2 == "Smith")
            {
                Entities.Smith smith = new Entities.Smith(ContentManagerName);
                smith.setPosicion(230, -120);
                smith.getBarra().SetPosicionFlip(smith.X, -250 -20);
                smith.getHP().SetPosicionFlip(-210, 240);
                smith.VoltearPersonaje();
                ListaJugadores.Add(smith);
                //smith.Destroy();
            }

        }

        private void ColisionMundo()
        {
            ListaJugadores[0].getCuerpo().CollideAgainstMove(this.LimitesMundo, 0, 1);
            ListaJugadores[1].getCuerpo().CollideAgainstMove(this.LimitesMundo, 0, 1);
        }

        private void ColisionEntreJugadores()
        {
            ListaJugadores[0].getCuerpo().CollideAgainstMove(ListaJugadores[1].getCuerpo(), 1, 1);
            ListaJugadores[1].getCuerpo().CollideAgainstMove(ListaJugadores[0].getCuerpo(), 1, 1);
        }

        private void ColisionPhys()
        {
            if (ListaJugadores[0].getCuerpo().CollideAgainst(ListaJugadores[1].getGolpe()))
            {
                ListaJugadores[0].RecibirDaño(ListaJugadores[1].DarDañoFisico());

            }
            else if (ListaJugadores[1].getCuerpo().CollideAgainst(ListaJugadores[0].getGolpe()))
            {
                ListaJugadores[1].RecibirDañoFlip(ListaJugadores[0].DarDañoFisico());
            }

        }

        private void ColisionAtaqueJugador1()
        {
            if (ListaJugadores[0].ListaAtaques.Count > 0)
            {
                foreach (Entities.Especial i in ListaJugadores[0].ListaAtaques)
                {
                    if(ListaJugadores[1].getCuerpo().CollideAgainst(i.getCuerpo()))
                    {

                        ListaJugadores[1].RecibirDañoFlip(i.giveDamage());
                        ListaJugadores[0].ActualizarBarra();
                        i.Destroy();
                        
                    }
                }
            }

            if (ListaJugadores[0].ListaAtaqueSuper.Count > 0)
            {
                foreach (Entities.SuperAtaque i in ListaJugadores[0].ListaAtaqueSuper)
                {

                    if(ListaJugadores[1].getCuerpo().CollideAgainst(i.getCuerpo()))
                    {
                        ListaJugadores[1].RecibirDañoFlip(i.giveDamage());
                        ListaJugadores[0].ActualizarBarra();
                        i.Destroy();
                    }
                }
            }
        }

        private void ColisionAtaqueJugador2()
        {
            if (ListaJugadores[1].ListaAtaques.Count > 0)
            {
                foreach (Entities.Especial i in ListaJugadores[1].ListaAtaques)
                {
                    if (ListaJugadores[0].getCuerpo().CollideAgainst(i.getCuerpo()))
                    {

                        ListaJugadores[0].RecibirDaño(i.giveDamage());
                        ListaJugadores[1].ActualizarBarraFlip();
                        i.Destroy();

                    }
                }
            }

            if (ListaJugadores[1].ListaAtaqueSuper.Count > 0)
            {
                foreach (Entities.SuperAtaque i in ListaJugadores[1].ListaAtaqueSuper)
                {

                    if (ListaJugadores[0].getCuerpo().CollideAgainst(i.getCuerpo()))
                    {
                        ListaJugadores[0].RecibirDaño(i.giveDamage());
                        ListaJugadores[1].ActualizarBarraFlip();
                        i.Destroy();
                    }
                }
            }
        }

        private void ColisionEntreAtaquesEspeciales()
        {
            if (ListaJugadores[0].ListaAtaques.Count > 0)
            {
                if (ListaJugadores[1].ListaAtaques.Count > 0)
                {
                    foreach (Entities.Especial i in ListaJugadores[0].ListaAtaques)
                    {

                        foreach (Entities.Especial j in ListaJugadores[1].ListaAtaques)
                        {
                            if (i.getCuerpo().CollideAgainst(j.getCuerpo()))
                            {
                                if (i.getTipo() == "Oro" && (j.getTipo() != "Oro" || j.getTipo() != "Malnutrido"))
                                {
                                    j.Destroy();
                                }
                                else if(i.getTipo()=="Oro" && j.getTipo()=="Malnutrido")
                                {
                                    j.setVida(j.getVida() - i.giveDamage());
                                    i.Destroy();

                                }
                                else if (i.getTipo() == "Oro" && j.getTipo() == "Oro")
                                {
                                    i.Destroy();
                                    j.Destroy();
                                }
                                else if (j.getTipo() == "Oro" && (i.getTipo() != "Oro" || i.getTipo() != "Malnutrido"))
                                {
                                    i.Destroy();
                                }
                                else if (j.getTipo() == "Oro" && i.getTipo() == "Malnutrido")
                                {
                                    i.setVida(i.getVida() - j.giveDamage());
                                    j.Destroy();
                                }
                                
                            }
                        }
                    }
                }
            }
        }

        private void ColisionSuperAtaques()
        {
            if (ListaJugadores[0].ListaAtaqueSuper.Count > 0)
            {
                if (ListaJugadores[1].ListaAtaqueSuper.Count > 0)
                {
                    foreach (Entities.SuperAtaque i in ListaJugadores[0].ListaAtaqueSuper)
                    {


                        foreach (Entities.SuperAtaque j in ListaJugadores[1].ListaAtaqueSuper)
                        {

                            if (i.getCuerpo().CollideAgainst(j.getCuerpo()))
                            {
                                i.Destroy();
                                j.Destroy();
                            }
                        }
                    }
                }
            }
        }

        private void ColisionSuperConEspecial1()
        {
            if (ListaJugadores[0].ListaAtaqueSuper.Count > 0)
            {
                if (ListaJugadores[1].ListaAtaques.Count > 0)
                {

                    foreach (Entities.SuperAtaque i in ListaJugadores[0].ListaAtaqueSuper)
                    {
                        foreach (Entities.Especial j in ListaJugadores[1].ListaAtaques)
                        {
                            if (i.getCuerpo().CollideAgainst(j.getCuerpo()))
                            {
                                j.Destroy();
                            }
                        }
                    }
                }
            }
        }

        private void ColisionSuperConEspecial2()
        {
            if (ListaJugadores[1].ListaAtaqueSuper.Count > 0)
            {
                if (ListaJugadores[0].ListaAtaques.Count > 0)
                {

                    foreach (Entities.SuperAtaque i in ListaJugadores[1].ListaAtaqueSuper)
                    {
                        foreach (Entities.Especial j in ListaJugadores[0].ListaAtaques)
                        {
                            if (i.getCuerpo().CollideAgainst(j.getCuerpo()))
                            {
                                j.Destroy();
                            }
                        }
                    }
                }
            }
        }

        private void ColisionBloqueo1()
        {
            if (ListaJugadores[0].ListaAtaques.Count > 0)
            {
                foreach (Entities.Especial i in ListaJugadores[0].ListaAtaques)
                {
                    if (i.getCuerpo().CollideAgainst(ListaJugadores[1].getShield()))
                    {
                        i.Destroy();
                    }
                }
            }

            if (ListaJugadores[0].ListaAtaqueSuper.Count > 0)
            {
                foreach (Entities.SuperAtaque i in ListaJugadores[0].ListaAtaqueSuper)
                {
                    if (i.getCuerpo().CollideAgainst(ListaJugadores[1].getShield()))
                    {
                        i.Destroy();
                    }
                }
            }
        }

        private void ColisionBloqueo2()
        {
            if (ListaJugadores[1].ListaAtaques.Count > 0)
            {
                foreach (Entities.Especial i in ListaJugadores[1].ListaAtaques)
                {
                    if (i.getCuerpo().CollideAgainst(ListaJugadores[0].getShield()))
                    {
                        i.Destroy();
                    }
                }
            }

            if (ListaJugadores[1].ListaAtaqueSuper.Count > 0)
            {
                foreach (Entities.SuperAtaque i in ListaJugadores[1].ListaAtaqueSuper)
                {
                    if (i.getCuerpo().CollideAgainst(ListaJugadores[0].getShield()))
                    {
                        i.Destroy();
                    }
                }
            }
        }

        private void Pausar()
        {
            //logic and stuff
            if (GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.Start) || GlobalData.getControl2().ButtonPushed(Xbox360GamePad.Button.Start) && pausa == false)
            {
                this.PauseTextInstance.SpriteVisible = true;
                FlatRedBall.Instructions.InstructionManager.PauseEngine();
                pausa = true;

            }
            else if (GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.Start) || GlobalData.getControl2().ButtonPushed(Xbox360GamePad.Button.Start) && pausa == true)
            {
                this.PauseTextInstance.SpriteVisible = false;
                FlatRedBall.Instructions.InstructionManager.UnpauseEngine();
                pausa = false;
            }
        }

        private bool FightOver()
        {
            
            if (ListaJugadores[0].getAlive() == false && murio==false)
            {
                //gameover = TimeManager.CurrentTime;
                //Entities.P2WinText p2win = new Entities.P2WinText(ContentManagerName);
                this.P2WinTextInstance.SpriteVisible = true;
                
                murio = true;
                flag = true;
                
            }
            else if (ListaJugadores[1].getAlive() == false && murio==false)
            {
                //gameover = TimeManager.CurrentTime;
                //Entities.P1WinText p1win = new Entities.P1WinText(ContentManagerName);
                this.P1WinTextInstance.SpriteVisible = true;
                                murio = true;
                                flag = true;
            }

            else if (ListaJugadores[1].getAlive() == false && ListaJugadores[0].getAlive() == false && murio==false)
            {
                //gameover = TimeManager.CurrentTime;
                //Entities.DoubleKOText ko = new Entities.DoubleKOText(ContentManagerName);
                this.DoubleKOTextInstance.SpriteVisible = true;
                murio = true;
                flag = true;
            }

           
            //Crear una variable en Player que sea boolean para revisar si esta muerto
            //Crear el metodo que devuelva la variable
            //La variable se cambia en el metodo de revisar vida de cada personaje

            return flag;
        }

        private void NuevaPantalla()
        {

            if (FightOver())
            {
                 if (GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.A) || GlobalData.getControl2().ButtonPushed(Xbox360GamePad.Button.A))
                
                MoveToScreen(typeof(CharSelect).FullName);
            }


        }

	}
}
