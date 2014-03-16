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
	public partial class Proyectil
	{

        float Xenemigo;
        double TimeCreated;
       
		private void CustomInitialize()
		{
            TimeCreated = TimeManager.CurrentTime;
            this.Cuerpo.ScaleX = this.Sprite.ScaleX;
            this.Cuerpo.ScaleY = this.Sprite.ScaleY;
            Cuerpo.AttachTo(this.Sprite, true);
            Cuerpo.Visible = false;
            //Shield.Visible = false;

            

		}

		private void CustomActivity()
		{
            Atacar();
            if (TimeManager.CurrentTime - TimeCreated >= 7.0)
            {
                this.Destroy();
            }

		}

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        public void setPosicion(float x, float y, float x1)
        {

            this.X = x;
            this.Y = y;
            this.Xenemigo = x1;
            
        }
        
       

        public void setTipo(string estado)
        {
            if (estado == "Oro")
            {
                this.CurrentState = VariableState.Oro;
                this.Damage = 200;
                this.Estado = estado;
                this.Tipo = estado;
            }

            else if (estado == "Alto")
            {
                this.CurrentState = VariableState.FiatAlto;
                this.Damage = 200;
                this.Estado = estado;
                this.Tipo = estado;
            }

            else if (estado == "Natural")
            {
                this.CurrentState = VariableState.FiatNatural;
                this.Damage = 90;
                this.Estado = estado;
                this.Tipo = estado;
            }

            else if (estado == "Bajo")
            {
                this.CurrentState = VariableState.FiatBajo;
                this.Damage = 50;
                this.Estado = estado;
                this.Tipo = estado;
            }
            
        }

        public void Atacar()
        {
            float distanciax;
           
            distanciax = Xenemigo - this.X;
            if (CurrentState==VariableState.Oro || CurrentState==VariableState.FiatAlto)
            {
                this.Velocidad = 85;
                if (distanciax > 0)
                {
                    this.XVelocity = Velocidad;
                }

                else
                {
                    this.XVelocity = -Velocidad;
                }

            }
            else if (CurrentState == VariableState.FiatNatural)
            {
                this.Velocidad = 130;
                if (distanciax > 0)
                {
                    this.XVelocity = Velocidad;
                }

                else
                {
                    this.XVelocity = -Velocidad;
                }
            }

            else if (CurrentState == VariableState.FiatBajo)
            {
                this.Velocidad = 235;
                if (distanciax > 0)
                {
                    this.XVelocity = Velocidad;
                }

                else
                {
                    this.XVelocity = -Velocidad;
                }
            }
        }

        public string getEstado()
        {
            return this.Estado;
        }
	}
}
