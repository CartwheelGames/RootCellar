using AssemblyCSharp.Assets.Data;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts.UI
{
	public sealed class WinScreenOverlay : ScreenOverlay
	{
		public override void Initialize(AppStateManager appStateManager)
		{
			base.Initialize(appStateManager);
			appStateManager.AddEnterListener(AppState.GameToWin, FadeIn);
			appStateManager.AddEnterListener(AppState.WinToTitle, FadeOut);
			appStateManager.AddEnterListener(AppState.Title, CleanUp);
		}

		private void Update()
		{
			if (appStateManager.CurrentState == AppState.Win)
			{
				if (Input.anyKeyDown)
				{
					appStateManager.ChangeState(AppState.WinToTitle);
				}
			}
		}
	}
}
