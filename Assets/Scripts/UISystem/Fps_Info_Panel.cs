using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RougeFW;
using TMPro;
using CodeStage.AdvancedFPSCounter;
using System;

public class Fps_Info_Panel : UIPanel
{
    public TextMeshProUGUI fps_info;
    public TextMeshProUGUI memory_info;
    private void Update()
    {
        UpdateFpsInfo();
    }
    private void UpdateFpsInfo()
    {
        fps_info.text = "FPS:" + AFPSCounter.Instance.fpsCounter.LastValue + Environment.NewLine
            + "AVG:" + AFPSCounter.Instance.fpsCounter.LastAverageValue + Environment.NewLine
            + "MIN:" + AFPSCounter.Instance.fpsCounter.LastMinimumValue + Environment.NewLine
            + "Max:" + AFPSCounter.Instance.fpsCounter.LastMaximumValue;
        memory_info.text = "Memory:" + Environment.NewLine
            + "Total:" + AFPSCounter.Instance.memoryCounter.LastTotalValue / (float)CodeStage.AdvancedFPSCounter.CountersData.MemoryCounterData.MemoryDivider + Environment.NewLine
            + "Allocated:" + AFPSCounter.Instance.memoryCounter.LastAllocatedValue + Environment.NewLine
            + "Mono:" + AFPSCounter.Instance.memoryCounter.LastMonoValue + Environment.NewLine
            + "Gfx:" + AFPSCounter.Instance.memoryCounter.LastGfxValue;
            ;
            
    }
           
}
