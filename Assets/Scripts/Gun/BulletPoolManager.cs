using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager
{
    private static List<GameObject> bulletList = new List<GameObject>();
    private static List<GameObject> basicEnemyBulletList = new List<GameObject>();
    private static List<GameObject> harpyEnemyBulletList = new List<GameObject>();
    private static List<GameObject> cyclopsEnemyBulletList = new List<GameObject>();
    private static GameObject basicEnemyBullet;
    private static GameObject harpyEnemyBullet;
    private static GameObject cyclopsEnemyBullet;
    private static GameObject playerBullet;

    private static GunController gunController;

    public BulletPoolManager(int amountPooled, GameObject bulletPrefab, GameObject enemyBulletPrefab, GameObject harypyBulletPrefab, GameObject cyclopsBulletPrefab)
    {
        gunController = GameObject.FindObjectOfType<GunController>();

        playerBullet = bulletPrefab;
        basicEnemyBullet = enemyBulletPrefab;
        harpyEnemyBullet = harypyBulletPrefab;
        cyclopsEnemyBullet = cyclopsBulletPrefab;

        for (int i = 0; i < amountPooled; i++)
        {
            GameObject enemyShot = GameObject.Instantiate(enemyBulletPrefab);
            GameObject harpyShot = GameObject.Instantiate(harypyBulletPrefab);
            GameObject cyclopsShot = GameObject.Instantiate(cyclopsBulletPrefab);
            basicEnemyBulletList.Add(enemyShot);
            harpyEnemyBulletList.Add(harpyShot);
            cyclopsEnemyBulletList.Add(cyclopsShot);
            enemyShot.SetActive(false);
            cyclopsShot.SetActive(false);
            harpyShot.SetActive(false);
            CreateNewPlayerBullet().SetActive(false);
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
        return CreateNewPlayerBullet();
    }

    public static GameObject InstantiateBasicEnemyBullet(Vector3 position)
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
    public static GameObject InstantiateHarpyEnemyBullet(Vector3 position)
    {
        for (int i = 0; i < harpyEnemyBulletList.Count; i++)
        {
            if (!harpyEnemyBulletList[i].activeInHierarchy)
            {
                harpyEnemyBulletList[i].SetActive(true);
                harpyEnemyBulletList[i].transform.position = position;
                return harpyEnemyBulletList[i];
            }
        }
        GameObject newShot = GameObject.Instantiate(harpyEnemyBullet);
        harpyEnemyBulletList.Add(newShot);
        return newShot;
    }
    public static GameObject InstantiateCyclopsEnemyBullet(Vector3 position)
    {
        for (int i = 0; i < cyclopsEnemyBulletList.Count; i++)
        {
            if (!cyclopsEnemyBulletList[i].activeInHierarchy)
            {
                cyclopsEnemyBulletList[i].SetActive(true);
                cyclopsEnemyBulletList[i].transform.position = position;
                return cyclopsEnemyBulletList[i];
            }
        }
        GameObject newShot = GameObject.Instantiate(cyclopsEnemyBullet);
        cyclopsEnemyBulletList.Add(newShot);
        return newShot;
    }

    public static GameObject CreateNewPlayerBullet()
    {
        GameObject newShot = GameObject.Instantiate(playerBullet);
        bulletList.Add(newShot);
        if (newShot.TryGetComponent<Bullet>(out Bullet b))
        {
            b.OneTimeInitalize(gunController.bulletEffects);
        }
        return newShot;
    }

    public static void Destroy(Bullet bullet)
    {
        bullet.OnDisableBullet();
        bullet.ResetBullet();
        bullet.gameObject.SetActive(false);
    }

    public static void AddPlayerBulletEffect(BulletEffect effect)
    {
        foreach(GameObject bullet in bulletList)
        {
            if(bullet.TryGetComponent<Bullet>(out Bullet b))
            {
                b.AddEffect(effect);
            }
        }
    }

    public void OnDisable()
    {
        Debug.Log("its disablin time");
        bulletList = new List<GameObject>();
    }
}
