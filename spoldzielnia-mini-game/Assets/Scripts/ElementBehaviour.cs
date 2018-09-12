using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementBehaviour : MonoBehaviour {

    public int initialNumber;
    public int currentIndex;
  
	void Start () {
        currentIndex = initialNumber;
        GetNeighbours();
	}
	
	void Update () {

    }

    public bool CheckIfCorrectPosition()
    {
        return currentIndex == initialNumber;
    }

    public List<ElementBehaviour> GetNeighbours()
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

    public void InitializeNumber(int number)
    {
        initialNumber = currentIndex = number;
    }
}
