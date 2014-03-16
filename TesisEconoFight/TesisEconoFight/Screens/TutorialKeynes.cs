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
	public partial class TutorialKeynes
	{

        void CustomInitialize()
        {
            this.CurrentState = VariableState.P1;

        }

        void CustomActivity(bool firstTimeCalled)
        {
            Adelante();
            Atras();
            Salir();

        }

        void CustomDestroy()
        {


        }

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        private void Adelante()
        {
            if (CurrentState == VariableState.P1 && GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.A))
            {
                CurrentState = VariableState.P2;
            }

            /*else if (CurrentState == VariableState.P2 && GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.A))
            {
                CurrentState = VariableState.P3;
            }*/

        }

        private void Atras()
        {
            if (CurrentState == VariableState.P2 && GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.B))
            {
                CurrentState = VariableState.P1;
            }

            /*else if (CurrentState == VariableState.P3 && GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.B))
            {
                CurrentState = VariableState.P2;
            }*/

        }

        private void Salir()
        {
            /*if (CurrentState == VariableState.P2 && GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.A))
            {
                MoveToScreen(typeof(MundoKeynes).FullName);
            }*/
            if (GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.Start))
            {

                MoveToScreen(typeof(MundoKeynes).FullName);
            }

        }

	}
}
