using System;
using System.Collections.Generic;
using ServiceStack.Redis;

namespace RedisClientTest
{
	public class FlexCache
	{
		public void AddItemsToSet(string set_name, List<string> items)
		{
			using (IRedisClient client = new RedisClient())
			{
				client.AddRangeToSet(set_name, items);
			}
		}

		public HashSet<String> GetIntersectWithSet(string set_name, List<String> items)
		{
			HashSet<String> intersects;
			String temp_set = "some-temp-set"; //random guid can be generated here
			using (IRedisClient client = new RedisClient())
			{
				client.AddRangeToSet(temp_set, items);
				intersects = client.GetIntersectFromSets(set_name, temp_set);
				foreach (string item in items)
				{
					client.RemoveItemFromSet(temp_set, item);
				}
			}
			return intersects;
		}

		public void RemoveItemsFromSet(string set_name, List<String> items)
		{
			using (IRedisClient client = new RedisClient())
			{
				foreach (var item in items)
				{
					client.RemoveItemFromSet(set_name, item);
				}
			}
		}

		public void RemoveItemFromSet(string set_name, string item)
		{
			using (IRedisClient client = new RedisClient())
			{
				client.RemoveItemFromSet(set_name, item);
			}
		}
	}


}