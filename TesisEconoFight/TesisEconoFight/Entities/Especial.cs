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
	public partial class Especial
	{
		private void CustomInitialize()
		{

            
		}

		private void CustomActivity()
		{


		}

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        public AxisAlignedRectangle getCuerpo()
        {
            return Cuerpo;
        }

        public float giveDamage()
        {

            return Damage;
        }

        public void setDamage(float d)
        {
            Damage =Damage+d;
        }

        public virtual string getTipo()
        {
            return Tipo;
        }

        public virtual void setVida(float damage)
        {
           
        }

        public virtual float getVida()
        {
            return 0;
        }
	}
}
