using System;
using System.Collections.Generic;
using System.Text;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;

namespace RedisClientTest
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			AddAckedIdsToCache();  // Everytime collections are acked, add the id's into set.

			List<String> unackedIdsFromDB = SimulateQuery(); // Simulate query against sql server and get unacked IDs

			HashSet<string> alreadyAckedIds = CheckAlreadyAckedIdsWith(unackedIdsFromDB); // Check which of these unacked id's are actually already acked. 

			Console.WriteLine("Non-Intersecting IDs");
			foreach (var id in unackedIdsFromDB)
			{
				if (!alreadyAckedIds.Contains(id))
				{
					Console.WriteLine(id); //This should go to display
				}
			}

			RemoveAckedIdsFromCache(); // Remove acked IDs once handler has processed create batch
		}

		private static void AddAckedIdsToCache()
		{
			List<string> ids = RawData.acked();
			FlexCache cache = new FlexCache();
			cache.AddItemsToSet("ackedCollectionSet", ids);
		}

		static List<String> SimulateQuery()
		{
			List<string> ids = RawData.unacked();
			return ids; 
		}

		private static HashSet<String> CheckAlreadyAckedIdsWith(List<String> ids)
		{
			FlexCache cache = new FlexCache();
			HashSet<String> ackedIds = cache.GetIntersectWithSet("ackedCollectionSet", ids);
			return ackedIds;
		}

		private static void RemoveAckedIdsFromCache()
		{
			List<string> ids = RawData.batchCreated();
			FlexCache cache = new FlexCache();
			cache.RemoveItemsFromSet("ackedCollectionSet", ids);
		}
	}
}