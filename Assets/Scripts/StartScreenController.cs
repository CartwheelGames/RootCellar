using AssemblyCSharp.Assets.Data;
using AssemblyCSharp.Assets.Scripts;
using System.Collections;
using UnityEngine;

namespace AssemblyCSharp.AssetsData.Logic
{
	public class StartScreenController : MonoBehaviour
	{
		private AppStateManager appStateManager;

		[SerializeField]
		private CanvasGroup canvasGroup;

		[SerializeField]
		private GameObject entireScreen;

		private float fadeProgress;

		[SerializeField, Range(0f, 10f)]
		private float fadeTime = 1f;

		public void Awake() => Hide();

		public void Initialize(AppStateManager appStateManager)
		{
			appStateManager.AddEnterListener(AppState.Title, Show);
			appStateManager.AddEnterListener(AppState.TitleToGame, StartTransitionToGame);
			appStateManager.AddLeaveListener(AppState.TitleToGame, Hide);
			this.appStateManager = appStateManager;
		}

		public void StartTransitionToGame() => StartCoroutine(TransitionToGame());

		private void Hide() => entireScreen.SetActive(false);

		private void Show()
		{
			entireScreen.SetActive(true);
			canvasGroup.alpha = 1;
		}

		private IEnumerator TransitionToGame()
		{
			fadeProgress = 0;
			while (fadeProgress < fadeTime)
			{
				fadeProgress += Time.deltaTime;
				canvasGroup.alpha = Mathf.InverseLerp(fadeTime, 0, fadeProgress);
				yield return null;
			}
			appStateManager.ChangeState(AppState.Game);
		}

		private void Update()
		{
			if (appStateManager.CurrentAppState == AppState.Title)
			{
				if (Input.anyKeyDown)
				{
					appStateManager.ChangeState(AppState.TitleToGame);
				}
			}
		}
	}
}
