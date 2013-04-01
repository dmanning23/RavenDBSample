using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client.Embedded;

namespace ConsoleApplication1
{
	/// <summary>
	/// This is a quick program just to get familiar with ravendb
	/// </summary>
	class Program
	{
		static void Main(string[] args)
		{
			//connect to the datastore
			var documentStore = new EmbeddableDocumentStore { DataDirectory = @"C:\junk\test" };
			documentStore.Initialize();

			Guid companyId;
			using (var session = documentStore.OpenSession())
			{
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

				//let's give him to mongoooo... i mean ravvveeennn
				session.Store(myRequest);
				session.SaveChanges();
				companyId = myRequest.Id;
			}


		}
	}
}
