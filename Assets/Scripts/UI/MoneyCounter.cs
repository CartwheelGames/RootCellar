using AssemblyCSharp.AssetsData.Data.Config;
using AssemblyCSharp.AssetsData.Data.State;
using UnityEngine;
using UnityEngine.UI;

namespace AssemblyCSharp.Assets.Scripts.UI
{
	public class MoneyCounter : MonoBehaviour
	{
		private CharacterData _character;

		private CharacterConfig _characterConfig;

		private int _currentMoneyCount = 0;

		[SerializeField] private TextFlyUpController _textFlyUpController;

		[SerializeField] private Text _txt;

		public void Initialize(CharacterConfig characterConfig, CharacterData character)
		{
			_character = character;
			_characterConfig = characterConfig;
		}

		private void Update()
		{
			if (_character != null)
			{
				if (_character.Money != _currentMoneyCount)
				{
					UpdateMoneyCounter(_character.Money, _character.Money - _currentMoneyCount);
					_currentMoneyCount = _character.Money;
				}
			}
		}

		private void UpdateMoneyCounter(int newMoneyAmount, int moneyAmountDifference)
		{
			_txt.text = "$" + newMoneyAmount;
			_textFlyUpController.SetTextAndFlyUp((moneyAmountDifference > 0 ? "+" : "-") + "$" + moneyAmountDifference);
		}
	}
}
