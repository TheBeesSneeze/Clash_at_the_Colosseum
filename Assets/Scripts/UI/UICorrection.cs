///
/// Slap this script onto any ui which is acting funky
/// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities
{
    public class UICorrection : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Correct();
        }

        public void Correct()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
            RectTransform[] bruteForce = GetComponentsInChildren<RectTransform>();
            foreach (RectTransform b in bruteForce)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(b);
            }
        }
    }
}