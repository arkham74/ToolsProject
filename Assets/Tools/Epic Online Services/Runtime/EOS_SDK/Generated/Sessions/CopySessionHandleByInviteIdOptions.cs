// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.Sessions
{
	/// <summary>
	/// Input parameters for the <see cref="SessionsInterface.CopySessionHandleByInviteId" /> function.
	/// </summary>
	public struct CopySessionHandleByInviteIdOptions
	{
		/// <summary>
		/// Invite ID for which to retrieve a session handle
		/// </summary>
		public Utf8String InviteId { get; set; }
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	internal struct CopySessionHandleByInviteIdOptionsInternal : ISettable<CopySessionHandleByInviteIdOptions>, System.IDisposable
	{
		private int m_ApiVersion;
		private System.IntPtr m_InviteId;

		public Utf8String InviteId
		{
			set
			{
				Helper.Set(value, ref m_InviteId);
			}
		}

		public void Set(ref CopySessionHandleByInviteIdOptions other)
		{
			m_ApiVersion = SessionsInterface.CopysessionhandlebyinviteidApiLatest;
			InviteId = other.InviteId;
		}

		public void Set(ref CopySessionHandleByInviteIdOptions? other)
		{
			if (other.HasValue)
			{
				m_ApiVersion = SessionsInterface.CopysessionhandlebyinviteidApiLatest;
				InviteId = other.Value.InviteId;
			}
		}

		public void Dispose()
		{
			Helper.Dispose(ref m_InviteId);
		}
	}
}