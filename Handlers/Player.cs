using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.Events.EventArgs;
using BetterHints.Components;

namespace BetterHints.Handlers
{
	public class Player
	{
		public void OnDied(DiedEventArgs ev)
		{
			if (TrackingAndMethods.QueuedHints.ContainsKey(ev.Target.Id))
			{
				TrackingAndMethods.QueuedHints[ev.Target.Id].RemoveAll((BetterHint hint) => hint.DisableOnDeath);
			}

			if (ev.Target.ReferenceHub.TryGetComponent(out BetterHintComponent comp))
			{
				if (comp.CurrentlyShownHint != null && comp.CurrentlyShownHint.DisableOnDeath)
				{
					comp.ShowNextHint();
				}
			}
		}

		public void OnJoined(JoinedEventArgs ev)
		{
			ev.Player.GameObject.AddComponent<BetterHintComponent>();
		}

		public void OnLeft(LeftEventArgs ev)
		{
			if (TrackingAndMethods.QueuedHints.ContainsKey(ev.Player.Id)) TrackingAndMethods.QueuedHints.Remove(ev.Player.Id);
		}
	}
}
