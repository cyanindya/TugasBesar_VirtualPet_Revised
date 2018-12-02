using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;

public class PetBehavior : MonoBehaviour {
    // Settings of the pet's parameters depletion
    const int normalEnergyDelay = 3; // minutes
    const int sleepEnergyDelay = normalEnergyDelay * 2; // minutes

    const int healthyEnergyThreshold = 60; // minutes
    const int tiredEnergyThreshold = 40; // minutes

    const int healthyHappinessDelay = 5; // minutes
    const int tiredHappinessDelay = 1; // minutes
    const int normalHappinessDelay = 2; // minutes

    const int maxFoodCount = 3;
    const int maxRubCount = 5;

    DateTime tmpDate;


    // key variables to be stored
    public string petName = "";

    [Range(0,100)] public int energy;
    [Range(0,100)] public int happiness;

    public Boolean hasTakenBath;

    public DateTime energyTimer, happinessTimer;

    public Boolean isSick;

    public Boolean isSleep;
    public DateTime sleepTimeLastTick;
    public TimeSpan remainingSleepDuration = TimeSpan.FromHours(10);

    public DateTime date;

    public int foodCount, rubCount;
    public bool eatingTimerOn, rubTimerOn;
    public DateTime mealTimerLastTick, rubTimerLastTick;
    public TimeSpan timeUntilNextMealAllowed = TimeSpan.FromHours(5);
    public TimeSpan timeUntilNextRubAllowed = TimeSpan.FromHours(3);

    public DateTime playTimerLastTick;
    public bool playTimerOn;
    public TimeSpan timeUntilCanPlayAgain = TimeSpan.FromMinutes(30);



    // Some basic operations of the pet
    public void Eat() {
        if (!isSick) // will only eat if pet is not sick
        {
            if (foodCount < maxFoodCount)
            {
                energy += 30;
                happiness += 10;

                foodCount += 1;

                eatingTimerOn = true;
                mealTimerLastTick = DateTime.Now;
            }
            else
            {
                happiness -= 1;
                
                Debug.Log("Hooman, I'm too full already! I won't be able to move at this rate!");
            }
        } else
        {
            //message = "Sorry hooman, but I'm not feeling well. :((";
            Debug.Log("Sorry hooman, but I'm not feeling well. :((");
        }
    }

    public void Bath() {
        if (!hasTakenBath)
        {
            happiness += 30;
            hasTakenBath = true;

            //message = "Thanks, hooman, I feel like new!";
        }
        else
        {
            happiness -= 3;

            //message = "I don't want to bathe again, hooman!";
            Debug.Log("I don't want to bathe again, hooman!");
        }
    }

    public void Rub() {
        if (rubCount < maxRubCount)
        {
            happiness += 5;
            rubCount += 1;

            rubTimerOn = true;
            rubTimerLastTick = DateTime.Now;

            //message = "Hooman's hand is nice.";
        }
        else
        {
            happiness -= 1;
            //message = "Hooman, I like you, but you're messing my hair at this rate!";
            Debug.Log("Hooman, I like you, but you're messing my hair at this rate!");
        }
    }

    public void Play() {
        happiness += 10;

        playTimerOn = true;
        playTimerLastTick = DateTime.Now;

        //message = "Hooman, let's play!";
    }

    public void SleepWake() {
        if (!isSleep)
        {
            if (remainingSleepDuration > TimeSpan.Zero)
            {
                sleepTimeLastTick = DateTime.Now;
                isSleep = true;
            }
            else
            {
                //message = "I've slept too much!";
                Debug.Log("I've slept too much!");
            }
        }
        else
        {
            remainingSleepDuration -= DateTime.Now - sleepTimeLastTick;
            sleepTimeLastTick = DateTime.Now;

            isSleep = false;

            //message = "Hi, hooman!";
        }
    }

    public void TreatSickness()
    {
        isSick = false;
        energy += 5;

        //message = "Thanks, hooman!";
    }

    // Special function to reset parameters
    private void ResetOnNewDay()
    {
        foodCount = 0;
        rubCount = 0;

        remainingSleepDuration = TimeSpan.FromHours(10);

        hasTakenBath = false;
    }
    


    private void Awake()
    {
        // Run only during first time
        string savePath = Path.Combine(Application.persistentDataPath, "saveFile.dat");

        if (!File.Exists(savePath))
        {

            GameObject nameStorage = GameObject.FindGameObjectWithTag("nameInput");
            if (nameStorage != null)
                petName = nameStorage.GetComponent<KeepNameInputValue>().petName;


            energy = 100;
            happiness = 100;

            isSick = false;

            energyTimer = happinessTimer = DateTime.Now;
            date = DateTime.Today;

            ResetOnNewDay();
        }

    }

