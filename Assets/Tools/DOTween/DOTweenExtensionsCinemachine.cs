#if TOOLS_CINEMACHINE
using Cinemachine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

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

	public static TweenerCore<float, float, FloatOptions> DOFov(this CinemachineVirtualCamera target, float endValue,
		float duration, bool snapping = false)
	{
		TweenerCore<float, float, FloatOptions> t = DOTween.To(() => target.m_Lens.FieldOfView,
			x => target.m_Lens.FieldOfView = x, endValue, duration);
		t.SetOptions(snapping).SetTarget(target);
		return t;
	}
}
#endif