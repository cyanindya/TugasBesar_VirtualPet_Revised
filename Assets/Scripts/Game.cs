using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

    string savePath;

    PetBehavior currentPet;
    GameStateData stateData;

    Scene titleScene;
    Scene mainScene;

    // flag to notify the pet data has been loaded
    public bool petDataLoaded = false;
    
    private void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "saveFile.dat");

        titleScene = SceneManager.GetSceneByBuildIndex(0);
        mainScene = SceneManager.GetSceneByBuildIndex(1);

        if (titleScene.isLoaded)
            SceneManager.UnloadSceneAsync(titleScene);
        if (mainScene.isLoaded)
            SceneManager.UnloadSceneAsync(mainScene);

    }


    // Use this for initialization
    void Start () {

        currentPet = GameObject.FindObjectOfType<PetBehavior>();
        
        if (File.Exists(savePath))
            Load();

        // Debug.Log(currentPet.isSleep);
        // currentPet.SleepWake();

        petDataLoaded = true;
    }

    private void OnDestroy()
    {
        Save();
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        
    }

    void Save() {
        
        GameDataWriter dataWriter = new GameDataWriter(savePath);

        stateData = new GameStateData
        {
            petName = currentPet.petName,

            energyLevel = currentPet.energy,
            happinessLevel = currentPet.happiness,

            isSleep = currentPet.isSleep,
            isSick = currentPet.isSick,

            hasTakenBath = currentPet.hasTakenBath,
            totalSleepDuration = currentPet.remainingSleepDuration,
            foodCount = currentPet.foodCount,
            rubCount = currentPet.rubCount,

            lastRecordedEnergyTimer = currentPet.energyTimer,
            lastRecordedHappinessTimer = currentPet.happinessTimer,
            lastRecordedSleepStartTime = currentPet.sleepTimeLastTick,
            lastRecordedDate = currentPet.date,

            isEatingTimerOn = currentPet.eatingTimerOn,
            isRubTimerOn = currentPet.rubTimerOn,
            isPlayTimerOn = currentPet.playTimerOn,

            eatCountdown = currentPet.timeUntilNextMealAllowed,
            rubCountdown = currentPet.timeUntilNextRubAllowed,
            playCountdown = currentPet.timeUntilCanPlayAgain,

            eatCountdownLastTick = currentPet.mealTimerLastTick,
            rubCountdownLastTick = currentPet.rubTimerLastTick,
            playCountdownLastTick = currentPet.playTimerLastTick
        };

        Debug.Log(stateData.isSleep);

        dataWriter.SaveData(stateData);
        

        Debug.Log("File saved.");
    }

    void Load()
    {
        GameDataReader stateReader = new GameDataReader(savePath);

        GameStateData stateData = stateReader.LoadData();

        currentPet.petName = stateData.petName;

        currentPet.energy = stateData.energyLevel;
        currentPet.happiness = stateData.happinessLevel;

        currentPet.isSleep = stateData.isSleep;
        currentPet.isSick = stateData.isSick;

        currentPet.hasTakenBath = stateData.hasTakenBath;
        currentPet.remainingSleepDuration = stateData.totalSleepDuration;
        currentPet.foodCount = stateData.foodCount;
        currentPet.rubCount = stateData.rubCount;

        currentPet.energyTimer = stateData.lastRecordedEnergyTimer;
        currentPet.happinessTimer = stateData.lastRecordedHappinessTimer;
        currentPet.sleepTimeLastTick = stateData.lastRecordedSleepStartTime;
        currentPet.date = stateData.lastRecordedDate;

        currentPet.eatingTimerOn = stateData.isEatingTimerOn;
        currentPet.rubTimerOn = stateData.isRubTimerOn;
        currentPet.playTimerOn = stateData.isPlayTimerOn;

        currentPet.timeUntilNextMealAllowed = stateData.eatCountdown;
        currentPet.timeUntilNextRubAllowed = stateData.rubCountdown;
        currentPet.timeUntilCanPlayAgain = stateData.playCountdown;

        currentPet.mealTimerLastTick = stateData.eatCountdownLastTick;
        currentPet.rubTimerLastTick = stateData.rubCountdownLastTick;
        currentPet.playTimerLastTick = stateData.playCountdownLastTick;

        // Debug.Log("File loaded.");
        //Debug.Log(stateData.petName);

        // desperate measure because Unity is being an ass
        //PetNameLabelController textController = FindObjectOfType<PetNameLabelController>();
        //textController.petNameLabel.text = stateData.petName;

    }
}
