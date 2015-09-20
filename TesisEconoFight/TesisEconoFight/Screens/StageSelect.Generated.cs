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
using FlatRedBall.Graphics.Animation;

namespace TesisEconoFight.Screens
{
	public partial class StageSelect : FlatRedBall.Screens.Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		public enum VariableState
		{
			Uninitialized = 0, //This exists so that the first set call actually does something
			Unknown = 1, //This exists so that if the entity is actually a child entity and has set a child state, you will get this
			Hayek = 2, 
			Keynes = 3, 
			Malthus = 4, 
			Smith = 5
		}
		protected int mCurrentState = 0;
		public Screens.StageSelect.VariableState CurrentState
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
					case  VariableState.Hayek:
						CurrentChainName = "Hayek";
						break;
					case  VariableState.Keynes:
						CurrentChainName = "Keynes";
						break;
					case  VariableState.Malthus:
						CurrentChainName = "Malthus";
						break;
					case  VariableState.Smith:
						CurrentChainName = "Smith";
						break;
				}
			}
		}
		protected FlatRedBall.Math.Geometry.ShapeCollection SeleccionStage;
		protected FlatRedBall.Scene SceneFile;
		protected FlatRedBall.Graphics.Animation.AnimationChainList AnimarSeleccion;
		
		private FlatRedBall.Math.PositionedObjectList<TesisEconoFight.Entities.Kursor> Cursores;
		private FlatRedBall.Sprite Sprite;
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

		public StageSelect()
			: base("StageSelect")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			if (!FlatRedBall.FlatRedBallServices.IsLoaded<FlatRedBall.Math.Geometry.ShapeCollection>(@"content/screens/stageselect/seleccionstage.shcx", ContentManagerName))
			{
			}
			SeleccionStage = FlatRedBall.FlatRedBallServices.Load<FlatRedBall.Math.Geometry.ShapeCollection>(@"content/screens/stageselect/seleccionstage.shcx", ContentManagerName);
			if (!FlatRedBall.FlatRedBallServices.IsLoaded<FlatRedBall.Scene>(@"content/screens/stageselect/scenefile.scnx", ContentManagerName))
			{
			}
			SceneFile = FlatRedBall.FlatRedBallServices.Load<FlatRedBall.Scene>(@"content/screens/stageselect/scenefile.scnx", ContentManagerName);
			if (!FlatRedBall.FlatRedBallServices.IsLoaded<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/screens/stageselect/animarseleccion.achx", ContentManagerName))
			{
			}
			AnimarSeleccion = FlatRedBall.FlatRedBallServices.Load<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/screens/stageselect/animarseleccion.achx", ContentManagerName);
			Cursores = new FlatRedBall.Math.PositionedObjectList<TesisEconoFight.Entities.Kursor>();
			Cursores.Name = "Cursores";
			Sprite = SceneFile.Sprites.FindByName("universitätfreiburg1");
			
			
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
			SeleccionStage.AddToManagers(mLayer);
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
				SeleccionStage.RemoveFromManagers(ContentManagerName != "Global");
			}
			else
			{
				SeleccionStage.RemoveFromManagers(false);
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
			if (Sprite != null)
			{
				FlatRedBall.SpriteManager.RemoveSprite(Sprite);
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
			if (Sprite != null)
			{
				FlatRedBall.SpriteManager.RemoveSpriteOneWay(Sprite);
			}
		}
		public virtual void AssignCustomVariables (bool callOnContainedElements)
		{
			if (callOnContainedElements)
			{
			}
			CurrentChainName = "Hayek";
			SpriteVisible = true;
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			SceneFile.ConvertToManuallyUpdated();
			for (int i = 0; i < Cursores.Count; i++)
			{
				Cursores[i].ConvertToManuallyUpdated();
			}
			FlatRedBall.SpriteManager.ConvertToManuallyUpdated(Sprite);
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
				case  VariableState.Hayek:
					break;
				case  VariableState.Keynes:
					break;
				case  VariableState.Malthus:
					break;
				case  VariableState.Smith:
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
				case  VariableState.Hayek:
					break;
				case  VariableState.Keynes:
					break;
				case  VariableState.Malthus:
					break;
				case  VariableState.Smith:
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
				case  VariableState.Hayek:
					if (interpolationValue < 1)
					{
						this.CurrentChainName = "Hayek";
					}
					break;
				case  VariableState.Keynes:
					if (interpolationValue < 1)
					{
						this.CurrentChainName = "Keynes";
					}
					break;
				case  VariableState.Malthus:
					if (interpolationValue < 1)
					{
						this.CurrentChainName = "Malthus";
					}
					break;
				case  VariableState.Smith:
					if (interpolationValue < 1)
					{
						this.CurrentChainName = "Smith";
					}
					break;
			}
			switch(secondState)
			{
				case  VariableState.Hayek:
					if (interpolationValue >= 1)
					{
						this.CurrentChainName = "Hayek";
					}
					break;
				case  VariableState.Keynes:
					if (interpolationValue >= 1)
					{
						this.CurrentChainName = "Keynes";
					}
					break;
				case  VariableState.Malthus:
					if (interpolationValue >= 1)
					{
						this.CurrentChainName = "Malthus";
					}
					break;
				case  VariableState.Smith:
					if (interpolationValue >= 1)
					{
						this.CurrentChainName = "Smith";
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
				case  "SeleccionStage":
					return SeleccionStage;
				case  "SceneFile":
					return SceneFile;
				case  "AnimarSeleccion":
					return AnimarSeleccion;
			}
			return null;
		}


	}
}
