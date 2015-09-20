#if ANDROID || IOS
#define REQUIRES_PRIMARY_THREAD_LOADING
#endif

using Color = Microsoft.Xna.Framework.Color;

namespace TesisEconoFight.Entities
{
	public partial class Precios : TesisEconoFight.Entities.Especial, FlatRedBall.Graphics.IDestroyable
	{
        // This is made static so that static lazy-loaded content can access it.
        public static new string ContentManagerName
        {
            get{ return Entities.Especial.ContentManagerName;}
            set{ Entities.Especial.ContentManagerName = value;}
        }

		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		static object mLockObject = new object();
		static System.Collections.Generic.List<string> mRegisteredUnloads = new System.Collections.Generic.List<string>();
		static System.Collections.Generic.List<string> LoadedContentManagers = new System.Collections.Generic.List<string>();
		protected static FlatRedBall.Graphics.Animation.AnimationChainList AnimacionPrecio;
		protected static FlatRedBall.Scene SceneFile;
		
		private FlatRedBall.Sprite mSprite;
		public FlatRedBall.Sprite Sprite
		{
			get
			{
				return mSprite;
			}
			private set
			{
				mSprite = value;
			}
		}
		public float Velocidad = 8f;

        public Precios()
            : this(FlatRedBall.Screens.ScreenManager.CurrentScreen.ContentManagerName, true)
        {

        }

        public Precios(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public Precios(string contentManagerName, bool addToManagers) :
			base(contentManagerName, addToManagers)
		{
			// Don't delete this:
            ContentManagerName = contentManagerName;
           

		}

		protected override void InitializeEntity(bool addToManagers)
		{
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			mSprite = SceneFile.Sprites.FindByName("flecha png 64 x 64 intocable!1").Clone();
			Cuerpo = new FlatRedBall.Math.Geometry.AxisAlignedRectangle();
			Cuerpo.Name = "Cuerpo";
			
			base.InitializeEntity(addToManagers);


		}

// Generated AddToManagers
		public override void ReAddToManagers (FlatRedBall.Graphics.Layer layerToAddTo)
		{
			base.ReAddToManagers(layerToAddTo);
			FlatRedBall.SpriteManager.AddToLayer(mSprite, LayerProvidedByContainer);
			FlatRedBall.Math.Geometry.ShapeManager.AddToLayer(Cuerpo, LayerProvidedByContainer);
		}
		public override void AddToManagers (FlatRedBall.Graphics.Layer layerToAddTo)
		{
			LayerProvidedByContainer = layerToAddTo;
			FlatRedBall.SpriteManager.AddToLayer(mSprite, LayerProvidedByContainer);
			FlatRedBall.Math.Geometry.ShapeManager.AddToLayer(Cuerpo, LayerProvidedByContainer);
			base.AddToManagers(layerToAddTo);
			CustomInitialize();
		}

		public override void Activity()
		{
			// Generated Activity
			base.Activity();
			
			CustomActivity();
			
			// After Custom Activity
		}

		public override void Destroy()
		{
			// Generated Destroy
			base.Destroy();
			
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
		public override void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			base.PostInitialize();
			if (mSprite.Parent == null)
			{
				mSprite.CopyAbsoluteToRelative();
				mSprite.AttachTo(this, false);
			}
			if (Cuerpo.Parent == null)
			{
				Cuerpo.CopyAbsoluteToRelative();
				Cuerpo.AttachTo(this, false);
			}
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public override void AddToManagersBottomUp (FlatRedBall.Graphics.Layer layerToAddTo)
		{
			base.AddToManagersBottomUp(layerToAddTo);
		}
		public override void RemoveFromManagers ()
		{
			base.RemoveFromManagers();
			base.RemoveFromManagers();
			if (Sprite != null)
			{
				FlatRedBall.SpriteManager.RemoveSpriteOneWay(Sprite);
			}
			if (Cuerpo != null)
			{
				FlatRedBall.Math.Geometry.ShapeManager.RemoveOneWay(Cuerpo);
			}
		}
		public override void AssignCustomVariables (bool callOnContainedElements)
		{
			base.AssignCustomVariables(callOnContainedElements);
			if (callOnContainedElements)
			{
			}
			Velocidad = 8f;
			Damage = 200f;
			Tipo = "Precio";
		}
		public override void ConvertToManuallyUpdated ()
		{
			base.ConvertToManuallyUpdated();
			this.ForceUpdateDependenciesDeep();
			FlatRedBall.SpriteManager.ConvertToManuallyUpdated(this);
			FlatRedBall.SpriteManager.ConvertToManuallyUpdated(Sprite);
		}
		public static new void LoadStaticContent (string contentManagerName)
		{
			if (string.IsNullOrEmpty(contentManagerName))
			{
				throw new System.ArgumentException("contentManagerName cannot be empty or null");
			}
			ContentManagerName = contentManagerName;
			TesisEconoFight.Entities.Especial.LoadStaticContent(contentManagerName);
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
						FlatRedBall.FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("PreciosStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
				if (!FlatRedBall.FlatRedBallServices.IsLoaded<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/entities/precios/animacionprecio.achx", ContentManagerName))
				{
					registerUnload = true;
				}
				AnimacionPrecio = FlatRedBall.FlatRedBallServices.Load<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/entities/precios/animacionprecio.achx", ContentManagerName);
				if (!FlatRedBall.FlatRedBallServices.IsLoaded<FlatRedBall.Scene>(@"content/entities/precios/scenefile.scnx", ContentManagerName))
				{
					registerUnload = true;
				}
				SceneFile = FlatRedBall.FlatRedBallServices.Load<FlatRedBall.Scene>(@"content/entities/precios/scenefile.scnx", ContentManagerName);
			}
			if (registerUnload && ContentManagerName != FlatRedBall.FlatRedBallServices.GlobalContentManager)
			{
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBall.FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBall.FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("PreciosStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
			}
			CustomLoadStaticContent(contentManagerName);
		}
		public static new void UnloadStaticContent ()
		{
			if (LoadedContentManagers.Count != 0)
			{
				LoadedContentManagers.RemoveAt(0);
				mRegisteredUnloads.RemoveAt(0);
			}
			if (LoadedContentManagers.Count == 0)
			{
				if (AnimacionPrecio != null)
				{
					AnimacionPrecio= null;
				}
				if (SceneFile != null)
				{
					SceneFile.RemoveFromManagers(ContentManagerName != "Global");
					SceneFile= null;
				}
			}
		}
		[System.Obsolete("Use GetFile instead")]
		public static new object GetStaticMember (string memberName)
		{
			switch(memberName)
			{
				case  "AnimacionPrecio":
					return AnimacionPrecio;
				case  "SceneFile":
					return SceneFile;
			}
			return null;
		}
		public static new object GetFile (string memberName)
		{
			switch(memberName)
			{
				case  "AnimacionPrecio":
					return AnimacionPrecio;
				case  "SceneFile":
					return SceneFile;
			}
			return null;
		}
		object GetMember (string memberName)
		{
			switch(memberName)
			{
				case  "AnimacionPrecio":
					return AnimacionPrecio;
				case  "SceneFile":
					return SceneFile;
			}
			return null;
		}
		public override void SetToIgnorePausing ()
		{
			base.SetToIgnorePausing();
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(Sprite);
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(Cuerpo);
		}
		public override void MoveToLayer (FlatRedBall.Graphics.Layer layerToMoveTo)
		{
			base.MoveToLayer(layerToMoveTo);
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
