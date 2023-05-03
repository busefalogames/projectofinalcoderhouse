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
    public Collider AOE;
    // Start is called before the first frame update
    void Start()
    {
        tutorial = false;   
        disapear = false;
        hunting = false;
        fleeing = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_enemyType)
        {
            case enemyType.tutorial:
                tutorial = true;
                if(AOE.CompareTag("Player"))
                {
                    Debug.Log("fuckme");
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
        Debug.Log("idetect");
        if (collider.CompareTag("player"))
        {
            Debug.Log("player");
            if (disapear)
            {
                Destroy(this);
            }
            if (tutorial)
            {
                currentcheckpoint = checkpoint1.transform.position;
            }
            hunting = true;
            fleeing = true;

        }
       
    }
    
    private void OnTriggerStay(Collider other)
    {
        
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("idetect2");
            walk();
        }
    }

    private void OnCollisionEnter(Collision collider)
    {
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
