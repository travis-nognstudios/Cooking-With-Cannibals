using UnityEngine;

namespace SceneObjects
{
    public class SodaBottlePour : MonoBehaviour
    {
        public ParticleSystem pourEffect;
        public Renderer liquidRenderer;

        [Range(0f,1f)]
        public float startingFillAmount;
        [Range(0.01f, 0.8f)]
        public float pourRate = 0.2f;
        

        private float currentFillAmount;
        private float pourAmountPerFrame;

        private readonly float pourMinAngle = 90f;
        private readonly float pourMaxAngle = 270f;

        private readonly string liquidShaderFillProperty = "Vector1_86B367DE";

        void Start()
        {

            currentFillAmount = startingFillAmount;

        }

        // Update is called once per frame
        void Update()
        {
            pourAmountPerFrame = pourRate * Time.deltaTime;

            DepleteLiquidAmount();
            ControlPour();

            float rotation = GetRotationAmount();

            float shaderFill = CalculateShaderFillAmount(currentFillAmount, rotation);
            ControlLiquidLevel(shaderFill);
        }

        bool IsPouring()
        {
            float x = transform.rotation.eulerAngles.x;
            float z = transform.rotation.eulerAngles.z;

            return IsInPourRange(x) || IsInPourRange(z);
        }

        bool IsInPourRange(float rotation)
        {
            if (rotation > pourMinAngle && rotation < pourMaxAngle)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool HasLiquid()
        {
            return currentFillAmount > 0;
        }

        void ControlPour()
        {
            if (IsPouring() && HasLiquid() && !pourEffect.isPlaying)
            {
                pourEffect.Play();
            }
            else if ((!IsPouring() && pourEffect.isPlaying) || !HasLiquid() )
            { 
                pourEffect.Stop();
            }
        }

        // Map 0->360 to 0->180
        private float NormalizeRotation(float rotation)
        {
            if (rotation >=0 && rotation <=180)
            {
                return rotation;
            }
            else
            {
                return 360 - rotation;
            }
        }

        private float GetRotationAmount()
        {
            float x = transform.rotation.eulerAngles.x;
            float normX = NormalizeRotation(x);

            float z = transform.rotation.eulerAngles.z;
            float normZ = NormalizeRotation(z);

            float normalizedRotation = Mathf.Max(normX, normZ);
            return normalizedRotation;
        }

        private void DepleteLiquidAmount()
        {
            if (IsPouring() && currentFillAmount > 0)
            {
                currentFillAmount -= pourAmountPerFrame;
                if (currentFillAmount < 0) currentFillAmount = 0;
            }
        }

        void ControlLiquidLevel(float shaderFill)
        {
            liquidRenderer.material.SetFloat(liquidShaderFillProperty, shaderFill);
        }

        /* 
        WARNING:
        Complex math & logic to control the bottle filling liquid shader

        Explanation:
        The shader works by controlling a property called "Fill Amount"
        But confusingly, it's actually the "emptiness" amount, meaning
        that higher "Fill Amount" actually makes the bottle less full of liquid
        And it also doesn't neatly go from 0 to 1

        Depending on the angle of the bottle, the "Fill Amount" makes the bottle seem more or less full
        to give an illusion of pouring
        But the illusion breaks when the bottle is tilted too much since it looks more full
        of liquid when tilted

        Below is the logic to modify the "Fill Amount" property dynamically so that
        the liquid level seems consistent at all tilt angles

        But first, some experiments I did to find what the "Fill Amount" values are at
        different angles and different liquid amounts
        All "magic numbers" used in calculations are derived from these values

        ANGLE   FULL	HALF	EMPTY	RANGE
        0		0.37	0.48	0.59	0.22
        45		0.42     --     0.62	0.20
        90		0.55	0.59	0.63	0.08
        135		0.56	 --		0.76	0.20
        180		0.59	0.70	0.81	0.22

        Derived formulas:
            Range = 0.14 * -sin(x) + 0.22
            >> OLD >> Min(full value) based on angle: log10(angle) / 10.2512 + 0.37

            >> NEW >> Min(full value):
                if 0<=angle<=90:  0.056*-log(-x+91)+0.48
                if 90<angle<=180: 0.056* log( x-89)+0.48

            >> NEW V2: Quadratic formulas for gradient control
                if 0<=angle<=90:  ((-x+90)^0.25)/-28 + 0.48
                if 90<angle<=180: (( x-90)^0.25)/ 28 + 0.48

            >> V3: STRAIGHT LINES GIVE THE BEST RESULTS FML
                if 0<=angle<=90: (0.18/90) *  angle     + 0.37  -> y=mx+c
                if 90<angle<=180:(0.04/90) * (angle-90) + 0.55  -> y=mx+c

            ^^ ToDo: Try to find an arc that's described by the 3 points (x=0,90,180)
                     It might be more accurate that the lines

        Note that since the shader works with the emptiness and not the fullness,
        this relationship needs to be followed to convert between fullness to emptiness
            EMPTY = Inverse of FILL
        where the value of fullness/emptiness ranges [0,1] and the inverse is 1-x

        */

        private float FillInvert(float amount)
        {
            return 1 - amount;
        }

        private float ToRadian(float degree)
        {
            return degree * Mathf.PI / 180;
        }

        // Calculates the "RANGE" value shown in the table above, given the ANGLE
        private float EmptyRangeAtAngle(float angle)
        {
            // Values from experiement, shown above
            // Use offset to reduce range, to compensate for calculation inaccuracy, if needed
            float offset = 0f;
            float maxRange = 0.22f - offset;
            float minRange = 0.08f - offset;
            float rangeSpread = maxRange - minRange;

            return rangeSpread * -Mathf.Sin(ToRadian(angle)) + maxRange;
        }

        // Calculates the "FULL" value shown in the table above, given the ANGLE
        private float MinEmptyAtAngle(float angle)
        {
            if (angle <= 0 || angle > 180)
            {
                return 0.37f;
            }
            else
            {
                if (angle > 0 && angle < 90)
                {
                    return (0.18f/90) * angle + 0.37f;
                }
                else
                {
                    return (0.04f / 90) * (angle - 90) + 0.55f;
                }
            }
        }

        // Find the value for the shader's "Fill Amount" field
        // given the actual fill amount [0,1] and the rotation angle
        private float CalculateShaderFillAmount(float fill, float rotation)
        {
            float shaderValueForEmpty = 1f;
            if (fill == 0) return shaderValueForEmpty;

            float empty = FillInvert(fill);
            float min = MinEmptyAtAngle(rotation);
            float max = min + EmptyRangeAtAngle(rotation);

            float shaderValue = empty * (max - min) + min;
            return shaderValue;
        }
    }
}