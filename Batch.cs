using System;
using System.Collections.Generic;
using System.Text;
using ServiceStack.Redis;

namespace RedisClientTest
{
	public class Batch
	{
		public long id { get; set; }
		public float amount { get; set; }
		public List<Collection> collections;
	}
}
