﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityModManagerNet;
using HarmonyLib;
#pragma warning disable

namespace Overlayer
{
    public class Settings : UnityModManager.ModSettings, IDrawable
    {
        public void OnChange() { }
        public static void Load(UnityModManager.ModEntry modEntry)
            => Instance = Load<Settings>(modEntry);
        public static void Save(UnityModManager.ModEntry modEntry)
            => Save(Instance, modEntry);
        public static Settings Instance { get; private set; }
        [Draw("Decimals On Displaying Accuracy")]
        public int AccuracyDecimals = 2;
        [Draw("Decimals On Displaying XAccuracy")]
        public int XAccuracyDecimals = 2;
        [Draw("Decimals On Displaying HitTiming")]
        public int TimingDecimals = 2;
        [Draw("Decimals On Displaying Progress")]
        public int ProgressDecimals = 2;
        [Draw("Decimals On Displaying Best Progress")]
        public int BestProgDecimals = 2;
        [Draw("Decimals On Displaying Start Progress")]
        public int StartProgDecimals = 2;
        [Draw("Decimals On Displaying Perceived Bpm")]
        public int PerceivedBpmDecimals = 2;
        [Draw("Decimals On Displaying Tile Bpm")]
        public int TileBpmDecimals = 2;
        [Draw("Decimals On Displaying Perceived KPS")]
        public int PerceivedKpsDecimals = 2;
        [Draw("Decimals On Displaying Current KPS")]
        public int KPSDecimals = 2;
        [Draw("Decimals On Displaying Current FPS")]
        public int FPSDecimals = 2;
        [Draw("Decimals On Displaying Current Frametime")]
        public int FrametimeDecimals = 2;
        [Draw("Reset Stats On Start")]
        public bool Reset = true;
        [Draw("KPS UpdateRate")]
        public int KPSUpdateRate = 20;
        [Draw("FPS UpdateRate")]
        public int FPSUpdateRate = 500;
        [Draw("FrameTime UpdateRate")]
        public int FrameTimeUpdateRate = 500;
        [Draw("Unlock ErrorMeter On Auto")]
        public bool UnlockErrorMeterAtAuto = true;
        [Draw("Add Multipress At ErrorMeter")]
        public bool AddMultipressAtErrorMeter = true;
    }
}
