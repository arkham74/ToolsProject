using System;
using UnityEngine;

namespace JD
{
	[Serializable]
	public struct SerializableDateTime
	{
		[SerializeField] private int year;
		[SerializeField] private int month;
		[SerializeField] private int day;
		[SerializeField] private int hour;
		[SerializeField] private int minute;
		[SerializeField] private int second;

		public TimeSpan GetFromDate(DateTime date2)
		{
			return this - date2;
		}

		public TimeSpan GetFromNow()
		{
			return GetFromDate(DateTime.Now);
		}

		public TimeSpan GetFromToday()
		{
			return GetFromDate(DateTime.Today);
		}

		public TimeSpan GetFromUtcNow()
		{
			return GetFromDate(DateTime.UtcNow);
		}

		public static implicit operator DateTime(SerializableDateTime date) => new DateTime(date.year, date.month, date.day, date.hour, date.minute, date.second);
	}
}