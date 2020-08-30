using UnityEngine;

namespace SceneObjects
{
    public class SodaBottlePour : MonoBehaviour
    {
        public ParticleSystem pourEffect;
        private Rigidbody rb;

        private float pourMinAngle = 90f;
        private float pourMaxAngle = 270f;

        // Use this for initialization
        void Start()
        {
            rb = GetComponent<Rigidbody>();

            float fullAt0 = CalculateShaderFillAmount(1f, 0f);
            float halfAt0 = CalculateShaderFillAmount(0.5f, 0f);
            float emptyAt0 = CalculateShaderFillAmount(0f, 0f);

            Debug.Log($"90: {fullAt0} {halfAt0} {emptyAt0}");

            float fullAt90 = CalculateShaderFillAmount(1f, 90f);
            float halfAt90 = CalculateShaderFillAmount(0.5f, 90f);
            float emptyAt90 = CalculateShaderFillAmount(0f, 90f);

            Debug.Log($"90: {fullAt90} {halfAt90} {emptyAt90}");

            float fullAt180 = CalculateShaderFillAmount(1f, 180f);
            float halfAt180 = CalculateShaderFillAmount(0.5f, 180f);
            float emptyAt180 = CalculateShaderFillAmount(0f, 180f);

            Debug.Log($"`80: {fullAt180} {halfAt180} {emptyAt180}");
        }

        // Update is called once per frame
        void Update()
        {
            ControlPour();
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

        void ControlPour()
        {
            if (IsPouring() && !pourEffect.isPlaying)
            {
                pourEffect.Play();
            }
            else if (!IsPouring() && pourEffect.isPlaying)
            {
                pourEffect.Stop();
            }
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
            Min(full value) based on angle: log10(angle) / 10.2512 + 0.37

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
            return 0.14f * -Mathf.Sin(ToRadian(angle)) + 0.22f;
        }

        // Calculates the "FULL" value shown in the table above, given the ANGLE
        private float MinEmptyAtAngle(float angle)
        {
            if (angle == 0)
            {
                return 0.37f;
            }
            else
            {
                return Mathf.Log10(angle) / 10.2512f + 0.37f;
            }
        }

        // Find the value for the shader's "Fill Amount" field
        // given the actual fill amount [0,1] and the rotation angle
        private float CalculateShaderFillAmount(float fill, float rotation)
        {
            if (fill < 0 || fill > 1) throw new System.Exception("Fill amount must be in range [0,1]");

            float empty = FillInvert(fill);
            float min = MinEmptyAtAngle(rotation);
            float max = min + EmptyRangeAtAngle(rotation);

            float shaderValue = empty * (max - min) + min;
            return shaderValue;
        }
    }
}