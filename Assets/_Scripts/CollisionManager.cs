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
                switch (LargestSeparation_Cube(a, b))
                {
                    case 'x':
                        a.Type = CubeBehaviour.typeCollision.SIDES;
                        break;
                    case 'y':
                        a.Type = CubeBehaviour.typeCollision.TOP_DOWN;
                        break;
                    case 'z':
                        a.Type = CubeBehaviour.typeCollision.FRONT_BACK;
                        break;
                }
            }
        }
        else
        {
            if (a.contacts.Contains(b))
            {
                a.contacts.Remove(b);
                a.isColliding = false;
                a.Type = CubeBehaviour.typeCollision.NONE;
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
                switch(LargestSeparation_Bullet(b, c))
                {
                    case 'x':
                        b.Type = BulletBehaviour.typeCollision.SIDES;
                        break;
                    case 'y':
                        b.Type = BulletBehaviour.typeCollision.TOP_DOWN;
                        break;
                    case 'z':
                        b.Type = BulletBehaviour.typeCollision.FRONT_BACK;
                        break;
                }
                _updateDirection(b, c, b.Type);
            }
        }
        else
        {
            if (b.cube_contacts.Contains(c))
            {
                b.cube_contacts.Remove(c);
                b.isColliding = false;
                b.Type = BulletBehaviour.typeCollision.NONE;
            }

        } 
    }
    public static char LargestSeparation_Bullet(BulletBehaviour b, CubeBehaviour c)
    {
        float distX = Mathf.Abs(b.transform.position.x - c.transform.position.x);
        float distY = Mathf.Abs(b.transform.position.y - c.transform.position.y);
        float distZ = Mathf.Abs(b.transform.position.z - c.transform.position.z);

        List<float> distanceDiff = new List<float>();
        distanceDiff.Add(distX);
        distanceDiff.Add(distY);
        distanceDiff.Add(distZ);
        distanceDiff.Sort();
        
        if(distanceDiff[2] == distX)
        {
            return 'x';
        }
        else if (distanceDiff[2] == distY)
        {
            return 'y';
        }
        else
        {
            return 'z';
        }
    }
    public static char LargestSeparation_Cube(CubeBehaviour c1, CubeBehaviour c2)
    {
        float distX = Mathf.Abs(c1.transform.position.x - c2.transform.position.x);
        float distY = Mathf.Abs(c1.transform.position.y - c2.transform.position.y);
        float distZ = Mathf.Abs(c1.transform.position.z - c2.transform.position.z);

        List<float> distanceDiff = new List<float>();
        distanceDiff.Add(distX);
        distanceDiff.Add(distY);
        distanceDiff.Add(distZ);
        distanceDiff.Sort();

        if (distanceDiff[2] == distX)
        {
            return 'x';
        }
        else if (distanceDiff[2] == distY)
        {
            return 'y';
        }
        else
        {
            return 'z';
        }
    }

    private static void _updateDirection(BulletBehaviour b, CubeBehaviour c, BulletBehaviour.typeCollision ty)
    {
        Vector3 b_pos = b.transform.position;
        Vector3 c_pos = c.transform.position;
        float centerDst = Mathf.Sqrt((b_pos.x - c_pos.x)* (b_pos.x - c_pos.x) + (b_pos.y - c_pos.y) * (b_pos.y - c_pos.y) + (b_pos.z - c_pos.z) * (b_pos.z - c_pos.z));
        Vector3 centerDiff = b_pos - c_pos;

        //Normalize
        centerDiff /= centerDst; //direction cube will be going

        //momentum m1v1 + m2u1 = m1v2 + m2u2

        float total_momentum = b.mass * getVectorLength(b.direction) + c.mass * getVectorLength(c.direction);

        c.direction -= centerDiff * b.mass / c.mass;

        //b.direction += centerDiff * c.mass / b.mass;

        //float v_b = ((b.mass - c.mass)/(b.mass + c.mass));
        //float v_c = ((c.mass - b.mass) / (c.mass + b.mass));

        //b.direction *= 1.0f / v_b;

        switch (ty)
        {
            case BulletBehaviour.typeCollision.FRONT_BACK:
                break;
            case BulletBehaviour.typeCollision.SIDES:
                break;
            case BulletBehaviour.typeCollision.TOP_DOWN:
                break;
        }

    }

    private static float getVectorLength(Vector3 vec)
    {
        return Mathf.Sqrt(vec.x * vec.x + vec.y * vec.y + vec.z * vec.z);
    }
}
