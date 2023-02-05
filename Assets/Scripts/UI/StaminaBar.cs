using AssemblyCSharp.Assets.Data;
using AssemblyCSharp.AssetsData.Data.Config;
using AssemblyCSharp.AssetsData.Data.State;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts.UI
{
	public class StaminaBar : MonoBehaviour
	{
		[SerializeField]
		private RectTransform _fill;

		private AppStateManager appStateManager;

		private CharacterData character;

		private CharacterConfig characterConfig;

		public void Initialize(
			AppStateManager appStateManager,
			CharacterConfig characterConfig,
			CharacterData character)
		{
			this.appStateManager = appStateManager;
			this.character = character;
			this.characterConfig = characterConfig;
		}

		private void Update()
		{
			if (character != null && appStateManager.CurrentState == AppState.Game)
			{
				// Update the UI bar
				float newFillPercentage = character.Stamina / 100f;
				newFillPercentage = Mathf.Clamp01(newFillPercentage);
				_fill.localScale = new Vector3(newFillPercentage, _fill.localScale.y, _fill.localScale.z);

				// Over time, Stamina naturally decreases
				character.Stamina -= Time.deltaTime * characterConfig.staminaDecreaseSpeed;
				if (character.Stamina <= 0)
				{
					appStateManager.ChangeState(AppState.GameToLose);
				}
			}
		}
	}
}
