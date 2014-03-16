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
using FlatRedBall.Math.Geometry;
using FlatRedBall.Graphics.Animation;

namespace TesisEconoFight.Screens
{
	public partial class GlosarioSistemasJuego : Screen
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
			P3 = 4, 
			P4 = 5
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
					case  VariableState.P1:
						PantallaCurrentChainName = "Pantalla1";
						break;
					case  VariableState.P2:
						PantallaCurrentChainName = "Pantalla2";
						break;
					case  VariableState.P3:
						PantallaCurrentChainName = "Pantalla3";
						break;
					case  VariableState.P4:
						PantallaCurrentChainName = "Pantalla4";
						break;
				}
			}
		}
		private FlatRedBall.Math.Geometry.ShapeCollection Opciones;
		private FlatRedBall.Scene SceneFile;
		private FlatRedBall.Graphics.Animation.AnimationChainList AnimacionPantalla;
		
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

		public GlosarioSistemasJuego()
			: base("GlosarioSistemasJuego")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			if (!FlatRedBallServices.IsLoaded<FlatRedBall.Math.Geometry.ShapeCollection>(@"content/screens/glosariosistemasjuego/opciones.shcx", ContentManagerName))
			{
			}
			Opciones = FlatRedBallServices.Load<FlatRedBall.Math.Geometry.ShapeCollection>(@"content/screens/glosariosistemasjuego/opciones.shcx", ContentManagerName);
			if (!FlatRedBallServices.IsLoaded<FlatRedBall.Scene>(@"content/screens/glosariosistemasjuego/scenefile.scnx", ContentManagerName))
			{
			}
			SceneFile = FlatRedBallServices.Load<FlatRedBall.Scene>(@"content/screens/glosariosistemasjuego/scenefile.scnx", ContentManagerName);
			if (!FlatRedBallServices.IsLoaded<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/screens/glosariosistemasjuego/animacionpantalla.achx", ContentManagerName))
			{
			}
			AnimacionPantalla = FlatRedBallServices.Load<FlatRedBall.Graphics.Animation.AnimationChainList>(@"content/screens/glosariosistemasjuego/animacionpantalla.achx", ContentManagerName);
			KursorInstance = new TesisEconoFight.Entities.Kursor(ContentManagerName, false);
			KursorInstance.Name = "KursorInstance";
			Pantalla = SceneFile.Sprites.FindByName("glosario sistema de juegos 11");
			
			
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
				Pantalla.Detach(); SpriteManager.RemoveSprite(Pantalla);
			}

			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			PantallaCurrentChainName = "Pantalla1";
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public virtual void AddToManagersBottomUp ()
		{
			Opciones.AddToManagers(mLayer);
			SceneFile.AddToManagers(mLayer);
			KursorInstance.AddToManagers(mLayer);
			PantallaCurrentChainName = "Pantalla1";
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			SceneFile.ConvertToManuallyUpdated();
			KursorInstance.ConvertToManuallyUpdated();
			SpriteManager.ConvertToManuallyUpdated(Pantalla);
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
		public Instruction InterpolateToState (VariableState stateToInterpolateTo, double secondsToTake)
		{
			switch(stateToInterpolateTo)
			{
				case  VariableState.P1:
					break;
				case  VariableState.P2:
					break;
				case  VariableState.P3:
					break;
				case  VariableState.P4:
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
				case  VariableState.P1:
					break;
				case  VariableState.P2:
					break;
				case  VariableState.P3:
					break;
				case  VariableState.P4:
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
				case  VariableState.P4:
					if (interpolationValue < 1)
					{
						this.PantallaCurrentChainName = "Pantalla4";
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
				case  VariableState.P4:
					if (interpolationValue >= 1)
					{
						this.PantallaCurrentChainName = "Pantalla4";
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
				case  "Opciones":
					return Opciones;
				case  "SceneFile":
					return SceneFile;
				case  "AnimacionPantalla":
					return AnimacionPantalla;
			}
			return null;
		}


	}
}
