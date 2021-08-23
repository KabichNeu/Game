using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;   
    Rigidbody rigidBody;
    AudioSource audioSource;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();    
    }

    void OnCollisionEnter(Collision collision){
        switch (collision.gameObject.tag){
            case "friendly":
                print("do nothing");
                break;
            case "Finish":
                SceneManager.LoadScene(1);
                break;
            default:
                SceneManager.LoadScene(0);
                print("Dead");
                break;
        }
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true; //manual control of rotation
        float rotationThisFrame = rcsThrust*Time.deltaTime;

        if (Input.GetKey(KeyCode.A)){
            transform.Rotate(Vector3.forward* rotationThisFrame);
            print("A pressed");

        }
        else if (Input.GetKey(KeyCode.D)){
            transform.Rotate(-Vector3.forward* rotationThisFrame);
            print("D pressed");

        }
        rigidBody.freezeRotation = false; 
    }

    private void Thrust(){
         if (Input.GetKey(KeyCode.Space)){ //can thrust while rotating
            rigidBody.AddRelativeForce(Vector3.up*mainThrust);
            if (!audioSource.isPlaying){
                audioSource.Play();
            }
            print("Space pressed");
        }
        else {
            audioSource.Stop();
        }
    }
}
