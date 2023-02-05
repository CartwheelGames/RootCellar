using AssemblyCSharp.Assets.Data;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts.UI
{
	public sealed class LoseScreenOverlay : ScreenOverlay
	{
		public override void Initialize(AppStateManager appStateManager)
		{
			base.Initialize(appStateManager);
			appStateManager.AddEnterListener(AppState.GameToLose, FadeIn);
			appStateManager.AddEnterListener(AppState.LoseToTitle, FadeOut);
			appStateManager.AddEnterListener(AppState.Title, CleanUp);
		}

		private void Update()
		{
			if (appStateManager.CurrentState == AppState.Lose)
			{
				if (Input.anyKeyDown)
				{
					appStateManager.ChangeState(AppState.LoseToTitle);
				}
			}
		}
	}
}
