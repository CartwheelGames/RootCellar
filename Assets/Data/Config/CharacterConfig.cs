using System;
using UnityEngine;

namespace AssemblyCSharp.AssetsData.Data.Config
{
	[Serializable]
	public sealed class CharacterConfig
	{
		public float baseSpeed;
		
		[Range(1, 50)] public float staminaGrowth = 2; // how much stamina earned from eating 1 crop.  Max stamina is always 100.

		[Range(0.00001f, 1)] public float staminaDecreasePerFrame = 0.00001f; // how much stamina naturally decreases per frame.
	}
}
