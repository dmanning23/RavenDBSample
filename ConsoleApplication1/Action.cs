using System;
using System.Collections.Generic;

namespace ConsoleApplication1
{
	[Serializable]
	public class Action
	{
		public IList<Action> Actions { get; set; }
		public string Type { get; set; }
		public string Name { get; set; }
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
	}
}
