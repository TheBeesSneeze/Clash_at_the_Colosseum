using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

namespace UI
{
    public class FadeToBlack : MonoBehaviour
    {
        public Image FadeImage;
        private bool fadeStart = false;
        [SerializeField] private int Delay;
        public float FadeTime;
        private float timePassed;

        void Start()
        {
            PublicEvents.HydraDeath.AddListener(Fading);
        }

        async public void Fading()
        {
            await Task.Delay((int)(Delay * 1000));
            while (timePassed < FadeTime)
            {
                timePassed += Time.deltaTime;
                FadeImage.color = new Color(FadeImage.color.r, FadeImage.color.g, FadeImage.color.b, timePassed / FadeTime);
                if (timePassed >= FadeTime)
                {
                    Debug.LogWarning("replace this");
                    SceneManager.LoadScene("WinScreen");
                }

                await Task.Yield();
            }


        }

    }
}
