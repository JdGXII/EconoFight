using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Model;

using FlatRedBall.Input;
using FlatRedBall.Utilities;

using FlatRedBall.Instructions;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
// Generated Usings
using TesisEconoFight.Screens;
using FlatRedBall.Graphics;
using FlatRedBall.Math;
using TesisEconoFight.Entities;
using FlatRedBall;
using FlatRedBall.Screens;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Graphics.Animation;

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
#endif

#if FRB_XNA && !MONODROID
using Model = Microsoft.Xna.Framework.Graphics.Model;
#endif

namespace TesisEconoFight.Entities
{
	public partial class Keynes : TesisEconoFight.Entities.Player, IDestroyable
	{
        // This is made global so that static lazy-loaded content can access it.
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
		public VariableState CurrentState
		{
			get
			{
				if (Enum.IsDefined(typeof(VariableState), mCurrentState))
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
		static List<string> mRegisteredUnloads = new List<string>();
		static List<string> LoadedContentManagers = new List<string>();
		private static FlatRedBall.Graphics.Animation.AnimationChainList AnimacionKeynes;
		private static FlatRedBall.Scene SceneFile;
		
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
		public int Index { get; set; }
		public bool Used { get; set; }

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
			Sprite = SceneFile.Sprites.FindByName("keynes 131").Clone();
			Shield = new FlatRedBall.Math.Geometry.AxisAlignedRectangle();
			mListaAtaques = new PositionedObjectList<Especial>();
			Golpe = new FlatRedBall.Math.Geometry.AxisAlignedRectangle();
			mListaAtaqueSuper = new PositionedObjectList<SuperAtaque>();
			
			base.InitializeEntity(addToManagers);


		}

// Generated AddToManagers
		public override void AddToManagers (Layer layerToAddTo)
		{
			LayerProvidedByContainer = layerToAddTo;
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
			
			if (Cuerpo != null)
			{
				Cuerpo.Detach(); ShapeManager.Remove(Cuerpo);
			}
			if (Sprite != null)
			{
				Sprite.Detach(); SpriteManager.RemoveSprite(Sprite);
			}
			if (Shield != null)
			{
				Shield.Detach(); ShapeManager.Remove(Shield);
			}
			for (int i = ListaAtaques.Count - 1; i > -1; i--)
			{
				ListaAtaques[i].Destroy();
			}
			if (Golpe != null)
			{
				Golpe.Detach(); ShapeManager.Remove(Golpe);
			}
			for (int i = ListaAtaqueSuper.Count - 1; i > -1; i--)
			{
				ListaAtaqueSuper[i].Destroy();
			}


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
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public override void AddToManagersBottomUp (Layer layerToAddTo)
		{
			base.AddToManagersBottomUp(layerToAddTo);
			// We move this back to the origin and unrotate it so that anything attached to it can just use its absolute position
			float oldRotationX = RotationX;
			float oldRotationY = RotationY;
			float oldRotationZ = RotationZ;
			
			float oldX = X;
			float oldY = Y;
			float oldZ = Z;
			
			X = 0;
			Y = 0;
			Z = 0;
			RotationX = 0;
			RotationY = 0;
			RotationZ = 0;
			ShapeManager.AddToLayer(Cuerpo, layerToAddTo);
			SpriteManager.AddToLayer(Sprite, layerToAddTo);
			ShapeManager.AddToLayer(Shield, layerToAddTo);
			ShapeManager.AddToLayer(Golpe, layerToAddTo);
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
			X = oldX;
			Y = oldY;
			Z = oldZ;
			RotationX = oldRotationX;
			RotationY = oldRotationY;
			RotationZ = oldRotationZ;
		}
		public override void ConvertToManuallyUpdated ()
		{
			base.ConvertToManuallyUpdated();
			this.ForceUpdateDependenciesDeep();
			SpriteManager.ConvertToManuallyUpdated(this);
			SpriteManager.ConvertToManuallyUpdated(Sprite);
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
				throw new ArgumentException("contentManagerName cannot be empty or null");
			}
			ContentManagerName = contentManagerName;
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
			bool registerUnload = false;
			if (LoadedContentManagers.Contains(contentManagerName) == false)
			{
				LoadedContentManagers.Add(contentManagerName);
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("KeynesStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
				if (!FlatRedBallServices.IsLoaded<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/entities/keynes/animacionkeynes.achx", ContentManagerName))
				{
					registerUnload = true;
				}
				AnimacionKeynes = FlatRedBallServices.Load<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/entities/keynes/animacionkeynes.achx", ContentManagerName);
				if (!FlatRedBallServices.IsLoaded<FlatRedBall.Scene>(@"content/entities/keynes/scenefile.scnx", ContentManagerName))
				{
					registerUnload = true;
				}
				SceneFile = FlatRedBallServices.Load<FlatRedBall.Scene>(@"content/entities/keynes/scenefile.scnx", ContentManagerName);
			}
			if (registerUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
			{
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("KeynesStaticUnload", UnloadStaticContent);
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
		public Instruction InterpolateToState (VariableState stateToInterpolateTo, double secondsToTake)
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
			var instruction = new DelegateInstruction<VariableState>(StopStateInterpolation, stateToInterpolateTo);
			instruction.TimeToExecute = TimeManager.CurrentTime + secondsToTake;
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
				throw new Exception("interpolationValue cannot be NaN");
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
		}
		public static void PreloadStateContent (VariableState state, string contentManagerName)
		{
			ContentManagerName = contentManagerName;
			object throwaway;
			switch(state)
			{
				case  VariableState.Parado:
					throwaway = "Parado";
					break;
				case  VariableState.Caminando:
					throwaway = "Caminar";
					break;
				case  VariableState.Agachado:
					throwaway = "Agachado";
					break;
				case  VariableState.BloqueoArriba:
					throwaway = "BloqueoArriba";
					break;
				case  VariableState.BloqueoAbajo:
					throwaway = "BloqueoAbajo";
					break;
				case  VariableState.GolpeArriba:
					throwaway = "GolpeArriba";
					break;
				case  VariableState.GolpeAbajo:
					throwaway = "GolpeAbajo";
					break;
				case  VariableState.Especial1:
					throwaway = "Especial1";
					break;
				case  VariableState.Especial2:
					throwaway = "Especial2";
					break;
				case  VariableState.Super:
					throwaway = "Super";
					break;
				case  VariableState.Golpeado:
					throwaway = "Golpeado";
					break;
				case  VariableState.Muerto:
					throwaway = "Muerto";
					break;
				case  VariableState.FlipParado:
					throwaway = "FlipParado";
					break;
				case  VariableState.FlipCaminando:
					throwaway = "FlipCaminar";
					break;
				case  VariableState.FlipAgachado:
					throwaway = "FlipAgachado";
					break;
				case  VariableState.FlipBloqueoArriba:
					throwaway = "FlipBloqueoArriba";
					break;
				case  VariableState.FlipBloqueoAbajo:
					throwaway = "FlipBloqueoAbajo";
					break;
				case  VariableState.FlipGolpeArriba:
					throwaway = "FlipGolpeArriba";
					break;
				case  VariableState.FlipGolpeAbajo:
					throwaway = "FlipGolpeAbajo";
					break;
				case  VariableState.FlipEspecial1:
					throwaway = "FlipEspecial1";
					break;
				case  VariableState.FlipEspecial2:
					throwaway = "FlipEspecial2";
					break;
				case  VariableState.FlipSuper:
					throwaway = "FlipSuper";
					break;
				case  VariableState.FlipGolpeado:
					throwaway = "FlipGolpeado";
					break;
				case  VariableState.FlipMuerto:
					throwaway = "FlipMuerto";
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
			InstructionManager.IgnorePausingFor(Cuerpo);
			InstructionManager.IgnorePausingFor(Sprite);
			InstructionManager.IgnorePausingFor(Shield);
			InstructionManager.IgnorePausingFor(Golpe);
		}
		public void MoveToLayer (Layer layerToMoveTo)
		{
			if (LayerProvidedByContainer != null)
			{
				LayerProvidedByContainer.Remove(Sprite);
			}
			SpriteManager.AddToLayer(Sprite, layerToMoveTo);
			LayerProvidedByContainer = layerToMoveTo;
		}

    }
	
	
	// Extra classes
	public static class KeynesExtensionMethods
	{
	}
	
}
