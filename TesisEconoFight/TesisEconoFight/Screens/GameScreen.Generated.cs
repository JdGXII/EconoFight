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
	public partial class GameScreen : FlatRedBall.Screens.Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		public enum VariableState
		{
			Uninitialized = 0, //This exists so that the first set call actually does something
			Unknown = 1, //This exists so that if the entity is actually a child entity and has set a child state, you will get this
			MundoHayek = 2, 
			MundoKeynes = 3, 
			MundoMalthus = 4, 
			MundoSmith = 5
		}
		protected int mCurrentState = 0;
		public Screens.GameScreen.VariableState CurrentState
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
					case  VariableState.MundoHayek:
						CurrentChain = "Hayek";
						break;
					case  VariableState.MundoKeynes:
						CurrentChain = "Keynes";
						break;
					case  VariableState.MundoMalthus:
						CurrentChain = "Malthus";
						break;
					case  VariableState.MundoSmith:
						CurrentChain = "Smith";
						break;
				}
			}
		}
		protected FlatRedBall.Math.Geometry.ShapeCollection LimitesMundo;
		protected FlatRedBall.Scene SceneFile;
		protected FlatRedBall.Graphics.Animation.AnimationChainList AnimacionMundo;
		
		private FlatRedBall.Math.PositionedObjectList<TesisEconoFight.Entities.Player> ListaJugadores;
		private TesisEconoFight.Entities.PauseText PauseTextInstance;
		private FlatRedBall.Sprite Fondo;
		private TesisEconoFight.Entities.P1WinText P1WinTextInstance;
		private TesisEconoFight.Entities.P2WinText P2WinTextInstance;
		private TesisEconoFight.Entities.DoubleKOText DoubleKOTextInstance;
		public string CurrentChain
		{
			get
			{
				return Fondo.CurrentChainName;
			}
			set
			{
				Fondo.CurrentChainName = value;
			}
		}

		public GameScreen()
			: base("GameScreen")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			if (!FlatRedBall.FlatRedBallServices.IsLoaded<FlatRedBall.Math.Geometry.ShapeCollection>(@"content/screens/gamescreen/limitesmundo.shcx", ContentManagerName))
			{
			}
			LimitesMundo = FlatRedBall.FlatRedBallServices.Load<FlatRedBall.Math.Geometry.ShapeCollection>(@"content/screens/gamescreen/limitesmundo.shcx", ContentManagerName);
			if (!FlatRedBall.FlatRedBallServices.IsLoaded<FlatRedBall.Scene>(@"content/screens/gamescreen/scenefile.scnx", ContentManagerName))
			{
			}
			SceneFile = FlatRedBall.FlatRedBallServices.Load<FlatRedBall.Scene>(@"content/screens/gamescreen/scenefile.scnx", ContentManagerName);
			if (!FlatRedBall.FlatRedBallServices.IsLoaded<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/screens/gamescreen/animacionmundo.achx", ContentManagerName))
			{
			}
			AnimacionMundo = FlatRedBall.FlatRedBallServices.Load<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/screens/gamescreen/animacionmundo.achx", ContentManagerName);
			ListaJugadores = new FlatRedBall.Math.PositionedObjectList<TesisEconoFight.Entities.Player>();
			ListaJugadores.Name = "ListaJugadores";
			PauseTextInstance = new TesisEconoFight.Entities.PauseText(ContentManagerName, false);
			PauseTextInstance.Name = "PauseTextInstance";
			Fondo = SceneFile.Sprites.FindByName("universitätfreiburg1");
			P1WinTextInstance = new TesisEconoFight.Entities.P1WinText(ContentManagerName, false);
			P1WinTextInstance.Name = "P1WinTextInstance";
			P2WinTextInstance = new TesisEconoFight.Entities.P2WinText(ContentManagerName, false);
			P2WinTextInstance.Name = "P2WinTextInstance";
			DoubleKOTextInstance = new TesisEconoFight.Entities.DoubleKOText(ContentManagerName, false);
			DoubleKOTextInstance.Name = "DoubleKOTextInstance";
			
			
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
			LimitesMundo.AddToManagers(mLayer);
			SceneFile.AddToManagers(mLayer);
			PauseTextInstance.AddToManagers(mLayer);
			FlatRedBall.SpriteManager.AddToLayer(Fondo, SpriteManager.UnderAllDrawnLayer);
			P1WinTextInstance.AddToManagers(mLayer);
			P2WinTextInstance.AddToManagers(mLayer);
			DoubleKOTextInstance.AddToManagers(mLayer);
			base.AddToManagers();
			AddToManagersBottomUp();
			CustomInitialize();
		}


		public override void Activity(bool firstTimeCalled)
		{
			// Generated Activity
			if (!IsPaused)
			{
				
				for (int i = ListaJugadores.Count - 1; i > -1; i--)
				{
					if (i < ListaJugadores.Count)
					{
						// We do the extra if-check because activity could destroy any number of entities
						ListaJugadores[i].Activity();
					}
				}
				PauseTextInstance.Activity();
				P1WinTextInstance.Activity();
				P2WinTextInstance.Activity();
				DoubleKOTextInstance.Activity();
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
				LimitesMundo.RemoveFromManagers(ContentManagerName != "Global");
			}
			else
			{
				LimitesMundo.RemoveFromManagers(false);
			}
			if (this.UnloadsContentManagerWhenDestroyed && ContentManagerName != "Global")
			{
				SceneFile.RemoveFromManagers(ContentManagerName != "Global");
			}
			else
			{
				SceneFile.RemoveFromManagers(false);
			}
			
			ListaJugadores.MakeOneWay();
			for (int i = ListaJugadores.Count - 1; i > -1; i--)
			{
				ListaJugadores[i].Destroy();
			}
			if (PauseTextInstance != null)
			{
				PauseTextInstance.Destroy();
				PauseTextInstance.Detach();
			}
			if (Fondo != null)
			{
				FlatRedBall.SpriteManager.RemoveSprite(Fondo);
			}
			if (P1WinTextInstance != null)
			{
				P1WinTextInstance.Destroy();
				P1WinTextInstance.Detach();
			}
			if (P2WinTextInstance != null)
			{
				P2WinTextInstance.Destroy();
				P2WinTextInstance.Detach();
			}
			if (DoubleKOTextInstance != null)
			{
				DoubleKOTextInstance.Destroy();
				DoubleKOTextInstance.Detach();
			}
			ListaJugadores.MakeTwoWay();

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
			for (int i = ListaJugadores.Count - 1; i > -1; i--)
			{
				ListaJugadores[i].Destroy();
			}
			PauseTextInstance.RemoveFromManagers();
			if (Fondo != null)
			{
				FlatRedBall.SpriteManager.RemoveSpriteOneWay(Fondo);
			}
			P1WinTextInstance.RemoveFromManagers();
			P2WinTextInstance.RemoveFromManagers();
			DoubleKOTextInstance.RemoveFromManagers();
		}
		public virtual void AssignCustomVariables (bool callOnContainedElements)
		{
			if (callOnContainedElements)
			{
				PauseTextInstance.AssignCustomVariables(true);
				P1WinTextInstance.AssignCustomVariables(true);
				P2WinTextInstance.AssignCustomVariables(true);
				DoubleKOTextInstance.AssignCustomVariables(true);
			}
			CurrentChain = "Keynes";
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			SceneFile.ConvertToManuallyUpdated();
			for (int i = 0; i < ListaJugadores.Count; i++)
			{
				ListaJugadores[i].ConvertToManuallyUpdated();
			}
			PauseTextInstance.ConvertToManuallyUpdated();
			FlatRedBall.SpriteManager.ConvertToManuallyUpdated(Fondo);
			P1WinTextInstance.ConvertToManuallyUpdated();
			P2WinTextInstance.ConvertToManuallyUpdated();
			DoubleKOTextInstance.ConvertToManuallyUpdated();
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
			TesisEconoFight.Entities.PauseText.LoadStaticContent(contentManagerName);
			TesisEconoFight.Entities.P1WinText.LoadStaticContent(contentManagerName);
			TesisEconoFight.Entities.P2WinText.LoadStaticContent(contentManagerName);
			TesisEconoFight.Entities.DoubleKOText.LoadStaticContent(contentManagerName);
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
				case  VariableState.MundoHayek:
					break;
				case  VariableState.MundoKeynes:
					break;
				case  VariableState.MundoMalthus:
					break;
				case  VariableState.MundoSmith:
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
				case  VariableState.MundoHayek:
					break;
				case  VariableState.MundoKeynes:
					break;
				case  VariableState.MundoMalthus:
					break;
				case  VariableState.MundoSmith:
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
				case  VariableState.MundoHayek:
					if (interpolationValue < 1)
					{
						this.CurrentChain = "Hayek";
					}
					break;
				case  VariableState.MundoKeynes:
					if (interpolationValue < 1)
					{
						this.CurrentChain = "Keynes";
					}
					break;
				case  VariableState.MundoMalthus:
					if (interpolationValue < 1)
					{
						this.CurrentChain = "Malthus";
					}
					break;
				case  VariableState.MundoSmith:
					if (interpolationValue < 1)
					{
						this.CurrentChain = "Smith";
					}
					break;
			}
			switch(secondState)
			{
				case  VariableState.MundoHayek:
					if (interpolationValue >= 1)
					{
						this.CurrentChain = "Hayek";
					}
					break;
				case  VariableState.MundoKeynes:
					if (interpolationValue >= 1)
					{
						this.CurrentChain = "Keynes";
					}
					break;
				case  VariableState.MundoMalthus:
					if (interpolationValue >= 1)
					{
						this.CurrentChain = "Malthus";
					}
					break;
				case  VariableState.MundoSmith:
					if (interpolationValue >= 1)
					{
						this.CurrentChain = "Smith";
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
				case  "LimitesMundo":
					return LimitesMundo;
				case  "SceneFile":
					return SceneFile;
				case  "AnimacionMundo":
					return AnimacionMundo;
			}
			return null;
		}


	}
}
