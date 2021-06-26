using UnityEngine;
using System.Collections;

public class LookAtTarget : MonoBehaviour {

    [Tooltip("This is the object that the script's game object will look at by default")]
    public GameObject defaultTarget; // the default target that the camera should look at

    [Tooltip("This is the object that the script's game object is currently look at based on the player clicking on a gameObject")]
    public GameObject currentTarget; // the target that the camera should look at

    // Start happens once at the beginning of playing. This is a great place to setup the behavior for this gameObject
	void Start () {
		if (defaultTarget == null) 
		{
            defaultTarget = this.gameObject;
			Debug.Log ("defaultTarget target not specified. Defaulting to parent GameObject");
		}

        if (currentTarget == null)
        {
            currentTarget = this.gameObject;
            Debug.Log("currentTarget target not specified. Defaulting to parent GameObject");
        }
    }
	
	// Update is called once per frame
    // For clarity, Update happens constantly as your game is running
    void Update()
    {
        // if primary mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // determine the ray from the camera to the mousePosition
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // cast a ray to see if it hits any gameObjects
            RaycastHit[] hits;
            hits = Physics.RaycastAll(ray);

            // if there are hits
            if (hits.Length>0)
            {
                // get the first object hit
                RaycastHit hit = hits[0];
                currentTarget = hit.collider.gameObject;

                Debug.Log("defaultTarget changed to "+currentTarget.name);
            }
        } else if (Input.GetMouseButtonDown(1)) // if the second mouse button is pressed
        {
            currentTarget = defaultTarget;
            Debug.Log("defaultTarget changed to " + currentTarget.name);
        }

       // if a currentTarget is set, then look at it
        if (currentTarget!=null)
        {
            // transform here refers to the attached gameobject this script is on.
            // the LookAt function makes a transform point it's Z axis towards another point in space
            // In this case it is pointing towards the target.transform
            transform.LookAt(currentTarget.transform);
        } else // reset the look at back to the default
        {
            currentTarget = defaultTarget;
            Debug.Log("defaultTarget changed to " + currentTarget.name);
        }
    }
}
