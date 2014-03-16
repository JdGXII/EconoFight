using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.Math.Geometry;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Input;
using FlatRedBall.IO;
using FlatRedBall.Instructions;
using FlatRedBall.Math.Splines;
using FlatRedBall.Utilities;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

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
using Microsoft.Xna.Framework.Media;
#endif

// Generated Usings
using TesisEconoFight.Entities;
using FlatRedBall;
using FlatRedBall.Screens;
using FlatRedBall.Math;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Graphics.Animation;

namespace TesisEconoFight.Screens
{
	public partial class StageSelect : Screen
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
		private FlatRedBall.Math.Geometry.ShapeCollection SeleccionStage;
		private FlatRedBall.Scene SceneFile;
		private FlatRedBall.Graphics.Animation.AnimationChainList AnimarSeleccion;
		
		private PositionedObjectList<Kursor> Cursores;
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
			if (!FlatRedBallServices.IsLoaded<FlatRedBall.Math.Geometry.ShapeCollection>(@"content/screens/stageselect/seleccionstage.shcx", ContentManagerName))
			{
			}
			SeleccionStage = FlatRedBallServices.Load<FlatRedBall.Math.Geometry.ShapeCollection>(@"content/screens/stageselect/seleccionstage.shcx", ContentManagerName);
			if (!FlatRedBallServices.IsLoaded<FlatRedBall.Scene>(@"content/screens/stageselect/scenefile.scnx", ContentManagerName))
			{
			}
			SceneFile = FlatRedBallServices.Load<FlatRedBall.Scene>(@"content/screens/stageselect/scenefile.scnx", ContentManagerName);
			if (!FlatRedBallServices.IsLoaded<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/screens/stageselect/animarseleccion.achx", ContentManagerName))
			{
			}
			AnimarSeleccion = FlatRedBallServices.Load<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/screens/stageselect/animarseleccion.achx", ContentManagerName);
			Cursores = new PositionedObjectList<Kursor>();
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
			
			for (int i = Cursores.Count - 1; i > -1; i--)
			{
				Cursores[i].Destroy();
			}
			if (Sprite != null)
			{
				Sprite.Detach(); SpriteManager.RemoveSprite(Sprite);
			}

			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			CurrentChainName = "Hayek";
			SpriteVisible = true;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public virtual void AddToManagersBottomUp ()
		{
			SeleccionStage.AddToManagers(mLayer);
			SceneFile.AddToManagers(mLayer);
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
			SpriteManager.ConvertToManuallyUpdated(Sprite);
		}
		public static void LoadStaticContent (string contentManagerName)
		{
			if (string.IsNullOrEmpty(contentManagerName))
			{
				throw new ArgumentException("contentManagerName cannot be empty or null");
			}
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
		public Instruction InterpolateToState (VariableState stateToInterpolateTo, double secondsToTake)
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
			var instruction = new DelegateInstruction<VariableState>(StopStateInterpolation, stateToInterpolateTo);
			instruction.TimeToExecute = TimeManager.CurrentTime + secondsToTake;
			InstructionManager.Add(instruction);
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
				throw new Exception("interpolationValue cannot be NaN");
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
		}
		public override void MoveToState (int state)
		{
			this.CurrentState = (VariableState)state;
		}
		
		/// <summary>Sets the current state, and pushes that state onto the back stack.</summary>
		public void PushState (VariableState state)
		{
			this.CurrentState = state;
			
			ScreenManager.PushStateToStack((int)this.CurrentState);
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
