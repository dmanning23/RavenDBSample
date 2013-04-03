using System;
using System.Text;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using Raven.Client.Connection;
using System.Collections.Specialized;

namespace ConsoleApplication1
{
	/// <summary>
	/// This is a quick program just to get familiar with ravendb
	/// </summary>
	class Program
	{
		[Serializable]
		class DbResponse
		{
			public string Key { get; set; }
			public string ETag { get; set; }
		}

		static void Main(string[] args)
		{
			//create the document we want to store

			//make up a request
			var myRequest = new Request
			{
				Url = "http:\\google.com",
				Ip = "127.0.0.1",
				RequestType = "put",
				PostData = "test",
				UserKey = "Dude",
				Start = DateTime.MinValue,
				End = DateTime.MaxValue,
				Actions = new List<Action>()
			};

			//add an action
			var myAction = new Action()
			{
				Type = "action type1",
				Name = "action name1",
				Start = DateTime.MinValue,
				End = DateTime.Now,
				Actions = new List<Action>()
			};

			myRequest.Actions.Add(myAction);

			//add some sub-actions
			for (int i = 0; i < 3; i++)
			{
				myAction.Actions.Add(new Action()
				{
					Type = "action type" + i.ToString(),
					Name = "action name" + i.ToString(),
					Start = DateTime.MinValue,
					End = DateTime.Now,
					Actions = new List<Action>()
				});
			}

			//serialize that thing to json
			String webOut = JsonConvert.SerializeObject(myRequest);
			using (WebClient restQuery = new WebClient())
			{
				var jsonResponse = restQuery.UploadString("http://localhost:8080/databases/stuff/docs", webOut);
				var myDbResponse = JsonConvert.DeserializeObject<DbResponse>(jsonResponse);
			}
		}
	}
}
