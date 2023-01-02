using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 3f;
    public float reverse = 1f;
    public float trucX = 0f;
    public float trucY = 0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(trucX * reverse * speed * Time.deltaTime / 0.01f, trucY * reverse * speed * Time.deltaTime / 0.01f, 0f, Space.Self);
    }
}