    private void Start()
    {

    }

    private void OnApplicationQuit()
    {
        if (!isSleep)
            SleepWake();
    }

    private void Update()
    {
        // round values when it goes to below or under limits
        if (energy > 100)
            energy = 100;
        else if (energy < 0)
            energy = 0;

        if (happiness > 100)
            happiness = 100;
        else if (happiness < 0)
            happiness = 0;

        // update based on time passed
        DateTime newEnergyTimer, newHappinessTimer;
        newEnergyTimer = newHappinessTimer = DateTime.Now;

        TimeSpan energyTimerSpan = newEnergyTimer - energyTimer;
        TimeSpan happinessTimerSpan = newHappinessTimer - happinessTimer;

        // force sleep timer to be on zero if it runs out
        if (remainingSleepDuration < TimeSpan.Zero)
            remainingSleepDuration = TimeSpan.Zero;

        /// gradually reduce energy. sleeping reduces energy slower.
        if (isSleep)
        {
            //message = "Zzzz....";

            if (energyTimerSpan.TotalMinutes >= sleepEnergyDelay)
            {
                energy -= (int)energyTimerSpan.TotalMinutes / sleepEnergyDelay;
                energyTimer = newEnergyTimer;
            }

            // forces to wake up when pet already sleeps too much, otherwise reduce
            // timer normally
            if (remainingSleepDuration <= TimeSpan.Zero) {
                SleepWake();
            } else
            {
                remainingSleepDuration -= DateTime.Now - sleepTimeLastTick;
                sleepTimeLastTick = DateTime.Now;
            }

        } else
        {
            if (energyTimerSpan.TotalMinutes >= normalEnergyDelay)
            {
                energy -= (int)energyTimerSpan.TotalMinutes / normalEnergyDelay;
                energyTimer = newEnergyTimer;
            }
        }
        

        /// gradually reduce happiness depending on energy
        if (energy >= healthyEnergyThreshold)
        {
            if (happinessTimerSpan.TotalMinutes >= healthyHappinessDelay)
            {
                happiness -= (int)happinessTimerSpan.TotalMinutes /healthyHappinessDelay;
                happinessTimer = newHappinessTimer;
            }
        }
        else if (energy == 0) // pet is sick
        {
            isSick = true;

            //message = "I'm not feeling well...";

            if (happinessTimerSpan.TotalMinutes >= tiredHappinessDelay)
            {
                happiness -= (int)happinessTimerSpan.TotalMinutes / tiredHappinessDelay * 2; // penalty
                happinessTimer = newHappinessTimer;
            }

        }
        else if (energy < tiredEnergyThreshold)
        {
            if (happinessTimerSpan.TotalMinutes >= tiredHappinessDelay)
            {
                happiness -= (int)happinessTimerSpan.TotalMinutes / tiredHappinessDelay;
                happinessTimer = newHappinessTimer;
            }
        }
        else {
            if (happinessTimerSpan.TotalMinutes >= normalHappinessDelay)
            {
                happiness -= (int)happinessTimerSpan.TotalMinutes / normalHappinessDelay;
                happinessTimer = newHappinessTimer;
            }
        }

        // metabolism management timers
        if (eatingTimerOn || rubTimerOn || playTimerOn)
            tmpDate = DateTime.Now;
        
        if (eatingTimerOn)
        {
            timeUntilNextMealAllowed -= tmpDate - mealTimerLastTick;
            mealTimerLastTick = tmpDate;

            if (timeUntilNextMealAllowed <= TimeSpan.Zero)
            {
                eatingTimerOn = false;
                timeUntilNextMealAllowed = TimeSpan.FromHours(5);
            }

        }

        if (rubTimerOn)
        {
            timeUntilNextRubAllowed -= tmpDate - rubTimerLastTick;
            rubTimerLastTick = tmpDate;

            if (timeUntilNextRubAllowed <= TimeSpan.Zero)
            {
                rubTimerOn = false;
                timeUntilNextRubAllowed = TimeSpan.FromHours(3);
            }

        }
        
        if (playTimerOn)
        {
            timeUntilCanPlayAgain -= DateTime.Now - playTimerLastTick;
            playTimerLastTick = DateTime.Now;
            
            if (timeUntilCanPlayAgain <= TimeSpan.Zero)
            {
                playTimerOn = false;
                timeUntilCanPlayAgain = TimeSpan.FromMinutes(30);
            }

        }


        // Reset certain parameters when day changes
        if (DateTime.Today > date)
        {
            ResetOnNewDay();

            // workaround if pet is still sleeping during day change, so
            // the sleep time is counted from midnight
            if (isSleep)
            {
                sleepTimeLastTick = DateTime.Today;
            }

            date = DateTime.Today;
        }
        
    }
}
