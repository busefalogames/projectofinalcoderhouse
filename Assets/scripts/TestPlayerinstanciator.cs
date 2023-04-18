using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerinstanciator : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        GameObject playerr = Instantiate(player, transform.position, rotation: (Quaternion)Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
