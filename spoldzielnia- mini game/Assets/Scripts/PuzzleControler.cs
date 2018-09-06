using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleControler : MonoBehaviour {

    [SerializeField]private GameObject frame;
    [SerializeField] private Sprite lockedFrame;
    [SerializeField] private Sprite freeFrame;


    private SpriteRenderer frameSpriteRenderer;
    private List<Vector3> positions;
    private List<Transform> puzzleElements;
    private int indexOfCurrentPosition = 8;

    private bool isHolding = false;
    private bool isShuffled = false;

    // Use this for initialization
    private void Awake()
    {
        frameSpriteRenderer = frame.GetComponent<SpriteRenderer>();
   
        //puzzleElements = GetComponent<PuzzleRandomizer>().getPuzzleElements();
    }

    

    // Update is called once per frame
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
            Vector3 newPosition = positions[--indexOfCurrentPosition];
            if (!isHolding)
            {
                frame.transform.position = new Vector3(newPosition.x, newPosition.y, frame.transform.position.z);
            }
            else
            {
                MoveToNewPosition(newPosition);
            }
        }

    }
    private void MoveRight()
    {
        if (indexOfCurrentPosition != 2 && indexOfCurrentPosition != 5 && indexOfCurrentPosition != 8)
        {
            Vector3 newPosition = positions[++indexOfCurrentPosition];
            if (!isHolding)
            {
                frame.transform.position = new Vector3(newPosition.x, newPosition.y, frame.transform.position.z);
            }
            else
            {
                MoveToNewPosition(newPosition);
            }
        }

    }
    private void MoveUp()
    {
        if (indexOfCurrentPosition != 0 && indexOfCurrentPosition != 1 && indexOfCurrentPosition != 2)
        {

            indexOfCurrentPosition -= 3;
            Vector3 newPosition = positions[indexOfCurrentPosition];
            if (!isHolding)
            {
                frame.transform.position = new Vector3(newPosition.x, newPosition.y, frame.transform.position.z);
            }
            else
            {
                MoveToNewPosition(newPosition);
            }
        }

    }
    private void MoveDown()
    {
        if (indexOfCurrentPosition != 6 && indexOfCurrentPosition != 7 && indexOfCurrentPosition != 8)
        {

            indexOfCurrentPosition += 3;
            Vector3 newPosition = positions[indexOfCurrentPosition];
            if (!isHolding)
            {
                frame.transform.position = new Vector3(newPosition.x, newPosition.y, frame.transform.position.z);
            }
            else
            {
                MoveToNewPosition(newPosition);
            }
        }

    }

    private void MoveToNewPosition(Vector3 newPosition)
    {
        if (IsPositionEmpty(newPosition))
        {
            Debug.Log(IsPositionEmpty(newPosition));
            Transform takenPuzzleElement = GetElementAtPosition(frame.transform.position);
            frame.transform.position = new Vector3(newPosition.x, newPosition.y, frame.transform.position.z);
            takenPuzzleElement.position = new Vector3(newPosition.x, newPosition.y, takenPuzzleElement.position.z);
            takenPuzzleElement.GetComponent<ElementBehaviour>().currentPosition = indexOfCurrentPosition;
        }
    }

    private bool IsPositionEmpty(Vector3 position)
    {
        foreach(Transform element in puzzleElements)
        {
            if(element.position.x==position.x && element.position.y == position.y)
            {
                return false;
            }
        
        }

        return true;
    }

    private Transform GetElementAtPosition(Vector3 position)
    {
        foreach (Transform element in puzzleElements)
        {
            if (element.position.x == position.x && element.position.y == position.y)
            {
                return element;
            }

        }
        return null;
    }


    private void HoldElement()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isHolding)
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

    public void SetPositionsAndPuzzlesLists(List<Vector3> positions, List<Transform> puzzleElements)
    {
        SetPositionList(positions);
        SetPuzzleElementsList(puzzleElements);
        isShuffled = true;
    }

    public void SetPositionList(List<Vector3> positions)
    {
        this.positions = positions;
    }

    public void SetPuzzleElementsList(List<Transform> puzzleElements)
    {
        this.puzzleElements = puzzleElements;
    }

}
