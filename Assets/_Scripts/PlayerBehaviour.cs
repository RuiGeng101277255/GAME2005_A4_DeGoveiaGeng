using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject bullet;
    public int fireRate;


    public BulletManager bulletManager;
    public BulletBehaviour[] m_BulletList;

    void start()
    {
        m_BulletList = FindObjectsOfType<BulletBehaviour>();

        for (int m = 0; m < m_BulletList.Length; m++)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        _Fire();
    }

    private void _Fire()
    {
        if (Input.GetAxisRaw("Fire1") > 0.0f)
        {
            // delays firing
            if (Time.frameCount % fireRate == 0)
            {
                var tempBullet = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
                tempBullet.GetComponent<BulletBehaviour>().direction = bulletSpawn.forward;

                tempBullet.transform.SetParent(bulletManager.gameObject.transform);

                //for (int n = 0; n < m_BulletList.Length; n++)
                //{
                //    if(!m_BulletList[n].spawned)
                //    {
                //        m_BulletList[n]._activate(bulletSpawn);
                //        n += m_BulletList.Length;
                //    }
                //}
            }

        }
    }
}
