using AssemblyCSharp.Assets.Data;

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
	}
}
