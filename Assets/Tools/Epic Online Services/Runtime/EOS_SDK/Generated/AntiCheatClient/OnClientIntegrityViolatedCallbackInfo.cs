// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.AntiCheatClient
{
	/// <summary>
	/// Structure containing details about integrity violation related to the local client
	/// </summary>
	public struct OnClientIntegrityViolatedCallbackInfo : ICallbackInfo
	{
		/// <summary>
		/// Caller-specified context data
		/// </summary>
		public object ClientData { get; set; }

		/// <summary>
		/// Code describing the violation that occurred
		/// </summary>
		public AntiCheatClientViolationType ViolationType { get; set; }

		/// <summary>
		/// String describing the violation which should be displayed to the user
		/// </summary>
		public Utf8String ViolationMessage { get; set; }

		public Result? GetResultCode()
		{
			return null;
		}

		internal void Set(ref OnClientIntegrityViolatedCallbackInfoInternal other)
		{
			ClientData = other.ClientData;
			ViolationType = other.ViolationType;
			ViolationMessage = other.ViolationMessage;
		}
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	internal struct OnClientIntegrityViolatedCallbackInfoInternal : ICallbackInfoInternal, IGettable<OnClientIntegrityViolatedCallbackInfo>, ISettable<OnClientIntegrityViolatedCallbackInfo>, System.IDisposable
	{
		private System.IntPtr m_ClientData;
		private AntiCheatClientViolationType m_ViolationType;
		private System.IntPtr m_ViolationMessage;

		public object ClientData
		{
			get
			{
				object value;
				Helper.Get(m_ClientData, out value);
				return value;
			}

			set
			{
				Helper.Set(value, ref m_ClientData);
			}
		}

		public System.IntPtr ClientDataAddress
		{
			get
			{
				return m_ClientData;
			}
		}

		public AntiCheatClientViolationType ViolationType
		{
			get
			{
				return m_ViolationType;
			}

			set
			{
				m_ViolationType = value;
			}
		}

		public Utf8String ViolationMessage
		{
			get
			{
				Utf8String value;
				Helper.Get(m_ViolationMessage, out value);
				return value;
			}

			set
			{
				Helper.Set(value, ref m_ViolationMessage);
			}
		}

		public void Set(ref OnClientIntegrityViolatedCallbackInfo other)
		{
			ClientData = other.ClientData;
			ViolationType = other.ViolationType;
			ViolationMessage = other.ViolationMessage;
		}

		public void Set(ref OnClientIntegrityViolatedCallbackInfo? other)
		{
			if (other.HasValue)
			{
				ClientData = other.Value.ClientData;
				ViolationType = other.Value.ViolationType;
				ViolationMessage = other.Value.ViolationMessage;
			}
		}

		public void Dispose()
		{
			Helper.Dispose(ref m_ClientData);
			Helper.Dispose(ref m_ViolationMessage);
		}

		public void Get(out OnClientIntegrityViolatedCallbackInfo output)
		{
			output = new OnClientIntegrityViolatedCallbackInfo();
			output.Set(ref this);
		}
	}
}