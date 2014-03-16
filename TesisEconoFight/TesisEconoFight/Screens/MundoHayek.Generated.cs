using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.Math.Geometry;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Input;
using FlatRedBall.IO;
using FlatRedBall.Instructions;
using FlatRedBall.Math.Splines;
using FlatRedBall.Utilities;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if XNA4 || WINDOWS_8
using Color = Microsoft.Xna.Framework.Color;
#elif FRB_MDX
using Color = System.Drawing.Color;
#else
using Color = Microsoft.Xna.Framework.Graphics.Color;
#endif

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Microsoft.Xna.Framework.Media;
#endif

// Generated Usings
using TesisEconoFight.Entities;
using FlatRedBall;
using FlatRedBall.Screens;
using FlatRedBall.Math;
using FlatRedBall.Math.Geometry;

namespace TesisEconoFight.Screens
{
	public partial class MundoHayek : Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		private FlatRedBall.Scene SceneFile;
		private FlatRedBall.Math.Geometry.ShapeCollection LimitesMundo;
		
		private FlatRedBall.Sprite Fondo;
		private TesisEconoFight.Entities.Hayek HayekInstance;
		private TesisEconoFight.Entities.PauseText PauseTextInstance;
		private TesisEconoFight.Entities.P2WinText P2WinTextInstance;
		private TesisEconoFight.Entities.P1WinText P1WinTextInstance;
		private TesisEconoFight.Entities.DoubleKOText DoubleKOTextInstance;
		private PositionedObjectList<Player> ListaJugadores;

		public MundoHayek()
			: base("MundoHayek")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			if (!FlatRedBallServices.IsLoaded<FlatRedBall.Scene>(@"content/screens/mundohayek/scenefile.scnx", ContentManagerName))
			{
			}
			SceneFile = FlatRedBallServices.Load<FlatRedBall.Scene>(@"content/screens/mundohayek/scenefile.scnx", ContentManagerName);
			if (!FlatRedBallServices.IsLoaded<FlatRedBall.Math.Geometry.ShapeCollection>(@"content/screens/mundohayek/limitesmundo.shcx", ContentManagerName))
			{
			}
			LimitesMundo = FlatRedBallServices.Load<FlatRedBall.Math.Geometry.ShapeCollection>(@"content/screens/mundohayek/limitesmundo.shcx", ContentManagerName);
			Fondo = SceneFile.Sprites.FindByName("universitätfreiburg1");
			HayekInstance = new TesisEconoFight.Entities.Hayek(ContentManagerName, false);
			HayekInstance.Name = "HayekInstance";
			PauseTextInstance = new TesisEconoFight.Entities.PauseText(ContentManagerName, false);
			PauseTextInstance.Name = "PauseTextInstance";
			P2WinTextInstance = new TesisEconoFight.Entities.P2WinText(ContentManagerName, false);
			P2WinTextInstance.Name = "P2WinTextInstance";
			P1WinTextInstance = new TesisEconoFight.Entities.P1WinText(ContentManagerName, false);
			P1WinTextInstance.Name = "P1WinTextInstance";
			DoubleKOTextInstance = new TesisEconoFight.Entities.DoubleKOText(ContentManagerName, false);
			DoubleKOTextInstance.Name = "DoubleKOTextInstance";
			ListaJugadores = new PositionedObjectList<Player>();
			
			
			PostInitialize();
			base.Initialize(addToManagers);
			if (addToManagers)
			{
				AddToManagers();
			}

        }
        
