using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp.AssetsData.Data.Config;
using AssemblyCSharp.AssetsData.Data.State;

public class StaminaBar : MonoBehaviour
{
    private Character _character;
    [SerializeField] private RectTransform _fill;
    public void Initialize(Character character)
    {
        _character = character;
    }

    void Update()
    {
        if (_character != null)
        {
            float newFillPercentage = _character.Stamina / 100f;
            newFillPercentage = Math.Clamp(newFillPercentage, 0, 1);
            _fill.localScale = new Vector3(newFillPercentage, _fill.localScale.y, _fill.localScale.z);
        }
    }
}
