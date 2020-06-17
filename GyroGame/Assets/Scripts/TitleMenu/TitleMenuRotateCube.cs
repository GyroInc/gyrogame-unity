using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenuRotateCube: MonoBehaviour
{
    public bool random;
    public Vector3 direction;
    public float speed;

    private void Start()
    {
        if (random)
            direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    void Update()
    {        
        transform.Rotate(direction.normalized * speed * Time.deltaTime);
    }

    public void Randomize()
    {
        direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }
}
