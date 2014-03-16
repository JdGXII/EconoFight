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
	public partial class SuperBarra
	{

        float mBaseX;
        Sprite llena;
        Sprite vacia;
        

		private void CustomInitialize()
		{

            SpriteManager.Camera.UsePixelCoordinates(false);
            vacia = SpriteManager.AddSprite("CompletaVacia.png");  
            llena= SpriteManager.AddSprite("CompletaLlena.png");
                    
            llena.PixelSize = .5f;
            vacia.PixelSize = .5f;
		}

		private void CustomActivity()
		{


		}

		private void CustomDestroy()
		{
            SpriteManager.RemoveSprite(llena);
            SpriteManager.RemoveSprite(vacia);
            
		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        public override void SetPosicion(float x, float y)
        {
            float auxiliar;
            llena.Y = vacia.Y = y;
            llena.X = x;
            auxiliar = llena.X;
            vacia.X = auxiliar;
            mBaseX = vacia.X - vacia.ScaleX;
            //llena.X = (llena.X) * -1;
            
            

        }

        public override void SetPosicionFlip(float x, float y)
        {
            float auxiliar;
            llena.Y = vacia.Y = y;
            llena.X = x;
            auxiliar = llena.X;
            vacia.X = auxiliar;
            mBaseX = vacia.X - vacia.ScaleX;
            //vacia.X = vacia.X * -1;

            base.SetPosicionFlip(x, y);
        }

        public override void UpdateFill()
        {
            this.CantidadActual = this.CantidadActual + this.FactorLlenado;
            RevisarCantidad();
            vacia.RightTextureCoordinate =1- this.CantidadActual / this.CantidadTotal;
            vacia.X = +vacia.ScaleX + mBaseX;
            //fraccion.X = (fraccion.X) * -1;
        }

        public override void UpdateFillFlip()
        {
            /*this.CantidadActual = this.CantidadActual + this.FactorLlenado;
            RevisarCantidad();
            vacia.LeftTextureCoordinate = this.CantidadActual / this.CantidadTotal;
            vacia.X = -vacia.ScaleX - mBaseX;
            base.UpdateFillFlip();*/
            this.CantidadActual = this.CantidadActual + this.FactorLlenado;
            RevisarCantidad();
            vacia.RightTextureCoordinate = 1 - this.CantidadActual / this.CantidadTotal;
            vacia.X = +vacia.ScaleX + mBaseX;

        }

       


        private void RevisarCantidad()
        {
            if (this.CantidadActual > this.CantidadTotal)
            {
                this.CantidadActual = this.CantidadTotal;
            }
        }

        public override void VaciarBarraPoruso()
        {
            CantidadActual = 0;
            vacia.LeftTextureCoordinate = CantidadActual / CantidadTotal;
            //vacia.X = -vacia.ScaleX - mBaseX;
        }

        public override void VaciarBarraPorusoFlip()
        {
            CantidadActual = 0;
            vacia.LeftTextureCoordinate = CantidadActual / CantidadTotal;
            base.VaciarBarraPorusoFlip();
        }

        public override void VaciarBarraGolpe()
        {
            
            
        }

        public override void VaciarBarraGolpeFlip()
        {
            base.VaciarBarraGolpeFlip();
        }

        public override string getTipo()
        {
            
            return "Full";
        }
        public override float getCantidadActual()
        {
            return this.CantidadActual;
        }
	}
}
