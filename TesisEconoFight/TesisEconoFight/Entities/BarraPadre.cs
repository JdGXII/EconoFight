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
	public partial class BarraPadre
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

        public virtual void SetPosicion(float x, float y)
        {
           

        }

        public virtual void SetPosicionFlip(float x, float y)
        {

        }

        public virtual void UpdateFillFlip()
        {

        }

        public virtual void UpdateFill()
        {
            
        }

        
        public virtual void VaciarBarraPoruso()
        {
            
        }

        public virtual void VaciarBarraPorusoFlip()
        {


        }

        public virtual void VaciarBarraGolpe()
        {

            
        }

        public virtual void VaciarBarraGolpeFlip()
        {

        }

        public virtual string getTipo()
        {
            return "";
        }

        public virtual float getCantidadActual()
        {
            return CantidadActual;
        }
	}
}
