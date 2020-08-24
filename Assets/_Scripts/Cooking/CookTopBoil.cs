using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneObjects;

namespace Cooking
{
    public class CookTopBoil : CookTop
    {
        [Header("Lid Detectors")]
        public BoilingLidDetector[] lidDetectors;

        override protected void SyncHeat(HeatSource heatSource)
        {
            if (heatSource.IsOn() && LidIsOn() && !IsHot())
            {
                MakeHot();
            }
            else
            {
                MakeCold();
            }
        }

        private bool LidIsOn()
        {
            bool allDetectorsHaveLid = true;
            foreach (BoilingLidDetector lidDetector in lidDetectors)
            {
                allDetectorsHaveLid = allDetectorsHaveLid && lidDetector.hasLid;
            }

            return allDetectorsHaveLid;
        }

    }
}