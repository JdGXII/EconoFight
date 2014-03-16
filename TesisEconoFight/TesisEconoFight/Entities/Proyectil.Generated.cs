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
	public partial class Proyectil : TesisEconoFight.Entities.Especial, IDestroyable
	{
        // This is made global so that static lazy-loaded content can access it.
        public static new string ContentManagerName
        {
            get{ return Entities.Especial.ContentManagerName;}
            set{ Entities.Especial.ContentManagerName = value;}
        }

		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		public enum VariableState
		{
			Uninitialized = 0, //This exists so that the first set call actually does something
			Unknown = 1, //This exists so that if the entity is actually a child entity and has set a child state, you will get this
			Oro = 2, 
			FiatNatural = 3, 
			FiatAlto = 4, 
			FiatBajo = 5
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
					case  VariableState.Oro:
						ChainName = "Oro";
						break;
					case  VariableState.FiatNatural:
						ChainName = "Natural";
						break;
					case  VariableState.FiatAlto:
						ChainName = "Alto";
						break;
					case  VariableState.FiatBajo:
						ChainName = "Bajo";
						break;
				}
			}
		}
		static object mLockObject = new object();
		static List<string> mRegisteredUnloads = new List<string>();
		static List<string> LoadedContentManagers = new List<string>();
		private static FlatRedBall.Graphics.Animation.AnimationChainList AnimacionProyectil;
		private static FlatRedBall.Scene SceneFile;
		
		private FlatRedBall.Sprite Sprite;
		public float Velocidad;
		public string Estado = "";
		public string ChainName
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
		public int Index { get; set; }
		public bool Used { get; set; }

        public Proyectil(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public Proyectil(string contentManagerName, bool addToManagers) :
			base(contentManagerName, addToManagers)
		{
			// Don't delete this:
            ContentManagerName = contentManagerName;
           

		}

		protected override void InitializeEntity(bool addToManagers)
		{
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			Sprite = SceneFile.Sprites.FindByName("moneda 1 moneda png 64 x 64 intocable!1").Clone();
			Cuerpo = new FlatRedBall.Math.Geometry.AxisAlignedRectangle();
			
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
			
			CustomActivity();
			
			// After Custom Activity
		}

		public override void Destroy()
		{
			// Generated Destroy
			base.Destroy();
			
			if (Sprite != null)
			{
				Sprite.Detach(); SpriteManager.RemoveSprite(Sprite);
			}
			if (Cuerpo != null)
			{
				Cuerpo.Detach(); ShapeManager.Remove(Cuerpo);
			}


			CustomDestroy();
		}

		// Generated Methods
		public override void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			base.PostInitialize();
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
			Estado = "";
			Tipo = "";
			ChainName = "Oro";
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
			SpriteManager.AddToLayer(Sprite, layerToAddTo);
			ShapeManager.AddToLayer(Cuerpo, layerToAddTo);
			Estado = "";
			Tipo = "";
			ChainName = "Oro";
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
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("ProyectilStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
				if (!FlatRedBallServices.IsLoaded<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/entities/proyectil/animacionproyectil.achx", ContentManagerName))
				{
					registerUnload = true;
				}
				AnimacionProyectil = FlatRedBallServices.Load<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/entities/proyectil/animacionproyectil.achx", ContentManagerName);
				if (!FlatRedBallServices.IsLoaded<FlatRedBall.Scene>(@"content/entities/proyectil/scenefile.scnx", ContentManagerName))
				{
					registerUnload = true;
				}
				SceneFile = FlatRedBallServices.Load<FlatRedBall.Scene>(@"content/entities/proyectil/scenefile.scnx", ContentManagerName);
			}
			if (registerUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
			{
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("ProyectilStaticUnload", UnloadStaticContent);
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
				if (AnimacionProyectil != null)
				{
					AnimacionProyectil= null;
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
				case  VariableState.Oro:
					break;
				case  VariableState.FiatNatural:
					break;
				case  VariableState.FiatAlto:
					break;
				case  VariableState.FiatBajo:
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
				case  VariableState.Oro:
					break;
				case  VariableState.FiatNatural:
					break;
				case  VariableState.FiatAlto:
					break;
				case  VariableState.FiatBajo:
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
				case  VariableState.Oro:
					if (interpolationValue < 1)
					{
						this.ChainName = "Oro";
					}
					break;
				case  VariableState.FiatNatural:
					if (interpolationValue < 1)
					{
						this.ChainName = "Natural";
					}
					break;
				case  VariableState.FiatAlto:
					if (interpolationValue < 1)
					{
						this.ChainName = "Alto";
					}
					break;
				case  VariableState.FiatBajo:
					if (interpolationValue < 1)
					{
						this.ChainName = "Bajo";
					}
					break;
			}
			switch(secondState)
			{
				case  VariableState.Oro:
					if (interpolationValue >= 1)
					{
						this.ChainName = "Oro";
					}
					break;
				case  VariableState.FiatNatural:
					if (interpolationValue >= 1)
					{
						this.ChainName = "Natural";
					}
					break;
				case  VariableState.FiatAlto:
					if (interpolationValue >= 1)
					{
						this.ChainName = "Alto";
					}
					break;
				case  VariableState.FiatBajo:
					if (interpolationValue >= 1)
					{
						this.ChainName = "Bajo";
					}
					break;
			}
		}
		public static void PreloadStateContent (VariableState state, string contentManagerName)
		{
			ContentManagerName = contentManagerName;
			object throwaway;
			switch(state)
			{
				case  VariableState.Oro:
					throwaway = "Oro";
					break;
				case  VariableState.FiatNatural:
					throwaway = "Natural";
					break;
				case  VariableState.FiatAlto:
					throwaway = "Alto";
					break;
				case  VariableState.FiatBajo:
					throwaway = "Bajo";
					break;
			}
		}
		[System.Obsolete("Use GetFile instead")]
		public static new object GetStaticMember (string memberName)
		{
			switch(memberName)
			{
				case  "AnimacionProyectil":
					return AnimacionProyectil;
				case  "SceneFile":
					return SceneFile;
			}
			return null;
		}
		public static new object GetFile (string memberName)
		{
			switch(memberName)
			{
				case  "AnimacionProyectil":
					return AnimacionProyectil;
				case  "SceneFile":
					return SceneFile;
			}
			return null;
		}
		object GetMember (string memberName)
		{
			switch(memberName)
			{
				case  "AnimacionProyectil":
					return AnimacionProyectil;
				case  "SceneFile":
					return SceneFile;
			}
			return null;
		}
		public override void SetToIgnorePausing ()
		{
			base.SetToIgnorePausing();
			InstructionManager.IgnorePausingFor(Sprite);
			InstructionManager.IgnorePausingFor(Cuerpo);
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
	public static class ProyectilExtensionMethods
	{
	}
	
}
