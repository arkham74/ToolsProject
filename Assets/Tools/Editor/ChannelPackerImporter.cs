using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor.AssetImporters;
using UnityEditor;
using UnityEngine.Experimental.Rendering;
using Unity.Jobs;
using Unity.Collections;
using UnityEngine.Rendering;
using System.Threading.Tasks;

#pragma warning disable 649

namespace TankGame
{
	[CustomEditor(typeof(ChannelPackerImporter))]
	public class ChannelPackerEditor : ScriptedImporterEditor
	{
		private SerializedProperty sizeProp;
		private SerializedProperty redTexProp;
		private SerializedProperty greenTexProp;
		private SerializedProperty blueTexProp;
		private SerializedProperty alphaTexProp;
		private SerializedProperty redChannelProp;
		private SerializedProperty greenChannelProp;
		private SerializedProperty blueChannelProp;
		private SerializedProperty alphaChannelProp;

		public override void OnEnable()
		{
			base.OnEnable();
			ChannelPackerImporter tar;
			sizeProp = serializedObject.FindProperty(nameof(tar.size));
			redTexProp = serializedObject.FindProperty(nameof(tar.redTex));
			greenTexProp = serializedObject.FindProperty(nameof(tar.greenTex));
			blueTexProp = serializedObject.FindProperty(nameof(tar.blueTex));
			alphaTexProp = serializedObject.FindProperty(nameof(tar.alphaTex));
			redChannelProp = serializedObject.FindProperty(nameof(tar.redChannel));
			greenChannelProp = serializedObject.FindProperty(nameof(tar.greenChannel));
			blueChannelProp = serializedObject.FindProperty(nameof(tar.blueChannel));
			alphaChannelProp = serializedObject.FindProperty(nameof(tar.alphaChannel));
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			EditorGUILayout.PropertyField(sizeProp);

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PropertyField(redChannelProp);
			EditorGUILayout.PropertyField(redTexProp, GUIContent.none);
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PropertyField(greenChannelProp);
			EditorGUILayout.PropertyField(greenTexProp, GUIContent.none);
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PropertyField(blueChannelProp);
			EditorGUILayout.PropertyField(blueTexProp, GUIContent.none);
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PropertyField(alphaChannelProp);
			EditorGUILayout.PropertyField(alphaTexProp, GUIContent.none);
			EditorGUILayout.EndHorizontal();

			serializedObject.ApplyModifiedProperties();
			ApplyRevertGUI();
		}
	}

	[ScriptedImporter(1, "packedtexture")]
	public class ChannelPackerImporter : ScriptedImporter
	{
		public enum Channel
		{
			Red,
			Green,
			Blue,
			Alpha
		}

		public Vector2Int size = new Vector2Int(512, 512);
		public Texture2D redTex;
		public Texture2D greenTex;
		public Texture2D blueTex;
		public Texture2D alphaTex;
		public Channel redChannel = Channel.Red;
		public Channel greenChannel = Channel.Green;
		public Channel blueChannel = Channel.Blue;
		public Channel alphaChannel = Channel.Alpha;

		[MenuItem("Assets/Create/Packed Texture")]
		public static void CreateNewAsset() =>
			ProjectWindowUtil.CreateAssetWithContent("NewPackedTexture.packedtexture", "");

		public override void OnImportAsset(AssetImportContext ctx)
		{
			int width = size.x;
			int height = size.y;
			Texture2D red = Check(redTex, Texture2D.blackTexture);
			Texture2D green = Check(greenTex, Texture2D.blackTexture);
			Texture2D blue = Check(blueTex, Texture2D.blackTexture);
			Texture2D alpha = Check(alphaTex, Texture2D.whiteTexture);

			Texture2D texture = new Texture2D(width, height);
			Texture2D r = Scale(red, width, height);
			Texture2D g = Scale(green, width, height);
			Texture2D b = Scale(blue, width, height);
			Texture2D a = Scale(alpha, width, height);

			int count = width * height;
			Color32[] colors = new Color32[count];
			Color32[] rColors = r.GetPixels32();
			Color32[] gColors = g.GetPixels32();
			Color32[] bColors = b.GetPixels32();
			Color32[] aColors = a.GetPixels32();

			for (int i = 0; i < count; i++)
			{
				colors[i].r = GetChannel(i, rColors, redChannel);
				colors[i].g = GetChannel(i, gColors, greenChannel);
				colors[i].b = GetChannel(i, bColors, blueChannel);
				colors[i].a = GetChannel(i, aColors, alphaChannel);
			}

			texture.SetPixels32(colors);
			texture.Apply();

			ctx.AddObjectToAsset("packedtexture", texture);
			ctx.SetMainObject(texture);

			DestroyImmediate(r);
			DestroyImmediate(g);
			DestroyImmediate(b);
			DestroyImmediate(a);
		}

		private Texture2D Check(Texture2D tex, Texture2D defaultTex)
		{
			if (tex && !tex.isReadable)
			{
				Debug.LogWarning($"{tex} is not readable.");
			}

			return tex && tex.isReadable ? tex : defaultTex;
		}

		private static byte GetChannel(int i, IList<Color32> colors, Channel channel)
		{
			return channel switch
			{
				Channel.Red => colors[i].r,
				Channel.Green => colors[i].g,
				Channel.Blue => colors[i].b,
				Channel.Alpha => colors[i].a,
				_ => (byte) 0
			};
		}


		private static Texture2D Scale(Texture2D src, int width, int height, FilterMode mode = FilterMode.Trilinear)
		{
			Rect texR = new Rect(0, 0, width, height);

			//We need the source texture in VRAM because we render with it
			src.filterMode = mode;
			src.Apply(true);

			//Using RTT for best quality and performance. Thanks, Unity 5
			RenderTexture rtt = new RenderTexture(width, height, 32);

			//Set the RTT in order to render to it
			Graphics.SetRenderTarget(rtt);

			//Setup 2D matrix in range 0..1, so nobody needs to care about sized
			GL.LoadPixelMatrix(0, 1, 1, 0);

			//Then clear & draw the texture to fill the entire RTT.
			GL.Clear(true, true, new Color(0, 0, 0, 0));
			Graphics.DrawTexture(new Rect(0, 0, 1, 1), src);

			//Get rendered data back to a new texture
			Texture2D result = new Texture2D(width, height, TextureFormat.ARGB32, true);
			result.ReadPixels(texR, 0, 0, true);
			return result;
		}
	}
}