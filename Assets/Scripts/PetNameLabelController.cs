using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetNameLabelController : MonoBehaviour {

    private Text petNameLabel;

    GameObject nameStorage;
    Game gameScreen;

    PetBehavior pet;

    IEnumerator LoadPetName()
    {
        yield return new WaitUntil(() => gameScreen.petDataLoaded);
    }

    private void Awake()
    {
        gameScreen = FindObjectOfType<Game>();
        petNameLabel = transform.gameObject.GetComponent<Text>();

        pet = FindObjectOfType<PetBehavior>();
    }

    // Use this for initialization
    void Start () {
        nameStorage = GameObject.FindGameObjectWithTag("nameInput");
        
        if (nameStorage != null)
            petNameLabel.text = nameStorage.GetComponent<KeepNameInputValue>().petName;
        else
        {
            StartCoroutine(LoadPetName());
            petNameLabel.text = pet.petName;
        }
        
    }
	
	// Update is called once per frame
	void Update () {

    }
}
