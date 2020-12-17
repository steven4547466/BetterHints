using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterHints
{
	public static class API
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
		public static void QueueHint(Player player, string message, float duration = 3f, int priority = 0, bool disableOnDeath = false, bool overrideHint = false)
		{
			player.QueueHint(message, duration, priority, disableOnDeath, overrideHint);
		}
	}
}
