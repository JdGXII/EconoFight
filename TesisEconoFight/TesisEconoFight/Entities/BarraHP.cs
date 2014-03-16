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
	public partial class BarraHP
	{
        float mBaseX;
        Sprite llena;
        Sprite vacia;

		private void CustomInitialize()
		{
            SpriteManager.Camera.UsePixelCoordinates(false);
            vacia= SpriteManager.AddSprite("BarraVidaVacia.png");
            llena= SpriteManager.AddSprite("BarraVidaLlena.png");
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

        public void SetPosicion(float x, float y)
        {
            float auxiliar;
            llena.Y = vacia.Y = y;
            llena.X = x;
            auxiliar = llena.X;

            mBaseX = llena.X - llena.ScaleX;
            llena.X = (llena.X) * -1;
            vacia.X = auxiliar;

        }

        public void SetPosicionFlip(float x, float y)
        {
            float auxiliar;
            llena.Y = vacia.Y = y;
            llena.X = x;
            auxiliar = llena.X;

            mBaseX = llena.X - llena.ScaleX;
            llena.X = (llena.X)*-1;
            vacia.X = auxiliar;

        }

        public void UpdateFill(float vidactual, float vidatotal)
        {
            
            llena.LeftTextureCoordinate = 1 - vidactual / vidatotal;
            llena.X = -llena.ScaleX - mBaseX;
            //llena.X = (llena.X) * -1;
        }

        public void UpdateFillFlip(float vidactual, float vidatotal)
        {

            llena.LeftTextureCoordinate = 1 - vidactual / vidatotal;
            llena.X = -llena.ScaleX - mBaseX;
            /*llena.RightTextureCoordinate = vidactual / vidatotal; 
            llena.X = +llena.ScaleX + mBaseX;
            //llena.X = (llena.X) * -1;*/
        }
                
            
	}
}
