using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace JD
{
	[RequireComponent(typeof(Image))]
	public class ProceduralUICircle : BaseMeshEffect
	{
		[SerializeField][Range(0, 1)] private float radius = 1.0f;
		[SerializeField][Range(0, 1)] private float width = 0.25f;
		[SerializeField][HideInInspector] private Material circleMaterial;
		private Image image;

#if UNITY_EDITOR
		protected override void Reset()
		{
			base.Reset();
			image = GetComponent<Image>();
			image.material = circleMaterial;
		}
#endif

		public override void ModifyMesh(VertexHelper vh)
		{
			UIVertex vert = new UIVertex();
			for (int i = 0; i < vh.currentVertCount; i++)
			{
				vh.PopulateUIVertex(ref vert, i);
				vert.uv1 = new Vector4(radius, width, 0, 0);
				vh.SetUIVertex(vert, i);
			}
		}
	}
}