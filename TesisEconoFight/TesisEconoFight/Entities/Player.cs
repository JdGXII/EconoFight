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
	public partial class Player
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

        public virtual void RecibirDaño(float daño)
        {


        }

        public virtual void RecibirDañoFlip(float daño)
        {


        }

        public virtual float DarDañoFisico()
        {

            return PhysDamage;
        }

        /* public virtual float DarDañoEcono1()
         {
             return EconDamage1;

         }

         public virtual float DarDañoEcono2()
         {
             return EconDamage2;

         }

         public virtual float DarSuperDaño()
         {
             return SuperDamage;

         }*/

        public virtual void RecibirDañoBarra()
        {


        }

        public virtual void LlenarBarra(BarraPadre barra)
        {

        }

        public virtual void ActualizarBarra()
        {

        }

        public virtual void ActualizarBarraFlip()
        {

        }

        public void setPosicionEnemigo(float x)
        {

            EnemigoX = x;
        }

        public float getPosicion()
        {

            return this.X;
        }

        public AxisAlignedRectangle getCuerpo()
        {
            return Cuerpo;
        }

        public AxisAlignedRectangle getShield()
        {

            return Shield;
        }

        public AxisAlignedRectangle getGolpe()
        {

            return Golpe;
        }

      public virtual void ActividadNormal(Xbox360GamePad control, float x)
      {

      }

      public virtual void ActividadFlip(Xbox360GamePad control, float x)
      {

      }

      public bool getAlive()
      {
          return isAlive;
      }

	}
}
