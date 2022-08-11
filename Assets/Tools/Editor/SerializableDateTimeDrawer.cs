using UnityEngine;
using UnityEditor;

namespace JD.Editor
{
	[CustomPropertyDrawer(typeof(SerializableDateTime))]
	public class SerializableDateTimeDrawer : PropertyDrawer
	{
		private static GUIContent label2 = new GUIContent("Date");
		private static GUIContent label1 = new GUIContent("Time");

		private static GUIContent[] subLabels1 = new GUIContent[3]
		{
			new GUIContent("Year"),
			new GUIContent("Month"),
			new GUIContent("Day"),
		};

		private static GUIContent[] subLabels2 = new GUIContent[3]{
			new GUIContent("Hour"),
			new GUIContent("Minute"),
			new GUIContent("Second"),
		};

		private static int[] values1 = new int[3];
		private static int[] values2 = new int[3];

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return base.GetPropertyHeight(property, label) * 2f + EditorGUIUtility.standardVerticalSpacing;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			var year = property.FindPropertyRelative("year");
			var month = property.FindPropertyRelative("month");
			var day = property.FindPropertyRelative("day");
			var hour = property.FindPropertyRelative("hour");
			var minute = property.FindPropertyRelative("minute");
			var second = property.FindPropertyRelative("second");

			values1[0] = year.intValue;
			values1[1] = month.intValue;
			values1[2] = day.intValue;
			values2[0] = hour.intValue;
			values2[1] = minute.intValue;
			values2[2] = second.intValue;

			position.height *= 0.5f;

			var orgPos = position;

			position = EditorGUI.PrefixLabel(position, label2);
			EditorGUI.MultiIntField(position, subLabels1, values1);

			position = orgPos;

			position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
			position = EditorGUI.PrefixLabel(position, label1);
			EditorGUI.MultiIntField(position, subLabels2, values2);

			year.intValue = values1[0];
			month.intValue = values1[1];
			day.intValue = values1[2];
			hour.intValue = values2[0];
			minute.intValue = values2[1];
			second.intValue = values2[2];

			EditorGUI.EndProperty();
		}
	}
}