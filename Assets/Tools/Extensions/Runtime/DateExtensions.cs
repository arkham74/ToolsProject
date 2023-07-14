using System;
using System.Runtime.CompilerServices;

namespace JD
{
	public static class DateExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TimeSpan GetSpanSinceEpoch() => GetSpanSince(DateTime.UnixEpoch);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TimeSpan GetSpanSince(this DateTime dateTime) => DateTime.Today - dateTime;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetSecondsSinceEpoch() => GetSecondsSince(DateTime.UnixEpoch);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetSecondsSince(this DateTime dateTime) => GetSpanSince(dateTime).Seconds;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetMinutesSinceEpoch() => GetMinutesSince(DateTime.UnixEpoch);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetMinutesSince(this DateTime dateTime) => GetSpanSince(dateTime).Minutes;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetMillisecondsSinceEpoch() => GetMillisecondsSince(DateTime.UnixEpoch);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetMillisecondsSince(this DateTime dateTime) => GetSpanSince(dateTime).Milliseconds;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetHoursSinceEpoch() => GetHoursSince(DateTime.UnixEpoch);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetHoursSince(this DateTime dateTime) => GetSpanSince(dateTime).Hours;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetDaysSinceEpoch() => GetDaysSince(DateTime.UnixEpoch);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetDaysSince(this DateTime dateTime) => GetSpanSince(dateTime).Days;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double GetTotalSecondsSinceEpoch() => GetTotalSecondsSince(DateTime.UnixEpoch);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double GetTotalSecondsSince(this DateTime dateTime) => GetSpanSince(dateTime).TotalSeconds;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double GetTotalMinutesSinceEpoch() => GetTotalMinutesSince(DateTime.UnixEpoch);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double GetTotalMinutesSince(this DateTime dateTime) => GetSpanSince(dateTime).TotalMinutes;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double GetTotalMillisecondsSinceEpoch() => GetTotalMillisecondsSince(DateTime.UnixEpoch);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double GetTotalMillisecondsSince(this DateTime dateTime) => GetSpanSince(dateTime).TotalMilliseconds;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double GetTotalHoursSinceEpoch() => GetTotalHoursSince(DateTime.UnixEpoch);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double GetTotalHoursSince(this DateTime dateTime) => GetSpanSince(dateTime).TotalHours;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double GetTotalDaysSinceEpoch() => GetTotalDaysSince(DateTime.UnixEpoch);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double GetTotalDaysSince(this DateTime dateTime) => GetSpanSince(dateTime).TotalDays;
	}
}