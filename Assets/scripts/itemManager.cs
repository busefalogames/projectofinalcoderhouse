using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isgrounded = true;
            Debug.Log("piso");
        }


        if (collision.gameObject.CompareTag("Victory"))
        {
            Victory();
        }
        if (collision.gameObject.CompareTag("deathzone"))
        {
            transform.position = startzone.transform.position;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Recoveryzone"))
        {
            heal();

        }
        if (other.gameObject.CompareTag("Dangerzone"))
        {
            damage();
            Debug.Log("metoyvolviendoloco");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Batery"))
        {
            interactablebatery = true;
            Debug.Log("bateria");
        }
        if (other.gameObject.CompareTag("Door"))
        {
            interactabledoor = true;
            Debug.Log("puerta");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        interactablebatery = false;
        interactabledoor = false;
        Debug.Log("no more interactableshit");
    }
}
