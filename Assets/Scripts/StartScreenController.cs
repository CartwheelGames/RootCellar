using AssemblyCSharp.Assets.Data;
using AssemblyCSharp.Assets.Scripts;
using System.Collections;
using UnityEngine;

namespace AssemblyCSharp.AssetsData.Logic
{
	public class StartScreenController : MonoBehaviour
	{
		[SerializeField]
		private Animator animator;

		private AppStateManager appStateManager;

		[SerializeField]
		private GameObject entireScreen;

		public void Awake()
		{
			animator.Play("OnIdle");
			entireScreen.SetActive(true);
		}

		public void Initialize(AppStateManager appStateManager)
		{
			appStateManager.AddEnterListener(AppState.Title, Show);
			appStateManager.AddLeaveListener(AppState.Title, Hide);
			this.appStateManager = appStateManager;
		}

		public void OnPlayButtonPress()
		{
			animator.Play("OnStartButtonClicked", 0);
			StartCoroutine(WaitThenTurnOff());
		}

		private void Hide() => entireScreen.SetActive(false);

		private void Show() => entireScreen.SetActive(true);

		private IEnumerator WaitThenTurnOff()
		{
			yield return new WaitForSeconds(10);
			appStateManager.ChangeState(AppState.Game);
		}
	}
}
