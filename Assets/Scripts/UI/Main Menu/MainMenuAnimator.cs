///
/// Unity animator broke so i get to hardcode it! yay!
///

using mainMenu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mainMenu
{
    public class MainMenuAnimator : MonoBehaviour
    {
        MainMenuInitializer menu;
        // Start is called before the first frame update
        void Start()
        {
            menu = GetComponent<MainMenuInitializer>();
        }

        public void Start_toGunSelect()
        {
            StartCoroutine(Start_toGunSelectAnimation());
        }

        private IEnumerator Start_toGunSelectAnimation()
        {
            yield return null;
        }
    }

}