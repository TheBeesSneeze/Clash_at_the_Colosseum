using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    public List<GameObject> bulletList = new List<GameObject>();

    [Tooltip ("Amount that gets pooled, won't need serializefield later")]
    [SerializeField] private int amountToPool;
    [SerializeField] private GameObject bullet;

    public static BulletPoolManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject shot = Instantiate(bullet);
            bulletList.Add(shot);
            shot.SetActive(false);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < bulletList.Count; i++)
        {
            if (!bulletList[i].activeInHierarchy)
            {
                bulletList[i].SetActive(true);
                return bulletList[i];
            }
        }
        GameObject newShot = Instantiate(bullet);
        bulletList.Add(newShot);
        return newShot;
    }


}
