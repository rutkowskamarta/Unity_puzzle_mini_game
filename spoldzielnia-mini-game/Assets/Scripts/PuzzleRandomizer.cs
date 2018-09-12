using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PuzzleRandomizer : MonoBehaviour {
    private List<Transform> puzzleElementsOrdered;
    private List<Vector3> existingPositions;

    private ElementBehaviour removedElement;
    private ElementBehaviour previouslyMovedElement;
    private const int SHUFFLE_NUMBER = 30;

    private void Awake()
    {
        puzzleElementsOrdered = new List<Transform>(GetComponentsInChildren<Transform>());
        puzzleElementsOrdered.RemoveAt(0);
        InitializeElementsNumbersAndIndices();
    }

    void Start () {
        InitializePositionsArray();
        RemoveLastElement();
        ShuffleElements();
    }

    private void InitializeElementsNumbersAndIndices()
    {
        ElementBehaviour element;
        for (int i = 0; i < puzzleElementsOrdered.Count; i++)
        {
            element = puzzleElementsOrdered[i].GetComponent<ElementBehaviour>();
            element.InitializeNumber(i + 1);
        }
    }

    private void InitializePositionsArray()
    {
        existingPositions = new List<Vector3>();
        for (int i = 0; i < puzzleElementsOrdered.Count; i++)
            existingPositions.Add(puzzleElementsOrdered[i].position);
    }

    private void RemoveLastElement()
    {
        removedElement = puzzleElementsOrdered[8].GetComponent<ElementBehaviour>();
        removedElement.GetComponent<SpriteRenderer>().enabled = false;
        previouslyMovedElement = removedElement;
    }

    private void ShuffleElements()
    {

        for (int i = 0; i < SHUFFLE_NUMBER; i++)
        {
            MoveNeighbourElementToEmpyField();
        }

        GetComponent<PuzzleControler>().InitializeController(existingPositions, puzzleElementsOrdered, removedElement);

    }

    private void MoveNeighbourElementToEmpyField()
    {
        ElementBehaviour neighbour = GetRandomNeighbourNumberOfElement(removedElement);
        SwitchElements(neighbour, removedElement);
        previouslyMovedElement = neighbour;
    }

    private ElementBehaviour GetRandomNeighbourNumberOfElement(ElementBehaviour element)
    {
        List<ElementBehaviour> list = element.GetComponent<ElementBehaviour>().GetNeighbours();
        list.Remove(previouslyMovedElement);
        return list[Random.Range(0, list.Count)];
    }


    private void SwitchElements(ElementBehaviour elementA, ElementBehaviour elementB)
    {
        SwitchElementsPositions(elementA, elementB);
        SwitchElementsIndices(elementA, elementB);
    }

    private void SwitchElementsPositions(ElementBehaviour elementA, ElementBehaviour elementB)
    {
        Vector3 previousPosition = elementA.transform.position;
        elementA.transform.position = elementB.transform.position;
        elementB.transform.position = previousPosition;
    }

    private void SwitchElementsIndices(ElementBehaviour elementA, ElementBehaviour elementB)
    {
        int previousIndex = elementA.currentIndex;
        elementA.currentIndex = elementB.currentIndex;
        elementB.currentIndex = previousIndex;
    }

}
