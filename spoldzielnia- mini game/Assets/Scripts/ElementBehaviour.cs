using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementBehaviour : MonoBehaviour {

    [SerializeField]private int correctPosition;
    public int currentPosition;
    private bool isAtCorrectPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void CheckIfCorrectPosition()
    {
        isAtCorrectPosition = currentPosition == correctPosition;
    }
}
