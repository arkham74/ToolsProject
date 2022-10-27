#if DOTWEEN_ENABLED
#if TOOLS_CINEMACHINE
using Cinemachine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

namespace JD
{
	public static partial class DOTweenExtensions
	{
		public static TweenerCore<float, float, FloatOptions> DODutch(this CinemachineVirtualCamera target, float endValue,
			float duration, bool snapping = false)
		{
			TweenerCore<float, float, FloatOptions> t = DOTween.To(() => target.m_Lens.Dutch, x => target.m_Lens.Dutch = x,
				endValue, duration);
			t.SetOptions(snapping).SetTarget(target);
			return t;
		}

		public static TweenerCore<float, float, FloatOptions> DOFieldOfView(this CinemachineVirtualCamera target, float endValue, float duration)
		{
			TweenerCore<float, float, FloatOptions> t = DOTween.To(() => target.m_Lens.FieldOfView, x => target.m_Lens.FieldOfView = x, endValue, duration);
			t.SetTarget(target);
			return t;
		}

		public static TweenerCore<float, float, FloatOptions> DOOrthographicSize(this CinemachineVirtualCamera target, float endValue, float duration)
		{
			TweenerCore<float, float, FloatOptions> t = DOTween.To(() => target.m_Lens.OrthographicSize, x => target.m_Lens.OrthographicSize = x, endValue, duration);
			t.SetTarget(target);
			return t;
		}
	}
}
#endif
#endif