using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class PetNameInput : MonoBehaviour {

    public GameObject petNameField;
    private string petName;

    Scene titleScene;
    Scene mainScene;

    // Use this for initialization
    void Awake () {

        titleScene = SceneManager.GetSceneByBuildIndex(0);
        mainScene = SceneManager.GetSceneByBuildIndex(2);

        if (titleScene.isLoaded)
            SceneManager.UnloadSceneAsync(titleScene);
        if (mainScene.isLoaded)
            SceneManager.UnloadSceneAsync(mainScene);

        petName = petNameField.GetComponent<InputField>().text;
	}
	
	// Update is called once per frame
	void Update () {
        petName = petNameField.GetComponent<InputField>().text;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("StartScreenScene");
        }
    }

    public void SubmitButtonAction()
    {
        if (petName != "")
        {
            petName = petNameField.GetComponent<InputField>().text;

            GameObject.FindGameObjectWithTag("nameInput")
                .GetComponent<KeepNameInputValue>()
                .petName = petName;
            
            if (Input.GetMouseButtonUp(0))
                SceneManager.LoadScene("PetSimulatorScene");
        }
        else
        {
            Debug.LogWarning("Pet name is empty.");
        }
    }
}
