#if ANDROID || IOS
#define REQUIRES_PRIMARY_THREAD_LOADING
#endif

using Color = Microsoft.Xna.Framework.Color;

namespace TesisEconoFight.Entities
{
	public partial class Kursor : FlatRedBall.PositionedObject, FlatRedBall.Graphics.IDestroyable
	{
        // This is made static so that static lazy-loaded content can access it.
        public static string ContentManagerName
        {
            get;
            set;
        }

		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		static object mLockObject = new object();
		static System.Collections.Generic.List<string> mRegisteredUnloads = new System.Collections.Generic.List<string>();
		static System.Collections.Generic.List<string> LoadedContentManagers = new System.Collections.Generic.List<string>();
		protected static FlatRedBall.Graphics.Animation.AnimationChainList AnimacionCursor;
		protected static FlatRedBall.Scene SceneFile;
		
		private FlatRedBall.Sprite Sprite;
		private FlatRedBall.Math.Geometry.AxisAlignedRectangle Cuerpo;
		protected FlatRedBall.Graphics.Layer LayerProvidedByContainer = null;

        public Kursor()
            : this(FlatRedBall.Screens.ScreenManager.CurrentScreen.ContentManagerName, true)
        {

        }

        public Kursor(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public Kursor(string contentManagerName, bool addToManagers) :
			base()
		{
			// Don't delete this:
            ContentManagerName = contentManagerName;
            InitializeEntity(addToManagers);

		}

		protected virtual void InitializeEntity(bool addToManagers)
		{
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			Sprite = SceneFile.Sprites.FindByName("moneda 1 moneda png 64 x 64 intocable!1").Clone();
			Cuerpo = new FlatRedBall.Math.Geometry.AxisAlignedRectangle();
			Cuerpo.Name = "Cuerpo";
			
			PostInitialize();
			if (addToManagers)
			{
				AddToManagers(null);
			}


		}

// Generated AddToManagers
		public virtual void ReAddToManagers (FlatRedBall.Graphics.Layer layerToAddTo)
		{
			LayerProvidedByContainer = layerToAddTo;
			FlatRedBall.SpriteManager.AddPositionedObject(this);
			FlatRedBall.SpriteManager.AddToLayer(Sprite, LayerProvidedByContainer);
			FlatRedBall.Math.Geometry.ShapeManager.AddToLayer(Cuerpo, LayerProvidedByContainer);
		}
		public virtual void AddToManagers (FlatRedBall.Graphics.Layer layerToAddTo)
		{
			LayerProvidedByContainer = layerToAddTo;
			FlatRedBall.SpriteManager.AddPositionedObject(this);
			FlatRedBall.SpriteManager.AddToLayer(Sprite, LayerProvidedByContainer);
			FlatRedBall.Math.Geometry.ShapeManager.AddToLayer(Cuerpo, LayerProvidedByContainer);
			AddToManagersBottomUp(layerToAddTo);
			CustomInitialize();
		}

		public virtual void Activity()
		{
			// Generated Activity
			
			CustomActivity();
			
			// After Custom Activity
		}

		public virtual void Destroy()
		{
			// Generated Destroy
			FlatRedBall.SpriteManager.RemovePositionedObject(this);
			
			if (Sprite != null)
			{
				FlatRedBall.SpriteManager.RemoveSprite(Sprite);
			}
			if (Cuerpo != null)
			{
				FlatRedBall.Math.Geometry.ShapeManager.Remove(Cuerpo);
			}


			CustomDestroy();
		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			if (Sprite.Parent == null)
			{
				Sprite.CopyAbsoluteToRelative();
				Sprite.AttachTo(this, false);
			}
			if (Cuerpo.Parent == null)
			{
				Cuerpo.CopyAbsoluteToRelative();
				Cuerpo.AttachTo(this, false);
			}
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public virtual void AddToManagersBottomUp (FlatRedBall.Graphics.Layer layerToAddTo)
		{
			AssignCustomVariables(false);
		}
		public virtual void RemoveFromManagers ()
		{
			FlatRedBall.SpriteManager.ConvertToManuallyUpdated(this);
			if (Sprite != null)
			{
				FlatRedBall.SpriteManager.RemoveSpriteOneWay(Sprite);
			}
			if (Cuerpo != null)
			{
				FlatRedBall.Math.Geometry.ShapeManager.RemoveOneWay(Cuerpo);
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
			this.ForceUpdateDependenciesDeep();
			FlatRedBall.SpriteManager.ConvertToManuallyUpdated(this);
			FlatRedBall.SpriteManager.ConvertToManuallyUpdated(Sprite);
		}
		public static void LoadStaticContent (string contentManagerName)
		{
			if (string.IsNullOrEmpty(contentManagerName))
			{
				throw new System.ArgumentException("contentManagerName cannot be empty or null");
			}
			ContentManagerName = contentManagerName;
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
			bool registerUnload = false;
			if (LoadedContentManagers.Contains(contentManagerName) == false)
			{
				LoadedContentManagers.Add(contentManagerName);
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBall.FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBall.FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("KursorStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
				if (!FlatRedBall.FlatRedBallServices.IsLoaded<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/entities/kursor/animacioncursor.achx", ContentManagerName))
				{
					registerUnload = true;
				}
				AnimacionCursor = FlatRedBall.FlatRedBallServices.Load<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/entities/kursor/animacioncursor.achx", ContentManagerName);
				if (!FlatRedBall.FlatRedBallServices.IsLoaded<FlatRedBall.Scene>(@"content/entities/kursor/scenefile.scnx", ContentManagerName))
				{
					registerUnload = true;
				}
				SceneFile = FlatRedBall.FlatRedBallServices.Load<FlatRedBall.Scene>(@"content/entities/kursor/scenefile.scnx", ContentManagerName);
			}
			if (registerUnload && ContentManagerName != FlatRedBall.FlatRedBallServices.GlobalContentManager)
			{
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBall.FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBall.FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("KursorStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
			}
			CustomLoadStaticContent(contentManagerName);
		}
		public static void UnloadStaticContent ()
		{
			if (LoadedContentManagers.Count != 0)
			{
				LoadedContentManagers.RemoveAt(0);
				mRegisteredUnloads.RemoveAt(0);
			}
			if (LoadedContentManagers.Count == 0)
			{
				if (AnimacionCursor != null)
				{
					AnimacionCursor= null;
				}
				if (SceneFile != null)
				{
					SceneFile.RemoveFromManagers(ContentManagerName != "Global");
					SceneFile= null;
				}
			}
		}
		[System.Obsolete("Use GetFile instead")]
		public static object GetStaticMember (string memberName)
		{
			switch(memberName)
			{
				case  "AnimacionCursor":
					return AnimacionCursor;
				case  "SceneFile":
					return SceneFile;
			}
			return null;
		}
		public static object GetFile (string memberName)
		{
			switch(memberName)
			{
				case  "AnimacionCursor":
					return AnimacionCursor;
				case  "SceneFile":
					return SceneFile;
			}
			return null;
		}
		object GetMember (string memberName)
		{
			switch(memberName)
			{
				case  "AnimacionCursor":
					return AnimacionCursor;
				case  "SceneFile":
					return SceneFile;
			}
			return null;
		}
		protected bool mIsPaused;
		public override void Pause (FlatRedBall.Instructions.InstructionList instructions)
		{
			base.Pause(instructions);
			mIsPaused = true;
		}
		public virtual void SetToIgnorePausing ()
		{
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(this);
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(Sprite);
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(Cuerpo);
		}
		public virtual void MoveToLayer (FlatRedBall.Graphics.Layer layerToMoveTo)
		{
			if (LayerProvidedByContainer != null)
			{
				LayerProvidedByContainer.Remove(Sprite);
			}
			FlatRedBall.SpriteManager.AddToLayer(Sprite, layerToMoveTo);
			LayerProvidedByContainer = layerToMoveTo;
		}

    }
	
	
	// Extra classes
	
}
