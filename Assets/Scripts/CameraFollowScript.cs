using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour {

    public GameObject player;        //Public variable to store a reference to the player game object
    public bool followY;

    private Vector3 offset;            //Private variable to store the offset distance between the player and camera

// Use this for initialization
    void Start () 
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
    }

// LateUpdate is called after Update each frame
    void LateUpdate () 
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        if(followY)
        {
            transform.position = player.transform.position + offset; // we should follow the player Y movements, so we copy the entire position vector
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x, 0, -10); // we just copy the X and Z values
        }
    }
}