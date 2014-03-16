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
	public partial class GlosarioKeynes
	{
        void CustomInitialize()
        {
            this.Opciones.Visible = false;
            this.CurrentState = VariableState.P1;

        }

        void CustomActivity(bool firstTimeCalled)
        {
            KursorInstance.Actividad(GlobalData.getControl1());
            Navegar();
            Regresar();

        }

        void CustomDestroy()
        {


        }

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        private void Regresar()
        {
            if (CurrentState == VariableState.P1 &&  /*KursorInstance.getCuerpo().CollideAgainst(Opciones.AxisAlignedRectangles.FindByName("MenuGlosario")) &&*/ GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.B))
            {
                MoveToScreen(typeof(MenuGlosario).FullName);
            }
            /*else if (GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.B))
            {
                MoveToScreen(typeof(MenuPrincipal).FullName);
            }*/

            if (CurrentState == VariableState.P2 && /*KursorInstance.getCuerpo().CollideAgainst(Opciones.AxisAlignedRectangles.FindByName("MenuGlosario")) &&*/ GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.B))
            {
                CurrentState = VariableState.P1;
            }
            else if (CurrentState == VariableState.P3 && /*KursorInstance.getCuerpo().CollideAgainst(Opciones.AxisAlignedRectangles.FindByName("MenuGlosario")) &&*/ GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.B))
            {
                CurrentState = VariableState.P2;
            }
        }

        private void Navegar()
        {

            if (CurrentState == VariableState.P1 &&  /*KursorInstance.getCuerpo().CollideAgainst(Opciones.AxisAlignedRectangles.FindByName("Siguiente")) &&*/ GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.A))
            {
                CurrentState = VariableState.P2;
            }

            else if (CurrentState == VariableState.P2 && /*KursorInstance.getCuerpo().CollideAgainst(Opciones.AxisAlignedRectangles.FindByName("Siguiente")) &&*/ GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.A))
            {
                CurrentState = VariableState.P3;
            }

        }

	}
}
