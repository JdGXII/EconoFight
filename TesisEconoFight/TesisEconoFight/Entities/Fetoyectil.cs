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
	public partial class Fetoyectil
	{
        float Xenemigo;
        double TimeCreated;
        float posi;
		private void CustomInitialize()
		{
            TimeCreated=TimeManager.CurrentTime;
            this.Cuerpo.ScaleX = this.Sprite.ScaleX;
            this.Cuerpo.ScaleY = this.Sprite.ScaleY;
            Cuerpo.AttachTo(this.Sprite, true);
            Cuerpo.Visible = false;
            //Shield.Visible = false;
            
		}

		private void CustomActivity()
		{
            
            Atacar();
            if (TimeManager.CurrentTime - TimeCreated >= 5.0)
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
            posi = x;

        }

        public void Atacar()
        {
            float distanciax;

            distanciax = Xenemigo - posi;
            this.YVelocity = VelocidadY;
            this.YAcceleration = -5000; //Desasceleracion;
            
                if (distanciax > 0)
                {
                    this.XVelocity = VelocidadX*2;
                }

                else
                {
                    this.XVelocity = -VelocidadX*2;
                }

       }
            
	}
}
