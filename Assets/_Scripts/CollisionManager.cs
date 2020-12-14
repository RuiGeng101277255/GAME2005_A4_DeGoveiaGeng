using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CollisionManager : MonoBehaviour
{
    public CubeBehaviour[] actors;
    public BulletBehaviour[] actors2;

    // Start is called before the first frame update
    void Start()
    {
        actors = FindObjectsOfType<CubeBehaviour>();
        actors2 = FindObjectsOfType<BulletBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
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
        for (int i = 0; i < actors.Length; i++)
        {
            for (int j = 0; j < actors2.Length; j++)
            {
                if (i != j)
                {
                    CheckAABBs_Bullet(actors[i], actors2[j]);
                }
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

    public static void CheckAABBs_Bullet(CubeBehaviour a, BulletBehaviour b)
    {
        if ((a.min.x <= b.max.x && a.max.x >= b.min.x) &&
            (a.min.y <= b.max.y && a.max.y >= b.min.y) &&
            (a.min.z <= b.max.z && a.max.z >= b.min.z))
        {
            if (!a.contacts2.Contains(b))
            {
                a.contacts2.Add(b);
                a.isColliding = true;
            }
        }
        else
        {
            if (a.contacts2.Contains(b))
            {
                a.contacts2.Remove(b);
                a.isColliding = false;
            }

        }
    }
}
