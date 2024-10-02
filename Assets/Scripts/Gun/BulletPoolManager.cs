using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager
{
    private static List<GameObject> bulletList = new List<GameObject>();
    private static List<GameObject> basicEnemyBulletList = new List<GameObject>();
    private static GameObject basicEnemyBullet;
    private static GameObject harpyEnemyBullet;
    private static GameObject cyclopsEnemyBullet;
    private static GameObject playerBullet;

    public BulletPoolManager(int amountPooled, GameObject bulletPrefab, GameObject enemyBulletPrefab, GameObject harypyBulletPrefab, GameObject cyclopsBulletPrefab)
    {
        playerBullet = bulletPrefab;
        basicEnemyBullet = enemyBulletPrefab;
        harpyEnemyBullet = harypyBulletPrefab;
        cyclopsEnemyBullet = cyclopsBulletPrefab;

        for (int i = 0; i < amountPooled; i++)
        {
            GameObject shot = GameObject.Instantiate(bulletPrefab);
            GameObject enemyShot = GameObject.Instantiate(enemyBulletPrefab);
            bulletList.Add(shot);
            basicEnemyBulletList.Add(enemyShot);
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
        for (int i = 0; i < basicEnemyBulletList.Count; i++)
        {
            if (!basicEnemyBulletList[i].activeInHierarchy)
            {
                basicEnemyBulletList[i].SetActive(true);
                basicEnemyBulletList[i].transform.position = position;
                return basicEnemyBulletList[i];
            }
        }
        GameObject newShot = GameObject.Instantiate(basicEnemyBullet);
        basicEnemyBulletList.Add(newShot);
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
