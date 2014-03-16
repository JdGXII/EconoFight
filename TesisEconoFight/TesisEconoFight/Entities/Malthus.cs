using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;


#endif

namespace TesisEconoFight.Entities
{
	public partial class Malthus
	{
        VariableState lastState;
        SuperBarra completa;
        BarraHP HP;
        BarraPoblacional poblacion;
        
        
		private void CustomInitialize()
		{
            this.Cuerpo.ScaleX = this.Sprite.ScaleX * 0.4f;
            this.Cuerpo.ScaleY = this.Sprite.ScaleY * 0.5f;
            this.Cuerpo.AttachTo(this.Sprite, true);
            HP = new BarraHP(ContentManagerName);
            completa = new SuperBarra(ContentManagerName);
            poblacion = new BarraPoblacional(ContentManagerName);
            CurrentState = VariableState.Parado;
            Cuerpo.Visible = false;
            Shield.Visible = false;
            this.Golpe.ScaleX = 0;
            this.Golpe.ScaleY = 0;
		}

		private void CustomActivity()
		{


		}

		private void CustomDestroy()
		{

            this.HP.Destroy();
            this.completa.Destroy();
            this.poblacion.Destroy();
            if (ListaAtaques.Count > 0)
            {
                foreach (Especial i in ListaAtaques)
                {
                    i.Destroy();
                }
            }
            if (ListaAtaqueSuper.Count > 0)
            {
                foreach (SuperAtaque i in ListaAtaqueSuper)
                {
                    i.Destroy();
                }
            }
		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        public override void ActualizarBarra()
        {
           completa.UpdateFill();

            base.ActualizarBarra();
        }

        public override void ActualizarBarraFlip()
        {
            completa.UpdateFillFlip();
            base.ActualizarBarraFlip();
        }

        public void VoltearPersonaje()
        {
            this.CurrentState = VariableState.FlipParado;
        }

        public void setPosicion(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public BarraHP getHP()
        {
            return HP;
        }

        public BarraPoblacional getPoblacion()
        {
            return this.poblacion;
        }

        public SuperBarra getBarra()
        {
            return completa;
        }
        public override void ActividadNormal(Xbox360GamePad controln, float x)
        {
            setPosicionEnemigo(x);
            Caminar(controln);
            Detenerse(controln);
            Agacharse(controln);
            Pararse(controln);
            BloquearArriba(controln);
            BloquearAbajo(controln);
            //GolpeArriba(controln);
           // GolpeBajo(controln);
            AtaqueEspecial1(controln);
            AtaqueEspecial2(controln);
            SuperAtaque(controln);
            sobrePoblacion();
            AccionarAtaques();
            Desatacar1();
            //Desgolpear();
            Morir();

        }

        public override void ActividadFlip(Xbox360GamePad controln2, float x)
        {
            setPosicionEnemigo(x);
            CaminarFlip(controln2);
            DetenerseFlip(controln2);
            AgacharseFlip(controln2);
            PararseFlip(controln2);
            BloquearArribaFlip(controln2);
            BloquearAbajoFlip(controln2);
            //GolpeArribaFlip(controln2);
            //GolpeBajoFlip(controln2);
            AtaqueEspecial1Flip(controln2);
            AtaqueEspecial2Flip(controln2);
            SuperAtaqueFlip(controln2);
            sobrePoblacionFlip();
            AccionarAtaques();
            Morir();
            

        }

        public void ActividadAI(float x)
        {
            setPosicionEnemigo(x);
            DesplazarAI();
            AtacarAI();
            SuperAtacarAI();
            AccionarAtaques();
            MorirAI();

        }

        private void Caminar(Xbox360GamePad control)
        {
            if (this.CurrentState == VariableState.Parado || lastState == VariableState.Caminando && CurrentState != VariableState.Golpeado)
            {
                if (control.ButtonDown(Xbox360GamePad.Button.DPadRight))
                {
                    this.Shield.ScaleX = 0;
                    this.Shield.ScaleY = 0;
                    this.X = this.X + this.Velocidad;
                    this.CurrentState = VariableState.Caminando;
                    this.lastState = VariableState.Caminando;
                }

                else if (control.ButtonDown(Xbox360GamePad.Button.DPadLeft))
                {
                    this.Shield.ScaleX = 0;
                    this.Shield.ScaleY = 0;
                    this.X = this.X - this.Velocidad;
                    this.CurrentState = VariableState.Caminando;
                    this.lastState = VariableState.Caminando;
                }


            }


        }

        private void CaminarFlip(Xbox360GamePad control)
        {
            if (this.CurrentState == VariableState.FlipParado || lastState == VariableState.FlipCaminando && CurrentState != VariableState.FlipGolpeado)
            {
                if (control.ButtonDown(Xbox360GamePad.Button.DPadRight))
                {
                    this.Shield.ScaleX = 0;
                    this.Shield.ScaleY = 0;
                    this.X = this.X + this.Velocidad;
                    this.CurrentState = VariableState.FlipCaminando;
                    this.lastState = VariableState.FlipCaminando;
                }

                else if (control.ButtonDown(Xbox360GamePad.Button.DPadLeft))
                {
                    this.Shield.ScaleX = 0;
                    this.Shield.ScaleY = 0;
                    this.X = this.X - this.Velocidad;
                    this.CurrentState = VariableState.FlipCaminando;
                    this.lastState = VariableState.FlipCaminando;
                }


            }


        }

        private void Detenerse(Xbox360GamePad control)
        {

            if (this.lastState == VariableState.Caminando && CurrentState != VariableState.Golpeado)
            {
                if (control.ButtonReleased(Xbox360GamePad.Button.DPadRight) || control.ButtonReleased(Xbox360GamePad.Button.DPadLeft))
                {
                    this.X = this.X;
                    this.CurrentState = VariableState.Parado;

                }

            }
        }

        private void DetenerseFlip(Xbox360GamePad control)
        {

            if (this.lastState == VariableState.FlipCaminando && CurrentState != VariableState.FlipGolpeado)
            {
                if (control.ButtonReleased(Xbox360GamePad.Button.DPadRight) || control.ButtonReleased(Xbox360GamePad.Button.DPadLeft))
                {
                    this.X = this.X;
                    this.CurrentState = VariableState.FlipParado;

                }

            }
        }

        private void Agacharse(Xbox360GamePad control)
        {
            if (this.CurrentState == VariableState.Parado || this.lastState == VariableState.Caminando || lastState == VariableState.BloqueoArriba || lastState == VariableState.BloqueoAbajo && CurrentState != VariableState.Golpeado)
            {
                if (control.ButtonDown(Xbox360GamePad.Button.DPadDown) && !control.AnyButtonPushed())
                {
                    this.X = this.X;
                    // this.Shield = null;
                    this.Shield.ScaleX = 0;
                    this.Shield.ScaleY = 0;
                    this.Cuerpo.Detach();
                    this.Cuerpo.Y = this.Cuerpo.Y * 2;
                    this.Cuerpo.ScaleY = this.Cuerpo.ScaleY * 0.7f;
                    this.CurrentState = VariableState.Agachado;
                    lastState = VariableState.Agachado;

                }

            }

        }

        private void AgacharseFlip(Xbox360GamePad control)
        {
            if (this.CurrentState == VariableState.FlipParado || this.lastState == VariableState.FlipCaminando || lastState == VariableState.FlipBloqueoArriba || lastState == VariableState.FlipBloqueoAbajo && CurrentState != VariableState.FlipGolpeado)
            {
                if (control.ButtonDown(Xbox360GamePad.Button.DPadDown) && !control.AnyButtonPushed())
                {
                    this.X = this.X;
                    // this.Shield = null;
                    this.Shield.ScaleX = 0;
                    this.Shield.ScaleY = 0;
                    this.Cuerpo.Detach();
                    this.Cuerpo.Y = this.Cuerpo.Y * 2;
                    this.Cuerpo.ScaleY = this.Cuerpo.ScaleY * 0.7f;
                    this.CurrentState = VariableState.FlipAgachado;
                    lastState = VariableState.FlipAgachado;

                }

            }

        }

        private void Pararse(Xbox360GamePad control)
        {
            if (lastState == VariableState.Agachado || lastState == VariableState.BloqueoAbajo && CurrentState != VariableState.Golpeado)
            {
                if (control.ButtonReleased(Xbox360GamePad.Button.DPadDown) && !control.ButtonDown(Xbox360GamePad.Button.A))
                {
                    //Shield.Detach();
                    // this.Shield = null;
                    this.Shield.ScaleX = 0;
                    this.Shield.ScaleY = 0;
                    this.Cuerpo.ScaleY = this.Sprite.ScaleY * 0.5f;
                    //this.Cuerpo.Y = this.Cuerpo.Y/2;
                    this.Cuerpo.Y = this.Sprite.Y;
                    this.Cuerpo.AttachTo(this.Sprite, true);
                    this.CurrentState = VariableState.Parado;


                }

            }

            else if (lastState == VariableState.BloqueoArriba && CurrentState != VariableState.Golpeado)
            {
                if (control.ButtonReleased(Xbox360GamePad.Button.DPadLeft) || control.ButtonReleased(Xbox360GamePad.Button.A))
                {
                    //Shield.Detach();
                    // this.Shield = null;
                    this.Shield.ScaleX = 0;
                    this.Shield.ScaleY = 0;
                    this.Cuerpo.ScaleY = this.Sprite.ScaleY * 0.5f;
                    //this.Cuerpo.Y = this.Cuerpo.Y/2;
                    this.Cuerpo.Y = this.Sprite.Y;
                    this.Cuerpo.AttachTo(this.Sprite, true);
                    this.CurrentState = VariableState.Parado;
                }

            }
        }

        private void PararseFlip(Xbox360GamePad control)
        {
            if (lastState == VariableState.FlipAgachado || lastState == VariableState.FlipBloqueoAbajo && CurrentState != VariableState.FlipGolpeado)
            {
                if (control.ButtonReleased(Xbox360GamePad.Button.DPadDown) && !control.ButtonDown(Xbox360GamePad.Button.A))
                {
                    //Shield.Detach();
                    // this.Shield = null;
                    this.Shield.ScaleX = 0;
                    this.Shield.ScaleY = 0;
                    this.Cuerpo.ScaleY = this.Sprite.ScaleY * 0.5f;
                    //this.Cuerpo.Y = this.Cuerpo.Y/2;
                    this.Cuerpo.Y = this.Sprite.Y;
                    this.Cuerpo.AttachTo(this.Sprite, true);
                    this.CurrentState = VariableState.FlipParado;


                }

            }

            else if (lastState == VariableState.FlipBloqueoArriba && CurrentState != VariableState.FlipGolpeado)
            {
                if (control.ButtonReleased(Xbox360GamePad.Button.DPadLeft) || control.ButtonReleased(Xbox360GamePad.Button.A))
                {
                    //Shield.Detach();
                    // this.Shield = null;
                    this.Shield.ScaleX = 0;
                    this.Shield.ScaleY = 0;
                    this.Cuerpo.ScaleY = this.Sprite.ScaleY * 0.5f;
                    //this.Cuerpo.Y = this.Cuerpo.Y/2;
                    this.Cuerpo.Y = this.Sprite.Y;
                    this.Cuerpo.AttachTo(this.Sprite, true);
                    this.CurrentState = VariableState.FlipParado;
                }

            }
        }

        private void BloquearArriba(Xbox360GamePad control)
        {
            if (CurrentState == VariableState.Parado || CurrentState == VariableState.Caminando || lastState == VariableState.BloqueoArriba && CurrentState != VariableState.Golpeado)
            {
                if (control.ButtonDown(Xbox360GamePad.Button.DPadLeft) && control.ButtonDown(Xbox360GamePad.Button.A))
                {
                    this.X = this.X;
                    //AxisAlignedRectangle blq = ShapeManager.AddAxisAlignedRectangle();
                    this.Shield.ScaleX = this.Sprite.ScaleX * 0.5f;
                    this.Shield.ScaleY = this.Sprite.ScaleY * 0.4f;
                    this.Shield.X = this.X + 100;
                    this.Shield.Y = this.Y;

                    //this.Shield.AttachTo(this.Sprite, true);
                    //ShapeManager.Remove(blq);
                    CurrentState = VariableState.BloqueoArriba;
                    lastState = VariableState.BloqueoArriba;

                }
            }

            else if (lastState == VariableState.BloqueoAbajo || lastState == VariableState.Agachado && CurrentState != VariableState.Golpeado)
            {
                if (control.ButtonReleased(Xbox360GamePad.Button.DPadDown) && control.ButtonDown(Xbox360GamePad.Button.DPadLeft) && control.ButtonDown(Xbox360GamePad.Button.A))
                {
                    this.Shield.ScaleX = this.Sprite.ScaleX * 0.5f;
                    this.Shield.ScaleY = this.Sprite.ScaleY * 0.4f;
                    this.Shield.X = this.X + 100;
                    this.Shield.Y = this.Y;
                    CurrentState = VariableState.BloqueoArriba;
                    lastState = VariableState.BloqueoArriba;
                }
            }

        }

        private void BloquearArribaFlip(Xbox360GamePad control)
        {
            if (CurrentState == VariableState.FlipParado || CurrentState == VariableState.FlipCaminando || lastState == VariableState.FlipBloqueoArriba && CurrentState != VariableState.FlipGolpeado)
            {
                if (control.ButtonDown(Xbox360GamePad.Button.DPadRight) && control.ButtonDown(Xbox360GamePad.Button.A))
                {
                    this.Shield.ScaleX = this.Sprite.ScaleX * 0.5f;
                    this.Shield.ScaleY = this.Sprite.ScaleY * 0.4f;
                    this.Shield.X = this.X + 100;
                    this.Shield.Y = this.Y;
                    CurrentState = VariableState.FlipBloqueoArriba;
                    lastState = VariableState.FlipBloqueoArriba;

                }
            }

            else if (lastState == VariableState.FlipBloqueoAbajo || lastState == VariableState.FlipAgachado && CurrentState != VariableState.FlipGolpeado)
            {
                if (control.ButtonReleased(Xbox360GamePad.Button.DPadDown) && control.ButtonDown(Xbox360GamePad.Button.DPadRight) && control.ButtonDown(Xbox360GamePad.Button.A))
                {
                    this.Shield.ScaleX = this.Sprite.ScaleX * 0.5f;
                    this.Shield.ScaleY = this.Sprite.ScaleY * 0.4f;
                    this.Shield.X = this.X;
                    this.Shield.Y = this.Y + 40;
                    CurrentState = VariableState.FlipBloqueoArriba;
                    lastState = VariableState.FlipBloqueoArriba;
                }
            }

        }

        private void BloquearAbajo(Xbox360GamePad control)
        {
            if (CurrentState == VariableState.Agachado || lastState == VariableState.BloqueoAbajo && CurrentState != VariableState.Golpeado)
            {
                if (control.ButtonDown(Xbox360GamePad.Button.DPadDown) && control.ButtonDown(Xbox360GamePad.Button.A))
                {

                    //this.Shield.Detach();
                    this.Shield.ScaleX = this.Sprite.ScaleX * 0.5f;
                    this.Shield.ScaleY = this.Sprite.ScaleY * 0.4f;
                    this.Shield.X = this.X + 100;
                    this.Shield.Y = this.Y;
                    CurrentState = VariableState.BloqueoAbajo;
                    lastState = VariableState.BloqueoAbajo;

                }
            }

            else if (lastState == VariableState.BloqueoArriba && CurrentState != VariableState.Golpeado)
            {
                if (control.ButtonReleased(Xbox360GamePad.Button.DPadLeft) && control.ButtonDown(Xbox360GamePad.Button.DPadDown) && control.ButtonDown(Xbox360GamePad.Button.A))
                {
                    this.Shield.ScaleX = this.Sprite.ScaleX * 0.5f;
                    this.Shield.ScaleY = this.Sprite.ScaleY * 0.4f;
                    this.Shield.X = this.X + 100;
                    this.Shield.Y = this.Y;
                    CurrentState = VariableState.BloqueoAbajo;
                    lastState = VariableState.BloqueoAbajo;
                }
            }

            else if (lastState == VariableState.Caminando || CurrentState == VariableState.Parado && CurrentState != VariableState.Golpeado)
            {
                if (control.ButtonDown(Xbox360GamePad.Button.DPadDown) && control.ButtonDown(Xbox360GamePad.Button.A))
                {
                    this.X = this.X;
                    this.Shield.ScaleX = this.Sprite.ScaleX * 0.5f;
                    this.Shield.ScaleY = this.Sprite.ScaleY * 0.4f;
                    this.Shield.X = this.X + 100;
                    this.Shield.Y = this.Y;
                    CurrentState = VariableState.BloqueoAbajo;
                    lastState = VariableState.BloqueoAbajo;

                }


            }


        }

        private void BloquearAbajoFlip(Xbox360GamePad control)
        {
            if (CurrentState == VariableState.FlipAgachado || lastState == VariableState.FlipBloqueoAbajo && CurrentState != VariableState.FlipGolpeado)
            {
                if (control.ButtonDown(Xbox360GamePad.Button.DPadDown) && control.ButtonDown(Xbox360GamePad.Button.A))
                {
                    this.Shield.ScaleX = this.Sprite.ScaleX * 0.5f;
                    this.Shield.ScaleY = this.Sprite.ScaleY * 0.4f;
                    this.Shield.X = this.X + 100;
                    this.Shield.Y = this.Y;
                    CurrentState = VariableState.FlipBloqueoAbajo;
                    lastState = VariableState.FlipBloqueoAbajo;

                }
            }

            else if (lastState == VariableState.FlipBloqueoArriba && CurrentState != VariableState.FlipGolpeado)
            {
                if (control.ButtonReleased(Xbox360GamePad.Button.DPadRight) && control.ButtonDown(Xbox360GamePad.Button.DPadDown) && control.ButtonDown(Xbox360GamePad.Button.A))
                {
                    this.Shield.ScaleX = this.Sprite.ScaleX * 0.5f;
                    this.Shield.ScaleY = this.Sprite.ScaleY * 0.4f;
                    this.Shield.X = this.X + 100;
                    this.Shield.Y = this.Y;
                    CurrentState = VariableState.FlipBloqueoAbajo;
                    lastState = VariableState.FlipBloqueoAbajo;
                }
            }

            else if (lastState == VariableState.FlipCaminando || CurrentState == VariableState.FlipParado && CurrentState != VariableState.FlipGolpeado)
            {
                if (control.ButtonDown(Xbox360GamePad.Button.DPadDown) && control.ButtonDown(Xbox360GamePad.Button.A))
                {
                    this.X = this.X;
                    this.Shield.ScaleX = this.Sprite.ScaleX * 0.5f;
                    this.Shield.ScaleY = this.Sprite.ScaleY * 0.4f;
                    this.Shield.X = this.X + 100;
                    this.Shield.Y = this.Y;
                    CurrentState = VariableState.FlipBloqueoAbajo;
                    lastState = VariableState.FlipBloqueoAbajo;

                }


            }


        }

        /*private void GolpeArriba(Xbox360GamePad control)
        {
            if (CurrentState == VariableState.Parado || CurrentState == VariableState.Caminando || CurrentState == VariableState.BloqueoArriba && CurrentState != VariableState.Golpeado)
            {
                if (control.ButtonPushed(Xbox360GamePad.Button.B))
                {
                    this.X = this.X;
                    this.Shield.ScaleX = 0;
                    this.Shield.ScaleY = 0;
                    AxisAlignedRectangle glp = ShapeManager.AddAxisAlignedRectangle();
                    glp.ScaleX = this.Sprite.ScaleX * 1.5f;
                    glp.ScaleY = this.Sprite.ScaleY * 0.1f;
                    glp.X = this.Sprite.X;
                    glp.Y = this.Sprite.Y;
                    glp.XVelocity = 35;
                    this.Golpe = glp;
                    ShapeManager.Remove(glp);
                    CurrentState = VariableState.GolpeArriba;

                }
            }

            if (CurrentState == VariableState.GolpeArriba && this.Sprite.CurrentFrameIndex == 2)
            {
                Golpe.ScaleX = 0;
                Golpe.ScaleY = 0;
                Golpe.XVelocity=0;
                CurrentState = VariableState.Parado;
            }
        }

        private void GolpeArribaFlip(Xbox360GamePad control)
        {
            if (CurrentState == VariableState.FlipParado || CurrentState == VariableState.FlipCaminando || CurrentState == VariableState.FlipBloqueoArriba && CurrentState != VariableState.FlipGolpeado)
            {
                if (control.ButtonPushed(Xbox360GamePad.Button.B))
                {
                    this.X = this.X;
                    this.Shield.ScaleX = 0;
                    this.Shield.ScaleY = 0;
                    AxisAlignedRectangle glp = ShapeManager.AddAxisAlignedRectangle();
                    glp.ScaleX = this.Sprite.ScaleX * 1.5f;
                    glp.ScaleY = this.Sprite.ScaleY * 0.1f;
                    glp.X = this.Sprite.X;
                    glp.Y = this.Sprite.Y;
                    glp.XVelocity = -35;
                    this.Golpe = glp;
                    ShapeManager.Remove(glp);
                    CurrentState = VariableState.FlipGolpeArriba;

                }
            }

            if (CurrentState == VariableState.FlipGolpeArriba && this.Sprite.CurrentFrameIndex == 2)
            {
                Golpe.ScaleX = 0;
                Golpe.ScaleY = 0;
              Golpe.XVelocity=0;
                CurrentState = VariableState.FlipParado;
            }
        }

        private void GolpeBajo(Xbox360GamePad control)
        {
            if (CurrentState == VariableState.Agachado || CurrentState == VariableState.BloqueoAbajo && CurrentState != VariableState.Golpeado)
            {
                if (control.ButtonPushed(Xbox360GamePad.Button.B))
                {
                    this.Shield.ScaleX = 0;
                    this.Shield.ScaleY = 0;
                    AxisAlignedRectangle glp = ShapeManager.AddAxisAlignedRectangle();
                    glp.ScaleX = this.Sprite.ScaleX * 1.5f;
                    glp.ScaleY = this.Sprite.ScaleY * 0.1f;
                    glp.X = this.Sprite.X;
                    glp.Y = this.Sprite.Y;
                    Golpe.XVelocity = 35;
                    this.Golpe = glp;
                    ShapeManager.Remove(glp);
                    CurrentState = VariableState.GolpeAbajo;

                }
            }

            if (CurrentState == VariableState.GolpeAbajo && this.Sprite.CurrentFrameIndex == 2)
            {
                Golpe.ScaleX = 0;
                Golpe.ScaleY = 0;
                Golpe.XVelocity=0;
                if (control.ButtonDown(Xbox360GamePad.Button.DPadDown))
                {
                    CurrentState = VariableState.Agachado;
                }
                else
                {
                    CurrentState = VariableState.Parado;
                }
            }
        }

        private void GolpeBajoFlip(Xbox360GamePad control)
        {
            if (CurrentState == VariableState.FlipAgachado || CurrentState == VariableState.FlipBloqueoAbajo && CurrentState != VariableState.FlipGolpeado)
            {
                if (control.ButtonPushed(Xbox360GamePad.Button.B))
                {
                    this.Shield.ScaleX = 0;
                    this.Shield.ScaleY = 0;
                    AxisAlignedRectangle glp = ShapeManager.AddAxisAlignedRectangle();
                    glp.ScaleX = this.Sprite.ScaleX * 1.5f;
                    glp.ScaleY = this.Sprite.ScaleY * 0.1f;
                    glp.X = this.Sprite.X;
                    glp.Y = this.Sprite.Y;
                    Golpe.XVelocity = -35;
                    this.Golpe = glp;
                    ShapeManager.Remove(glp);
                    CurrentState = VariableState.FlipGolpeAbajo;

                }
            }

            if (CurrentState == VariableState.FlipGolpeAbajo && this.Sprite.CurrentFrameIndex == 2)
            {
                Golpe.ScaleX = 0;
                Golpe.ScaleY = 0;
                Golpe.XVelocity=0;
                if (control.ButtonDown(Xbox360GamePad.Button.DPadDown))
                {
                    CurrentState = VariableState.FlipAgachado;
                }
                else
                {
                    CurrentState = VariableState.FlipParado;
                }
            }
        }*/

        public override void RecibirDaño(float daño)
        {
            RevisarVida(daño);
            HP.UpdateFill(this.VidaActual, this.VidaTotal);
            completa.VaciarBarraGolpe();
            base.RecibirDaño(daño);
        }

        public override void RecibirDañoFlip(float daño)
        {
            RevisarVidaFlip(daño);
            HP.UpdateFillFlip(this.VidaActual, this.VidaTotal);
            completa.VaciarBarraGolpeFlip();
            base.RecibirDañoFlip(daño);
        }

        private void RevisarVida(float damage)
        {
            if (VidaActual - damage <= 0)
            {
                VidaActual = 0;
                this.CurrentState = VariableState.Muerto;
                this.isAlive = false;
            }

            else
            {
                VidaActual = VidaActual - damage;
                CurrentState = VariableState.Golpeado;

            }

            
        }

        private void Desgolpear()
        {
            if (CurrentState == VariableState.Golpeado && this.Sprite.CurrentFrameIndex == 2)
            {

                CurrentState = VariableState.Parado;
            }
        }

        private void RevisarVidaFlip(float damage)
        {
            if (VidaActual - damage <= 0)
            {
                VidaActual = 0;
                this.CurrentState = VariableState.FlipMuerto;
                this.isAlive = false;
            }

            else
            {
                VidaActual = VidaActual - damage;
                CurrentState = VariableState.FlipGolpeado;
            }

            if (CurrentState == VariableState.FlipGolpeado && this.Sprite.CurrentFrameIndex == 2)
            {

                CurrentState = VariableState.FlipParado;
            }
        }

        private void AtaqueEspecial1(Xbox360GamePad control)
        {
            if (CurrentState == VariableState.Parado)
            {
                if (control.ButtonDown(Xbox360GamePad.Button.DPadUp) && control.ButtonPushed(Xbox360GamePad.Button.X))
                {
                    
                    this.CurrentState = VariableState.Especial1;
                    Fetoyectil feto = new Fetoyectil(ContentManagerName);
                    //feto.X = this.X;
                    //feto.Y = this.Y;
                    feto.setPosicion(this.X + 3,this.Y + 2, this.EnemigoX);
                    ListaAtaques.Add(feto);
                    //feto.Destroy();
                    poblacion.UpdateFillFlip();
                }

            }

            if (CurrentState == VariableState.Especial1 && this.Sprite.CurrentFrameIndex == 1)
            {
                //Fetoyectil feto = new Fetoyectil(ContentManagerName);
               // feto.setPosicion(400/*this.X + 3*/, -120/*this.Y + 2*/, this.EnemigoX);
                //ListaAtaques.Add(feto);
                //feto.Destroy();
               // poblacion.UpdateFillFlip();
               
            }

            
        }

        private void Desatacar1()
        {
            if (this.Sprite.CurrentFrameIndex == 1 && CurrentState == VariableState.Especial1)
            {

                CurrentState = VariableState.Parado;
            }
        }

        private void AtaqueEspecial1Flip(Xbox360GamePad control)
        {
            if (CurrentState == VariableState.FlipParado)
            {
                if (control.ButtonDown(Xbox360GamePad.Button.DPadUp) && control.ButtonPushed(Xbox360GamePad.Button.X))
                {
                    
                    this.CurrentState = VariableState.FlipEspecial1;

                }

            }

            if (CurrentState == VariableState.FlipEspecial1 && this.Sprite.CurrentFrameIndex == 1)
            {

                Fetoyectil feto = new Fetoyectil(ContentManagerName);
                feto.setPosicion(this.X - 3, this.Y + 2, this.EnemigoX);
                ListaAtaques.Add(feto);
                //feto.Destroy();
                poblacion.UpdateFillFlip();
            }

            if (CurrentState == VariableState.FlipEspecial1 && this.Sprite.CurrentFrameIndex == 2)
            {

                CurrentState = VariableState.FlipParado;
            }
        }

        private void AtaqueEspecial2(Xbox360GamePad control)
        {
            if (CurrentState == VariableState.Parado)
            {
                if (control.ButtonDown(Xbox360GamePad.Button.DPadUp) && control.ButtonPushed(Xbox360GamePad.Button.Y))
                {
                    
                    this.CurrentState = VariableState.Especial2;

                }


            }

            if (CurrentState == VariableState.Especial2 && this.Sprite.CurrentFrameIndex == 2)
            {

                Malnutrido malnutrido = new Malnutrido(ContentManagerName);
                malnutrido.setPosicion(this.X, this.Y, this.EnemigoX);
                ListaAtaques.Add(malnutrido);
                //malnutrido.Destroy();
                poblacion.UpdateFillFlip();
            }

            if (CurrentState == VariableState.Especial2 && this.Sprite.CurrentFrameIndex == 3)
            {

                CurrentState = VariableState.Parado;
            }
        }

        private void AtaqueEspecial2Flip(Xbox360GamePad control)
        {
            if (CurrentState == VariableState.FlipParado)
            {
                if (control.ButtonDown(Xbox360GamePad.Button.DPadUp) && control.ButtonPushed(Xbox360GamePad.Button.Y))
                {
                    
                    this.CurrentState = VariableState.FlipEspecial2;

                }


            }

            if (CurrentState == VariableState.FlipEspecial2 && this.Sprite.CurrentFrameIndex == 2)
            {

                Malnutrido malnutrido = new Malnutrido(ContentManagerName);
                malnutrido.setPosicion(this.X, this.Y, this.EnemigoX);
                ListaAtaques.Add(malnutrido);
                //malnutrido.Destroy();
                poblacion.UpdateFillFlip();
            }


            if (CurrentState == VariableState.FlipEspecial2 && this.Sprite.CurrentFrameIndex == 3)
            {

                CurrentState = VariableState.FlipParado;
            }
        }

        private void SuperAtaque(Xbox360GamePad control)
        {
            if (CurrentState == VariableState.Parado)
            {

                if (completa.getCantidadActual() == 100)
                {
                    if(control.ButtonDown(Xbox360GamePad.Button.B))//if (control.ButtonDown(Xbox360GamePad.Button.LeftTrigger) && control.ButtonDown(Xbox360GamePad.Button.RightTrigger))
                    {
                        completa.VaciarBarraPoruso();
                        //hacer super ataque
                        SuperAtaque super = new SuperAtaque(ContentManagerName);
                        super.setPosicion(this.EnemigoX);
                        super.setAnimacion(this.Nombre);
                        ListaAtaqueSuper.Add(super);
                        //super.Destroy();
                        CurrentState = VariableState.Super;
                    }
                }
            }


            if (CurrentState == VariableState.Super && this.Sprite.CurrentFrameIndex == 2)
            {

                CurrentState = VariableState.Parado;
            }
        }

        private void SuperAtaqueFlip(Xbox360GamePad control)
        {
            if (CurrentState == VariableState.FlipParado)
            {


                if (completa.getCantidadActual() == 100)
                {
                    if (control.ButtonDown(Xbox360GamePad.Button.B))//if (control.ButtonPushed(Xbox360GamePad.Button.LeftTrigger) && control.ButtonPushed(Xbox360GamePad.Button.RightTrigger))
                    {
                        completa.VaciarBarraPoruso();
                        //hacer super ataque
                        SuperAtaque super = new SuperAtaque(ContentManagerName);
                        super.setPosicion(this.EnemigoX);
                        super.setAnimacion(this.Nombre);
                        ListaAtaqueSuper.Add(super);
                        //super.Destroy();
                        CurrentState = VariableState.FlipSuper;
                    }
                }

            }

            if (CurrentState == VariableState.FlipSuper && this.Sprite.CurrentFrameIndex == 2)
            {

                CurrentState = VariableState.FlipParado;
            }
        }

        private void AccionarAtaques()
        {
            if (ListaAtaques.Count > 0)
            {
                foreach (Especial i in ListaAtaques)
                {
                    i.Activity();
                }
            }

            if (ListaAtaqueSuper.Count > 0)
            {
                foreach (SuperAtaque i in ListaAtaqueSuper)
                {
                    i.Activity();
                }
            }
        }

        private void sobrePoblacion()
        {
            if (poblacion.getCantidadActual() == 100)
            {
                HP.UpdateFill(this.VidaActual - 500, this.VidaTotal);

                
            }
        }

        private void sobrePoblacionFlip()
        {
            if (poblacion.getCantidadActual() == 100)
            {
                HP.UpdateFillFlip(this.VidaActual - 500, this.VidaTotal);


            }
        }

        private void Morir()
        {
            if (this.VidaActual == 0)
            {
                this.Sprite.Visible = false;
                
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        //                                                                                                   //
        //                                                                                                   //
        //                                                                                                   //
        //                                          INTELIGENCIA ARTIFICIAL DESPUES DE ESTO                  //
        //                                                                                                   //
        ///////////////////////////////////////////////////////////////////////////////////////////////////////

        private void DesplazarAI()
        {
            float distancia;
            float absoluta;

            distancia = this.EnemigoX - this.X;
            absoluta = Math.Abs(distancia);
            if (distancia > 0)
            {
                if (absoluta < 10)
                {
                    this.X = this.X - this.Velocidad;
                }
            }
            else if (distancia < 0)
            {
                if (absoluta < 10 && (CurrentState == VariableState.FlipParado || CurrentState == VariableState.FlipCaminando))
                {
                    CurrentState = VariableState.FlipCaminando;
                    this.X = this.X + Velocidad;

                }
            }
            if (absoluta > 10)
            {
                CurrentState = VariableState.FlipParado;
            }
        }

        private void AtacarAI()
        {
            Random random = new Random();
            double numero = random.NextDouble() * 100;
            double numero2 = random.NextDouble() * 100;
            double tiempo = TimeManager.CurrentTime + 3;
            if (numero < 8 )
            {
                if (numero2 > 90)
                {
                    Fetoyectil feto = new Fetoyectil(ContentManagerName);
                    feto.setPosicion(this.X - 3, this.Y + 2, this.EnemigoX);
                    ListaAtaques.Add(feto);
                    //feto.Destroy();
                    poblacion.UpdateFillFlip();
                    this.CurrentState = VariableState.FlipEspecial1;
                }

            }

            else if (numero > 90)
            {

                if (numero2 < 5)
                {
                    Malnutrido malnutrido = new Malnutrido(ContentManagerName);
                    malnutrido.setPosicion(this.X, this.Y, this.EnemigoX);
                    ListaAtaques.Add(malnutrido);
                    //malnutrido.Destroy();
                    poblacion.UpdateFillFlip();
                    this.CurrentState = VariableState.FlipEspecial2;
                }
            }

            /*if (CurrentState == VariableState.FlipEspecial1 && this.Sprite.CurrentFrameIndex == 1)
            {

                Fetoyectil feto = new Fetoyectil(ContentManagerName);
                feto.setPosicion(this.X - 3, this.Y + 2, this.EnemigoX);
                ListaAtaques.Add(feto);
                //feto.Destroy();
                poblacion.UpdateFillFlip();
            }

            else if (CurrentState == VariableState.FlipEspecial2 && this.Sprite.CurrentFrameIndex == 2)
            {

                Malnutrido malnutrido = new Malnutrido(ContentManagerName);
                malnutrido.setPosicion(this.X, this.Y, this.EnemigoX);
                ListaAtaques.Add(malnutrido);
                //malnutrido.Destroy();
                poblacion.UpdateFillFlip();
            }

            if (CurrentState == VariableState.FlipEspecial1 && this.Sprite.CurrentFrameIndex == 2)
            {

                CurrentState = VariableState.FlipParado;
            }

            else if (CurrentState == VariableState.FlipEspecial2 && this.Sprite.CurrentFrameIndex == 3)
            {

                CurrentState = VariableState.FlipParado;
            }*/

        }

        private void SuperAtacarAI()
        {
            //double tiempo = TimeManager.CurrentTime + 1;
            if (completa.getCantidadActual() == 100)
            {
               completa.VaciarBarraPorusoFlip();
                //hacer super ataque
                SuperAtaque super = new SuperAtaque(ContentManagerName);
                super.setPosicion(this.EnemigoX);
                super.setAnimacion(this.Nombre);
                ListaAtaqueSuper.Add(super);
                CurrentState = VariableState.FlipSuper;
            }

            if (CurrentState == VariableState.FlipSuper && this.Sprite.CurrentFrameIndex == 2)
            {

                CurrentState = VariableState.FlipParado;
            }
        }

        private void MorirAI()
        {
            if (this.VidaActual == 0)
            {
                this.Sprite.Visible = false;
                foreach (Especial i in ListaAtaques)
                {
                    i.Destroy();
                }
            }
        }
	}
}
