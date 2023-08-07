// using System;
// using System.Linq;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.Audio;
// using UnityEngine.Events;
// using UnityEngine.SceneManagement;
// using UnityEngine.Serialization;
// using TMPro;
// using JD;
// using Freya;
// using Random = UnityEngine.Random;
// using Text = TMPro.TextMeshProUGUI;
// using UnityEditor;
// using Object = UnityEngine.Object;
// using UnityEditor.Callbacks;
// using System.IO;
// using System.Text;
// using System.Reflection;

// #if ENABLE_INPUT_SYSTEM
// using UnityEngine.InputSystem;
// #endif

// namespace JD.Editor
// {
// 	public static class GenerateEditorExtensions
// 	{
// 		[InitializeOnLoadMethod]
// 		public static void OnInitializeOnLoadMethod()
// 		{
// 			StringBuilder sb = Pools.GetStringBuilder();

// 			sb.AppendLine(@"using System;");
// 			sb.AppendLine(@"using UnityEngine;");
// 			sb.AppendLine(@"using UnityEditor;");
// 			sb.AppendLine(@"using UnityEngine.SceneManagement;");
// 			sb.AppendLine(@"using Object = UnityEngine.Object;");
// 			sb.AppendLine(@"using static UnityEditor.EditorUtility;");
// 			sb.AppendLine();
// 			sb.AppendLine(@"namespace JD.Editor");
// 			sb.AppendLine(@"{");
// 			sb.AppendLine(@"	public static class EditorExtensions");
// 			sb.AppendLine(@"	{");

// 			MethodInfo[] methods = typeof(EditorUtility).GetMethods(BindingFlags.Static | BindingFlags.Public);

// 			foreach (MethodInfo method in methods)
// 			{
// 				ObsoleteAttribute obsAttr = method.GetCustomAttribute<ObsoleteAttribute>();
// 				if (obsAttr is { }) continue;

// 				ParameterInfo[] fields = method.GetParameters();
// 				if (fields.Any())
// 				{
// 					string name = method.ReturnType.Name;
// 					if (name == "Void") name = "void";

// 					sb.Append($"		public static {name} {method.Name}(this ");
// 					foreach (ParameterInfo field in fields)
// 					{
// 						sb.Append(field.ParameterType.Name);
// 						sb.Append(" param" + field.Position);
// 						sb.Append(", ");
// 					}
// 					sb.RemoveLast(2);
// 					sb.Append($") => EditorUtility.{method.Name}(");
// 					foreach (ParameterInfo field in fields)
// 					{
// 						sb.Append("param" + field.Position);
// 						sb.Append(", ");
// 					}
// 					sb.RemoveLast(2);
// 					sb.AppendLine($");");
// 				}
// 			}

// 			sb.AppendLine(@"	}");
// 			sb.AppendLine(@"}");

// 			File.WriteAllText("Assets/Tools/Extensions/Editor/EditorExtensions.cs", sb.ToString());
// 			AssetDatabase.Refresh();
// 			Pools.Release(sb);
// 		}
// 	}
// }