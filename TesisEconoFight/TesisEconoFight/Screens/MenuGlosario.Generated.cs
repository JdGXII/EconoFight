#if ANDROID || IOS
#define REQUIRES_PRIMARY_THREAD_LOADING
#endif

using Color = Microsoft.Xna.Framework.Color;

// Generated Usings
using TesisEconoFight.Entities;
using FlatRedBall;
using FlatRedBall.Screens;
using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.Math;
using FlatRedBall.Math.Geometry;

namespace TesisEconoFight.Screens
{
	public partial class MenuGlosario : FlatRedBall.Screens.Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		protected FlatRedBall.Math.Geometry.ShapeCollection AltsGlosario;
		protected FlatRedBall.Scene SceneFile;
		
		private FlatRedBall.Math.PositionedObjectList<TesisEconoFight.Entities.Kursor> Cursores;

		public MenuGlosario()
			: base("MenuGlosario")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			if (!FlatRedBall.FlatRedBallServices.IsLoaded<FlatRedBall.Math.Geometry.ShapeCollection>(@"content/screens/menuglosario/altsglosario.shcx", ContentManagerName))
			{
			}
			AltsGlosario = FlatRedBall.FlatRedBallServices.Load<FlatRedBall.Math.Geometry.ShapeCollection>(@"content/screens/menuglosario/altsglosario.shcx", ContentManagerName);
			if (!FlatRedBall.FlatRedBallServices.IsLoaded<FlatRedBall.Scene>(@"content/screens/menuglosario/scenefile.scnx", ContentManagerName))
			{
			}
			SceneFile = FlatRedBall.FlatRedBallServices.Load<FlatRedBall.Scene>(@"content/screens/menuglosario/scenefile.scnx", ContentManagerName);
			Cursores = new FlatRedBall.Math.PositionedObjectList<TesisEconoFight.Entities.Kursor>();
			Cursores.Name = "Cursores";
			
			
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
			AltsGlosario.AddToManagers(mLayer);
			SceneFile.AddToManagers(mLayer);
			base.AddToManagers();
			AddToManagersBottomUp();
			CustomInitialize();
		}


		public override void Activity(bool firstTimeCalled)
		{
			// Generated Activity
			if (!IsPaused)
			{
				
				for (int i = Cursores.Count - 1; i > -1; i--)
				{
					if (i < Cursores.Count)
					{
						// We do the extra if-check because activity could destroy any number of entities
						Cursores[i].Activity();
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
				AltsGlosario.RemoveFromManagers(ContentManagerName != "Global");
			}
			else
			{
				AltsGlosario.RemoveFromManagers(false);
			}
			if (this.UnloadsContentManagerWhenDestroyed && ContentManagerName != "Global")
			{
				SceneFile.RemoveFromManagers(ContentManagerName != "Global");
			}
			else
			{
				SceneFile.RemoveFromManagers(false);
			}
			
			Cursores.MakeOneWay();
			for (int i = Cursores.Count - 1; i > -1; i--)
			{
				Cursores[i].Destroy();
			}
			Cursores.MakeTwoWay();

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
			CameraSetup.ResetCamera(SpriteManager.Camera);
			AssignCustomVariables(false);
		}
		public virtual void RemoveFromManagers ()
		{
			for (int i = Cursores.Count - 1; i > -1; i--)
			{
				Cursores[i].Destroy();
			}
		}
		public virtual void AssignCustomVariables (bool callOnContainedElements)
		{
			if (callOnContainedElements)
			{
			}
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			SceneFile.ConvertToManuallyUpdated();
			for (int i = 0; i < Cursores.Count; i++)
			{
				Cursores[i].ConvertToManuallyUpdated();
			}
		}
		public static void LoadStaticContent (string contentManagerName)
		{
			if (string.IsNullOrEmpty(contentManagerName))
			{
				throw new System.ArgumentException("contentManagerName cannot be empty or null");
			}
			#if DEBUG
			if (contentManagerName == FlatRedBall.FlatRedBallServices.GlobalContentManager)
			{
				HasBeenLoadedWithGlobalContentManager = true;
			}
			else if (HasBeenLoadedWithGlobalContentManager)
			{
				throw new System.Exception("This type has been loaded with a Global content manager, then loaded with a non-global.  This can lead to a lot of bugs");
			}
			#endif
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
				case  "AltsGlosario":
					return AltsGlosario;
				case  "SceneFile":
					return SceneFile;
			}
			return null;
		}


	}
}
