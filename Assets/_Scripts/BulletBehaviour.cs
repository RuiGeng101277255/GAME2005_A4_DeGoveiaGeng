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
    private float radius;

    // Start is called before the first frame update
    void Start()
    {
        debug = false;
        _calcRadius(scale);
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
        _CheckBounds();
    }

    private void _Move()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void _CheckBounds()
    {
        if (Vector3.Distance(transform.position, Vector3.zero) > range)
        {
            Destroy(gameObject);
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
}
