using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PuzzleRandomizer : MonoBehaviour {
    private List<Transform> puzzleElements;
    private List<Vector3> positions;

    private int indexOfRemovedElement;


    private void Awake()
    {
        puzzleElements = new List<Transform>(GetComponentsInChildren<Transform>());
        puzzleElements.RemoveAt(0);
    }

    void Start () {
        InitializePositionsArray();
        ShuffleElements();
        RemoveLastElement();
    }

    void Update () {
		
	}

    private void InitializePositionsArray()
    {
        positions = new List<Vector3>();
        for (int i = 0; i < puzzleElements.Count; i++)
            positions.Add(puzzleElements[i].position);
    }

    private void RemoveLastElement()
    {
        indexOfRemovedElement = 8;
        Destroy(puzzleElements[indexOfRemovedElement].gameObject);
        puzzleElements.RemoveAt(indexOfRemovedElement);
        
    }

    private void ShuffleElements()
    {
        List<int> indicesOfAvailablePlaces = new List<int>();
        for(int i=0; i < puzzleElements.Count; i++)
        {
            indicesOfAvailablePlaces.Add(i);
        }
        

        for(int i = 0; i < puzzleElements.Count; i++)
        {
                int randomIndex = Random.Range(0, indicesOfAvailablePlaces.Count);
                int indexOfShufflePlace = indicesOfAvailablePlaces[randomIndex];
                indicesOfAvailablePlaces.RemoveAt(randomIndex);
                
                puzzleElements[i].position=positions[indexOfShufflePlace];
                puzzleElements[i].GetComponent<ElementBehaviour>().currentPosition = indexOfShufflePlace;
        }
        
        GetComponent<PuzzleControler>().SetPositionsAndPuzzlesLists(positions, puzzleElements);


    }


    
}
