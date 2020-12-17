using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.Events.EventArgs;

namespace BetterHints.Handlers
{
	public class Server
	{
		public void OnRestartingRound()
		{
			TrackingAndMethods.QueuedHints.Clear();
		}
	}
}
