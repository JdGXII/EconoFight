#if ANDROID || IOS
#define REQUIRES_PRIMARY_THREAD_LOADING
#endif

using Color = Microsoft.Xna.Framework.Color;

namespace TesisEconoFight.Entities
{
	public partial class Keynes : TesisEconoFight.Entities.Player, FlatRedBall.Graphics.IDestroyable
	{
        // This is made static so that static lazy-loaded content can access it.
        public static new string ContentManagerName
        {
            get{ return Entities.Player.ContentManagerName;}
            set{ Entities.Player.ContentManagerName = value;}
        }

		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		public enum VariableState
		{
			Uninitialized = 0, //This exists so that the first set call actually does something
			Unknown = 1, //This exists so that if the entity is actually a child entity and has set a child state, you will get this
			Parado = 2, 
			Caminando = 3, 
			Agachado = 4, 
			BloqueoArriba = 5, 
			BloqueoAbajo = 6, 
			GolpeArriba = 7, 
			GolpeAbajo = 8, 
			Especial1 = 9, 
			Especial2 = 10, 
			Super = 11, 
			Golpeado = 12, 
			Muerto = 13, 
			FlipParado = 14, 
			FlipCaminando = 15, 
			FlipAgachado = 16, 
			FlipBloqueoArriba = 17, 
			FlipBloqueoAbajo = 18, 
			FlipGolpeArriba = 19, 
			FlipGolpeAbajo = 20, 
			FlipEspecial1 = 21, 
			FlipEspecial2 = 22, 
			FlipSuper = 23, 
			FlipGolpeado = 24, 
			FlipMuerto = 25, 
			Boom = 26, 
			Bust = 27, 
			FlipBoom = 28, 
			FlipBust = 29, 
			Nulo = 30
		}
		protected int mCurrentState = 0;
		public Entities.Keynes.VariableState CurrentState
		{
			get
			{
				if (System.Enum.IsDefined(typeof(VariableState), mCurrentState))
				{
					return (VariableState)mCurrentState;
				}
				else
				{
					return VariableState.Unknown;
				}
			}
			set
			{
				mCurrentState = (int)value;
				switch(CurrentState)
				{
					case  VariableState.Uninitialized:
						break;
					case  VariableState.Unknown:
						break;
					case  VariableState.Parado:
						CurrentChainName = "Parado";
						break;
					case  VariableState.Caminando:
						CurrentChainName = "Caminar";
						break;
					case  VariableState.Agachado:
						CurrentChainName = "Agachado";
						break;
					case  VariableState.BloqueoArriba:
						CurrentChainName = "BloqueoArriba";
						break;
					case  VariableState.BloqueoAbajo:
						CurrentChainName = "BloqueoAbajo";
						break;
					case  VariableState.GolpeArriba:
						CurrentChainName = "GolpeArriba";
						break;
					case  VariableState.GolpeAbajo:
						CurrentChainName = "GolpeAbajo";
						break;
					case  VariableState.Especial1:
						CurrentChainName = "Especial1";
						break;
					case  VariableState.Especial2:
						CurrentChainName = "Especial2";
						break;
					case  VariableState.Super:
						CurrentChainName = "Super";
						break;
					case  VariableState.Golpeado:
						CurrentChainName = "Golpeado";
						break;
					case  VariableState.Muerto:
						CurrentChainName = "Muerto";
						break;
					case  VariableState.FlipParado:
						CurrentChainName = "FlipParado";
						break;
					case  VariableState.FlipCaminando:
						CurrentChainName = "FlipCaminar";
						break;
					case  VariableState.FlipAgachado:
						CurrentChainName = "FlipAgachado";
						break;
					case  VariableState.FlipBloqueoArriba:
						CurrentChainName = "FlipBloqueoArriba";
						break;
					case  VariableState.FlipBloqueoAbajo:
						CurrentChainName = "FlipBloqueoAbajo";
						break;
					case  VariableState.FlipGolpeArriba:
						CurrentChainName = "FlipGolpeArriba";
						break;
					case  VariableState.FlipGolpeAbajo:
						CurrentChainName = "FlipGolpeAbajo";
						break;
					case  VariableState.FlipEspecial1:
						CurrentChainName = "FlipEspecial1";
						break;
					case  VariableState.FlipEspecial2:
						CurrentChainName = "FlipEspecial2";
						break;
					case  VariableState.FlipSuper:
						CurrentChainName = "FlipSuper";
						break;
					case  VariableState.FlipGolpeado:
						CurrentChainName = "FlipGolpeado";
						break;
					case  VariableState.FlipMuerto:
						CurrentChainName = "FlipMuerto";
						break;
					case  VariableState.Boom:
						break;
					case  VariableState.Bust:
						break;
					case  VariableState.FlipBoom:
						break;
					case  VariableState.FlipBust:
						break;
					case  VariableState.Nulo:
						break;
				}
			}
		}
		static object mLockObject = new object();
		static System.Collections.Generic.List<string> mRegisteredUnloads = new System.Collections.Generic.List<string>();
		static System.Collections.Generic.List<string> LoadedContentManagers = new System.Collections.Generic.List<string>();
		protected static FlatRedBall.Graphics.Animation.AnimationChainList AnimacionKeynes;
		protected static FlatRedBall.Scene SceneFile;
		
