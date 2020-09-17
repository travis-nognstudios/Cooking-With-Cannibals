using UnityEngine;

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
            else if (!heatSource.IsOn() || !LidIsOn())
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

        override protected void ManageSmoke()
        {
            if (smoke != null)
            {
                if (hasFood && LidIsOn())
                {
                    smoke.CookSmoke();
                }
                else
                {
                    smoke.ClearSmoke();
                }
            }
        }

    }
}