using System;
using System.Collections;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts.UI
{
	public abstract class ScreenOverlay : MonoBehaviour
	{
		protected AppStateManager appStateManager;

		[SerializeField]
		private CanvasGroup canvasGroup;

		private float fadeProgress;

		[SerializeField, Range(0f, 10f)]
		private float fadeTime = 1f;

		protected event Action OnFaded;

		public void Awake() => Hide();

		public virtual void Initialize(AppStateManager appStateManager) =>
			this.appStateManager = appStateManager;

		protected void CleanUp()
		{
			OnFaded = null;
			fadeProgress = 0;
			StopAllCoroutines();
		}

		protected void FadeIn() => StartCoroutine(Fade(true));

		protected void FadeOut() => StartCoroutine(Fade(false));

		protected void Hide()
		{
			canvasGroup.alpha = 0f;
		}

		protected void Show()
		{
			canvasGroup.alpha = 1f;
		}

		private IEnumerator Fade(bool isFadingIn)
		{
			canvasGroup.alpha = isFadingIn ? 0f : 1f;
			fadeProgress = 0f;
			while (fadeProgress < fadeTime)
			{
				fadeProgress += Time.deltaTime;
				canvasGroup.alpha = isFadingIn
					? Mathf.InverseLerp(0, fadeTime, fadeProgress)
					: Mathf.InverseLerp(fadeTime, 0, fadeProgress);
				yield return null;
			}
			canvasGroup.alpha = isFadingIn ? 1f : 0f;
			OnFaded?.Invoke();
		}
	}
}