		public string Estado = "Bajo";
		public string CurrentChainName
		{
			get
			{
				return Sprite.CurrentChainName;
			}
			set
			{
				Sprite.CurrentChainName = value;
			}
		}
		public bool SpriteVisible
		{
			get
			{
				return Sprite.Visible;
			}
			set
			{
				Sprite.Visible = value;
			}
		}

        public Keynes()
            : this(FlatRedBall.Screens.ScreenManager.CurrentScreen.ContentManagerName, true)
        {

        }

        public Keynes(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public Keynes(string contentManagerName, bool addToManagers) :
			base(contentManagerName, addToManagers)
		{
			// Don't delete this:
            ContentManagerName = contentManagerName;
           

		}

		protected override void InitializeEntity(bool addToManagers)
		{
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			Cuerpo = new FlatRedBall.Math.Geometry.AxisAlignedRectangle();
			Cuerpo.Name = "Cuerpo";
			Sprite = SceneFile.Sprites.FindByName("keynes 131").Clone();
			Shield = new FlatRedBall.Math.Geometry.AxisAlignedRectangle();
			Shield.Name = "Shield";
			mListaAtaques = new FlatRedBall.Math.PositionedObjectList<TesisEconoFight.Entities.Especial>();
			mListaAtaques.Name = "mListaAtaques";
			Golpe = new FlatRedBall.Math.Geometry.AxisAlignedRectangle();
			Golpe.Name = "Golpe";
			mListaAtaqueSuper = new FlatRedBall.Math.PositionedObjectList<TesisEconoFight.Entities.SuperAtaque>();
			mListaAtaqueSuper.Name = "mListaAtaqueSuper";
			
			base.InitializeEntity(addToManagers);


		}

// Generated AddToManagers
		public override void ReAddToManagers (FlatRedBall.Graphics.Layer layerToAddTo)
		{
			base.ReAddToManagers(layerToAddTo);
			FlatRedBall.Math.Geometry.ShapeManager.AddToLayer(Cuerpo, LayerProvidedByContainer);
			FlatRedBall.SpriteManager.AddToLayer(Sprite, LayerProvidedByContainer);
			FlatRedBall.Math.Geometry.ShapeManager.AddToLayer(Shield, LayerProvidedByContainer);
			FlatRedBall.Math.Geometry.ShapeManager.AddToLayer(Golpe, LayerProvidedByContainer);
		}
		public override void AddToManagers (FlatRedBall.Graphics.Layer layerToAddTo)
		{
			LayerProvidedByContainer = layerToAddTo;
			FlatRedBall.Math.Geometry.ShapeManager.AddToLayer(Cuerpo, LayerProvidedByContainer);
			FlatRedBall.SpriteManager.AddToLayer(Sprite, LayerProvidedByContainer);
			FlatRedBall.Math.Geometry.ShapeManager.AddToLayer(Shield, LayerProvidedByContainer);
			FlatRedBall.Math.Geometry.ShapeManager.AddToLayer(Golpe, LayerProvidedByContainer);
			base.AddToManagers(layerToAddTo);
			CustomInitialize();
		}

		public override void Activity()
		{
			// Generated Activity
			base.Activity();
			
			for (int i = ListaAtaques.Count - 1; i > -1; i--)
			{
				if (i < ListaAtaques.Count)
				{
					// We do the extra if-check because activity could destroy any number of entities
					ListaAtaques[i].Activity();
				}
			}
			for (int i = ListaAtaqueSuper.Count - 1; i > -1; i--)
			{
				if (i < ListaAtaqueSuper.Count)
				{
					// We do the extra if-check because activity could destroy any number of entities
					ListaAtaqueSuper[i].Activity();
				}
			}
			CustomActivity();
			
			// After Custom Activity
		}

		public override void Destroy()
		{
			// Generated Destroy
			base.Destroy();
			
			ListaAtaques.MakeOneWay();
			ListaAtaqueSuper.MakeOneWay();
			if (Cuerpo != null)
			{
				FlatRedBall.Math.Geometry.ShapeManager.Remove(Cuerpo);
			}
			if (Sprite != null)
			{
				FlatRedBall.SpriteManager.RemoveSprite(Sprite);
			}
			if (Shield != null)
			{
				FlatRedBall.Math.Geometry.ShapeManager.Remove(Shield);
			}
			for (int i = ListaAtaques.Count - 1; i > -1; i--)
			{
				ListaAtaques[i].Destroy();
			}
			if (Golpe != null)
			{
				FlatRedBall.Math.Geometry.ShapeManager.Remove(Golpe);
			}
			for (int i = ListaAtaqueSuper.Count - 1; i > -1; i--)
			{
				ListaAtaqueSuper[i].Destroy();
			}
			ListaAtaques.MakeTwoWay();
			ListaAtaqueSuper.MakeTwoWay();


			CustomDestroy();
		}

		// Generated Methods
		public override void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			base.PostInitialize();
			if (Cuerpo.Parent == null)
			{
				Cuerpo.CopyAbsoluteToRelative();
				Cuerpo.AttachTo(this, false);
			}
			if (Sprite.Parent == null)
			{
				Sprite.CopyAbsoluteToRelative();
				Sprite.AttachTo(this, false);
			}
			if (Shield.Parent == null)
			{
				Shield.CopyAbsoluteToRelative();
				Shield.AttachTo(this, false);
			}
			if (Golpe.Parent == null)
			{
				Golpe.CopyAbsoluteToRelative();
				Golpe.AttachTo(this, false);
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
			if (Cuerpo != null)
			{
				FlatRedBall.Math.Geometry.ShapeManager.RemoveOneWay(Cuerpo);
			}
			if (Sprite != null)
			{
				FlatRedBall.SpriteManager.RemoveSpriteOneWay(Sprite);
			}
			if (Shield != null)
			{
				FlatRedBall.Math.Geometry.ShapeManager.RemoveOneWay(Shield);
			}
			for (int i = ListaAtaques.Count - 1; i > -1; i--)
			{
				ListaAtaques[i].Destroy();
			}
			if (Golpe != null)
			{
				FlatRedBall.Math.Geometry.ShapeManager.RemoveOneWay(Golpe);
			}
			for (int i = ListaAtaqueSuper.Count - 1; i > -1; i--)
			{
				ListaAtaqueSuper[i].Destroy();
			}
		}
		public override void AssignCustomVariables (bool callOnContainedElements)
		{
			base.AssignCustomVariables(callOnContainedElements);
			if (callOnContainedElements)
			{
			}
			VidaTotal = 110000f;
			VidaActual = 110000f;
			Velocidad = 4f;
			PhysDamage = 700f;
			SuperCompleta = 100f;
			SuperActual = 0f;
			Nombre = "Keynes";
			Estado = "Bajo";
			isAlive = true;
			CurrentChainName = "Parado";
			SpriteVisible = true;
		}
		public override void ConvertToManuallyUpdated ()
		{
			base.ConvertToManuallyUpdated();
			this.ForceUpdateDependenciesDeep();
			FlatRedBall.SpriteManager.ConvertToManuallyUpdated(this);
			FlatRedBall.SpriteManager.ConvertToManuallyUpdated(Sprite);
			for (int i = 0; i < ListaAtaques.Count; i++)
			{
				ListaAtaques[i].ConvertToManuallyUpdated();
			}
			for (int i = 0; i < ListaAtaqueSuper.Count; i++)
			{
				ListaAtaqueSuper[i].ConvertToManuallyUpdated();
			}
		}
		public static new void LoadStaticContent (string contentManagerName)
		{
			if (string.IsNullOrEmpty(contentManagerName))
			{
				throw new System.ArgumentException("contentManagerName cannot be empty or null");
			}
			ContentManagerName = contentManagerName;
			TesisEconoFight.Entities.Player.LoadStaticContent(contentManagerName);
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
						FlatRedBall.FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("KeynesStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
				if (!FlatRedBall.FlatRedBallServices.IsLoaded<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/entities/keynes/animacionkeynes.achx", ContentManagerName))
				{
					registerUnload = true;
				}
				AnimacionKeynes = FlatRedBall.FlatRedBallServices.Load<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/entities/keynes/animacionkeynes.achx", ContentManagerName);
				if (!FlatRedBall.FlatRedBallServices.IsLoaded<FlatRedBall.Scene>(@"content/entities/keynes/scenefile.scnx", ContentManagerName))
				{
					registerUnload = true;
				}
				SceneFile = FlatRedBall.FlatRedBallServices.Load<FlatRedBall.Scene>(@"content/entities/keynes/scenefile.scnx", ContentManagerName);
			}
			if (registerUnload && ContentManagerName != FlatRedBall.FlatRedBallServices.GlobalContentManager)
			{
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBall.FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBall.FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("KeynesStaticUnload", UnloadStaticContent);
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
				if (AnimacionKeynes != null)
				{
					AnimacionKeynes= null;
				}
				if (SceneFile != null)
				{
					SceneFile.RemoveFromManagers(ContentManagerName != "Global");
					SceneFile= null;
				}
			}
		}
		static VariableState mLoadingState = VariableState.Uninitialized;
		public static VariableState LoadingState
		{
			get
			{
				return mLoadingState;
			}
			set
			{
				mLoadingState = value;
			}
		}
		public FlatRedBall.Instructions.Instruction InterpolateToState (VariableState stateToInterpolateTo, double secondsToTake)
		{
			switch(stateToInterpolateTo)
			{
				case  VariableState.Parado:
					break;
				case  VariableState.Caminando:
					break;
				case  VariableState.Agachado:
					break;
				case  VariableState.BloqueoArriba:
					break;
				case  VariableState.BloqueoAbajo:
					break;
				case  VariableState.GolpeArriba:
					break;
				case  VariableState.GolpeAbajo:
					break;
				case  VariableState.Especial1:
					break;
				case  VariableState.Especial2:
					break;
				case  VariableState.Super:
					break;
				case  VariableState.Golpeado:
					break;
				case  VariableState.Muerto:
					break;
				case  VariableState.FlipParado:
					break;
				case  VariableState.FlipCaminando:
					break;
				case  VariableState.FlipAgachado:
					break;
				case  VariableState.FlipBloqueoArriba:
					break;
				case  VariableState.FlipBloqueoAbajo:
					break;
				case  VariableState.FlipGolpeArriba:
					break;
				case  VariableState.FlipGolpeAbajo:
					break;
				case  VariableState.FlipEspecial1:
					break;
				case  VariableState.FlipEspecial2:
					break;
				case  VariableState.FlipSuper:
					break;
				case  VariableState.FlipGolpeado:
					break;
				case  VariableState.FlipMuerto:
					break;
				case  VariableState.Boom:
					break;
				case  VariableState.Bust:
					break;
				case  VariableState.FlipBoom:
					break;
				case  VariableState.FlipBust:
					break;
				case  VariableState.Nulo:
					break;
			}
			var instruction = new FlatRedBall.Instructions.DelegateInstruction<VariableState>(StopStateInterpolation, stateToInterpolateTo);
			instruction.TimeToExecute = FlatRedBall.TimeManager.CurrentTime + secondsToTake;
			this.Instructions.Add(instruction);
			return instruction;
		}
		public void StopStateInterpolation (VariableState stateToStop)
		{
			switch(stateToStop)
			{
				case  VariableState.Parado:
					break;
				case  VariableState.Caminando:
					break;
				case  VariableState.Agachado:
					break;
				case  VariableState.BloqueoArriba:
					break;
				case  VariableState.BloqueoAbajo:
					break;
				case  VariableState.GolpeArriba:
					break;
				case  VariableState.GolpeAbajo:
					break;
				case  VariableState.Especial1:
					break;
				case  VariableState.Especial2:
					break;
				case  VariableState.Super:
					break;
				case  VariableState.Golpeado:
					break;
				case  VariableState.Muerto:
					break;
				case  VariableState.FlipParado:
					break;
				case  VariableState.FlipCaminando:
					break;
				case  VariableState.FlipAgachado:
					break;
				case  VariableState.FlipBloqueoArriba:
					break;
				case  VariableState.FlipBloqueoAbajo:
					break;
				case  VariableState.FlipGolpeArriba:
					break;
				case  VariableState.FlipGolpeAbajo:
					break;
				case  VariableState.FlipEspecial1:
					break;
				case  VariableState.FlipEspecial2:
					break;
				case  VariableState.FlipSuper:
					break;
				case  VariableState.FlipGolpeado:
					break;
				case  VariableState.FlipMuerto:
					break;
				case  VariableState.Boom:
					break;
				case  VariableState.Bust:
					break;
				case  VariableState.FlipBoom:
					break;
				case  VariableState.FlipBust:
					break;
				case  VariableState.Nulo:
					break;
			}
			CurrentState = stateToStop;
		}
		public void InterpolateBetween (VariableState firstState, VariableState secondState, float interpolationValue)
		{
			#if DEBUG
			if (float.IsNaN(interpolationValue))
			{
				throw new System.Exception("interpolationValue cannot be NaN");
			}
			#endif
			switch(firstState)
			{
				case  VariableState.Parado:
					if (interpolationValue < 1)
					{
						this.CurrentChainName = "Parado";
					}
					break;
				case  VariableState.Caminando:
					if (interpolationValue < 1)
					{
						this.CurrentChainName = "Caminar";
					}
					break;
				case  VariableState.Agachado:
					if (interpolationValue < 1)
					{
						this.CurrentChainName = "Agachado";
					}
					break;
				case  VariableState.BloqueoArriba:
					if (interpolationValue < 1)
					{
						this.CurrentChainName = "BloqueoArriba";
					}
					break;
				case  VariableState.BloqueoAbajo:
					if (interpolationValue < 1)
					{
						this.CurrentChainName = "BloqueoAbajo";
					}
					break;
				case  VariableState.GolpeArriba:
					if (interpolationValue < 1)
					{
						this.CurrentChainName = "GolpeArriba";
					}
					break;
				case  VariableState.GolpeAbajo:
					if (interpolationValue < 1)
					{
						this.CurrentChainName = "GolpeAbajo";
					}
					break;
				case  VariableState.Especial1:
					if (interpolationValue < 1)
					{
						this.CurrentChainName = "Especial1";
					}
					break;
				case  VariableState.Especial2:
					if (interpolationValue < 1)
					{
						this.CurrentChainName = "Especial2";
					}
					break;
				case  VariableState.Super:
					if (interpolationValue < 1)
					{
						this.CurrentChainName = "Super";
					}
					break;
				case  VariableState.Golpeado:
					if (interpolationValue < 1)
					{
						this.CurrentChainName = "Golpeado";
					}
					break;
				case  VariableState.Muerto:
					if (interpolationValue < 1)
					{
						this.CurrentChainName = "Muerto";
					}
					break;
				case  VariableState.FlipParado:
					if (interpolationValue < 1)
					{
						this.CurrentChainName = "FlipParado";
					}
					break;
				case  VariableState.FlipCaminando:
					if (interpolationValue < 1)
					{
						this.CurrentChainName = "FlipCaminar";
					}
					break;
				case  VariableState.FlipAgachado:
					if (interpolationValue < 1)
					{
						this.CurrentChainName = "FlipAgachado";
					}
					break;
				case  VariableState.FlipBloqueoArriba:
					if (interpolationValue < 1)
					{
						this.CurrentChainName = "FlipBloqueoArriba";
					}
					break;
				case  VariableState.FlipBloqueoAbajo:
					if (interpolationValue < 1)
					{
						this.CurrentChainName = "FlipBloqueoAbajo";
					}
					break;
				case  VariableState.FlipGolpeArriba:
					if (interpolationValue < 1)
					{
						this.CurrentChainName = "FlipGolpeArriba";
					}
					break;
				case  VariableState.FlipGolpeAbajo:
					if (interpolationValue < 1)
					{
						this.CurrentChainName = "FlipGolpeAbajo";
					}
					break;
				case  VariableState.FlipEspecial1:
					if (interpolationValue < 1)
					{
						this.CurrentChainName = "FlipEspecial1";
					}
					break;
				case  VariableState.FlipEspecial2:
					if (interpolationValue < 1)
					{
						this.CurrentChainName = "FlipEspecial2";
					}
					break;
				case  VariableState.FlipSuper:
					if (interpolationValue < 1)
					{
						this.CurrentChainName = "FlipSuper";
					}
					break;
				case  VariableState.FlipGolpeado:
					if (interpolationValue < 1)
					{
						this.CurrentChainName = "FlipGolpeado";
					}
					break;
				case  VariableState.FlipMuerto:
					if (interpolationValue < 1)
					{
						this.CurrentChainName = "FlipMuerto";
					}
					break;
				case  VariableState.Boom:
					break;
				case  VariableState.Bust:
					break;
				case  VariableState.FlipBoom:
					break;
				case  VariableState.FlipBust:
					break;
				case  VariableState.Nulo:
					break;
			}
			switch(secondState)
			{
				case  VariableState.Parado:
					if (interpolationValue >= 1)
					{
						this.CurrentChainName = "Parado";
					}
					break;
				case  VariableState.Caminando:
					if (interpolationValue >= 1)
					{
						this.CurrentChainName = "Caminar";
					}
					break;
				case  VariableState.Agachado:
					if (interpolationValue >= 1)
					{
						this.CurrentChainName = "Agachado";
					}
					break;
				case  VariableState.BloqueoArriba:
					if (interpolationValue >= 1)
					{
						this.CurrentChainName = "BloqueoArriba";
					}
					break;
				case  VariableState.BloqueoAbajo:
					if (interpolationValue >= 1)
					{
						this.CurrentChainName = "BloqueoAbajo";
					}
					break;
				case  VariableState.GolpeArriba:
					if (interpolationValue >= 1)
					{
						this.CurrentChainName = "GolpeArriba";
					}
					break;
				case  VariableState.GolpeAbajo:
					if (interpolationValue >= 1)
					{
						this.CurrentChainName = "GolpeAbajo";
					}
					break;
				case  VariableState.Especial1:
					if (interpolationValue >= 1)
					{
						this.CurrentChainName = "Especial1";
					}
					break;
				case  VariableState.Especial2:
					if (interpolationValue >= 1)
					{
						this.CurrentChainName = "Especial2";
					}
					break;
				case  VariableState.Super:
					if (interpolationValue >= 1)
					{
						this.CurrentChainName = "Super";
					}
					break;
				case  VariableState.Golpeado:
					if (interpolationValue >= 1)
					{
						this.CurrentChainName = "Golpeado";
					}
					break;
				case  VariableState.Muerto:
					if (interpolationValue >= 1)
					{
						this.CurrentChainName = "Muerto";
					}
					break;
				case  VariableState.FlipParado:
					if (interpolationValue >= 1)
					{
						this.CurrentChainName = "FlipParado";
					}
					break;
				case  VariableState.FlipCaminando:
					if (interpolationValue >= 1)
					{
						this.CurrentChainName = "FlipCaminar";
					}
					break;
				case  VariableState.FlipAgachado:
					if (interpolationValue >= 1)
					{
						this.CurrentChainName = "FlipAgachado";
					}
					break;
				case  VariableState.FlipBloqueoArriba:
					if (interpolationValue >= 1)
					{
						this.CurrentChainName = "FlipBloqueoArriba";
					}
					break;
				case  VariableState.FlipBloqueoAbajo:
					if (interpolationValue >= 1)
					{
						this.CurrentChainName = "FlipBloqueoAbajo";
					}
					break;
				case  VariableState.FlipGolpeArriba:
					if (interpolationValue >= 1)
					{
						this.CurrentChainName = "FlipGolpeArriba";
					}
					break;
				case  VariableState.FlipGolpeAbajo:
					if (interpolationValue >= 1)
					{
						this.CurrentChainName = "FlipGolpeAbajo";
					}
					break;
				case  VariableState.FlipEspecial1:
					if (interpolationValue >= 1)
					{
						this.CurrentChainName = "FlipEspecial1";
					}
					break;
				case  VariableState.FlipEspecial2:
					if (interpolationValue >= 1)
					{
						this.CurrentChainName = "FlipEspecial2";
					}
					break;
				case  VariableState.FlipSuper:
					if (interpolationValue >= 1)
					{
						this.CurrentChainName = "FlipSuper";
					}
					break;
				case  VariableState.FlipGolpeado:
					if (interpolationValue >= 1)
					{
						this.CurrentChainName = "FlipGolpeado";
					}
					break;
				case  VariableState.FlipMuerto:
					if (interpolationValue >= 1)
					{
						this.CurrentChainName = "FlipMuerto";
					}
					break;
				case  VariableState.Boom:
					break;
				case  VariableState.Bust:
					break;
				case  VariableState.FlipBoom:
					break;
				case  VariableState.FlipBust:
					break;
				case  VariableState.Nulo:
					break;
			}
			if (interpolationValue < 1)
			{
				mCurrentState = (int)firstState;
			}
			else
			{
				mCurrentState = (int)secondState;
			}
		}
		public static void PreloadStateContent (VariableState state, string contentManagerName)
		{
			ContentManagerName = contentManagerName;
			switch(state)
			{
				case  VariableState.Parado:
					{
						object throwaway = "Parado";
					}
					break;
				case  VariableState.Caminando:
					{
						object throwaway = "Caminar";
					}
					break;
				case  VariableState.Agachado:
					{
						object throwaway = "Agachado";
					}
					break;
				case  VariableState.BloqueoArriba:
					{
						object throwaway = "BloqueoArriba";
					}
					break;
				case  VariableState.BloqueoAbajo:
					{
						object throwaway = "BloqueoAbajo";
					}
					break;
				case  VariableState.GolpeArriba:
					{
						object throwaway = "GolpeArriba";
					}
					break;
				case  VariableState.GolpeAbajo:
					{
						object throwaway = "GolpeAbajo";
					}
					break;
				case  VariableState.Especial1:
					{
						object throwaway = "Especial1";
					}
					break;
				case  VariableState.Especial2:
					{
						object throwaway = "Especial2";
					}
					break;
				case  VariableState.Super:
					{
						object throwaway = "Super";
					}
					break;
				case  VariableState.Golpeado:
					{
						object throwaway = "Golpeado";
					}
					break;
				case  VariableState.Muerto:
					{
						object throwaway = "Muerto";
					}
					break;
				case  VariableState.FlipParado:
					{
						object throwaway = "FlipParado";
					}
					break;
				case  VariableState.FlipCaminando:
					{
						object throwaway = "FlipCaminar";
					}
					break;
				case  VariableState.FlipAgachado:
					{
						object throwaway = "FlipAgachado";
					}
					break;
				case  VariableState.FlipBloqueoArriba:
					{
						object throwaway = "FlipBloqueoArriba";
					}
					break;
				case  VariableState.FlipBloqueoAbajo:
					{
						object throwaway = "FlipBloqueoAbajo";
					}
					break;
				case  VariableState.FlipGolpeArriba:
					{
						object throwaway = "FlipGolpeArriba";
					}
					break;
				case  VariableState.FlipGolpeAbajo:
					{
						object throwaway = "FlipGolpeAbajo";
					}
					break;
				case  VariableState.FlipEspecial1:
					{
						object throwaway = "FlipEspecial1";
					}
					break;
				case  VariableState.FlipEspecial2:
					{
						object throwaway = "FlipEspecial2";
					}
					break;
				case  VariableState.FlipSuper:
					{
						object throwaway = "FlipSuper";
					}
					break;
				case  VariableState.FlipGolpeado:
					{
						object throwaway = "FlipGolpeado";
					}
					break;
				case  VariableState.FlipMuerto:
					{
						object throwaway = "FlipMuerto";
					}
					break;
				case  VariableState.Boom:
					break;
				case  VariableState.Bust:
					break;
				case  VariableState.FlipBoom:
					break;
				case  VariableState.FlipBust:
					break;
				case  VariableState.Nulo:
					break;
			}
		}
		[System.Obsolete("Use GetFile instead")]
		public static new object GetStaticMember (string memberName)
		{
			switch(memberName)
			{
				case  "AnimacionKeynes":
					return AnimacionKeynes;
				case  "SceneFile":
					return SceneFile;
			}
			return null;
		}
		public static new object GetFile (string memberName)
		{
			switch(memberName)
			{
				case  "AnimacionKeynes":
					return AnimacionKeynes;
				case  "SceneFile":
					return SceneFile;
			}
			return null;
		}
		object GetMember (string memberName)
		{
			switch(memberName)
			{
				case  "AnimacionKeynes":
					return AnimacionKeynes;
				case  "SceneFile":
					return SceneFile;
			}
			return null;
		}
		public override void SetToIgnorePausing ()
		{
			base.SetToIgnorePausing();
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(Cuerpo);
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(Sprite);
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(Shield);
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(Golpe);
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
