using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
  

    // Update is called once per frame
    void Update()
    {
        UpdateSpeed();
    }

    private void UpdateSpeed()
    {
        transform.Translate(Time.deltaTime * speed * Vector3.up);

        if (transform.position.y > 6.0f)
        {
            if(transform.parent)
                Destroy(transform.parent.gameObject);
            Destroy(this.gameObject);   
        }
    }
}
