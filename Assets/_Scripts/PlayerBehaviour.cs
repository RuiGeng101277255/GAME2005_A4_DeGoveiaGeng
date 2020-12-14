using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject bullet;
    public int fireRate;


    public BulletManager bulletManager;

    void start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            // delays firing
            //if (Time.frameCount % fireRate == 0)
           // {
                //var tempBullet = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
                //tempBullet.GetComponent<BulletBehaviour>().direction = bulletSpawn.forward;

                //tempBullet.transform.SetParent(bulletManager.gameObject.transform);
                Debug.Log("c pressed");
                bulletManager.shoot(bulletSpawn);
            //}
        }
        _Fire();
    }

    private void _Fire()
    {
        if (Input.GetAxisRaw("Fire1") > 0.0f)
        {
            // delays firing
            if (Time.frameCount % fireRate == 0)
            {
                //var tempBullet = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
                //tempBullet.GetComponent<BulletBehaviour>().direction = bulletSpawn.forward;

                //tempBullet.transform.SetParent(bulletManager.gameObject.transform);

                bulletManager.shoot(bulletSpawn);
            }

        }
        
    }
}
