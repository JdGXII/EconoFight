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

namespace TesisEconoFight.Screens
{
	public partial class SplashScreen : FlatRedBall.Screens.Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		public enum VariableState
		{
			Uninitialized = 0, //This exists so that the first set call actually does something
			Unknown = 1, //This exists so that if the entity is actually a child entity and has set a child state, you will get this
			Opaque = 2, 
			Transparent = 3
		}
		protected int mCurrentState = 0;
		public Screens.SplashScreen.VariableState CurrentState
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
					case  VariableState.Opaque:
						SpriteObjectAlpha = 1f;
						break;
					case  VariableState.Transparent:
						SpriteObjectAlpha = 0f;
						break;
				}
			}
		}
		protected FlatRedBall.Scene SceneFile;
		
		private FlatRedBall.Sprite SpriteObject;
		public float SpriteObjectAlpha
		{
			get
			{
				return SpriteObject.Alpha;
			}
			set
			{
				SpriteObject.Alpha = value;
			}
		}

		public SplashScreen()
			: base("SplashScreen")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			if (!FlatRedBall.FlatRedBallServices.IsLoaded<FlatRedBall.Scene>(@"content/screens/splashscreen/scenefile.scnx", ContentManagerName))
			{
			}
			SceneFile = FlatRedBall.FlatRedBallServices.Load<FlatRedBall.Scene>(@"content/screens/splashscreen/scenefile.scnx", ContentManagerName);
			SpriteObject = SceneFile.Sprites.FindByName("frblogo5121");
			
			this.NextScreen = typeof(TesisEconoFight.Screens.MenuPrincipal).FullName;
			
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
		}
		public virtual void AssignCustomVariables (bool callOnContainedElements)
		{
			if (callOnContainedElements)
			{
			}
			SpriteObjectAlpha = 0f;
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			SceneFile.ConvertToManuallyUpdated();
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
				case  VariableState.Opaque:
					SpriteObject.AlphaRate = (1f - SpriteObject.Alpha) / (float)secondsToTake;
					break;
				case  VariableState.Transparent:
					SpriteObject.AlphaRate = (0f - SpriteObject.Alpha) / (float)secondsToTake;
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
				case  VariableState.Opaque:
					SpriteObject.AlphaRate =  0;
					break;
				case  VariableState.Transparent:
					SpriteObject.AlphaRate =  0;
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
			bool setSpriteObjectAlpha = true;
			float SpriteObjectAlphaFirstValue= 0;
			float SpriteObjectAlphaSecondValue= 0;
			switch(firstState)
			{
				case  VariableState.Opaque:
					SpriteObjectAlphaFirstValue = 1f;
					break;
				case  VariableState.Transparent:
					SpriteObjectAlphaFirstValue = 0f;
					break;
			}
			switch(secondState)
			{
				case  VariableState.Opaque:
					SpriteObjectAlphaSecondValue = 1f;
					break;
				case  VariableState.Transparent:
					SpriteObjectAlphaSecondValue = 0f;
					break;
			}
			if (setSpriteObjectAlpha)
			{
				SpriteObjectAlpha = SpriteObjectAlphaFirstValue * (1 - interpolationValue) + SpriteObjectAlphaSecondValue * interpolationValue;
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
				case  "SceneFile":
					return SceneFile;
			}
			return null;
		}


	}
}
