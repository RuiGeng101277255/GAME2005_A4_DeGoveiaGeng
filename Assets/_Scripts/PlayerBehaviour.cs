using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject bullet;
    public float delayTime;
    private float fireDelay;

    public BulletManager bulletManager;

    void start()
    {
        fireDelay = 0.0f;
        delayTime = GetComponent<float>();
    }

    // Update is called once per frame
    void Update()
    {
        if(fireDelay > 0.0f)
        {
            fireDelay -= Time.deltaTime;
        }
        else
        {
            fireDelay = 0.0f;
        }
        _Fire();
    }

    private void _Fire()
    {
        if (Input.GetAxisRaw("Fire1") > 0.0f)
        {
            // delays firing
            if (fireDelay == 0.0f)
            {
                var tempBullet = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
                tempBullet.GetComponent<BulletBehaviour>().direction = bulletSpawn.forward;

                tempBullet.transform.SetParent(bulletManager.gameObject.transform);

                fireDelay = delayTime;
                //bulletManager.shoot(bulletSpawn);
            }

        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            // delays firing
            if (fireDelay == 0.0f)
            {
                //var tempBullet = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
                //tempBullet.GetComponent<BulletBehaviour>().direction = bulletSpawn.forward;

                //tempBullet.transform.SetParent(bulletManager.gameObject.transform);
                Debug.Log("c pressed");
                bulletManager.shoot(bulletSpawn);
                fireDelay = delayTime;
            }
        }

    }
}
