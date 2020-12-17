using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hints;

namespace BetterHints
{
	public static partial class TrackingAndMethods
	{
		/// <summary>
		/// A mapping of a list of sorted hints to a <see cref="Exiled.API.Features.Player"/>'s id
		/// </summary>
		public static Dictionary<int, List<BetterHint>> QueuedHints = new Dictionary<int, List<BetterHint>>(); // playerId -> List. -1 will be used as global
	}
}
