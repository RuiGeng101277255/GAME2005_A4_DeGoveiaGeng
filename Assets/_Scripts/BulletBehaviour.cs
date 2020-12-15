using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletBehaviour : MonoBehaviour
{
    public float speed;
    private float current_speed;
    public Vector3 direction;
    public float range;
    public float mass;
    public float friction;
    public float gravity;

    public bool debug;
    public Vector3 scale;
    public Vector3 max;
    public Vector3 min;
    private float radius;

    private MeshFilter bulletMeshFilter;
    private Bounds bounds;

    public List<CubeBehaviour> cube_contacts;
    public List<BulletBehaviour> bullet_contacts;

    //pol
    public bool inUse;
    public bool isColliding;
    public enum typeCollision
    {
        NONE,
        TOP_DOWN,
        SIDES,
        FRONT_BACK
    };
    public typeCollision Type;

    // Start is called before the first frame update
    void Start()
    {
        Type = typeCollision.NONE;
        mass = 1.0f;
        gravity = -0.98f;
        debug = false;
        isColliding = false;
        _reset();
        _calcRadius(scale);

        bulletMeshFilter = GetComponent<MeshFilter>();
        bounds = bulletMeshFilter.mesh.bounds;
        scale = bounds.size;
        _calcRadius(scale);
    }

    // Update is called once per frame
    void Update()
    {
        max = Vector3.Scale(bounds.max, transform.localScale) + transform.position;
        min = Vector3.Scale(bounds.min, transform.localScale) + transform.position;

        direction = new Vector3(direction.x, direction.y + gravity * Time.deltaTime, direction.z);

        if (inUse)
        {
            _Move();
            _CheckBounds();
            if (Mathf.Abs(current_speed) < 0.5f)
            {
                _reset();
            }
        }
    }

    private void _Move()
    {
        transform.position += direction * current_speed * Time.deltaTime;
    }

    private void _CheckBounds()
    {
        if (Vector3.Distance(transform.position, Vector3.zero) > range)
        {
            //Destroy(gameObject);
            _reset();
        }
        //if(isColliding)
        //{
        //    direction.z *= -1.0f;
        //    transform.position -= new Vector3(0.0f, 0.0f, 1.0f);
        //    isColliding = false;
        //}

        //Vector3 MoveAmount = new Vector3(0.0f, 0.0f, 0.0f);
        switch (Type)
        {
            case typeCollision.TOP_DOWN:
                direction.y *= -0.75f;
                //if(direction.y > 0.0f)
                //{
                //    MoveAmount.y += 0.2f;
                //}
                //else
                //{
                //    MoveAmount.y -= 0.2f;
                //}
                //transform.position += MoveAmount;
                
                break;
            case typeCollision.SIDES:
                direction.x *= -0.75f;
                //if (direction.x > 0.0f)
                //{
                //    MoveAmount.x += 0.2f;
                //}
                //else
                //{
                //    MoveAmount.x -= 0.2f;
                //}
                //transform.position += MoveAmount;
                
                break;
            case typeCollision.FRONT_BACK:
                direction.z *= -0.75f;
                //if (direction.z > 0.0f)
                //{
                //    MoveAmount.z += 0.2f;
                //}
                //else
                //{
                //    MoveAmount.z -= 0.2f;
                //}
                //transform.position += MoveAmount;
                
                break;
            case typeCollision.NONE:
                //MoveAmount = new Vector3(0.0f, 0.0f, 0.0f);
                break;
        }
        Type = typeCollision.NONE;

        if (transform.position.y <= -0.5f)
        {
            Type = typeCollision.TOP_DOWN;
            transform.position = new Vector3(transform.position.x, -0.5f, transform.position.z);
        }
    }

    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = new Color(1.0f, 1.0f, 1.0f);
            Gizmos.DrawWireSphere(transform.position, 0.05f);
        }
    }

    private void _calcRadius(Vector3 size)
    {
        float x = size.x;// /2.0f;
        float y = size.y;// /2.0f;
        float z = size.z;// /2.0f;
        radius = Mathf.Sqrt(x * x + y * y + z * z);
        Debug.Log("Bullet radius: " + radius);
    }

    private void _reset()
    {
        inUse = false;
        current_speed = speed;
        Type = typeCollision.NONE;
        transform.position = new Vector3(0.0f, -1000.0f, 0.0f);
    }

    public void activate(Transform tra)
    {
        //transform.SetParent(tra);
        direction = tra.forward;
        transform.position = tra.position;
        inUse = true;
        Type = typeCollision.NONE;
        Debug.Log("Bullet Shot");
    }
}
