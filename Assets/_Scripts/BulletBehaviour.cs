using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletBehaviour : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public float range;

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

    // Start is called before the first frame update
    void Start()
    {
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

        if (inUse)
        {
            _Move();
            _CheckBounds();
        }
    }

    private void _Move()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void _CheckBounds()
    {
        if (Vector3.Distance(transform.position, Vector3.zero) > range)
        {
            //Destroy(gameObject);
        }
        if(isColliding)
        {
            direction.z *= -1.0f;
            transform.position -= new Vector3(0.0f, 0.0f, 1.0f);
            isColliding = false;
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
        transform.position = new Vector3(0.0f, -1000.0f, 0.0f);
    }

    public void activate(Transform tra)
    {
        //transform.SetParent(tra);
        direction = tra.forward;
        transform.position = tra.position;
        inUse = true;
        Debug.Log("Bullet Shot");
    }
}
