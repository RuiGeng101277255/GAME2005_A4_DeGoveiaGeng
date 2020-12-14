using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletBehaviour : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public float range;
    public Vector3 size;
    public Vector3 max;
    public Vector3 min;
    public bool isColliding;
    public bool spawned;
    public Bounds bounds;
    public List<CubeBehaviour> contacts;
    public GameObject bullet;
    

    // Start is called before the first frame update
    void Start()
    {
        spawned = true;
        //Instantiate(bullet, transform.position, Quaternion.identity);
        transform.SetParent(gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawned)
        {
            _Move();
            _CheckBounds();
            bounds.center = direction;
            bounds.size = size;
            max = Vector3.Scale(bounds.max, transform.localScale) + transform.position;
            min = Vector3.Scale(bounds.min, transform.localScale) + transform.position;
        }
    }

    private void _Move()
    {
        if(isColliding)
        {
            direction.x *= -1;
            direction.z *= -1;
            Debug.Log("Bullet Collision");
            isColliding = !isColliding;
        }
        transform.position += direction * speed * Time.deltaTime;
    }

    private void _CheckBounds()
    {
        if (Vector3.Distance(transform.position, Vector3.zero) > range)
        {
            //Destroy(gameObject);
            _reset();
        }
    }

    private void _reset()
    {
        spawned = false;
        transform.position.Set(0.0f, -1000.0f, 0.0f);
        //direction = Vector3.zero;
    }

    public void _activate(Transform trans)
    {
        spawned = true;
        transform.position.Set(trans.position.x, trans.position.y, trans.position.z);
        direction.Set(trans.forward.x, trans.forward.y, trans.forward.z);
    }
}
