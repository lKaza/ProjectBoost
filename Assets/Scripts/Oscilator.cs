using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscilator : MonoBehaviour
{
    private Vector3 startingPos;
    private Vector3 startingRot;
    [SerializeField] Vector3 movementVector = new Vector3( 10f ,10f ,10f );
    [SerializeField] Vector3 rotationVector = new Vector3(10f,10f,10f);
    [SerializeField] float period = 2f;
 
    // to DO move from inspector
    [Range(0,1)][SerializeField] float movementFactor; //1 fully move 0 not moved
    [Range(0,90)][SerializeField] float rotationFactor;

        void Start()
    {
        startingPos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon){
            return;     
        }
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);

        movementFactor = rawSinWave /2f +0.5f;

        Vector3 offset = movementVector * movementFactor;
        Vector3 rotationOffset = rotationVector * rotationFactor;
        transform.position = startingPos + offset;
       
    }
}
