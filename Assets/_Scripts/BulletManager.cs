using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public int PoolSize;

    public List<BulletBehaviour> bullet_Pool;
    public BulletBehaviour bullet_object;

    // Start is called before the first frame update
    void Start()
    {
        bullet_Pool = new List<BulletBehaviour>();
        for(int a = 0; a<PoolSize; a++)
        {
            //Debug.Log("Bullet # " + a.ToString());
            BulletBehaviour b = (BulletBehaviour)Instantiate(bullet_object);
            bullet_Pool.Add(b);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shoot(Transform tra)
    {
        for (int i = 0; i < PoolSize; i++)
        {
            if(!bullet_Pool[i].inUse)
            {
                bullet_Pool[i].activate(tra);
                bullet_Pool[i].transform.SetParent(bullet_Pool[i].transform);
                i += PoolSize;
            }
        }
    }
}
