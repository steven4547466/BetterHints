using Hints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Mirror;

namespace BetterHints
{
	public static partial class TrackingAndMethods
	{
		/// <summary>
		/// Queues a hint to be sent to the <see cref="Player"/>.
		/// </summary>
		/// <param name="player">The <see cref="Player"/> to queue the hint for.</param>
		/// <param name="message">The message to show.</param>
		/// <param name="duration">The duration of the hint.</param>
		/// <param name="priority">The priority of the hint.</param>
		/// <param name="disableOnDeath">Whether or not this hint will be removed from the queue or cleared on death.</param>
		/// <param name="overrideHint">Whether or not the hint should override the currently shown hint.</param>
		public static void QueueHint(this Player player, string message, float duration = 3f, int priority = 0, bool disableOnDeath = false, bool overrideHint = false)
		{
			try
			{
				HintParameter[] parameters = new HintParameter[]
				{
					new StringHintParameter(message),
				};

				if (!QueuedHints.ContainsKey(player.Id)) QueuedHints.Add(player.Id, new List<BetterHint>());

				QueuedHints[player.Id].AddSorted(new BetterHint(new TextHint(message, parameters, null, duration), priority, disableOnDeath, overrideHint));
			}catch(Exception e)
			{
				Log.Error(e);
			}
		}

		/// <summary>
		/// Sends a hint to the <see cref="Player"/>.
		/// </summary>
		/// <param name="player">The <see cref="Player"/> to send a hint to.</param>
		/// <param name="hint">The <see cref="BetterHint"/> to send to the player.</param>
		public static void SendHint(this Player player, BetterHint hint)
		{
			NetworkServer.SendToClientOfPlayer<HintMessage>(player.HintDisplay.netIdentity, new HintMessage(hint.Hint));
		}

		/// <summary>
		/// Sends a hint to the <see cref="Player"/>.
		/// </summary>
		/// <param name="player">The <see cref="Player"/> to send a hint to.</param>
		/// <param name="message">The message to show.</param>
		/// <param name="duration">The duration of the hint.</param>
		public static void SendHint(this Player player, string message, float duration = 3f)
		{
			HintParameter[] parameters = new HintParameter[]
				{
					new StringHintParameter(message),
				};
			NetworkServer.SendToClientOfPlayer<HintMessage>(player.HintDisplay.netIdentity, new HintMessage(new TextHint(message, parameters, null, duration)));
		}

		//public static void SortHints(this Player player)
		//{
		//	if (!QueuedHints.ContainsKey(player.Id)) return;
		//	QueuedHints[player.Id].Sort((h1, h2) => (h2.Priority + h2.QueuedAt).CompareTo(h1.Priority + h1.QueuedAt));
		//}

		/// <summary>
		/// Adds a hint into the proper place in the list provided.
		/// </summary>
		/// <param name="hints">The hints list.</param>
		/// <param name="hint">The hint to add.</param>
		public static void AddSorted(this List<BetterHint> hints, BetterHint hint)
		{
			if (hints.Count == 0) hints.Add(hint);
			else if (hint.Priority == int.MaxValue) hints.Insert(0, hint);
			else if (hint.Priority == int.MinValue) hints.Add(hint);
			else
			{
				if (hints.Count == 1)
				{
					if (hints[0].Priority >= hint.Priority)
					{
						hints.Add(hint);
					}
					else
					{
						hints.Insert(0, hint);
					}
				}
				else
				{
					for (int i = 0; i < hints.Count; i++)
					{
						if (hints[i].Priority < hint.Priority)
						{
							hints.Insert(i, hint);
							return;
						}
					}
					hints.Add(hint);
				}
			}
		}
	}
}
