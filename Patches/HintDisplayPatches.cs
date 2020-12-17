using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Hints;
using Mirror;
using Exiled.API.Features;

namespace BetterHints.Patches
{
	[HarmonyPatch(typeof(HintDisplay), nameof(HintDisplay.Show))]
	class HintDisplayShowHintPatch
	{
		public static bool Prefix(HintDisplay __instance, Hint hint)
		{
			if (hint == null)
			{
				throw new ArgumentNullException("hint");
			}
			if (__instance.isLocalPlayer)
			{
				throw new InvalidOperationException("Cannot display a hint to the local player (headless server).");
			}
			if (NetworkServer.active)
			{
				Player player = Player.Get(__instance.gameObject);
				if (player == null) return false;

				if (hint is TextHint textHint)
				{
					player.QueueHint(textHint.Text, hint.DurationScalar, int.MaxValue, false, true);
					return false;
				}
				NetworkServer.SendToClientOfPlayer<HintMessage>(__instance.netIdentity, new HintMessage(hint));
				return false;
			}
			return false;
		}
	}
}
