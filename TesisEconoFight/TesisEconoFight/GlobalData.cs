using System;
using System.Collections.Generic;
using System.Linq;
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

namespace TesisEconoFight
{
    public static class GlobalData
    {
        public static string Personaje1 = "Malthus";
        public static string Personaje2 = "Hayek";
        public static string proyectilP1 = "Oro";
        public static string proyectilP2="Oro";
        public static string bancaP1= "Fraccionaria";
        public static string bancaP2= "Completa";
        //public static string monetarioP1;
        //public static string monetarioP2;
        public static bool AI;
        public static Xbox360GamePad control1;
        public static Xbox360GamePad control2;
        public static string mundo = "Malthus";

        public static void setPersonaje1(string p1)
        {
            Personaje1 = p1;

        }

        public static void setPersonaje2(string p2)
        {
            Personaje2 = p2;

        }

        public static Xbox360GamePad getControl1()
        {
            control1 = InputManager.Xbox360GamePads[0];
            return control1;

        }

        public static Xbox360GamePad getControl2()
        {
            control2 = InputManager.Xbox360GamePads[1];
            return control2;

        }






    }
}
