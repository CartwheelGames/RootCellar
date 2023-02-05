using AssemblyCSharp.Assets.Data;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts.UI
{
	public sealed class TitleScreenOverlay : ScreenOverlay
	{
		public override void Initialize(AppStateManager appStateManager)
		{
			base.Initialize(appStateManager);
			appStateManager.AddEnterListener(AppState.Title, Show);
			appStateManager.AddEnterListener(AppState.TitleToGame, FadeOut);
			appStateManager.AddEnterListener(AppState.LoseToTitle, FadeIn);
			appStateManager.AddEnterListener(AppState.WinToTitle, FadeIn);
			appStateManager.AddLeaveListener(AppState.TitleToGame, Hide);
			appStateManager.AddLeaveListener(AppState.TitleToGame, CleanUp);
			this.appStateManager = appStateManager;
		}


		private void Update()
		{
			if (appStateManager.CurrentState == AppState.Title)
			{
				if (Input.anyKeyDown)
				{
					appStateManager.ChangeState(AppState.TitleToGame);
				}
			}
		}
	}
}
