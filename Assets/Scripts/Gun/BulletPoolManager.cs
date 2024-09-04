using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager
{
    private static List<GameObject> bulletList = new List<GameObject>();
    private static GameObject bullet;

    public BulletPoolManager(int amountPooled, GameObject bulletPrefab)
    {
        bullet = bulletPrefab;

        for (int i = 0; i < amountPooled; i++)
        {
            GameObject shot = GameObject.Instantiate(bulletPrefab);
            bulletList.Add(shot);
            shot.SetActive(false);
        }
    }

    public static GameObject GetPooledObject()
    {
        for (int i = 0; i < bulletList.Count; i++)
        {
            if (!bulletList[i].activeInHierarchy)
            {
                bulletList[i].SetActive(true);
                return bulletList[i];
            }
        }
        GameObject newShot = GameObject.Instantiate(bullet);
        bulletList.Add(newShot);
        return newShot;
    }


}
