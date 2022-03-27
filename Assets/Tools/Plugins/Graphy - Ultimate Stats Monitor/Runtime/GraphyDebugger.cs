﻿/* ---------------------------------------
 * Author:          Martin Pane (martintayx@gmail.com) (@tayx94)
 * Contributors:    https://github.com/Tayx94/graphy/graphs/contributors
 * Project:         Graphy - Ultimate Stats Monitor
 * Date:            23-Dec-17
 * Studio:          Tayx
 *
 * Git repo:        https://github.com/Tayx94/graphy
 *
 * This project is released under the MIT license.
 * Attribution is not required, but it is always welcomed!
 * -------------------------------------*/

using UnityEngine;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;

using System;
using System.Collections.Generic;
using System.Linq;

using Tayx.Graphy.Audio;
using Tayx.Graphy.Fps;
using Tayx.Graphy.Ram;
using Tayx.Graphy.Utils;

namespace Tayx.Graphy
{
	/// <summary>
	/// Main class to access the Graphy Debugger API.
	/// </summary>
	public class GraphyDebugger : G_Singleton<GraphyDebugger>
	{
		/* ----- TODO: ----------------------------
		 * Add summaries to the variables.
		 * Add summaries to the functions.
		 * Ask why we're not using System.Serializable instead for the helper class.
		 * Simplify the initializers of the DebugPackets, but check wether we should as some wont work with certain lists.
		 * --------------------------------------*/

		protected GraphyDebugger() { }

		#region Enums -> Public

		public enum DebugVariable
		{
			Fps,
			Fps_Min,
			Fps_Max,
			Fps_Avg,
			Ram_Allocated,
			Ram_Reserved,
			Ram_Mono,
			Audio_DB
		}

		public enum DebugComparer
		{
			Less_than,
			Equals_or_less_than,
			Equals,
			Equals_or_greater_than,
			Greater_than
		}

		public enum ConditionEvaluation
		{
			All_conditions_must_be_met,
			Only_one_condition_has_to_be_met,

		}

		public enum MessageType
		{
			Log,
			Warning,
			Error
		}

		#endregion

		#region Structs -> Public

		[Serializable]
		public struct DebugCondition
		{
			[Tooltip("Variable to compare against")]
			public DebugVariable Variable;
			[Tooltip("Comparer operator to use")]
			public DebugComparer Comparer;
			[Tooltip("Value to compare against the chosen variable")]
			public float Value;
		}

		#endregion

		#region Helper Classes

		[Serializable]
		public class DebugPacket
		{

			[Tooltip("If false, it won't be checked")]
			public bool Active = true;
			[Tooltip("Optional Id. It's used to get or remove DebugPackets in runtime")]
			public int Id;
			[Tooltip("If true, once the actions are executed, this DebugPacket will delete itself")]
			public bool ExecuteOnce = true;
			[Tooltip("Time to wait before checking if conditions are met (use this to avoid low fps drops triggering the conditions when loading the game)")]
			public float InitSleepTime = 2;
			[Tooltip("Time to wait before checking if conditions are met again (once they have already been met and if ExecuteOnce is false)")]
			public float ExecuteSleepTime = 2;

			public ConditionEvaluation ConditionEvaluation = ConditionEvaluation.All_conditions_must_be_met;
			[Tooltip("List of conditions that will be checked each frame")]
			public List<DebugCondition> DebugConditions = new List<DebugCondition>();

			// Actions on conditions met

			public MessageType MessageType;
			[Multiline]
			public string Message = string.Empty;
			public bool TakeScreenshot = false;
			public string ScreenshotFileName = "Graphy_Screenshot";
			[Tooltip("If true, it pauses the editor")]
			public bool DebugBreak = false;
			public UnityEvent UnityEvents;
			public List<Action> Callbacks = new List<Action>();


			private bool canBeChecked = false;
			private bool executed = false;

			private float timePassed = 0;

			public bool Check { get { return canBeChecked; } }

