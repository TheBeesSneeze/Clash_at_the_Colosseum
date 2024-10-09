using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthSystem : MonoBehaviour
{
    public Slider healCharge;
    [SerializeField] private float maxChargeNeeded;
    [SerializeField] private PlayerBehaviour playerBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        InputEvents.Instance.HealStarted.AddListener(heal);
        healCharge.value = 0;
        healCharge.maxValue = maxChargeNeeded;
    }

    public void addCharge(float charge)
    {
        healCharge.value += charge;
    }

    public void heal()
    {
        if (healCharge.value >= maxChargeNeeded)
        {
            print("healing");
            playerBehaviour.RegenHealth();
            healCharge.value = 0;
        }
        else
        {
            print("bar not full");
        }
        return;
    }
}
