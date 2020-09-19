using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstraintMovement : MonoBehaviour
{
    private float xBound = 175.0f;
    private float zBound = 50.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ConstraintObjectMovement();
        DestroyOutOfBounds();
    }

    // Restricts the players movement within a boundary
    void ConstraintObjectMovement()
    {
        //restrict player's movement within a boundary
        if(transform.position.x > xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        }
        if(transform.position.z > zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
        }
        if(transform.position.z < -zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBound);
        }
    }

    void DestroyOutOfBounds()
    {
        if(!gameObject.CompareTag("Player") && transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
