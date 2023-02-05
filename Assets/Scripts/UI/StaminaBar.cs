using AssemblyCSharp.Assets.Data;
using AssemblyCSharp.Assets.Scripts;
using AssemblyCSharp.AssetsData.Data.Config;
using AssemblyCSharp.AssetsData.Data.State;
using System;
using UnityEngine;

public class StaminaBar : MonoBehaviour
{
	private Character _character;

	private CharacterConfig _characterConfig;

	[SerializeField]
	private RectTransform _fill;

	private AppStateManager appStateManager;

	public void Initialize(
		AppStateManager appStateManager,
		CharacterConfig characterConfig,
		Character character)
	{
		this.appStateManager = appStateManager;
		_character = character;
		_characterConfig = characterConfig;
	}

	private void Update()
	{
		if (_character != null)
		{
			// Update the UI bar
			float newFillPercentage = _character.Stamina / 100f;
			newFillPercentage = Math.Clamp(newFillPercentage, 0, 1);
			_fill.localScale = new Vector3(newFillPercentage, _fill.localScale.y, _fill.localScale.z);

			// Over time, Stamina naturally decreases
			_character.Stamina -= Math.Max(0, _characterConfig.staminaDecreasePerFrame);
			if (_character.Stamina <= 0)
			{
				appStateManager.ChangeState(AppState.GameToLose);
			}
		}
	}
}
