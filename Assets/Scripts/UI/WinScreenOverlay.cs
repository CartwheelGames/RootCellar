using AssemblyCSharp.Assets.Data;

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
	}
}
