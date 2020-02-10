﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cooking
{
    public class Smoke : MonoBehaviour
    {

        public ParticleSystem cookingSmoke;
        public ParticleSystem burningSmoke;


        void Start()
        {
            cookingSmoke.Stop();
            burningSmoke.Stop();
        }


        public void clearSmoke()
        {
            if (cookingSmoke.isPlaying)
                cookingSmoke.Stop();
            if (burningSmoke.isPlaying)
                burningSmoke.Stop();
        }

        public void cookSmoke()
        {
            if (!cookingSmoke.isPlaying)
                cookingSmoke.Play();
            if (burningSmoke.isPlaying)
                burningSmoke.Stop();
        }

        public void burnSmoke()
        {
            if (cookingSmoke.isPlaying)
                cookingSmoke.Stop();
            if (!burningSmoke.isPlaying)
                burningSmoke.Play();
        }

    }
}