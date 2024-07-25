using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;


public class ManHandler : MonoBehaviour
{
    [SerializeField] private GameObject manPrefab;

    public GameObject gameHandler;

    Pathfinder pathfinder;

    // Start is called before the first frame update
    void Start()
    {
        CreateManAtTile("firstman", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateManAtTile(string name, int x, int y)
    {
        if (manPrefab != null)
        {
            Vector2 position = Utils.TileToWorld(new Vector2(x,y));
            // Instantiate the prefab at the origin (0, 0, 0) with no rotation
            GameObject instance = Instantiate(manPrefab, Vector3.zero, Quaternion.identity);

            // Optionally, set properties of the instantiated object
            instance.name = name;
            instance.transform.position = new Vector3(position.x, position.y, 5); // Set position
            instance.GetComponent<SpriteRenderer>().sortingOrder = 5;

            instance.GetComponent<Man>().x = x;
            instance.GetComponent<Man>().y = y;

            instance.GetComponent<Man>().gameHandler = gameHandler;

        }
        else
        {
            Debug.LogError("Prefab is not assigned in the inspector.");
        }
    }


}
