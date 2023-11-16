// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.Lobby
{
	/// <summary>
	/// Input parameters for the <see cref="LobbyInterface.JoinLobbyById" /> function.
	/// </summary>
	public struct JoinLobbyByIdOptions
	{
		/// <summary>
		/// The ID of the lobby
		/// </summary>
		public Utf8String LobbyId { get; set; }

		/// <summary>
		/// The Product User ID of the local user joining the lobby
		/// </summary>
		public ProductUserId LocalUserId { get; set; }

		/// <summary>
		/// If true, this lobby will be associated with the user's presence information. A user can only associate one lobby at a time with their presence information.
		/// This affects the ability of the Social Overlay to show game related actions to take in the user's social graph.
		/// The Social Overlay can handle only one of the following three options at a time:
		/// using the bPresenceEnabled flags within the Sessions interface
		/// using the bPresenceEnabled flags within the Lobby interface
		/// using EOS_PresenceModification_SetJoinInfo
		/// <seealso cref="Presence.PresenceModificationSetJoinInfoOptions" />
		/// <seealso cref="CreateLobbyOptions" />
		/// <seealso cref="JoinLobbyOptions" />
		/// <seealso cref="JoinLobbyByIdOptions" />
		/// <seealso cref="Sessions.CreateSessionModificationOptions" />
		/// <seealso cref="Sessions.JoinSessionOptions" />
		/// </summary>
		public bool PresenceEnabled { get; set; }

		/// <summary>
		/// (Optional) Set this value to override the default local options for the RTC Room, if it is enabled for this lobby. Set this to <see langword="null" /> if
		/// your application does not use the Lobby RTC Rooms feature, or if you would like to use the default settings. This option is ignored if
		/// the specified lobby does not have an RTC Room enabled and will not cause errors.
		/// </summary>
		public LocalRTCOptions? LocalRTCOptions { get; set; }

		/// <summary>
		/// This value indicates whether or not the local user allows crossplay interactions. If it is false, the local user
		/// will be treated as allowing crossplay.
		/// </summary>
		public bool CrossplayOptOut { get; set; }
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	internal struct JoinLobbyByIdOptionsInternal : ISettable<JoinLobbyByIdOptions>, System.IDisposable
	{
		private int m_ApiVersion;
		private System.IntPtr m_LobbyId;
		private System.IntPtr m_LocalUserId;
		private int m_PresenceEnabled;
		private System.IntPtr m_LocalRTCOptions;
		private int m_CrossplayOptOut;

		public Utf8String LobbyId
		{
			set
			{
				Helper.Set(value, ref m_LobbyId);
			}
		}

		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref m_LocalUserId);
			}
		}

		public bool PresenceEnabled
		{
			set
			{
				Helper.Set(value, ref m_PresenceEnabled);
			}
		}

		public LocalRTCOptions? LocalRTCOptions
		{
			set
			{
				Helper.Set<LocalRTCOptions, LocalRTCOptionsInternal>(ref value, ref m_LocalRTCOptions);
			}
		}

		public bool CrossplayOptOut
		{
			set
			{
				Helper.Set(value, ref m_CrossplayOptOut);
			}
		}

		public void Set(ref JoinLobbyByIdOptions other)
		{
			m_ApiVersion = LobbyInterface.JoinlobbybyidApiLatest;
			LobbyId = other.LobbyId;
			LocalUserId = other.LocalUserId;
			PresenceEnabled = other.PresenceEnabled;
			LocalRTCOptions = other.LocalRTCOptions;
			CrossplayOptOut = other.CrossplayOptOut;
		}

		public void Set(ref JoinLobbyByIdOptions? other)
		{
			if (other.HasValue)
			{
				m_ApiVersion = LobbyInterface.JoinlobbybyidApiLatest;
				LobbyId = other.Value.LobbyId;
				LocalUserId = other.Value.LocalUserId;
				PresenceEnabled = other.Value.PresenceEnabled;
				LocalRTCOptions = other.Value.LocalRTCOptions;
				CrossplayOptOut = other.Value.CrossplayOptOut;
			}
		}

		public void Dispose()
		{
			Helper.Dispose(ref m_LobbyId);
			Helper.Dispose(ref m_LocalUserId);
			Helper.Dispose(ref m_LocalRTCOptions);
		}
	}
}