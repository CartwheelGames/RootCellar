using AssemblyCSharp.Assets.Data;

namespace AssemblyCSharp.Assets.Scripts.UI
{
	public sealed class GameScreenOverlay : ScreenOverlay
	{
		public override void Initialize(AppStateManager appStateManager)
		{
			base.Initialize(appStateManager);
			appStateManager.AddEnterListener(AppState.Game, FadeIn);
			appStateManager.AddLeaveListener(AppState.Game, FadeOut);
		}
	}
}
