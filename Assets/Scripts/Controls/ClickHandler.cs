using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera mainCamera;



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }






    public Vector2 GetTileAtMouse()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);

        // Adjust z-coordinate to ensure it's in front of the camera
        mouseWorldPosition.z = 0; // Set to 0 if working with 2D games, or adjust as needed for 3D

        //   Debug.Log("Mouse Position in World Space: " + mouseWorldPosition);
        // Calculate the tile position
        Vector2 tilePosition = Utils.WorldToTilePosition(mouseWorldPosition);

        // Debug log the tile position


        return tilePosition;
    }
}
