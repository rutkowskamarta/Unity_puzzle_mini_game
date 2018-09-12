using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleControler : MonoBehaviour {

    [SerializeField]private GameObject frame;
    [SerializeField] private Sprite lockedFrame;
    [SerializeField] private Sprite freeFrame;


    private SpriteRenderer frameSpriteRenderer;
    private List<Vector3> positions;
    private List<Transform> puzzleElementsOrdered;
    private ElementBehaviour removedElement;
    private ElementBehaviour frameElement;

    private int numberOfCurrentElement = 8;

    private bool isHolding = false;
    private bool isShuffled = false;
    
    private void Awake()
    { 
        frameSpriteRenderer = frame.GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        FindFrameElement();
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
            LoadWinningScene();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
            LoadWinningScene();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            MoveDown();
            LoadWinningScene();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveRight();
            LoadWinningScene();
        }

    }

    private void MoveUp()
    {
        
        List<ElementBehaviour> allNeighbours = frameElement.GetAllNeighbours();
        if (isHolding)
        {
            MoveFrameWhileHolding(-3, allNeighbours[0]);

            SwitchElements(frameElement, allNeighbours[0]);
        }
        else
        {
            MoveFrame(-3, allNeighbours[0]);
        }

    }
    
    private void MoveLeft()
    {
        List<ElementBehaviour> allNeighbours = frameElement.GetAllNeighbours();
        if (isHolding)
        {
            MoveFrameWhileHolding(-1, allNeighbours[3]);

            SwitchElements(frameElement, allNeighbours[3]);
        }
        else
        {

            MoveFrame(-1, allNeighbours[3]);
        }

    }
    private void MoveRight()
    {
        List<ElementBehaviour> allNeighbours = frameElement.GetAllNeighbours();
        if (isHolding)
        {
            MoveFrameWhileHolding(1, allNeighbours[1]);

            SwitchElements(frameElement, allNeighbours[1]);
        }
        else
        {

            MoveFrame(1, allNeighbours[1]);
        }
        

    }
    
    private void MoveDown()
    {
        List<ElementBehaviour> allNeighbours = frameElement.GetAllNeighbours();
        
        
        if (isHolding)

        {

            MoveFrameWhileHolding(-3, allNeighbours[2]);
            SwitchElements(frameElement, allNeighbours[2]);
        }
        else
        {
            MoveFrame(-3, allNeighbours[2]);
        }
        
    }

    private void MoveFrameWhileHolding(int modificator, ElementBehaviour neighbour)
    {
        if (neighbour != null && neighbour.initialNumber == removedElement.initialNumber)
        {
            numberOfCurrentElement += modificator;
            frame.transform.position = new Vector3(neighbour.transform.position.x, neighbour.transform.position.y, frame.transform.position.z);
        }
    }

    private void MoveFrame(int modificator, ElementBehaviour neighbour)
    {
        if (neighbour != null)
        {
            numberOfCurrentElement += modificator;
            frame.transform.position = new Vector3(neighbour.transform.position.x, neighbour.transform.position.y, frame.transform.position.z);
            frameElement = neighbour;
        }
    }

    private void SwitchElements(ElementBehaviour frameElement, ElementBehaviour destination)
    {
        if (destination!=null && destination.initialNumber == removedElement.initialNumber)
        {
            SwitchElementsPositions(frameElement, destination);
            SwitchElementsIndices(frameElement, destination);
        }
        
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



    private void HoldElement()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isHolding || frameElement.initialNumber == removedElement.GetComponent<ElementBehaviour>().initialNumber)
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

    private void FindFrameElement()
    {
        for (int i = 0; i < puzzleElementsOrdered.Count; i++)
        {
            if (puzzleElementsOrdered[i].GetComponent<ElementBehaviour>().currentIndex == numberOfCurrentElement)
            {
                frameElement = puzzleElementsOrdered[i].GetComponent<ElementBehaviour>();
            }
        }
    }

    private void LoadWinningScene()
    {
        if (CheckIfWon())
        {
            SceneManager.LoadScene("YouWonScene");
        }
    }
    private bool CheckIfWon()
    {
        for(int i = 0; i < puzzleElementsOrdered.Count; i++)
        {
            if (!puzzleElementsOrdered[i].GetComponent<ElementBehaviour>().CheckIfCorrectPosition())
            {
                return false;
            }
        }
        return true;
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
