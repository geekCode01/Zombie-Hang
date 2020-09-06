using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public AudioClip[] smashSounds;
    private AudioSource audioSource;
    public GameObject bloodEffect;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        //Constantly for input here. 

        if (Input.GetMouseButtonDown(0))
        {
            //we will raycast here. 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // we have reated our ray
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            //Check if raycast hit something.   
            if (hit.collider != null)
            {
                //If it hit something, tell me if it hit an enemy. 
                if (hit.collider.tag == "Enemy")
                {
                    //this is where we will kill enemy. 
                    //play to the smash audio
                    audioSource.PlayOneShot(smashSounds[Random.Range(0, smashSounds.Length)], 0.5f); //0,1,2,3
                    gameObject.GetComponent<GameManager>().KillEnemy();
                    Camera.main.GetComponent<Animator>().SetTrigger("Shake");
                    DisplayBloodEffect(Camera.main.ScreenToWorldPoint(Input.mousePosition)); // (x, y, z) => (x,y)
                }
            }

        }
    }

    private void DisplayBloodEffect(Vector2 pos)
    {
        bloodEffect.transform.position = pos;
        bloodEffect.GetComponent<Animator>().SetTrigger("Smashed");
    }
}
