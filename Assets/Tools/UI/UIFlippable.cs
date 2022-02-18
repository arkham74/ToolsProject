// Credit ChoMPHi
// Sourced from - http://forum.unity3d.com/threads/script-flippable-for-ui-graphics.291711/

namespace UnityEngine.UI.Extensions
{
	[RequireComponent(typeof(RectTransform), typeof(Graphic)), DisallowMultipleComponent]
	[AddComponentMenu("UI/Effects/Extensions/Flippable")]
	public class UIFlippable : BaseMeshEffect
	{
		[SerializeField] private bool horizontal;
		[SerializeField] private bool vertical;

		// protected override void Awake()
		// {
		// 	OnValidate();
		// }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="UIFlippable"/> should be flipped horizontally.
		/// </summary>
		/// <value><c>true</c> if horizontal; otherwise, <c>false</c>.</value>
		public bool Horizontal
		{
			get => horizontal;
			set => horizontal = value;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="UIFlippable"/> should be flipped vertically.
		/// </summary>
		/// <value><c>true</c> if vertical; otherwise, <c>false</c>.</value>
		public bool Vertical
		{
			get => vertical;
			set => vertical = value;
		}

		public override void ModifyMesh(VertexHelper verts)
		{
			RectTransform rt = transform as RectTransform;

			for (int i = 0; i < verts.currentVertCount; ++i)
			{
				UIVertex uiVertex = new UIVertex();
				verts.PopulateUIVertex(ref uiVertex, i);

				// Modify positions
				Rect rect = rt.rect;
				uiVertex.position = new Vector3(
					horizontal ? uiVertex.position.x + (rect.center.x - uiVertex.position.x) * 2 : uiVertex.position.x,
					vertical ? uiVertex.position.y + (rect.center.y - uiVertex.position.y) * 2 : uiVertex.position.y,
					uiVertex.position.z);

				// Apply
				verts.SetUIVertex(uiVertex, i);
			}
		}

		// protected override void OnValidate()
		// {
		// 	Component[] components = gameObject.GetComponents(typeof(BaseMeshEffect));
		// 	foreach (Component comp in components)
		// 	{
		// 		if (comp.GetType() != typeof(UIFlippable))
		// 		{
		// 			ComponentUtility.MoveComponentUp(this);
		// 		}
		// 		else
		// 		{
		// 			break;
		// 		}
		// 	}
		//
		// 	GetComponent<Graphic>().SetVerticesDirty();
		// 	base.OnValidate();
		// }
	}
}