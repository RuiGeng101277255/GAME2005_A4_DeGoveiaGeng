using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CollisionManager : MonoBehaviour
{
    public CubeBehaviour[] actors;
    public BulletBehaviour[] bullet_actors;

    // Start is called before the first frame update
    void Start()
    {
        actors = FindObjectsOfType<CubeBehaviour>();
        bullet_actors = FindObjectsOfType<BulletBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        //Cube checks
        for (int i = 0; i < actors.Length; i++)
        {
            for (int j = 0; j < actors.Length; j++)
            {
                if (i != j)
                {
                    CheckAABBs(actors[i], actors[j]);
                }
            }
        }

        //Bullet checks
        for (int b = 0; b < bullet_actors.Length; b++)
        {
            for (int c = 0; c < actors.Length; c++)
            {
                BulletCubeAABBs(bullet_actors[b], actors[c]);
            }
        }
    }

    public static void CheckAABBs(CubeBehaviour a, CubeBehaviour b)
    {
        if ((a.min.x <= b.max.x && a.max.x >= b.min.x) &&
            (a.min.y <= b.max.y && a.max.y >= b.min.y) &&
            (a.min.z <= b.max.z && a.max.z >= b.min.z))
        {
            if (!a.contacts.Contains(b))
            {
                a.contacts.Add(b);
                a.isColliding = true;
            }
        }
        else
        {
            if (a.contacts.Contains(b))
            {
                a.contacts.Remove(b);
                a.isColliding = false;
            }
           
        }
    }

    public static void BulletCubeAABBs(BulletBehaviour b, CubeBehaviour c)
    {
        if ((b.min.x <= c.max.x && b.max.x >= c.min.x) &&
            (b.min.y <= c.max.y && b.max.y >= c.min.y) &&
            (b.min.z <= c.max.z && b.max.z >= c.min.z))
        {
            if (!b.cube_contacts.Contains(c))
            {
                b.cube_contacts.Add(c);
                b.isColliding = true;
            }
        }
        else
        {
            if (b.cube_contacts.Contains(c))
            {
                b.cube_contacts.Remove(c);
                b.isColliding = false;
            }

        } 
    }
}
