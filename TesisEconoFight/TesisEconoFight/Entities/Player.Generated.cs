#if ANDROID || IOS
#define REQUIRES_PRIMARY_THREAD_LOADING
#endif

using Color = Microsoft.Xna.Framework.Color;

namespace TesisEconoFight.Entities
{
	public partial class Player : FlatRedBall.PositionedObject, FlatRedBall.Graphics.IDestroyable
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
		
		protected FlatRedBall.Math.Geometry.AxisAlignedRectangle Cuerpo;
		protected FlatRedBall.Sprite Sprite;
		protected FlatRedBall.Math.Geometry.AxisAlignedRectangle Shield;
		protected FlatRedBall.Math.PositionedObjectList<TesisEconoFight.Entities.Especial> mListaAtaques;
		public FlatRedBall.Math.PositionedObjectList<TesisEconoFight.Entities.Especial> ListaAtaques
		{
			get
			{
				return mListaAtaques;
			}
			protected set
			{
				mListaAtaques = value;
			}
		}
		protected FlatRedBall.Math.Geometry.AxisAlignedRectangle Golpe;
		protected FlatRedBall.Math.PositionedObjectList<TesisEconoFight.Entities.SuperAtaque> mListaAtaqueSuper;
		public FlatRedBall.Math.PositionedObjectList<TesisEconoFight.Entities.SuperAtaque> ListaAtaqueSuper
		{
			get
			{
				return mListaAtaqueSuper;
			}
			protected set
			{
				mListaAtaqueSuper = value;
			}
		}
		float mVidaTotal;
		public virtual float VidaTotal
		{
			set
			{
				mVidaTotal = value;
			}
			get
			{
				return mVidaTotal;
			}
		}
		float mVidaActual;
		public virtual float VidaActual
		{
			set
			{
				mVidaActual = value;
			}
			get
			{
				return mVidaActual;
			}
		}
		float mVelocidad;
		public virtual float Velocidad
		{
			set
			{
				mVelocidad = value;
			}
			get
			{
				return mVelocidad;
			}
		}
		float mPhysDamage;
		public virtual float PhysDamage
		{
			set
			{
				mPhysDamage = value;
			}
			get
			{
				return mPhysDamage;
			}
		}
		float mEnemigoX;
		public virtual float EnemigoX
		{
			set
			{
				mEnemigoX = value;
			}
			get
			{
				return mEnemigoX;
			}
		}
		float mSuperCompleta = 100f;
		public virtual float SuperCompleta
		{
			set
			{
				mSuperCompleta = value;
			}
			get
			{
				return mSuperCompleta;
			}
		}
		float mSuperActual = 0f;
		public virtual float SuperActual
		{
			set
			{
				mSuperActual = value;
			}
			get
			{
				return mSuperActual;
			}
		}
		string mNombre = "";
		public virtual string Nombre
		{
			set
			{
				mNombre = value;
			}
			get
			{
				return mNombre;
			}
		}
		bool misAlive = true;
		public virtual bool isAlive
		{
			set
			{
				misAlive = value;
			}
			get
			{
				return misAlive;
			}
		}
		protected FlatRedBall.Graphics.Layer LayerProvidedByContainer = null;

        public Player()
            : this(FlatRedBall.Screens.ScreenManager.CurrentScreen.ContentManagerName, true)
        {

        }

        public Player(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public Player(string contentManagerName, bool addToManagers) :
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
		}
		public virtual void AddToManagers (FlatRedBall.Graphics.Layer layerToAddTo)
		{
			LayerProvidedByContainer = layerToAddTo;
			FlatRedBall.SpriteManager.AddPositionedObject(this);
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
			


			CustomDestroy();
		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			if (Cuerpo!= null)
			{
				if (Cuerpo.Parent == null)
				{
					Cuerpo.CopyAbsoluteToRelative();
					Cuerpo.AttachTo(this, false);
				}
			}
			if (Sprite!= null)
			{
				if (Sprite.Parent == null)
				{
					Sprite.CopyAbsoluteToRelative();
					Sprite.AttachTo(this, false);
				}
			}
			if (Shield!= null)
			{
				if (Shield.Parent == null)
				{
					Shield.CopyAbsoluteToRelative();
					Shield.AttachTo(this, false);
				}
			}
			if (ListaAtaques!= null)
			{
			}
			if (Golpe!= null)
			{
				if (Golpe.Parent == null)
				{
					Golpe.CopyAbsoluteToRelative();
					Golpe.AttachTo(this, false);
				}
			}
			if (ListaAtaqueSuper!= null)
			{
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
		}
		public virtual void AssignCustomVariables (bool callOnContainedElements)
		{
			if (callOnContainedElements)
			{
			}
			SuperCompleta = 100f;
			SuperActual = 0f;
			Nombre = "";
			isAlive = true;
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			this.ForceUpdateDependenciesDeep();
			FlatRedBall.SpriteManager.ConvertToManuallyUpdated(this);
			if (Cuerpo != null)
			{
			}
			if (Sprite != null)
			{
				FlatRedBall.SpriteManager.ConvertToManuallyUpdated(Sprite);
			}
			if (Shield != null)
			{
			}
			if (ListaAtaques != null)
			{
				for (int i = 0; i < ListaAtaques.Count; i++)
				{
					ListaAtaques[i].ConvertToManuallyUpdated();
				}
			}
			if (Golpe != null)
			{
			}
			if (ListaAtaqueSuper != null)
			{
				for (int i = 0; i < ListaAtaqueSuper.Count; i++)
				{
					ListaAtaqueSuper[i].ConvertToManuallyUpdated();
				}
			}
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
						FlatRedBall.FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("PlayerStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
			}
			if (registerUnload && ContentManagerName != FlatRedBall.FlatRedBallServices.GlobalContentManager)
			{
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBall.FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBall.FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("PlayerStaticUnload", UnloadStaticContent);
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
			}
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
			if (Cuerpo != null)
			{
				FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(Cuerpo);
			}
			if (Sprite != null)
			{
				FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(Sprite);
			}
			if (Shield != null)
			{
				FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(Shield);
			}
			if (ListaAtaques != null)
			{
			}
			if (Golpe != null)
			{
				FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(Golpe);
			}
			if (ListaAtaqueSuper != null)
			{
			}
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
