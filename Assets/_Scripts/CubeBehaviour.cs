using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Color = UnityEngine.Color;


[System.Serializable]
public class CubeBehaviour : MonoBehaviour
{
    public Vector3 size;
    public Vector3 max;
    public Vector3 min;
    public bool isColliding;
    public bool debug;
    public List<CubeBehaviour> contacts;
    public List<BulletBehaviour> bullet_contacts;
    public float mass;

    private float gravity;
    private float falling_speed;
    private bool isMoving;
    public Vector3 direction;
    public float friction;
    private bool fell;
    public bool activated;

    private MeshFilter meshFilter;
    private Bounds bounds;

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
        debug = true;
        isMoving = false;
        fell = false;
        gravity = -0.98f;
        falling_speed = 0.0f;
        friction = 0.5f;
        direction = new Vector3(0.0f, falling_speed, 0.0f);
        mass = 10.0f;
        meshFilter = GetComponent<MeshFilter>();

        bounds = meshFilter.mesh.bounds;
        size = bounds.size;

    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            max = Vector3.Scale(bounds.max, transform.localScale) + transform.position;
            min = Vector3.Scale(bounds.min, transform.localScale) + transform.position;
            direction = new Vector3(direction.x, falling_speed, direction.z);

            if (isMoving)
            {
                transform.position += direction * Time.deltaTime;
            }

            if (fell)
            {
                direction = new Vector3(direction.x * friction, direction.y, direction.z * friction);
                fell = false;
            }
        }
    }

    void FixedUpdate()
    {
        // physics related calculations
        if (transform.position.y >= -0.5f)
        {
            if (!isColliding)
            {
                isMoving = true;
                falling_speed += gravity * Time.deltaTime;
                transform.position += new Vector3(0.0f, direction.y * Time.deltaTime, 0.0f);
            }
            else
            {
                falling_speed *= -0.5f;
                fell = true;
                //direction = new Vector3(direction.x, direction.y * -1.0f, direction.z) * 0.5f;

                //transform.position += new Vector3(0.0f, 0.2f, 0.0f);
                //isColliding = false;
            }
        }
        else
        {
            if(Mathf.Abs(falling_speed) < 0.01f)
            {
                isMoving = false;
                falling_speed = 0.0f;
            }
            else
            {
                transform.position = new Vector3(transform.position.x, -0.5f, transform.position.z);
                falling_speed *= -0.5f;
                fell = true;
                //direction = new Vector3(direction.x, direction.y * -1.0f, direction.z) * 0.5f;

            }
            //isMoving = false;
            //falling_speed *= -1.0f;
            //transform.position += new Vector3(0.0f, 0.2f, 0.0f);
            //isColliding = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.magenta;

            Gizmos.DrawWireCube(transform.position, Vector3.Scale(new Vector3(1.0f, 1.0f, 1.0f), transform.localScale));
            Gizmos.color = new Color(1.0f, 1.0f, 1.0f);
            Gizmos.DrawWireSphere(transform.position, 1.0f);
        }
    }
}
