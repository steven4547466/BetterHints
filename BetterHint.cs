using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hints;
using UnityEngine;

namespace BetterHints
{
	/// <summary>
	/// Represents a <see cref="Hints.Hint"/> with additional properties.
	/// </summary>
	public class BetterHint
	{
		/// <summary>
		///  Gets the base hint.
		/// </summary>
		public Hint Hint { get; private set; }

		/// <summary>
		/// Gets the priority of this hint.
		/// </summary>
		public int Priority { get; private set; }

		/// <summary>
		/// Whether or not this hint will override the currently shown hint.
		/// Only works if the priority is higher or equal to the currently shown hint.
		/// </summary>
		public bool OverrideHint { get; private set; }

		/// <summary>
		/// Whether or not this hint will be removed from the queue or cleared on death.
		/// </summary>
		public bool DisableOnDeath { get; private set; }

		/// <summary>
		/// Gets the time this hint was queued at.
		/// </summary>
		public float QueuedAt { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="BetterHint"/> class.
		/// </summary>
		/// <param name="hint">The base hint.</param>
		/// <param name="priority">The priority of the hint.</param>
		/// <param name="disableOnDeath">Whether or not this hint will be removed from the queue or cleared on death.</param>
		/// <param name="overrideHint">Whether or not the hint should override the currently shown hint.</param>
		public BetterHint(Hint hint, int priority, bool disableOnDeath = false, bool overrideHint = false)
		{
			Hint = hint;
			Priority = priority;
			OverrideHint = overrideHint;
			DisableOnDeath = disableOnDeath;
			QueuedAt = Time.time;
		}
	}
}
