// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.Sessions
{
	/// <summary>
	/// Internal details about a session, found on both active sessions and within search results
	/// </summary>
	public struct SessionDetailsInfo
	{
		/// <summary>
		/// Session ID assigned by the backend service
		/// </summary>
		public Utf8String SessionId { get; set; }

		/// <summary>
		/// IP address of this session as visible by the backend service
		/// </summary>
		public Utf8String HostAddress { get; set; }

		/// <summary>
		/// Number of remaining open spaces on the session (NumPublicConnections - RegisteredPlayers
		/// </summary>
		public uint NumOpenPublicConnections { get; set; }

		/// <summary>
		/// Reference to the additional settings associated with this session
		/// </summary>
		public SessionDetailsSettings? Settings { get; set; }

		/// <summary>
		/// The Product User ID of the session owner. Null if the session is not owned by a user.
		/// </summary>
		public ProductUserId OwnerUserId { get; set; }

		/// <summary>
		/// The client id of the session owner. Null if the session is not owned by a server. The session is owned by a server if <see cref="Platform.Options.IsServer" /> is <see langword="true" />.
		/// </summary>
		public Utf8String OwnerServerClientId { get; set; }

		internal void Set(ref SessionDetailsInfoInternal other)
		{
			SessionId = other.SessionId;
			HostAddress = other.HostAddress;
			NumOpenPublicConnections = other.NumOpenPublicConnections;
			Settings = other.Settings;
			OwnerUserId = other.OwnerUserId;
			OwnerServerClientId = other.OwnerServerClientId;
		}
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	internal struct SessionDetailsInfoInternal : IGettable<SessionDetailsInfo>, ISettable<SessionDetailsInfo>, System.IDisposable
	{
		private int m_ApiVersion;
		private System.IntPtr m_SessionId;
		private System.IntPtr m_HostAddress;
		private uint m_NumOpenPublicConnections;
		private System.IntPtr m_Settings;
		private System.IntPtr m_OwnerUserId;
		private System.IntPtr m_OwnerServerClientId;

		public Utf8String SessionId
		{
			get
			{
				Utf8String value;
				Helper.Get(m_SessionId, out value);
				return value;
			}

			set
			{
				Helper.Set(value, ref m_SessionId);
			}
		}

		public Utf8String HostAddress
		{
			get
			{
				Utf8String value;
				Helper.Get(m_HostAddress, out value);
				return value;
			}

			set
			{
				Helper.Set(value, ref m_HostAddress);
			}
		}

		public uint NumOpenPublicConnections
		{
			get
			{
				return m_NumOpenPublicConnections;
			}

			set
			{
				m_NumOpenPublicConnections = value;
			}
		}

		public SessionDetailsSettings? Settings
		{
			get
			{
				SessionDetailsSettings? value;
				Helper.Get<SessionDetailsSettingsInternal, SessionDetailsSettings>(m_Settings, out value);
				return value;
			}

			set
			{
				Helper.Set<SessionDetailsSettings, SessionDetailsSettingsInternal>(ref value, ref m_Settings);
			}
		}

		public ProductUserId OwnerUserId
		{
			get
			{
				ProductUserId value;
				Helper.Get(m_OwnerUserId, out value);
				return value;
			}

			set
			{
				Helper.Set(value, ref m_OwnerUserId);
			}
		}

		public Utf8String OwnerServerClientId
		{
			get
			{
				Utf8String value;
				Helper.Get(m_OwnerServerClientId, out value);
				return value;
			}

			set
			{
				Helper.Set(value, ref m_OwnerServerClientId);
			}
		}

		public void Set(ref SessionDetailsInfo other)
		{
			m_ApiVersion = SessionDetails.SessiondetailsInfoApiLatest;
			SessionId = other.SessionId;
			HostAddress = other.HostAddress;
			NumOpenPublicConnections = other.NumOpenPublicConnections;
			Settings = other.Settings;
			OwnerUserId = other.OwnerUserId;
			OwnerServerClientId = other.OwnerServerClientId;
		}

		public void Set(ref SessionDetailsInfo? other)
		{
			if (other.HasValue)
			{
				m_ApiVersion = SessionDetails.SessiondetailsInfoApiLatest;
				SessionId = other.Value.SessionId;
				HostAddress = other.Value.HostAddress;
				NumOpenPublicConnections = other.Value.NumOpenPublicConnections;
				Settings = other.Value.Settings;
				OwnerUserId = other.Value.OwnerUserId;
				OwnerServerClientId = other.Value.OwnerServerClientId;
			}
		}

		public void Dispose()
		{
			Helper.Dispose(ref m_SessionId);
			Helper.Dispose(ref m_HostAddress);
			Helper.Dispose(ref m_Settings);
			Helper.Dispose(ref m_OwnerUserId);
			Helper.Dispose(ref m_OwnerServerClientId);
		}

		public void Get(out SessionDetailsInfo output)
		{
			output = new SessionDetailsInfo();
			output.Set(ref this);
		}
	}
}