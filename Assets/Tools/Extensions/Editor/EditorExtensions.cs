using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
using static UnityEditor.EditorUtility;

namespace JD.Editor
{
	public static class EditorExtensions
	{
		public static String OpenFilePanel(this String param0, String param1, String param2) => EditorUtility.OpenFilePanel(param0, param1, param2);
		public static String OpenFilePanelWithFilters(this String param0, String param1, String[] param2) => EditorUtility.OpenFilePanelWithFilters(param0, param1, param2);
		public static void RevealInFinder(this String param0) => EditorUtility.RevealInFinder(param0);
		public static Boolean DisplayDialog(this String param0, String param1, String param2, String param3) => EditorUtility.DisplayDialog(param0, param1, param2, param3);
		public static Boolean DisplayDialog(this String param0, String param1, String param2) => EditorUtility.DisplayDialog(param0, param1, param2);
		public static Int32 DisplayDialogComplex(this String param0, String param1, String param2, String param3, String param4) => EditorUtility.DisplayDialogComplex(param0, param1, param2, param3, param4);
		public static String OpenFolderPanel(this String param0, String param1, String param2) => EditorUtility.OpenFolderPanel(param0, param1, param2);
		public static String SaveFolderPanel(this String param0, String param1, String param2) => EditorUtility.SaveFolderPanel(param0, param1, param2);
		public static Boolean WarnPrefab(this Object param0, String param1, String param2, String param3) => EditorUtility.WarnPrefab(param0, param1, param2, param3);
		public static Boolean IsPersistent(this Object param0) => EditorUtility.IsPersistent(param0);
		public static String SaveFilePanel(this String param0, String param1, String param2, String param3) => EditorUtility.SaveFilePanel(param0, param1, param2, param3);
		public static Int32 NaturalCompare(this String param0, String param1) => EditorUtility.NaturalCompare(param0, param1);
		public static Object InstanceIDToObject(this Int32 param0) => EditorUtility.InstanceIDToObject(param0);
		public static void CompressTexture(this Texture2D param0, TextureFormat param1, Int32 param2) => EditorUtility.CompressTexture(param0, param1, param2);
		public static void CompressCubemapTexture(this Cubemap param0, TextureFormat param1, Int32 param2) => EditorUtility.CompressCubemapTexture(param0, param1, param2);
		public static void SetDirty(this Object param0) => EditorUtility.SetDirty(param0);
		public static void ClearDirty(this Object param0) => EditorUtility.ClearDirty(param0);
		public static String InvokeDiffTool(this String param0, String param1, String param2, String param3, String param4, String param5) => EditorUtility.InvokeDiffTool(param0, param1, param2, param3, param4, param5);
		public static void CopySerialized(this Object param0, Object param1) => EditorUtility.CopySerialized(param0, param1);
		public static void CopySerializedManagedFieldsOnly(this Object param0, Object param1) => EditorUtility.CopySerializedManagedFieldsOnly(param0, param1);
		public static Object[] CollectDependencies(this Object[] param0) => EditorUtility.CollectDependencies(param0);
		public static Object[] CollectDeepHierarchy(this Object[] param0) => EditorUtility.CollectDeepHierarchy(param0);
		public static String FormatBytes(this Int64 param0) => EditorUtility.FormatBytes(param0);
		public static void DisplayProgressBar(this String param0, String param1, Single param2) => EditorUtility.DisplayProgressBar(param0, param1, param2);
		public static Boolean DisplayCancelableProgressBar(this String param0, String param1, Single param2) => EditorUtility.DisplayCancelableProgressBar(param0, param1, param2);
		public static Int32 GetObjectEnabled(this Object param0) => EditorUtility.GetObjectEnabled(param0);
		public static void SetObjectEnabled(this Object param0, Boolean param1) => EditorUtility.SetObjectEnabled(param0, param1);
		public static void SetSelectedRenderState(this Renderer param0, EditorSelectedRenderState param1) => EditorUtility.SetSelectedRenderState(param0, param1);
		public static void OpenWithDefaultApp(this String param0) => EditorUtility.OpenWithDefaultApp(param0);
		public static void SetCameraAnimateMaterials(this Camera param0, Boolean param1) => EditorUtility.SetCameraAnimateMaterials(param0, param1);
		public static void SetCameraAnimateMaterialsTime(this Camera param0, Single param1) => EditorUtility.SetCameraAnimateMaterialsTime(param0, param1);
		public static void UpdateGlobalShaderProperties(this Single param0) => EditorUtility.UpdateGlobalShaderProperties(param0);
		// public static void set_audioMasterMute(this Boolean param0) => EditorUtility.set_audioMasterMute(param0);
		public static Int32 GetDirtyCount(this Int32 param0) => EditorUtility.GetDirtyCount(param0);
		public static Int32 GetDirtyCount(this Object param0) => EditorUtility.GetDirtyCount(param0);
		public static Boolean IsDirty(this Int32 param0) => EditorUtility.IsDirty(param0);
		public static Boolean IsDirty(this Object param0) => EditorUtility.IsDirty(param0);
		public static Boolean LoadWindowLayout(this String param0) => EditorUtility.LoadWindowLayout(param0);
		public static void CompressTexture(this Texture2D param0, TextureFormat param1, TextureCompressionQuality param2) => EditorUtility.CompressTexture(param0, param1, param2);
		public static void CompressCubemapTexture(this Cubemap param0, TextureFormat param1, TextureCompressionQuality param2) => EditorUtility.CompressCubemapTexture(param0, param1, param2);
		public static String SaveFilePanelInProject(this String param0, String param1, String param2, String param3) => EditorUtility.SaveFilePanelInProject(param0, param1, param2, param3);
		public static String SaveFilePanelInProject(this String param0, String param1, String param2, String param3, String param4) => EditorUtility.SaveFilePanelInProject(param0, param1, param2, param3, param4);
		public static void CopySerializedIfDifferent(this Object param0, Object param1) => EditorUtility.CopySerializedIfDifferent(param0, param1);
		public static void UnloadUnusedAssetsImmediate(this Boolean param0) => EditorUtility.UnloadUnusedAssetsImmediate(param0);
		public static Boolean GetDialogOptOutDecision(this DialogOptOutDecisionType param0, String param1) => EditorUtility.GetDialogOptOutDecision(param0, param1);
		public static void SetDialogOptOutDecision(this DialogOptOutDecisionType param0, String param1, Boolean param2) => EditorUtility.SetDialogOptOutDecision(param0, param1, param2);
		public static Boolean DisplayDialog(this String param0, String param1, String param2, DialogOptOutDecisionType param3, String param4) => EditorUtility.DisplayDialog(param0, param1, param2, param3, param4);
		public static Boolean DisplayDialog(this String param0, String param1, String param2, String param3, DialogOptOutDecisionType param4, String param5) => EditorUtility.DisplayDialog(param0, param1, param2, param3, param4, param5);
		public static void DisplayPopupMenu(this Rect param0, String param1, MenuCommand param2) => EditorUtility.DisplayPopupMenu(param0, param1, param2);
		public static void DisplayCustomMenu(this Rect param0, GUIContent[] param1, Int32 param2, SelectMenuItemFunction param3, Object param4) => EditorUtility.DisplayCustomMenu(param0, param1, param2, param3, param4);
		public static void DisplayCustomMenu(this Rect param0, GUIContent[] param1, Int32 param2, SelectMenuItemFunction param3, Object param4, Boolean param5) => EditorUtility.DisplayCustomMenu(param0, param1, param2, param3, param4, param5);
		// public static void DisplayCustomMenu(this Rect param0, GUIContent[] param1, Func`2 param2, Int32 param3, SelectMenuItemFunction param4, Object param5, Boolean param6) => EditorUtility.DisplayCustomMenu(param0, param1, param2, param3, param4, param5, param6);
		public static String FormatBytes(this Int32 param0) => EditorUtility.FormatBytes(param0);
		public static GameObject CreateGameObjectWithHideFlags(this String param0, HideFlags param1, Type[] param2) => EditorUtility.CreateGameObjectWithHideFlags(param0, param1, param2);
		public static void DisplayCustomMenuWithSeparators(this Rect param0, String[] param1, Boolean[] param2, Boolean[] param3, Int32[] param4, SelectMenuItemFunction param5, Object param6) => EditorUtility.DisplayCustomMenuWithSeparators(param0, param1, param2, param3, param4, param5, param6);
		public static void SetCustomDiffTool(this String param0, String param1, String param2, String param3, Boolean param4) => EditorUtility.SetCustomDiffTool(param0, param1, param2, param3, param4);
		public static void SetDefaultParentObject(this GameObject param0) => EditorUtility.SetDefaultParentObject(param0);
		public static void ClearDefaultParentObject(this Scene param0) => EditorUtility.ClearDefaultParentObject(param0);
		public static void OpenPropertyEditor(this Object param0) => EditorUtility.OpenPropertyEditor(param0);
	}
}
