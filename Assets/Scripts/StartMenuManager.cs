using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class StartMenuManager : MonoBehaviour {

    public GameObject FlashText;
    string savePath;

    Scene nameInputScene;
    Scene mainScene;

	// Use this for initialization
	void Awake () {

        nameInputScene = SceneManager.GetSceneByBuildIndex(1);
        mainScene = SceneManager.GetSceneByBuildIndex(2);

        if (nameInputScene.isLoaded)
            SceneManager.UnloadSceneAsync(nameInputScene);
        if (mainScene.isLoaded)
            SceneManager.UnloadSceneAsync(mainScene);

        savePath = Path.Combine(Application.persistentDataPath, "saveFile.dat");
        
        // Flash the "Start" button repeatedly
        InvokeRepeating("FlashTheText", 0f, 0.5f);
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonUp(0))
        {
            if (File.Exists(savePath))
            {
                SceneManager.LoadScene("PetSimulatorScene");
            }
            else
            {
                SceneManager.LoadScene("PetNameScene");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
	}

    void FlashTheText()
    {
        if (FlashText.activeInHierarchy)
            FlashText.SetActive(false);
        else
            FlashText.SetActive(true);
    }
}
