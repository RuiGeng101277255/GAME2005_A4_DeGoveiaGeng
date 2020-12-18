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
    public SceneManager m_sceneManager;

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
        Debug.Log(transform.position);
    }

    private void _Fire()
    {
        if (Input.GetAxisRaw("Fire1") > 0.0f)
        {
            // delays firing
            if (fireDelay == 0.0f)
            {
                //var tempBullet = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
                //tempBullet.GetComponent<BulletBehaviour>().direction = bulletSpawn.forward;

                //tempBullet.transform.SetParent(bulletManager.gameObject.transform);

                //fireDelay = delayTime;
                //bulletManager.shoot(bulletSpawn);

                Debug.Log("c pressed");
                bulletManager.shoot(bulletSpawn);
                fireDelay = delayTime;
            }

        }

        if (Input.GetKey(KeyCode.M))
        {
            //Main menu
            m_sceneManager.GoToScene("StartScene");
        }

    }
}
