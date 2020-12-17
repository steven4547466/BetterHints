using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Exiled.API.Features;

namespace BetterHints.Components
{
	class BetterHintComponent : MonoBehaviour
	{
		Player player;

		public BetterHint CurrentlyShownHint;
		public float StartedShowingAt;
		public float RemainingDuration { 
			get 
			{
				if (CurrentlyShownHint == null) return 0;
				return (StartedShowingAt + CurrentlyShownHint.Hint.DurationScalar) - Time.time;
			}
		}

		public void Awake()
		{
			player = Player.Get(gameObject);
			if (player == null) DestroyImmediate(this);
		}

		public void Update()
		{
			if (!TrackingAndMethods.QueuedHints.ContainsKey(player.Id)) return;
			if (CurrentlyShownHint != null && TrackingAndMethods.QueuedHints[player.Id].Count == 0 && RemainingDuration <= 0)
			{
				ClearHint();
			}
			else if (TrackingAndMethods.QueuedHints[player.Id].Count > 0 && (RemainingDuration <= 0 ||
				(CurrentlyShownHint != null && 
				TrackingAndMethods.QueuedHints[player.Id][0].OverrideHint && CurrentlyShownHint.Priority <= TrackingAndMethods.QueuedHints[player.Id][0].Priority)))
			{
				ShowNextHint();
			}
		}

		public void ClearHint()
		{
			player.SendHint("", 0.1f);
			CurrentlyShownHint = null;
		}

		public void ShowNextHint()
		{
			if (!TrackingAndMethods.QueuedHints.ContainsKey(player.Id)) return;
			if (TrackingAndMethods.QueuedHints[player.Id].Count == 0)
			{
				ClearHint();
				return;
			}
			CurrentlyShownHint = TrackingAndMethods.QueuedHints[player.Id][0];
			StartedShowingAt = Time.time;
			TrackingAndMethods.QueuedHints[player.Id].RemoveAt(0);
			player.SendHint(CurrentlyShownHint);
		}
		
	}
}
