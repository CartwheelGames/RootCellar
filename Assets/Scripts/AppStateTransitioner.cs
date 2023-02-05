using AssemblyCSharp.Assets.Data;
using System.Collections;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts
{
	public class AppStateTransitioner : MonoBehaviour
	{
		private AppStateManager appStateManager;

		[SerializeField]
		private float endToTitleTime = 1f;

		[SerializeField]
		private float gameToEndTime = 1f;

		private float timeProgress;

		[SerializeField]
		private float titleToGameTime = 1f;

		public void Initialize(AppStateManager appStateManager)
		{
			this.appStateManager = appStateManager;
			appStateManager.AddEnterListener(AppState.TitleToGame, TransitionToGame);
			appStateManager.AddEnterListener(AppState.GameToLose, TransitionToLose);
			appStateManager.AddEnterListener(AppState.GameToWin, TransitionToWin);
			appStateManager.AddEnterListener(AppState.WinToTitle, TransitionToTitle);
			appStateManager.AddEnterListener(AppState.LoseToTitle, TransitionToTitle);
		}

		private void BeginTransition(AppState appState, float time)
		{
			StopAllCoroutines();
			StartCoroutine(Transition(appState, time));
		}

		private IEnumerator Transition(AppState appState, float time)
		{
			timeProgress = 0f;
			while (timeProgress < time)
			{
				yield return null;
				timeProgress += Time.deltaTime;
			}
			appStateManager.ChangeState(appState);
		}

		private void TransitionToGame() => BeginTransition(AppState.Game, titleToGameTime);

		private void TransitionToLose() => BeginTransition(AppState.Lose, gameToEndTime);

		private void TransitionToTitle() => BeginTransition(AppState.Win, endToTitleTime);

		private void TransitionToWin() => BeginTransition(AppState.Win, gameToEndTime);
	}
}
