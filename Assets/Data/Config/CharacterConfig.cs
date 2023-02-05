using System;
using UnityEngine;

namespace AssemblyCSharp.AssetsData.Data.Config
{
	[Serializable]
	public sealed class CharacterConfig
	{
		public float baseSpeed = 1f;

		public float chopTreeSpeed = 1f;

		public float digMoundSpeed = 1f;

		public float forageBushSpeed = 1f;

		public float hitRockSpeed = 1f;

		public float plowDirtSpeed = 1f;

		[Range(1, 50)]
		public float staminaGrowth = 2; // how much stamina earned from eating 1 crop.  Max stamina is always 100.

		[Range(0.00001f, 1)]
		public float staminaDecreaseSpeed = 0.00001f; // how much stamina naturally decreases per frame.
	}
}
