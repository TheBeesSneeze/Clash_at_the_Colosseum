using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager
{
    private static List<GameObject> bulletList = new List<GameObject>();
    private static List<GameObject> enemyBulletList = new List<GameObject>();
    private static GameObject enemyBullet;
    private static GameObject playerBullet;

    public BulletPoolManager(int amountPooled, GameObject bulletPrefab, GameObject enemyBulletPrefab)
    {
        playerBullet = bulletPrefab;
        enemyBullet = enemyBulletPrefab;

        for (int i = 0; i < amountPooled; i++)
        {
            GameObject shot = GameObject.Instantiate(bulletPrefab);
            GameObject enemyShot = GameObject.Instantiate(enemyBulletPrefab);
            bulletList.Add(shot);
            enemyBulletList.Add(enemyShot);
            enemyShot.SetActive(false);
            shot.SetActive(false);
        }
    }

    public static GameObject Instantiate(Vector3 position)
    {
        for (int i = 0; i < bulletList.Count; i++)
        {
            if (!bulletList[i].activeInHierarchy)
            {
                bulletList[i].SetActive(true);
                bulletList[i].transform.position = position;
                return bulletList[i];
            }
        }
        GameObject newShot = GameObject.Instantiate(playerBullet);
        bulletList.Add(newShot);
        return newShot;
    }

    public static GameObject InstantiateEnemyBullet(Vector3 position)
    {
        for (int i = 0; i < enemyBulletList.Count; i++)
        {
            if (!enemyBulletList[i].activeInHierarchy)
            {
                enemyBulletList[i].SetActive(true);
                enemyBulletList[i].transform.position = position;
                return enemyBulletList[i];
            }
        }
        GameObject newShot = GameObject.Instantiate(enemyBullet);
        enemyBulletList.Add(newShot);
        return newShot;
    }

    public static void Destroy(Bullet bullet)
    {
        bullet.ResetBullet();
        bullet.gameObject.SetActive(false);
    }

    public void OnDisable()
    {
        Debug.Log("its disablin time");
        bulletList = new List<GameObject>();
    }
}
