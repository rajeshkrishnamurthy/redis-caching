using System;
using System.Collections.Generic;

namespace RedisClientTest
{
	public static class RawData
	{
		public static List<string> acked()
		{
			string[] ids =  new string[] { "t1", "t2", "t3", "t4", "t5" };
			return new List<String>(ids);
		}

		public static List<string> unacked()
		{
			string[] ids = new string[] { "t3", "t6", "t8" };
			return new List<string>(ids);
		}

		public static List<string> batchCreated()
		{
			string[] ids = new string[] {"t3"};
			return new List<string>(ids);
		}
	}
}