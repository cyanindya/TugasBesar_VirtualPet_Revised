using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIControl : MonoBehaviour {

    PetBehavior pet;
    PetRenderer petRenderer;

    Slider energySlider;
    Slider happinessSlider;

    Button eatButton;
    Button bathButton;
    Button playButton;
    Button treatPetButton;
    Button sleepButton;
    Button patButton;

    Text eatTimer, playTimer, rubTimer, sleepTimer;
    
    // Use this for initialization
    void Awake () {
        // get existing pet object from screen
        pet = GameObject.FindObjectOfType<PetBehavior>();
        petRenderer = GameObject.FindObjectOfType<PetRenderer>();
        
        // set bar values, crude way
        energySlider = GameObject.FindGameObjectWithTag("energyBar").GetComponent<Slider>();
        happinessSlider = GameObject.FindGameObjectWithTag("happinessBar").GetComponent<Slider>();

        energySlider.value = pet.energy;
        happinessSlider.value = pet.happiness;

        // set buttons
        eatButton = GameObject.FindGameObjectWithTag("eatButton").GetComponent<Button>();
        bathButton = GameObject.FindGameObjectWithTag("bathButton").GetComponent<Button>();
        playButton = GameObject.FindGameObjectWithTag("playButton").GetComponent<Button>();
        sleepButton = GameObject.FindGameObjectWithTag("sleepButton").GetComponent<Button>();
        treatPetButton = GameObject.FindGameObjectWithTag("medicineButton").GetComponent<Button>();
        patButton = GameObject.FindGameObjectWithTag("patButton").GetComponent<Button>();

        // timers
        eatTimer = GameObject.FindGameObjectWithTag("foodTimer").GetComponent<Text>();
        playTimer = GameObject.FindGameObjectWithTag("playTimer").GetComponent<Text>();
        rubTimer = GameObject.FindGameObjectWithTag("rubTimer").GetComponent<Text>();
        sleepTimer = GameObject.FindGameObjectWithTag("sleepTimer").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update () {
        // Update sliders, crude way. Ideally, we want to use separate script for controlling sliders but
        // since we only have limited use on the slider, this should do.
        if (energySlider.value != pet.energy)
        {
            energySlider.value = pet.energy;
        }

        if (happinessSlider.value != pet.happiness)
        {
            happinessSlider.value = pet.happiness;
        }

        toggleEatButton();
        toggleBathButton();
        togglePlayButton();
        toggleMedicineButton();
        toggleSleepButton();
        toggleRubButton();

    }
    
    public void EatButtonAction()
    {
        pet.Eat();
        petRenderer.manualTrigger("eat");
    }


    public void BathButtonAction()
    {
        pet.Bath();
        petRenderer.manualTrigger("bath");
    }

    public void PlayButtonAction()
    {
        pet.Play();
        petRenderer.manualTrigger("play");
    }

    public void MedicineButtonAction()
    {
        pet.TreatSickness();
    }

    public void SleepButtonAction()
    {
        pet.SleepWake();
        petRenderer.manualTrigger("sleep");

        toggleSleepButton();
    }


    public void RubButtonAction() // probably use Raycast instead
    {
        pet.Rub();
        petRenderer.manualTrigger("pat");
    }

    void toggleEatButton()
    {
        if (pet.eatingTimerOn)
            eatButton.enabled = false;

        if (eatButton.enabled)
        {
            eatButton.GetComponent<Image>().color = Color.white;
            eatTimer.enabled = false;
        }
        else
        {
            eatButton.GetComponent<Image>().color = Color.gray;
            eatTimer.enabled = true;
        }

        if (eatTimer.enabled)
        {
            eatTimer.text = String.Format("Next: {0:00}:{1:00}:{2:00}", pet.timeUntilNextMealAllowed.Hours,
            pet.timeUntilNextMealAllowed.Minutes, pet.timeUntilNextMealAllowed.Seconds);
        }
    }

    void toggleBathButton()
    {
        if (pet.hasTakenBath)
        {
            bathButton.GetComponent<Image>().color = Color.gray;
        } else
        {
            bathButton.GetComponent<Image>().color = Color.white;
        }
    }

    void togglePlayButton()
    {
        if (pet.playTimerOn)
            playButton.enabled = false;

        if (playButton.enabled)
        {
            playButton.GetComponent<Image>().color = Color.white;
            playTimer.enabled = false;
        }
        else
        {
            playButton.GetComponent<Image>().color = Color.gray;
            playTimer.enabled = true;
        }

        if (playTimer.enabled)
        {
            playTimer.text = String.Format("Next: {0:00}:{1:00}:{2:00}", pet.timeUntilCanPlayAgain.Hours,
            pet.timeUntilCanPlayAgain.Minutes, pet.timeUntilCanPlayAgain.Seconds);
        }
        
    }

    void toggleSleepButton()
    {
        Text sleepWakeButtonText = sleepButton.GetComponentInChildren<Text>();

        if (pet.isSleep)
        {
            sleepWakeButtonText.text = "Wake up";

            eatButton.gameObject.SetActive(false);
            bathButton.gameObject.SetActive(false);
            playButton.gameObject.SetActive(false);
            treatPetButton.gameObject.SetActive(false);
            patButton.gameObject.SetActive(false);

            eatTimer.gameObject.SetActive(false);
            playTimer.gameObject.SetActive(false);
            rubTimer.gameObject.SetActive(false);
        }
        else
        {
            sleepWakeButtonText.text = "Sleep";

            eatButton.gameObject.SetActive(true);
            bathButton.gameObject.SetActive(true);
            playButton.gameObject.SetActive(true);
            treatPetButton.gameObject.SetActive(true);
            patButton.gameObject.SetActive(true);

            eatTimer.gameObject.SetActive(true);
            playTimer.gameObject.SetActive(true);
            rubTimer.gameObject.SetActive(true);
        }

        sleepTimer.text = String.Format("Left: {0:00}:{1:00}:{2:00}", pet.remainingSleepDuration.Hours,
            pet.remainingSleepDuration.Minutes, pet.remainingSleepDuration.Seconds);

        //sleepTimer.text = "Total: " + pet.remainingSleepDuration.Hours.ToString() + ":"
                //+ pet.remainingSleepDuration.Minutes.ToString() + ":"
                //+ pet.remainingSleepDuration.Seconds.ToString();

        if (pet.remainingSleepDuration <= TimeSpan.Zero)
        {
            sleepButton.GetComponent<Image>().color = Color.gray;
        } else
        {
            sleepButton.GetComponent<Image>().color = Color.white;
        }
        
    }

    void toggleMedicineButton()
    {

        if (pet.isSick)
        {
            treatPetButton.enabled = true;
            treatPetButton.GetComponent<Image>().color = Color.white;
        }
        else
        {
            treatPetButton.enabled = false;
            treatPetButton.GetComponent<Image>().color = Color.gray;
        }
    }

    void toggleRubButton()
    {
        if (pet.rubTimerOn)
            patButton.enabled = false;

        if (patButton.enabled)
        {
            patButton.GetComponent<Image>().color = Color.white;
            rubTimer.enabled = false;
        }
        else
        {
            patButton.GetComponent<Image>().color = Color.gray;
            rubTimer.enabled = true;
        }

        if (rubTimer.enabled)
        {
            rubTimer.text = String.Format("Next: {0:00}:{1:00}:{2:00}", pet.timeUntilNextRubAllowed.Hours,
            pet.timeUntilNextRubAllowed.Minutes, pet.timeUntilNextRubAllowed.Seconds);
        }
        
    }


}
