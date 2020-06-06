using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    float rcsThrust = 100f;
    [SerializeField]
    float shipThrust = 100f;
    AudioSource m_MyAudioSource;   
    Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        m_MyAudioSource = GetComponent<AudioSource>();
    }
  

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

void OnCollisionEnter(Collision other) {
    switch(other.gameObject.tag){

        case "Friendly":
            print("OK");
               break;

         default: 
            print("dead af");
            break;
    }
}
      private void Thrust(){
          
           if(Input.GetKey(KeyCode.Space))
        {
           rigidBody.AddRelativeForce(Vector3.up*shipThrust);

           if(!m_MyAudioSource.isPlaying){
           m_MyAudioSource.Play(); 
           }
            }else{
            m_MyAudioSource.Stop();
        }

      }
      void Rotate(){
        rigidBody.freezeRotation =true; // take manual rotation

        
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        if(Input.GetKey(KeyCode.A) )
        {
           
            transform.Rotate(Vector3.forward*rotationThisFrame);
        }
        else if(Input.GetKey(KeyCode.D))
        {        
            transform.Rotate(-Vector3.forward*rotationThisFrame);
        }
    
        rigidBody.freezeRotation=false; //resume world rotation
    }


    }
    

