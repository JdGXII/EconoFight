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
using FlatRedBall.Math.Geometry;
using FlatRedBall.Graphics.Animation;

namespace TesisEconoFight.Screens
{
	public partial class GlosarioKeynes : FlatRedBall.Screens.Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		public enum VariableState
		{
			Uninitialized = 0, //This exists so that the first set call actually does something
			Unknown = 1, //This exists so that if the entity is actually a child entity and has set a child state, you will get this
			P1 = 2, 
			P2 = 3, 
			P3 = 4
		}
		protected int mCurrentState = 0;
		public Screens.GlosarioKeynes.VariableState CurrentState
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
					case  VariableState.P1:
						PantallaCurrentChainName = "Pantalla1";
						break;
					case  VariableState.P2:
						PantallaCurrentChainName = "Pantalla2";
						break;
					case  VariableState.P3:
						PantallaCurrentChainName = "Pantalla3";
						break;
				}
			}
		}
		protected FlatRedBall.Math.Geometry.ShapeCollection Opciones;
		protected FlatRedBall.Scene SceneFile;
		protected FlatRedBall.Graphics.Animation.AnimationChainList AnimacionMundo;
		
		private TesisEconoFight.Entities.Kursor KursorInstance;
		private FlatRedBall.Sprite Pantalla;
		public string PantallaCurrentChainName
		{
			get
			{
				return Pantalla.CurrentChainName;
			}
			set
			{
				Pantalla.CurrentChainName = value;
			}
		}

		public GlosarioKeynes()
			: base("GlosarioKeynes")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			if (!FlatRedBall.FlatRedBallServices.IsLoaded<FlatRedBall.Math.Geometry.ShapeCollection>(@"content/screens/glosariokeynes/opciones.shcx", ContentManagerName))
			{
			}
			Opciones = FlatRedBall.FlatRedBallServices.Load<FlatRedBall.Math.Geometry.ShapeCollection>(@"content/screens/glosariokeynes/opciones.shcx", ContentManagerName);
			if (!FlatRedBall.FlatRedBallServices.IsLoaded<FlatRedBall.Scene>(@"content/screens/glosariokeynes/scenefile.scnx", ContentManagerName))
			{
			}
			SceneFile = FlatRedBall.FlatRedBallServices.Load<FlatRedBall.Scene>(@"content/screens/glosariokeynes/scenefile.scnx", ContentManagerName);
			if (!FlatRedBall.FlatRedBallServices.IsLoaded<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/screens/glosariokeynes/animacionmundo.achx", ContentManagerName))
			{
			}
			AnimacionMundo = FlatRedBall.FlatRedBallServices.Load<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/screens/glosariokeynes/animacionmundo.achx", ContentManagerName);
			KursorInstance = new TesisEconoFight.Entities.Kursor(ContentManagerName, false);
			KursorInstance.Name = "KursorInstance";
			Pantalla = SceneFile.Sprites.FindByName("glosario keynes 11");
			
			
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
			Opciones.AddToManagers(mLayer);
			SceneFile.AddToManagers(mLayer);
			KursorInstance.AddToManagers(mLayer);
			base.AddToManagers();
			AddToManagersBottomUp();
			CustomInitialize();
		}


		public override void Activity(bool firstTimeCalled)
		{
			// Generated Activity
			if (!IsPaused)
			{
				
				KursorInstance.Activity();
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
				Opciones.RemoveFromManagers(ContentManagerName != "Global");
			}
			else
			{
				Opciones.RemoveFromManagers(false);
			}
			if (this.UnloadsContentManagerWhenDestroyed && ContentManagerName != "Global")
			{
				SceneFile.RemoveFromManagers(ContentManagerName != "Global");
			}
			else
			{
				SceneFile.RemoveFromManagers(false);
			}
			
			if (KursorInstance != null)
			{
				KursorInstance.Destroy();
				KursorInstance.Detach();
			}
			if (Pantalla != null)
			{
				FlatRedBall.SpriteManager.RemoveSprite(Pantalla);
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
			CameraSetup.ResetCamera(SpriteManager.Camera);
			AssignCustomVariables(false);
		}
		public virtual void RemoveFromManagers ()
		{
			KursorInstance.RemoveFromManagers();
			if (Pantalla != null)
			{
				FlatRedBall.SpriteManager.RemoveSpriteOneWay(Pantalla);
			}
		}
		public virtual void AssignCustomVariables (bool callOnContainedElements)
		{
			if (callOnContainedElements)
			{
				KursorInstance.AssignCustomVariables(true);
			}
			PantallaCurrentChainName = "Pantalla1";
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			SceneFile.ConvertToManuallyUpdated();
			KursorInstance.ConvertToManuallyUpdated();
			FlatRedBall.SpriteManager.ConvertToManuallyUpdated(Pantalla);
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
			TesisEconoFight.Entities.Kursor.LoadStaticContent(contentManagerName);
			CustomLoadStaticContent(contentManagerName);
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
				case  VariableState.P1:
					break;
				case  VariableState.P2:
					break;
				case  VariableState.P3:
					break;
			}
			var instruction = new FlatRedBall.Instructions.DelegateInstruction<VariableState>(StopStateInterpolation, stateToInterpolateTo);
			instruction.TimeToExecute = FlatRedBall.TimeManager.CurrentTime + secondsToTake;
			FlatRedBall.Instructions.InstructionManager.Add(instruction);
			return instruction;
		}
		public void StopStateInterpolation (VariableState stateToStop)
		{
			switch(stateToStop)
			{
				case  VariableState.P1:
					break;
				case  VariableState.P2:
					break;
				case  VariableState.P3:
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
				case  VariableState.P1:
					if (interpolationValue < 1)
					{
						this.PantallaCurrentChainName = "Pantalla1";
					}
					break;
				case  VariableState.P2:
					if (interpolationValue < 1)
					{
						this.PantallaCurrentChainName = "Pantalla2";
					}
					break;
				case  VariableState.P3:
					if (interpolationValue < 1)
					{
						this.PantallaCurrentChainName = "Pantalla3";
					}
					break;
			}
			switch(secondState)
			{
				case  VariableState.P1:
					if (interpolationValue >= 1)
					{
						this.PantallaCurrentChainName = "Pantalla1";
					}
					break;
				case  VariableState.P2:
					if (interpolationValue >= 1)
					{
						this.PantallaCurrentChainName = "Pantalla2";
					}
					break;
				case  VariableState.P3:
					if (interpolationValue >= 1)
					{
						this.PantallaCurrentChainName = "Pantalla3";
					}
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
				case  "Opciones":
					return Opciones;
				case  "SceneFile":
					return SceneFile;
				case  "AnimacionMundo":
					return AnimacionMundo;
			}
			return null;
		}


	}
}
