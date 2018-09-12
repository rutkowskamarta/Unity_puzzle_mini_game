using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementBehaviour : MonoBehaviour {

    public int initialNumber;
    public int currentIndex;
  
	void Start () {
        currentIndex = initialNumber;
        GetNonNullNeighbours();
	}
	
	void Update () {

    }

    public bool CheckIfCorrectPosition()
    {
        return currentIndex == initialNumber;
    }

    public List<ElementBehaviour> GetNonNullNeighbours()
    {
        List<ElementBehaviour> neighbours = new List<ElementBehaviour>();
        RaycastHit2D[] results= new RaycastHit2D[1];

        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.Raycast(Vector2.up, results, 2f);
        if (results[0].collider != null) {
            neighbours.Add(results[0].collider.gameObject.GetComponent<ElementBehaviour>());
        }
        boxCollider2D.Raycast(Vector2.right, results, 2f);
        if (results[0].collider != null)
        {
            neighbours.Add(results[0].collider.gameObject.GetComponent<ElementBehaviour>());
        }
        boxCollider2D.Raycast(Vector2.down, results, 2f);
        if (results[0].collider != null)
        {
            neighbours.Add(results[0].collider.gameObject.GetComponent<ElementBehaviour>());
        }
        boxCollider2D.Raycast(Vector2.left, results, 2f);
        if (results[0].collider != null)
        {
            neighbours.Add(results[0].collider.gameObject.GetComponent<ElementBehaviour>());
        }
        return neighbours;
    }

    public List<ElementBehaviour> GetAllNeighbours()
    {
        List<ElementBehaviour> neighbours = new List<ElementBehaviour>();
        

        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();

        SendRaycast(boxCollider2D, Vector2.up, neighbours);
        SendRaycast(boxCollider2D, Vector2.right, neighbours);
        SendRaycast(boxCollider2D, Vector2.down, neighbours);
        SendRaycast(boxCollider2D, Vector2.left, neighbours);
        Debug.Log(neighbours[0] +" "+neighbours[1]+" " +neighbours[2]+" "+neighbours[3]);
        Debug.Log(neighbours.Count);
        return neighbours;
    }

    public void SendRaycast(BoxCollider2D boxCollider2D, Vector2 direction, List<ElementBehaviour> neighbours)
    {
        RaycastHit2D[] results = new RaycastHit2D[1];
        boxCollider2D.Raycast(direction, results, 2f);
        if (results[0].collider != null)
        {
            neighbours.Add(results[0].collider.gameObject.GetComponent<ElementBehaviour>());
        }
        else
        {
            neighbours.Add(null);
        }

    }
    public void InitializeNumber(int number)
    {
        initialNumber = currentIndex = number;
    }
}
