using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enemyType
{
    follow,
    disapear,
    goaway,
    tutorial,
}
public class enemyController : MonoBehaviour
{
    public float speed;
    [SerializeField] private enemyType _enemyType;
    public GameObject checkpoint1;
    public GameObject checkpoint2;
    public GameObject checkpoint3;
    public GameObject checkpoint4;
    private Vector3 currentcheckpoint;
    public GameObject player;
    private bool disapear;
    private bool hunting;
    private bool fleeing;
    private bool tutorial;
    private bool walking;
    void Start()
    {
        tutorial = false;   
        disapear = false;
        hunting = false;
        fleeing = false;
        currentcheckpoint = checkpoint1.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_enemyType)
        {
            case enemyType.tutorial:
                tutorial = true;
                if (walking)
                {
                    walk();
                }
                Debug.Log("tutorial");
                break;
            case enemyType.disapear:
                disapear = true;
                Debug.Log("disapearing");
                break;
            case enemyType.follow:
                if (hunting)
                {
                    hunt(); //this is a mess brub
                    Debug.Log("hunting");
                }
                break;
            case enemyType.goaway:
                if(fleeing)
                {
                    flee();
                    Debug.Log("flee");
                }
                break;
        }
        
    }
    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("idetecttrigger");
 
            if (disapear)
            {
                Destroy(this);
            }
            if (tutorial && collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("entered trigger");
                
                walking = true;
            }
            hunting = true;
            fleeing = true;

    }
    private void OnTriggerExit(Collider other)
    {
        if (tutorial && other.gameObject.CompareTag("Player"))
        {
            Debug.Log("exitedtrigger");
            walking = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("staytrigger");
        /*if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("triggerstay"); //this is triggering but the walk is not
            walking = true;
        }
        else
        {
            walking = false;
        }*/
    }

    private void OnCollisionEnter(Collision collider)
    {
        Debug.Log("idetectcolission");
        if (collider.gameObject.CompareTag("checkpoint1"))
        {
            currentcheckpoint = checkpoint2.transform.position;
            Debug.Log("checkpoint 2");
        }
        if (collider.gameObject.CompareTag("checkpoint2"))
        {
            currentcheckpoint = checkpoint3.transform.position;
            Debug.Log("checkpoint 3");
        }
        if (collider.gameObject.CompareTag("checkpoint3"))
        {
            currentcheckpoint = checkpoint4.transform.position;
            Debug.Log("checkpoint 4");
        }
        if(collider.gameObject.CompareTag("checkpoint4"))
        {
            Debug.Log("endofpathing");
            currentcheckpoint = transform.position;
        }
    }
    void walk()
    {
        var checkpoint = currentcheckpoint - transform.position;

        Move(checkpoint.normalized);
    }
   
    void hunt()
    {
        var p_distance = player.transform.position - transform.position;

        Move(p_distance.normalized);
    }
    void flee()
    {
        var p_position = player.transform.position + transform.position;
        Move(p_position.normalized);
    }
    private void Move(Vector3 direction)
    {
        transform.position += direction * speed * Time.deltaTime;
    }

}   
