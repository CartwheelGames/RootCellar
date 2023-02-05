using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts.UI
{
	public sealed class OverlayManager : MonoBehaviour
	{
		[SerializeField]
		private GameScreenOverlay gameScreenOverlay;

		[SerializeField]
		private LoseScreenOverlay loseScreenOverlay;

		[SerializeField]
		private TitleScreenOverlay titleScreenOverlay;

		[SerializeField]
		private WinScreenOverlay winScreenOverlay;

		public void Initialize(AppStateManager appStateManager)
		{
			winScreenOverlay.Initialize(appStateManager);
			loseScreenOverlay.Initialize(appStateManager);
			gameScreenOverlay.Initialize(appStateManager);
			titleScreenOverlay.Initialize(appStateManager);
		}
	}
}
