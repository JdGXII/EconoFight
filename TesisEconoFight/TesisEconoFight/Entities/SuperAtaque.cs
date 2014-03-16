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
	public partial class SuperAtaque
	{
        float Xenemigo;
        double TimeCreated;
		private void CustomInitialize()
		{
            TimeCreated = TimeManager.CurrentTime;
            this.X = 0;
            this.Y = 0;
            Cuerpo.ScaleX = this.Sprite.ScaleX;
            Cuerpo.ScaleY = this.Sprite.ScaleY;
            Cuerpo.AttachTo(this.Sprite, true);
            Cuerpo.Visible = false;
           // Shield.Visible = false;
		}

		private void CustomActivity()
		{
            Atacar();
            //Revisar TimeCreated, puede que no funciene asi
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

        public void setAnimacion(string nombre)
        {
            if(nombre=="Hayek")
            {
                CurrentState=VariableState.Hayek;
            }
            else if(nombre=="Keynes")
            {
                CurrentState = VariableState.Keynes;
            }

            else if (nombre == "Malthus")
            {
                CurrentState = VariableState.Malthus;
            }
            else if (nombre == "Smith")
            {
                CurrentState = VariableState.Smith;
            }

        }

        public void setPosicion(float xenemigo)
        {
            Xenemigo = xenemigo;
        }

        public void Atacar()
        {
            if (Xenemigo > 0)
            {
                this.XVelocity = Velocidad;
            }
            else
            {
                this.XVelocity = -Velocidad;
            }

            
        }

        public AxisAlignedRectangle getCuerpo()
        {
            return this.Cuerpo;
        }

        public float giveDamage()
        {
            return this.Damage;
        } 
	}
}
