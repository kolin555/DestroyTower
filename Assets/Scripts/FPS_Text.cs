using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CodeStage.AdvancedFPSCounter;
using TMPro;

public class FPS_Text : MonoBehaviour
{

	public TextMeshProUGUI text;
	private void Update()
    {
		text.text = "FPS: " + AFPSCounter.Instance.fpsCounter.LastValue +
							"  [" + AFPSCounter.Instance.fpsCounter.LastMillisecondsValue + " MS]" +
							"  AVG: " + AFPSCounter.Instance.fpsCounter.LastAverageValue +
							"  [" + AFPSCounter.Instance.fpsCounter.LastAverageMillisecondsValue + " MS]" +
							"\n  MIN: " + AFPSCounter.Instance.fpsCounter.LastMinimumValue +
							"  [" + AFPSCounter.Instance.fpsCounter.LastMinMillisecondsValue + " MS]" +
							"  MAX: " + AFPSCounter.Instance.fpsCounter.LastMaximumValue +
							"  [" + AFPSCounter.Instance.fpsCounter.LastMaxMillisecondsValue + " MS]" +
							"\n  RNDR: [" + AFPSCounter.Instance.fpsCounter.LastRenderValue + " MS]" 
							;

	}
}
