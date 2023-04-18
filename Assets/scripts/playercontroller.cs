using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    //******************************************************//
    //**********************VARIABLES***********************//
    public Camera pcamara;
    public Rigidbody player;
    public Camera mapcamera;
    public Light flashlight;
    public GameObject defetscreen;
    public GameObject victoryscreen;
    //******movement
    public float basespeed;
    public float speedmodifier;
    private float speed;
    private float xmovement;
    private float zmovement;
    public float playercamaraspeed;
    private float mousex;
    private float mousey;
    private float currentroationX;
    public float jumpforce;
    private bool isgrounded;
    //******** health
    public float sanity; //could also be life
    private float actualsanity;
    public GameObject startzone;
    //******flaslight
    public float flashlightbattery;
    private float flashlightrealbattery;
    public float lightlevel;
    private bool flashlighton;
    public float bateryconsumption;
    private float bateryrealconsumption;
    public float baterycharge;
    private bool lowbattery; //ill use this to make shure that the if that makes it so the light losses some intensity doesnt play more than once
    public float lowbaterrymodifier; //the value by witch the low batterymode affects intesity and consumption of battery
    //********interactableshit
    private bool interactablebatery;
    private bool interactabledoor;

    //******testing enables some wild shit, be shure to TURN IT OFF*******//
    public bool testing;
    //******************************************************//
    void Start()
    {
        flashlightrealbattery = flashlightbattery;
        bateryrealconsumption = bateryconsumption;
        lowbattery = true;
        testing = true;
        speed = basespeed;
        isgrounded = true;
        flashlight.intensity = lightlevel;
        flashlighton = true;
        actualsanity = sanity;
        defetscreen.SetActive(false);
        victoryscreen.SetActive(false);
        interactablebatery = false;

    }

    void Update()
    {
        Debug.Log(actualsanity);
        //**********MOVEMENT**********//
        xmovement = Input.GetAxisRaw("Horizontal") * speed;
        zmovement = Input.GetAxisRaw("Vertical") * speed;

        player.transform.Translate(xmovement, 0, zmovement);

        mousex = Input.GetAxis("Mouse X");
        mousey = Input.GetAxis("Mouse Y");
        player.transform.Rotate(0, mousex * playercamaraspeed, 0);

        currentroationX -= mousey * playercamaraspeed;
        currentroationX = Mathf.Clamp(currentroationX, -90, 90); // esto limita el movimiento de la camara en y para que no se rompa el cuello
        transform.localRotation = Quaternion.Euler(currentroationX, player.transform.localRotation.eulerAngles.y, 0); //esto utiliza valores normalizados para permitir la rotacion de camara sin romperle el cuello al jugador porque lo limitamos y usando el mouse

        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = basespeed * speedmodifier;
        }
        else
        {
            speed = basespeed;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isgrounded == true)
        {
            player.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
            isgrounded = false;
        }
        //**********flashlight**********//
        

        if (Input.GetKeyDown(KeyCode.O) && flashlighton == false && flashlightrealbattery > 0)
        {
            flashlighton = true;
        }

        if (Input.GetKeyDown(KeyCode.F) && flashlightrealbattery > 0)
        {
            flashlighton = !flashlighton;
        }
        if (flashlighton)
        {
            flashlight.intensity = lightlevel;
            flashlightrealbattery = flashlightrealbattery - bateryrealconsumption;
        }
        else
        {
            flashlight.intensity = 0;
        }
        //this makes it so that the last 10% of battery lasts a littlle bit more but also makes it so that it shines less
        if (flashlightrealbattery <= (flashlightbattery / 10) && lowbattery) ///lowers intensity when batery is low
        {
            bateryrealconsumption = bateryconsumption / lowbaterrymodifier;
            flashlight.intensity = flashlight.intensity / lowbaterrymodifier;
            lowbattery = false;
        }
        if (flashlightrealbattery <= 0) //turnsoff when batery dies
        {
            flashlight.intensity = 0;
            flashlightrealbattery = 0;
        }

        if (Input.GetKeyDown(KeyCode.G) && interactablebatery == true)
        {
            flashlightrealbattery += baterycharge;
            Debug.Log("la bateria es");
            Debug.Log(flashlightrealbattery);
        }


        //*****for testing only******//
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            testing = !testing;
        }
        if (testing)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                testing = !testing;
            }
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;
        }

    }
    //**********ANIMATIONS**********//

    //**********STATUSEFFECTS**********//
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
        if(other.gameObject.CompareTag("Batery"))
        {
            interactablebatery = true;
            Debug.Log("bateria");
        }
        if(other.gameObject.CompareTag("Door"))
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

    private void heal()
    {
        actualsanity++;
        Debug.Log(actualsanity);
        if (actualsanity >= sanity)
        {
            actualsanity = sanity;
        }
    }
    private void damage()
    {
        actualsanity--;
        if (actualsanity <= 0)
        {
            death();
        }
            
    }
    private void death()
    {
        Debug.Log("lmaoimded");
        if (testing)
        {
            transform.position = startzone.transform.position;
        }
        else
        {
            defetscreen.SetActive(true);
        }
    }
    private void Victory()
    {
        victoryscreen.SetActive(true);
    }
}