			public void Update()
			{
				if (!canBeChecked)
				{
					timePassed += Time.deltaTime;

					if ((executed && timePassed >= ExecuteSleepTime)
						 || (!executed && timePassed >= InitSleepTime))
					{
						canBeChecked = true;

						timePassed = 0;
					}
				}
			}

			public void Executed()
			{
				canBeChecked = false;
				executed = true;
			}
		}

		#endregion

		#region Variables -> Serialized Private

		[SerializeField] private List<DebugPacket> m_debugPackets = new List<DebugPacket>();

		#endregion

		#region Variables -> Private

		private G_FpsMonitor m_fpsMonitor = null;
		private G_RamMonitor m_ramMonitor = null;
		private G_AudioMonitor m_audioMonitor = null;

		#endregion

		#region Methods -> Unity Callbacks

		private void Start()
		{
			m_fpsMonitor = GetComponentInChildren<G_FpsMonitor>();
			m_ramMonitor = GetComponentInChildren<G_RamMonitor>();
			m_audioMonitor = GetComponentInChildren<G_AudioMonitor>();
		}

		private void Update()
		{
			CheckDebugPackets();
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Add a new DebugPacket.
		/// </summary>
		public void AddNewDebugPacket(DebugPacket newDebugPacket)
		{
			m_debugPackets?.Add(newDebugPacket);
		}

		/// <summary>
		/// Add a new DebugPacket.
		/// </summary>
		public void AddNewDebugPacket
		(
			 int newId,
			 DebugCondition newDebugCondition,
			 MessageType newMessageType,
			 string newMessage,
			 bool newDebugBreak,
					 Action newCallback
		)
		{
			DebugPacket newDebugPacket = new DebugPacket
			{
				Id = newId,
				MessageType = newMessageType,
				Message = newMessage,
				DebugBreak = newDebugBreak
			};
			newDebugPacket.DebugConditions.Add(newDebugCondition);
			newDebugPacket.Callbacks.Add(newCallback);

			AddNewDebugPacket(newDebugPacket);
		}

		/// <summary>
		/// Add a new DebugPacket.
		/// </summary>
		public void AddNewDebugPacket
		(
			 int newId,
			 List<DebugCondition> newDebugConditions,
			 MessageType newMessageType,
			 string newMessage,
			 bool newDebugBreak,
					 Action newCallback
		)
		{
			DebugPacket newDebugPacket = new DebugPacket
			{
				Id = newId,
				DebugConditions = newDebugConditions,
				MessageType = newMessageType,
				Message = newMessage,
				DebugBreak = newDebugBreak
			};
			newDebugPacket.Callbacks.Add(newCallback);

			AddNewDebugPacket(newDebugPacket);
		}

		/// <summary>
		/// Add a new DebugPacket.
		/// </summary>
		public void AddNewDebugPacket
		(
			 int newId,
			 DebugCondition newDebugCondition,
			 MessageType newMessageType,
			 string newMessage,
			 bool newDebugBreak,
			 List<Action> newCallbacks
		)
		{
			DebugPacket newDebugPacket = new DebugPacket
			{
				Id = newId,
				MessageType = newMessageType,
				Message = newMessage,
				DebugBreak = newDebugBreak,
				Callbacks = newCallbacks
			};
			newDebugPacket.DebugConditions.Add(newDebugCondition);

			AddNewDebugPacket(newDebugPacket);
		}

		/// <summary>
		/// Add a new DebugPacket.
		/// </summary>
		public void AddNewDebugPacket
		(
			 int newId,
			 List<DebugCondition> newDebugConditions,
			 MessageType newMessageType,
			 string newMessage,
			 bool newDebugBreak,
			 List<Action> newCallbacks
		)
		{
			DebugPacket newDebugPacket = new DebugPacket
			{
				Id = newId,
				DebugConditions = newDebugConditions,
				MessageType = newMessageType,
				Message = newMessage,
				DebugBreak = newDebugBreak,
				Callbacks = newCallbacks
			};

			AddNewDebugPacket(newDebugPacket);
		}

		/// <summary>
		/// Returns the first Packet with the specified ID in the DebugPacket list.
		/// </summary>
		/// <param name="packetId"></param>
		/// <returns></returns>
		public DebugPacket GetFirstDebugPacketWithId(int packetId)
		{
			return m_debugPackets.First(x => x.Id == packetId);
		}

		/// <summary>
		/// Returns a list with all the Packets with the specified ID in the DebugPacket list.
		/// </summary>
		/// <param name="packetId"></param>
		/// <returns></returns>
		public List<DebugPacket> GetAllDebugPacketsWithId(int packetId)
		{
			return m_debugPackets.FindAll(x => x.Id == packetId);
		}

		/// <summary>
		/// Removes the first Packet with the specified ID in the DebugPacket list.
		/// </summary>
		/// <param name="packetId"></param>
		/// <returns></returns>
		public void RemoveFirstDebugPacketWithId(int packetId)
		{
			if (m_debugPackets != null && GetFirstDebugPacketWithId(packetId) != null)
			{
				m_debugPackets.Remove(GetFirstDebugPacketWithId(packetId));
			}
		}

		/// <summary>
		/// Removes all the Packets with the specified ID in the DebugPacket list.
		/// </summary>
		/// <param name="packetId"></param>
		/// <returns></returns>
		public void RemoveAllDebugPacketsWithId(int packetId)
		{
			if (m_debugPackets != null)
			{
				m_debugPackets.RemoveAll(x => x.Id == packetId);
			}
		}

		/// <summary>
		/// Add an Action callback to the first Packet with the specified ID in the DebugPacket list.
		/// </summary>
		/// <param name="callback"></param>
		/// <param name="id"></param>
		public void AddCallbackToFirstDebugPacketWithId(Action callback, int id)
		{
			if (GetFirstDebugPacketWithId(id) != null)
			{
				GetFirstDebugPacketWithId(id).Callbacks.Add(callback);
			}
		}

		/// <summary>
		/// Add an Action callback to all the Packets with the specified ID in the DebugPacket list.
		/// </summary>
		/// <param name="callback"></param>
		/// <param name="id"></param>
		public void AddCallbackToAllDebugPacketWithId(Action callback, int id)
		{
			if (GetAllDebugPacketsWithId(id) != null)
			{
				foreach (DebugPacket debugPacket in GetAllDebugPacketsWithId(id))
				{
					if (callback != null)
					{
						debugPacket.Callbacks.Add(callback);
					}
				}
			}
		}

		#endregion

		#region Methods -> Private

		/// <summary>
		/// Checks all the Debug Packets to see if they have to be executed.
		/// </summary>
		private void CheckDebugPackets()
		{
			if (m_debugPackets == null)
			{
				return;
			}

			for (int i = 0; i < m_debugPackets.Count; i++)
			{
				DebugPacket packet = m_debugPackets[i];

				if (packet != null && packet.Active)
				{
					packet.Update();

					if (packet.Check)
					{
						switch (packet.ConditionEvaluation)
						{
							case ConditionEvaluation.All_conditions_must_be_met:
								int count = 0;

								foreach (DebugCondition packetDebugCondition in packet.DebugConditions)
								{
									if (CheckIfConditionIsMet(packetDebugCondition))
									{
										count++;
									}
								}

								if (count >= packet.DebugConditions.Count)
								{
									ExecuteOperationsInDebugPacket(packet);

									if (packet.ExecuteOnce)
									{
										m_debugPackets[i] = null;
									}
								}

								break;

							case ConditionEvaluation.Only_one_condition_has_to_be_met:
								foreach (DebugCondition packetDebugCondition in packet.DebugConditions)
								{
									if (CheckIfConditionIsMet(packetDebugCondition))
									{
										ExecuteOperationsInDebugPacket(packet);

										if (packet.ExecuteOnce)
										{
											m_debugPackets[i] = null;
										}

										break;
									}
								}

								break;
						}
					}
				}
			}

			m_debugPackets.RemoveAll((packet) => packet == null);
		}

		/// <summary>
		/// Returns true if a condition is met.
		/// </summary>
		/// <param name="debugCondition"></param>
		/// <returns></returns>
		private bool CheckIfConditionIsMet(DebugCondition debugCondition)
		{
			return debugCondition.Comparer switch
			{
				DebugComparer.Less_than => GetRequestedValueFromDebugVariable(debugCondition.Variable) < debugCondition.Value,
				DebugComparer.Equals_or_less_than => GetRequestedValueFromDebugVariable(debugCondition.Variable) <= debugCondition.Value,
				DebugComparer.Equals => Mathf.Approximately(GetRequestedValueFromDebugVariable(debugCondition.Variable), debugCondition.Value),
				DebugComparer.Equals_or_greater_than => GetRequestedValueFromDebugVariable(debugCondition.Variable) >= debugCondition.Value,
				DebugComparer.Greater_than => GetRequestedValueFromDebugVariable(debugCondition.Variable) > debugCondition.Value,
				_ => false,
			};
		}

		/// <summary>
		/// Obtains the requested value from the specified variable.
		/// </summary>
		/// <param name="debugVariable"></param>
		/// <returns></returns>
		private float GetRequestedValueFromDebugVariable(DebugVariable debugVariable)
		{
			return debugVariable switch
			{
				DebugVariable.Fps => m_fpsMonitor != null ? m_fpsMonitor.CurrentFPS : 0,
				DebugVariable.Fps_Min => m_fpsMonitor != null ? m_fpsMonitor.OnePercentFPS : 0,
				DebugVariable.Fps_Max => m_fpsMonitor != null ? m_fpsMonitor.Zero1PercentFps : 0,
				DebugVariable.Fps_Avg => m_fpsMonitor != null ? m_fpsMonitor.AverageFPS : 0,
				DebugVariable.Ram_Allocated => m_ramMonitor != null ? m_ramMonitor.AllocatedRam : 0,
				DebugVariable.Ram_Reserved => m_ramMonitor != null ? m_ramMonitor.AllocatedRam : 0,
				DebugVariable.Ram_Mono => m_ramMonitor != null ? m_ramMonitor.AllocatedRam : 0,
				DebugVariable.Audio_DB => m_audioMonitor != null ? m_audioMonitor.MaxDB : 0,
				_ => 0,
			};
		}

		/// <summary>
		/// Executes the operations in the DebugPacket specified.
		/// </summary>
		/// <param name="debugPacket"></param>
		private void ExecuteOperationsInDebugPacket(DebugPacket debugPacket)
		{
			if (debugPacket != null)
			{
				if (debugPacket.DebugBreak)
				{
					Debug.Break();
				}

				if (debugPacket.Message != "")
				{
					string message = "[Graphy] (" + System.DateTime.Now + "): " + debugPacket.Message;

					switch (debugPacket.MessageType)
					{
						case MessageType.Log:
							Debug.Log(message);
							break;
						case MessageType.Warning:
							Debug.LogWarning(message);
							break;
						case MessageType.Error:
							Debug.LogError(message);
							break;
					}
				}

				if (debugPacket.TakeScreenshot)
				{
					string path = debugPacket.ScreenshotFileName + "_" + System.DateTime.Now + ".png";
					path = path.Replace("/", "-").Replace(" ", "_").Replace(":", "-");

#if UNITY_2017_1_OR_NEWER
					ScreenCapture.CaptureScreenshot(path);
#else
                    Application.CaptureScreenshot(path);
#endif
				}

				debugPacket.UnityEvents.Invoke();

				foreach (Action callback in debugPacket.Callbacks)
				{
					callback?.Invoke();
				}

				debugPacket.Executed();
			}
		}

		#endregion
	}
}