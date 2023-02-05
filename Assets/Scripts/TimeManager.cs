using AssemblyCSharp.AssetsData.Data.State;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts
{
	public class TimeManager : MonoBehaviour
	{
		private AppStateManager appStateManager;
		private GameState gameState;
		public void Initialize(AppStateManager appStateManager, GameState gameState)
		{
			this.appStateManager = appStateManager;
			this.gameState = gameState;
		}

		private void Update()
		{
			if (appStateManager != null && appStateManager.CurrentState == Data.AppState.Game)
			{
				gameState.GameTime += Time.deltaTime;
			}
		}
	}
}
