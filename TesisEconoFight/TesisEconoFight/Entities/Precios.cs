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
	public partial class Precios
	{
        float Xenemigo;
        float Yenemigo;
        double TimeCreated;
		private void CustomInitialize()
		{
            TimeCreated = TimeManager.CurrentTime;
            Cuerpo.ScaleX = this.Sprite.ScaleX;
            Cuerpo.ScaleY = this.Sprite.ScaleY;
            Cuerpo.AttachTo(this.Sprite, true);
            this.Sprite.RelativeRotationYVelocity = 3;
            Cuerpo.Visible = false;
           // Shield.Visible = false;
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

        public void setPosicion(float x, float y, float x1, float y1)
        {

            this.X = x;
            this.Y = y;
            this.Xenemigo = x1;
            this.Yenemigo = y1;
        }

        public void Atacar()
        {
            float distanciax;
            float distanciay;
            distanciax = Xenemigo - this.X;
            distanciay= Yenemigo-this.Y;
            

            if (TimeManager.CurrentTime - TimeCreated >= 5.0)
            {
                if (distanciax > 0)
                {
                    this.X = this.X + 20;
                }
                else if (distanciax < 0)
                {
                    this.X = this.X - 20;
                }
                this.Y = this.Y + distanciay;
            }
            
        }
	}
}
