using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Serving
{
    public class TipJar : MonoBehaviour
    {
        [Header("Tip System")]
        public int capacity;

        [Header("UI")]
        public Text text;
        public Image fill;

        [Header("FX")]
        public ParticleSystem tip1;
        public ParticleSystem tip2;
        public ParticleSystem tip3;

        [Header("Sounds")]
        public AudioClip sfxSilver;
        public AudioClip sfxGold;
        public AudioClip sfxCash;

        private int amountInJar = 0;
        private AudioSource soundSource;

        public int numCashTips;
        public int numGoldTips;
        public int numSilverTips;

        void Start()
        {
            UpdateUI();
            soundSource = GetComponent<AudioSource>();
        }

        public void AddTip(int amount)
        {
            amountInJar += amount;
            UpdateUI();
            PlayFx(amount);
        }

        public int GetAmountInJar()
        {
            return amountInJar;
        }

        private void UpdateUI()
        {
            text.text = $"{amountInJar}/{capacity}";
            float fillAmount = amountInJar / (float) capacity;

            fill.fillAmount = fillAmount;
        }

        private void PlayFx(int amount)
        {
            if (amount >= 3)
            {
                tip3.Play();
                soundSource.clip = sfxCash;
                soundSource.Play();

                numCashTips++;
            }
            else if (amount == 2)
            {
                tip2.Play();
                soundSource.clip = sfxGold;
                soundSource.Play();

                numGoldTips++;
            }
            else if (amount == 1)
            {
                tip1.Play();
                soundSource.clip = sfxSilver;
                soundSource.Play();

                numSilverTips++;
            }
        }
    }
}