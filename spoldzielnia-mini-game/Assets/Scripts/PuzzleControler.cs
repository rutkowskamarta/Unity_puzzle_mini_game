using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleControler : MonoBehaviour {

    [SerializeField]private GameObject frame;
    [SerializeField] private Sprite lockedFrame;
    [SerializeField] private Sprite freeFrame;


    private SpriteRenderer frameSpriteRenderer;
    private List<Vector3> positions;
    private List<Transform> puzzleElementsOrdered;
    private ElementBehaviour removedElement;

    private int indexOfCurrentPosition = 8;

    private bool isHolding = false;
    private bool isShuffled = false;
    
    private void Awake()
    {
        frameSpriteRenderer = frame.GetComponent<SpriteRenderer>();
    }

    void Update () {
        if (isShuffled)
        {
            FrameControl();
            HoldElement();
        }
	}

    private void FrameControl()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveUp();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            MoveDown();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveRight();
        }

    }

    private void MoveLeft()
    {
        if(indexOfCurrentPosition!=0 && indexOfCurrentPosition != 3 && indexOfCurrentPosition != 6)
        {
            Vector3 newPosition = positions[indexOfCurrentPosition-1];
            if (!isHolding)
            {
                frame.transform.position = new Vector3(newPosition.x, newPosition.y, frame.transform.position.z);
            }
            else
            {
                frame.transform.position = new Vector3(newPosition.x, newPosition.y, frame.transform.position.z);
                MoveElementAtNewPosition(newPosition);
            }
            indexOfCurrentPosition--;
        }

    }
    private void MoveRight()
    {
        if (indexOfCurrentPosition != 2 && indexOfCurrentPosition != 5 && indexOfCurrentPosition != 8)
        {
            Vector3 newPosition = positions[indexOfCurrentPosition + 1];
            if (!isHolding)
            {
                frame.transform.position = new Vector3(newPosition.x, newPosition.y, frame.transform.position.z);
            }
            else
            {
                frame.transform.position = new Vector3(newPosition.x, newPosition.y, frame.transform.position.z);
                MoveElementAtNewPosition(newPosition);
            }
            indexOfCurrentPosition++;
        }

    }
    private void MoveUp()
    {
        if (indexOfCurrentPosition != 0 && indexOfCurrentPosition != 1 && indexOfCurrentPosition != 2)
        {
            Vector3 newPosition = positions[indexOfCurrentPosition-3];
            if (!isHolding)
            {
                frame.transform.position = new Vector3(newPosition.x, newPosition.y, frame.transform.position.z);
            }
            else
            {
                frame.transform.position = new Vector3(newPosition.x, newPosition.y, frame.transform.position.z);
                MoveElementAtNewPosition(newPosition);
            }

            indexOfCurrentPosition -= 3;
        }

    }
    private void MoveDown()
    {
        if (indexOfCurrentPosition != 6 && indexOfCurrentPosition != 7 && indexOfCurrentPosition != 8)
        {
            Vector3 newPosition = positions[indexOfCurrentPosition+3];
            if (!isHolding)
            {
                frame.transform.position = new Vector3(newPosition.x, newPosition.y, frame.transform.position.z);
            }
            else
            {
                frame.transform.position = new Vector3(newPosition.x, newPosition.y, frame.transform.position.z);
                MoveElementAtNewPosition(newPosition);
            }

            indexOfCurrentPosition += 3;
        }

    }

    private void MoveElementAtNewPosition(Vector3 newPosition)
    {
        ElementBehaviour heldElement = puzzleElementsOrdered[indexOfCurrentPosition].GetComponent<ElementBehaviour>();
        if (heldElement != removedElement)
        {
            if (newPosition == removedElement.transform.position)
            {
                removedElement.transform.position = puzzleElementsOrdered[indexOfCurrentPosition].position;
                removedElement.currentIndex = heldElement.currentIndex;
                heldElement.transform.position = newPosition;
                heldElement.currentIndex = indexOfCurrentPosition;
                
            }
            
        }
    }

    private void HoldElement()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isHolding || positions[indexOfCurrentPosition]  == removedElement.transform.position)
            {
                isHolding = false;
                frameSpriteRenderer.sprite = freeFrame;
            }
            else
            {
                isHolding = true;
                frameSpriteRenderer.sprite = lockedFrame;
            }
        }

    }

    public void InitializeController(List<Vector3> positions, List<Transform> puzzleElements, ElementBehaviour removedElement)
    {
        SetPositionList(positions);
        SetPuzzleElementsList(puzzleElements);
        SetRemovedElement(removedElement);
        isShuffled = true;
    }

    public void SetPositionList(List<Vector3> positions)
    {
        this.positions = positions;
    }

    public void SetPuzzleElementsList(List<Transform> puzzleElements)
    {
        this.puzzleElementsOrdered = puzzleElements;
    }

    public void SetRemovedElement(ElementBehaviour removedElement)
    {
        this.removedElement = removedElement;
    }

}
