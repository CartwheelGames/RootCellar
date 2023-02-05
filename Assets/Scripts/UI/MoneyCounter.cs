using System;
using AssemblyCSharp.AssetsData.Data.Config;
using UnityEngine;
using AssemblyCSharp.AssetsData.Data.State;
using UnityEngine.UI;

public class MoneyCounter : MonoBehaviour
{
    private Character _character;
    private CharacterConfig _characterConfig;
    private int _currentMoneyCount = 0;
    [SerializeField] private Text _txt;
    [SerializeField] private TextFlyUpController _textFlyUpController;
    
    public void Initialize(CharacterConfig characterConfig, Character character)
    {
        _character = character;
        _characterConfig = characterConfig;
    }

    void Update()
    {
        if (_character != null)
        {
            if (_character.Money != _currentMoneyCount)
            {
                UpdateMoneyCounter(_character.Money,  _character.Money - _currentMoneyCount);
                _currentMoneyCount = _character.Money;
            }
        }
    }

    void UpdateMoneyCounter(int newMoneyAmount, int moneyAmountDifference)
    {
        _txt.text = "$" + newMoneyAmount;
        _textFlyUpController.SetTextAndFlyUp( (moneyAmountDifference > 0 ? "+" : "-") + "$" + moneyAmountDifference);
    }
}
