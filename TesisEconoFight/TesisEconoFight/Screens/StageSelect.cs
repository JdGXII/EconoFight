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
	public partial class StageSelect
	{
        bool seleccion;
		void CustomInitialize()
		{
            Entities.Kursor P1 = new Entities.Kursor(ContentManagerName);
            //Entities.Kursor P2 = new Entities.Kursor(ContentManagerName);
            Cursores.Add(P1);
            //Cursores.Add(P2);
            seleccion = false;
            this.SeleccionStage.Visible = false;
            this.Sprite.Visible = false;
		}

		void CustomActivity(bool firstTimeCalled)
		{
            Cursores[0].Actividad(GlobalData.getControl1());
            SeleccionarMundo();
            Regresar();
            irPelea();

		}

		void CustomDestroy()
		{


		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        private void SeleccionarMundo()
        {
            if (Cursores[0].getCuerpo().CollideAgainst(SeleccionStage.AxisAlignedRectangles.FindByName("Hayek")) && GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.A))
            {
                GlobalData.mundo = "Hayek";
                seleccion = true;
                this.CurrentState = VariableState.Hayek;
                this.Sprite.Visible = true;
            }

            else if (Cursores[0].getCuerpo().CollideAgainst(SeleccionStage.AxisAlignedRectangles.FindByName("Keynes")) && GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.A))
            {
                GlobalData.mundo = "Keynes";
                seleccion = true;
                this.CurrentState = VariableState.Keynes;
                this.Sprite.Visible = true;
            }

            else if (Cursores[0].getCuerpo().CollideAgainst(SeleccionStage.AxisAlignedRectangles.FindByName("Malthus")) && GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.A))
            {
                GlobalData.mundo = "Malthus";
                seleccion = true;
                this.CurrentState = VariableState.Malthus;
                this.Sprite.Visible = true;
            }

            else if (Cursores[0].getCuerpo().CollideAgainst(SeleccionStage.AxisAlignedRectangles.FindByName("Smith")) && GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.A))
            {
                GlobalData.mundo = "Smith";
                seleccion = true;
                this.CurrentState = VariableState.Smith;
                this.Sprite.Visible = true;
            }
        }

        private void Regresar()
        {
            if (Cursores[0].getCuerpo().CollideAgainst(SeleccionStage.AxisAlignedRectangles.FindByName("Character")) && GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.A))
            {
                GlobalData.mundo = "";
                
                MoveToScreen(typeof(CharSelect).FullName);

            }
            
            
            else if (GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.B))
            {
                GlobalData.mundo = "";
                MoveToScreen(typeof(CharSelect).FullName);
            }
        }

        private void irPelea()
        {
            if (seleccion == true)
            {
                if (Cursores[0].getCuerpo().CollideAgainst(SeleccionStage.AxisAlignedRectangles.FindByName("Pelea")) && GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.A))
                {
                    MoveToScreen(typeof(GameScreen).FullName);
                }
                else if (GlobalData.getControl1().ButtonPushed(Xbox360GamePad.Button.Start))
                {
                    MoveToScreen(typeof(GameScreen).FullName);
                }
            }
        }

	}
}
