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
#endif

namespace TesisEconoFight.Screens
{
	public partial class MenuGlosario
	{

		void CustomInitialize()
		{
            Entities.Kursor P1 = new Entities.Kursor(ContentManagerName);
            //Entities.Kursor P2 = new Entities.Kursor(ContentManagerName);
            Cursores.Add(P1);
            this.AltsGlosario.Visible = false;

		}

		void CustomActivity(bool firstTimeCalled)
		{
            Cursores[0].Actividad(GlobalData.getControl1());
            SelectGlosario();
            Regresar();


		}

		void CustomDestroy()
		{


		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        private void SelectGlosario()
        {
            if (Cursores[0].getCuerpo().CollideAgainst(AltsGlosario.AxisAlignedRectangles.FindByName("Hayek")) && GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.A))
            {
                MoveToScreen(typeof(GlosarioHayek).FullName);
            }

            else if (Cursores[0].getCuerpo().CollideAgainst(AltsGlosario.AxisAlignedRectangles.FindByName("Keynes")) && GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.A))
            {
                MoveToScreen(typeof(GlosarioKeynes).FullName);
            }

            else if (Cursores[0].getCuerpo().CollideAgainst(AltsGlosario.AxisAlignedRectangles.FindByName("Malthus")) && GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.A))
            {
                MoveToScreen(typeof(GlosarioMalthus).FullName);
            }

            else if (Cursores[0].getCuerpo().CollideAgainst(AltsGlosario.AxisAlignedRectangles.FindByName("Smith")) && GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.A))
            {
                MoveToScreen(typeof(GlosarioSmith).FullName);
            }
            else if (Cursores[0].getCuerpo().CollideAgainst(AltsGlosario.AxisAlignedRectangles.FindByName("Sistemas")) && GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.A))
            {
                MoveToScreen(typeof(GlosarioSistemasJuego).FullName);
            }

        }



        private void Regresar()
        {
            if (Cursores[0].getCuerpo().CollideAgainst(AltsGlosario.AxisAlignedRectangles.FindByName("MainMenu")) && GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.A))
            {


                MoveToScreen(typeof(MenuPrincipal).FullName);

            }
            else if (GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.B))
            {
                MoveToScreen(typeof(MenuPrincipal).FullName);
            }
        }

	}
}
