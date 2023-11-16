// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.RTCAudio
{
	/// <summary>
	/// This struct is used to call <see cref="RTCAudioInterface.AddNotifyAudioBeforeRender" />.
	/// </summary>
	public struct AddNotifyAudioBeforeRenderOptions
	{
		/// <summary>
		/// The Product User ID of the user trying to request this operation.
		/// </summary>
		public ProductUserId LocalUserId { get; set; }

		/// <summary>
		/// The room this event is registered on.
		/// </summary>
		public Utf8String RoomName { get; set; }

		/// <summary>
		/// Mixed audio or unmixed audio.
		/// </summary>
		public bool UnmixedAudio { get; set; }
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyAudioBeforeRenderOptionsInternal : ISettable<AddNotifyAudioBeforeRenderOptions>, System.IDisposable
	{
		private int m_ApiVersion;
		private System.IntPtr m_LocalUserId;
		private System.IntPtr m_RoomName;
		private int m_UnmixedAudio;

		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref m_LocalUserId);
			}
		}

		public Utf8String RoomName
		{
			set
			{
				Helper.Set(value, ref m_RoomName);
			}
		}

		public bool UnmixedAudio
		{
			set
			{
				Helper.Set(value, ref m_UnmixedAudio);
			}
		}

		public void Set(ref AddNotifyAudioBeforeRenderOptions other)
		{
			m_ApiVersion = RTCAudioInterface.AddnotifyaudiobeforerenderApiLatest;
			LocalUserId = other.LocalUserId;
			RoomName = other.RoomName;
			UnmixedAudio = other.UnmixedAudio;
		}

		public void Set(ref AddNotifyAudioBeforeRenderOptions? other)
		{
			if (other.HasValue)
			{
				m_ApiVersion = RTCAudioInterface.AddnotifyaudiobeforerenderApiLatest;
				LocalUserId = other.Value.LocalUserId;
				RoomName = other.Value.RoomName;
				UnmixedAudio = other.Value.UnmixedAudio;
			}
		}

		public void Dispose()
		{
			Helper.Dispose(ref m_LocalUserId);
			Helper.Dispose(ref m_RoomName);
		}
	}
}