using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetDialogueController : MonoBehaviour {

    private GameObject petDialogueWindow;
    private Renderer rend;
    public Text petDialogueText;

    // Use this for initialization
    void Start () {
        petDialogueWindow = transform.gameObject;
        petDialogueText = petDialogueWindow.GetComponentInChildren<Text>();


        petDialogueText.text = "Hi hooman!";
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
