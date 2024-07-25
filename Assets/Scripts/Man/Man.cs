using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Man : MonoBehaviour
{
    // Start is called before the first frame update
    public int x;
    public int y;

    public GameObject gameHandler;

    public List<Vector2> myPath;

    public bool walking = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        gameHandler.GetComponent<GameHandler>().selectedMan = this;
        //    Debug.Log("Selected man " + this.name);
    }

    public void Walk()
    {
        walking = true;
        if (myPath.Count < 1)
        {
            Debug.Log("End of Path for " + this.name);
            return;
        }

        // step walk

        Vector2 nextTile = myPath[0];
        StepTo(nextTile);


        myPath.RemoveAt(0);
        Invoke(nameof(Walk), 100 / 1000f);


    }

    private void StepTo(Vector2 tile)
    {
        Vector3 WorldPosition = Utils.TileToWorld(tile);
        gameObject.transform.position = WorldPosition;

    }


}
