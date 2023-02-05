using System;
using AssemblyCSharp.AssetsData.Data.Config;
using UnityEngine;
using AssemblyCSharp.AssetsData.Data.State;

public class StaminaBar : MonoBehaviour
{
    private Character _character;
    private CharacterConfig _characterConfig;
    [SerializeField] private RectTransform _fill;
    public void Initialize(CharacterConfig characterConfig, Character character)
    {
        _character = character;
        _characterConfig = characterConfig;
    }

    void Update()
    {
        if (_character != null)
        {
            // Update the UI bar
            float newFillPercentage = _character.Stamina / 100f;
            newFillPercentage = Math.Clamp(newFillPercentage, 0, 1);
            _fill.localScale = new Vector3(newFillPercentage, _fill.localScale.y, _fill.localScale.z);
            
            // Over time, Stamina naturally decreases
            _character.Stamina -= Math.Max(0, _characterConfig.staminaDecreasePerFrame);
        }
    }
}
