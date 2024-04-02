using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //GameObject of player
    public GameObject player;
    //Offset to maintain from player
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {

        //Get offset between player and camera
        offset = transform.position - player.transform.position;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
