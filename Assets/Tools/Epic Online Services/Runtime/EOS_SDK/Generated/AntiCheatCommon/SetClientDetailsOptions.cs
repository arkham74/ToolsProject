// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.AntiCheatCommon
{
	public struct SetClientDetailsOptions
	{
		/// <summary>
		/// Locally unique value used in RegisterClient/RegisterPeer
		/// </summary>
		public System.IntPtr ClientHandle { get; set; }

		/// <summary>
		/// General flags associated with this client, if any
		/// </summary>
		public AntiCheatCommonClientFlags ClientFlags { get; set; }

		/// <summary>
		/// Input device being used by this client, if known
		/// </summary>
		public AntiCheatCommonClientInput ClientInputMethod { get; set; }
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	internal struct SetClientDetailsOptionsInternal : ISettable<SetClientDetailsOptions>, System.IDisposable
	{
		private int m_ApiVersion;
		private System.IntPtr m_ClientHandle;
		private AntiCheatCommonClientFlags m_ClientFlags;
		private AntiCheatCommonClientInput m_ClientInputMethod;

		public System.IntPtr ClientHandle
		{
			set
			{
				m_ClientHandle = value;
			}
		}

		public AntiCheatCommonClientFlags ClientFlags
		{
			set
			{
				m_ClientFlags = value;
			}
		}

		public AntiCheatCommonClientInput ClientInputMethod
		{
			set
			{
				m_ClientInputMethod = value;
			}
		}

		public void Set(ref SetClientDetailsOptions other)
		{
			m_ApiVersion = AntiCheatCommonInterface.SetclientdetailsApiLatest;
			ClientHandle = other.ClientHandle;
			ClientFlags = other.ClientFlags;
			ClientInputMethod = other.ClientInputMethod;
		}

		public void Set(ref SetClientDetailsOptions? other)
		{
			if (other.HasValue)
			{
				m_ApiVersion = AntiCheatCommonInterface.SetclientdetailsApiLatest;
				ClientHandle = other.Value.ClientHandle;
				ClientFlags = other.Value.ClientFlags;
				ClientInputMethod = other.Value.ClientInputMethod;
			}
		}

		public void Dispose()
		{
			Helper.Dispose(ref m_ClientHandle);
		}
	}
}