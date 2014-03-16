using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Graphics.Model;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
using FlatRedBall.Localization;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Microsoft.Xna.Framework.Media;
#endif

namespace TesisEconoFight.Screens
{
	public partial class MenuPrincipal
	{

        Song song;
		void CustomInitialize()
		{
            this.Alternativas.Visible = false;
            Microsoft.Xna.Framework.Media.MediaPlayer.IsRepeating = true;
            song =
            FlatRedBallServices.Load<Song>(@"Content/intro", ContentManagerName);
            Microsoft.Xna.Framework.Media.MediaPlayer.Play(song);
		}

		void CustomActivity(bool firstTimeCalled)
		{

            KursorInstance.Actividad(GlobalData.getControl1());
            ir2P();
            irGlosario();
            irArcade();
		}

		void CustomDestroy()
		{
            //CursorInstance.Destroy();

		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        private void irGlosario()
        {
            if (KursorInstance.getCuerpo().CollideAgainst(Alternativas.AxisAlignedRectangles.FindByName("Glosario")) && GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.A))
            {
                MoveToScreen(typeof(MenuGlosario).FullName);
            }
        }

        private void irArcade()
        {
            if (KursorInstance.getCuerpo().CollideAgainst(Alternativas.AxisAlignedRectangles.FindByName("Arcade")) && GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.A))
            {
                MoveToScreen(typeof(CharSelect).FullName);
                GlobalData.AI = true;
            }
        }

        private void ir2P()
        {
            if (KursorInstance.getCuerpo().CollideAgainst(Alternativas.AxisAlignedRectangles.FindByName("Vs")) && GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.A))
            {
                MoveToScreen(typeof(CharSelect).FullName);
                GlobalData.AI =false;
            }
        }
	}
}
