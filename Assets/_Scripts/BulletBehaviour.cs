﻿using System.Collections;
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

    private MeshFilter bulletMeshFilter;
    private Bounds bounds;

    //pol
    public bool inUse;

    // Start is called before the first frame update
    void Start()
    {
        debug = false;
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
        if(inUse)
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
