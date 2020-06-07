using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscilator : MonoBehaviour
{
    private Vector3 startingPos;
    [SerializeField] Vector3 movementVector;

    // to DO move from inspector
    [Range(0,1)][SerializeField] float movementFactorY; //1 fully move 0 not moved

        void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = movementVector * movementFactorY;
        transform.position = startingPos + offset;
    }
}