// Generated AddToManagers
		public override void AddToManagers ()
		{
			base.AddToManagers();
			AddToManagersBottomUp();
			CustomInitialize();
		}


		public override void Activity(bool firstTimeCalled)
		{
			// Generated Activity
			if (!IsPaused)
			{
				
				HayekInstance.Activity();
				PauseTextInstance.Activity();
				P2WinTextInstance.Activity();
				P1WinTextInstance.Activity();
				DoubleKOTextInstance.Activity();
				for (int i = ListaJugadores.Count - 1; i > -1; i--)
				{
					if (i < ListaJugadores.Count)
					{
						// We do the extra if-check because activity could destroy any number of entities
						ListaJugadores[i].Activity();
					}
				}
			}
			else
			{
			}
			base.Activity(firstTimeCalled);
			if (!IsActivityFinished)
			{
				CustomActivity(firstTimeCalled);
			}
			SceneFile.ManageAll();


				// After Custom Activity
				
            
		}

		public override void Destroy()
		{
			// Generated Destroy
			if (this.UnloadsContentManagerWhenDestroyed && ContentManagerName != "Global")
			{
				SceneFile.RemoveFromManagers(ContentManagerName != "Global");
			}
			else
			{
				SceneFile.RemoveFromManagers(false);
			}
			if (this.UnloadsContentManagerWhenDestroyed && ContentManagerName != "Global")
			{
				LimitesMundo.RemoveFromManagers(ContentManagerName != "Global");
			}
			else
			{
				LimitesMundo.RemoveFromManagers(false);
			}
			
			if (Fondo != null)
			{
				Fondo.Detach(); SpriteManager.RemoveSprite(Fondo);
			}
			if (HayekInstance != null)
			{
				HayekInstance.Destroy();
				HayekInstance.Detach();
			}
			if (PauseTextInstance != null)
			{
				PauseTextInstance.Destroy();
				PauseTextInstance.Detach();
			}
			if (P2WinTextInstance != null)
			{
				P2WinTextInstance.Destroy();
				P2WinTextInstance.Detach();
			}
			if (P1WinTextInstance != null)
			{
				P1WinTextInstance.Destroy();
				P1WinTextInstance.Detach();
			}
			if (DoubleKOTextInstance != null)
			{
				DoubleKOTextInstance.Destroy();
				DoubleKOTextInstance.Detach();
			}
			for (int i = ListaJugadores.Count - 1; i > -1; i--)
			{
				ListaJugadores[i].Destroy();
			}

			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public virtual void AddToManagersBottomUp ()
		{
			SceneFile.AddToManagers(mLayer);
			LimitesMundo.AddToManagers(mLayer);
			SpriteManager.AddToLayer(Fondo, SpriteManager.UnderAllDrawnLayer);
			HayekInstance.AddToManagers(mLayer);
			PauseTextInstance.AddToManagers(mLayer);
			P2WinTextInstance.AddToManagers(mLayer);
			P1WinTextInstance.AddToManagers(mLayer);
			DoubleKOTextInstance.AddToManagers(mLayer);
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			SceneFile.ConvertToManuallyUpdated();
			SpriteManager.ConvertToManuallyUpdated(Fondo);
			HayekInstance.ConvertToManuallyUpdated();
			PauseTextInstance.ConvertToManuallyUpdated();
			P2WinTextInstance.ConvertToManuallyUpdated();
			P1WinTextInstance.ConvertToManuallyUpdated();
			DoubleKOTextInstance.ConvertToManuallyUpdated();
			for (int i = 0; i < ListaJugadores.Count; i++)
			{
				ListaJugadores[i].ConvertToManuallyUpdated();
			}
		}
		public static void LoadStaticContent (string contentManagerName)
		{
			if (string.IsNullOrEmpty(contentManagerName))
			{
				throw new ArgumentException("contentManagerName cannot be empty or null");
			}
			#if DEBUG
			if (contentManagerName == FlatRedBallServices.GlobalContentManager)
			{
				HasBeenLoadedWithGlobalContentManager = true;
			}
			else if (HasBeenLoadedWithGlobalContentManager)
			{
				throw new Exception("This type has been loaded with a Global content manager, then loaded with a non-global.  This can lead to a lot of bugs");
			}
			#endif
			TesisEconoFight.Entities.Hayek.LoadStaticContent(contentManagerName);
			TesisEconoFight.Entities.PauseText.LoadStaticContent(contentManagerName);
			TesisEconoFight.Entities.P2WinText.LoadStaticContent(contentManagerName);
			TesisEconoFight.Entities.P1WinText.LoadStaticContent(contentManagerName);
			TesisEconoFight.Entities.DoubleKOText.LoadStaticContent(contentManagerName);
			CustomLoadStaticContent(contentManagerName);
		}
		[System.Obsolete("Use GetFile instead")]
		public static object GetStaticMember (string memberName)
		{
			return null;
		}
		public static object GetFile (string memberName)
		{
			return null;
		}
		object GetMember (string memberName)
		{
			switch(memberName)
			{
				case  "SceneFile":
					return SceneFile;
				case  "LimitesMundo":
					return LimitesMundo;
			}
			return null;
		}


	}
}
