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
    public float speed;
    public float sanity; //could also be life
    public float flashlightbattery;
    private float flashlightrealbattery;
    public float bateryconsumption;
    private float bateryrealconsumption;
    private bool lowbattery; //ill use this to make shure that the if that makes it so the light losses some intensity doesnt play more than once
    public float lowbaterrymodifier; //the value by witch the low batterymode affects intesity and consumption of battery
    private float xmovement;
    private float zmovement;
    //******************************************************//
    void Start()
    {
        flashlightrealbattery = flashlightbattery;
        bateryrealconsumption = bateryconsumption;
        lowbattery = true;
    }

    void Update()
    {
        //**********flashlight**********//
        Debug.Log(flashlightrealbattery);
        //this makes it so that the last 10% of battery lasts a littlle bit more but also makes it so that it shines less
        flashlightrealbattery = flashlightrealbattery - bateryrealconsumption;

        if (flashlightrealbattery <= (flashlightbattery / 10) && lowbattery) ///lowers intensity when batery is low
        {
            bateryrealconsumption = bateryconsumption / lowbaterrymodifier;
            flashlight.intensity = flashlight.intensity / lowbaterrymodifier;
            Debug.Log("bateriabaja");
            lowbattery = false;
        }
        if (flashlightrealbattery <= 0) //turnsoff when batery dies
        {
            flashlight.intensity = 0;
            flashlightrealbattery = 0;
            Debug.Log("no batery");
        }
        //**********MOVEMENT**********//
        xmovement = Input.GetAxisRaw("Horizontal") * speed;
        zmovement = Input.GetAxisRaw("Vertical") * speed;

        player.transform.Translate(xmovement, 0, zmovement);
    }
    //**********ANIMATIONS**********//

    //**********STATUSEFFECTS**********//

}
