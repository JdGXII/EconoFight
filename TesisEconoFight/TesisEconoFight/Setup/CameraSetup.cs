using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using Microsoft.Xna.Framework;

#if !FRB_MDX
using System.Linq;
#endif

namespace TesisEconoFight
{
	internal static class CameraSetup
	{
			internal static void SetupCamera (Camera cameraToSetUp, GraphicsDeviceManager graphicsDeviceManager)
			{
				SetupCamera(cameraToSetUp, graphicsDeviceManager, 800, 600);
			}
			internal static void SetupCamera (Camera cameraToSetUp, GraphicsDeviceManager graphicsDeviceManager, int width, int height)
			{
				cameraToSetUp.UsePixelCoordinates(false, 800, 600);
			}
			internal static void ResetCamera (Camera cameraToReset)
			{
				cameraToReset.X = 0;
				cameraToReset.Y = 0;
				cameraToReset.XVelocity = 0;
				cameraToReset.YVelocity = 0;
			}

	}
}
