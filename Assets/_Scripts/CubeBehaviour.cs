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

    private float gravity;
    private float falling_speed;
    private bool isMoving;

    private MeshFilter meshFilter;
    private Bounds bounds;

    public enum typeCollision
    {
        TOP_DOWN,
        SIDES,
        FRONT_BACK,
        NONE
    };
    public typeCollision Type;
    

    // Start is called before the first frame update
    void Start()
    {
        Type = typeCollision.NONE;
        debug = true;
        isMoving = false;
        gravity = -0.98f;
        falling_speed = 0.0f;
        meshFilter = GetComponent<MeshFilter>();

        bounds = meshFilter.mesh.bounds;
        size = bounds.size;

    }

    // Update is called once per frame
    void Update()
    {
        max = Vector3.Scale(bounds.max, transform.localScale) + transform.position;
        min = Vector3.Scale(bounds.min, transform.localScale) + transform.position;

        if(isMoving)
        {
            transform.position += new Vector3(0.0f, falling_speed * Time.deltaTime, 0.0f);
        }
    }

    void FixedUpdate()
    {
        // physics related calculations
        if (transform.position.y > -0.5f)
        {
            if (!isColliding)
            {
                isMoving = true;
                falling_speed += gravity * Time.deltaTime;
                transform.position += new Vector3(0.0f, falling_speed * Time.deltaTime, 0.0f);
            }
            else
            {
                falling_speed *= -1.0f;
                //transform.position += new Vector3(0.0f, 0.2f, 0.0f);
                //isColliding = false;
            }
        }
        else
        {
            if(Mathf.Abs(falling_speed) < 0.1f)
            {
                isMoving = false;
                falling_speed = 0.0f;
            }
            //isMoving = false;
            falling_speed *= -1.0f;
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
