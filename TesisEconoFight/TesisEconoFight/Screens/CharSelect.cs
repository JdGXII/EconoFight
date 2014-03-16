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
	public partial class CharSelect
	{
        bool seleccion1;
        bool seleccion2;
        Song song;

		void CustomInitialize()
		{
            this.EscogerChar.Visible = false;
            this.Sprite1.Visible = false;
            this.Sprite2.Visible = false;
            if (GlobalData.AI == false)
            {
                Entities.Kursor P1 = new Entities.Kursor(ContentManagerName);
                Entities.Kursor P2 = new Entities.Kursor(ContentManagerName);
                Cursores.Add(P1);
                Cursores.Add(P2);
            }
            else
            {
                Entities.Kursor P1 = new Entities.Kursor(ContentManagerName);
                Cursores.Add(P1);
            }
            seleccion1 = false;
            seleccion2 = false;

            Microsoft.Xna.Framework.Media.MediaPlayer.IsRepeating = true;
            song =
  FlatRedBallServices.Load<Song>(@"Content/intro", ContentManagerName);
            Microsoft.Xna.Framework.Media.MediaPlayer.Play(song);
           

		}

		void CustomActivity(bool firstTimeCalled)
		{
            for (int i = 0; i < Cursores.Count; i++)
            {
                Xbox360GamePad cont = InputManager.Xbox360GamePads[i];
                Cursores[i].Actividad(cont);
            }
            SeleccionarCharP1();
            SeleccionarCharP2();
            Regresar();
            irPelea();
            
		}

        void CustomDestroy()
		{


		}

        static void CustomLoadStaticContent(string contentManagerName)
        {
            

        }


        private void SeleccionarCharP1()
        {
            
                if (Cursores[0].getCuerpo().CollideAgainst(EscogerChar.AxisAlignedRectangles.FindByName("HayekOroFull")) && GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.A))
                {
                    GlobalData.Personaje1 = "Hayek";
                    GlobalData.proyectilP1 = "Oro";
                    GlobalData.bancaP1 = "Completa";
                    seleccion1 = true;
                    CurrentState = VariableState.Hayek;                    
                    this.Sprite1.Visible = true;
                }

                else if (Cursores[0].getCuerpo().CollideAgainst(EscogerChar.AxisAlignedRectangles.FindByName("HayekOroFraccion")) && GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.A))
                {
                    GlobalData.Personaje1 = "Hayek";
                    GlobalData.proyectilP1 = "Oro";
                    GlobalData.bancaP1 = "Fraccionaria";
                    seleccion1 = true;
                    CurrentState = VariableState.Hayek;
                    this.Sprite1.Visible = true;
                }

                else if (Cursores[0].getCuerpo().CollideAgainst(EscogerChar.AxisAlignedRectangles.FindByName("HayekFiatFull")) && GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.A))
                {
                    GlobalData.Personaje1 = "Hayek";
                    GlobalData.proyectilP1 = "Bajo";
                    GlobalData.bancaP1 = "Completa";
                    seleccion1 = true;
                    CurrentState = VariableState.Hayek;
                    this.Sprite1.Visible = true;
                }

                else if (Cursores[0].getCuerpo().CollideAgainst(EscogerChar.AxisAlignedRectangles.FindByName("HayekFiatFraccion")) && GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.A))
                {
                    GlobalData.Personaje1 = "Hayek";
                    GlobalData.proyectilP1 = "Bajo";
                    GlobalData.bancaP1 = "Fraccionaria";
                    seleccion1 = true;
                    CurrentState = VariableState.Hayek;
                    this.Sprite1.Visible = true;
                }

                else if (Cursores[0].getCuerpo().CollideAgainst(EscogerChar.AxisAlignedRectangles.FindByName("Keynes")) && GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.A))
                {
                    GlobalData.Personaje1 = "Keynes";
                    seleccion1 = true;
                    CurrentState = VariableState.Keynes;
                    this.Sprite1.Visible = true;
                    
                }
                else if (Cursores[0].getCuerpo().CollideAgainst(EscogerChar.AxisAlignedRectangles.FindByName("Malthus")) && GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.A))
                {
                    GlobalData.Personaje1 = "Malthus";
                    seleccion1 = true;
                    CurrentState = VariableState.Malthus;
                    this.Sprite1.Visible = true;

                }
                else if (Cursores[0].getCuerpo().CollideAgainst(EscogerChar.AxisAlignedRectangles.FindByName("Smith")) && GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.A))
                {
                    GlobalData.Personaje1 = "Smith";
                    CurrentState = VariableState.Smith;
                    this.Sprite1.Visible = true;
                    seleccion1 = true;

                }
            

        }

        private void SeleccionarCharP2()
        {
            if (Cursores.Count == 2)
            {
                if (Cursores[1].getCuerpo().CollideAgainst(EscogerChar.AxisAlignedRectangles.FindByName("HayekOroFull")) && GlobalData.getControl2().ButtonPushed(Xbox360GamePad.Button.A))
                {
                    GlobalData.Personaje2 = "Hayek";
                    GlobalData.proyectilP2 = "Oro";
                    GlobalData.bancaP2 = "Completa";
                    seleccion2 = true;
                    CurrentState = VariableState.Hayek2;
                    Sprite2.Visible = true;
                }

                else if (Cursores[1].getCuerpo().CollideAgainst(EscogerChar.AxisAlignedRectangles.FindByName("HayekOroFraccion")) && GlobalData.getControl2().ButtonPushed(Xbox360GamePad.Button.A))
                {
                    GlobalData.Personaje2 = "Hayek";
                    GlobalData.proyectilP2 = "Oro";
                    GlobalData.bancaP2 = "Fraccionaria";
                    seleccion2 = true;
                    CurrentState = VariableState.Hayek2;
                    Sprite2.Visible = true;
                }

                else if (Cursores[1].getCuerpo().CollideAgainst(EscogerChar.AxisAlignedRectangles.FindByName("HayekFiatFull")) && GlobalData.getControl2().ButtonPushed(Xbox360GamePad.Button.A))
                {
                    GlobalData.Personaje2 = "Hayek";
                    GlobalData.proyectilP2 = "Bajo";
                    GlobalData.bancaP2 = "Completa";
                    seleccion2 = true;
                    CurrentState = VariableState.Hayek2;
                    Sprite2.Visible = true;
                }

                else if (Cursores[1].getCuerpo().CollideAgainst(EscogerChar.AxisAlignedRectangles.FindByName("HayekFiatFraccion")) && GlobalData.getControl2().ButtonPushed(Xbox360GamePad.Button.A))
                {
                    GlobalData.Personaje2 = "Hayek";
                    GlobalData.proyectilP2 = "Bajo";
                    GlobalData.bancaP2 = "Fraccionaria";
                    seleccion2 = true;
                    CurrentState = VariableState.Hayek2;
                    Sprite2.Visible = true;
                }

                else if (Cursores[1].getCuerpo().CollideAgainst(EscogerChar.AxisAlignedRectangles.FindByName("Keynes")) && GlobalData.getControl2().ButtonPushed(Xbox360GamePad.Button.A))
                {
                    GlobalData.Personaje2 = "Keynes";
                    seleccion2 = true;
                    CurrentState = VariableState.Keynes2;
                    Sprite2.Visible = true;

                }
                else if (Cursores[1].getCuerpo().CollideAgainst(EscogerChar.AxisAlignedRectangles.FindByName("Malthus")) && GlobalData.getControl2().ButtonPushed(Xbox360GamePad.Button.A))
                {
                    GlobalData.Personaje2 = "Malthus";
                    seleccion2 = true;
                    CurrentState = VariableState.Malthus2;
                    Sprite2.Visible = true;

                }
                else if (Cursores[1].getCuerpo().CollideAgainst(EscogerChar.AxisAlignedRectangles.FindByName("Smith")) && GlobalData.getControl2().ButtonPushed(Xbox360GamePad.Button.A))
                {
                    GlobalData.Personaje2 = "Smith";
                    seleccion2 = true;
                    CurrentState = VariableState.Smith2;
                    Sprite2.Visible = true;

                }

            }

        }

        private void Regresar()
        {
            if (Cursores.Count == 2)
            {
                if (Cursores[0].getCuerpo().CollideAgainst(EscogerChar.AxisAlignedRectangles.FindByName("MainMenu")) && GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.A))
                {
                    GlobalData.Personaje2 = "";
                    GlobalData.proyectilP2 = "";
                    GlobalData.bancaP2 = "";
                    GlobalData.Personaje1 = "";
                    GlobalData.proyectilP1 = "";
                    GlobalData.bancaP1 = "";
                    MoveToScreen(typeof(MenuPrincipal).FullName);

                }
                else if (Cursores[1].getCuerpo().CollideAgainst(EscogerChar.AxisAlignedRectangles.FindByName("MainMenu")) && GlobalData.getControl2().ButtonPushed(Xbox360GamePad.Button.A))
                {
                    GlobalData.Personaje2 = "";
                    GlobalData.proyectilP2 = "";
                    GlobalData.bancaP2 = "";
                    GlobalData.Personaje1 = "";
                    GlobalData.proyectilP1 = "";
                    GlobalData.bancaP1 = "";
                    MoveToScreen(typeof(MenuPrincipal).FullName);
                }
                else if (GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.B) || GlobalData.getControl2().ButtonPushed(Xbox360GamePad.Button.B))
                {
                    GlobalData.Personaje2 = "";
                    GlobalData.proyectilP2 = "";
                    GlobalData.bancaP2 = "";
                    GlobalData.Personaje1 = "";
                    GlobalData.proyectilP1 = "";
                    GlobalData.bancaP1 = "";
                    MoveToScreen(typeof(MenuPrincipal).FullName);
                }


            }
            else
            {
                if (Cursores[0].getCuerpo().CollideAgainst(EscogerChar.AxisAlignedRectangles.FindByName("MainMenu")) && GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.A))
                {
                    GlobalData.Personaje2 = "";
                    GlobalData.proyectilP2 = "";
                    GlobalData.bancaP2 = "";
                    GlobalData.Personaje1 = "";
                    GlobalData.proyectilP1 = "";
                    GlobalData.bancaP1 = "";
                    MoveToScreen(typeof(MenuPrincipal).FullName);
                }
                else if (GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.B))
                {
                    GlobalData.Personaje2 = "";
                    GlobalData.proyectilP2 = "";
                    GlobalData.bancaP2 = "";
                    GlobalData.Personaje1 = "";
                    GlobalData.proyectilP1 = "";
                    GlobalData.bancaP1 = "";
                    MoveToScreen(typeof(MenuPrincipal).FullName);
                }
            }
        }


        private void irPelea()
        {
            if (Cursores.Count == 2)
            {
                if (seleccion2 == true && seleccion1 ==true)
                {
                    if (Cursores[0].getCuerpo().CollideAgainst(EscogerChar.AxisAlignedRectangles.FindByName("Pelear")) && GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.A) || Cursores[1].getCuerpo().CollideAgainst(EscogerChar.AxisAlignedRectangles.FindByName("Pelear")) && GlobalData.getControl2().ButtonPushed(Xbox360GamePad.Button.A))
                    {
                        MoveToScreen(typeof(StageSelect).FullName);
                    }
                    else if (GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.Start) || GlobalData.getControl2().ButtonPushed(Xbox360GamePad.Button.Start))
                    {
                        MoveToScreen(typeof(StageSelect).FullName);
                    }
                }
            }
            else
            {
                if (seleccion1 == true)
                {
                    if (Cursores[0].getCuerpo().CollideAgainst(EscogerChar.AxisAlignedRectangles.FindByName("Pelear")) && GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.A))
                    {
                        MoveToScreen(typeof(TutorialSmith).FullName);
                    }
                    else if (GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.Start))
                    {
                        MoveToScreen(typeof(TutorialSmith).FullName);
                    }
                }
            }
        }

	}
}
