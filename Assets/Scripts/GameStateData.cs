using System;
using UnityEngine;

[Serializable]
public class GameStateData {
    public string petName;

    public int energyLevel, happinessLevel;

    public Boolean isSleep, isSick;

    public Boolean hasTakenBath;
    public TimeSpan totalSleepDuration;

    public int foodCount, rubCount;

    public DateTime lastRecordedEnergyTimer, lastRecordedHappinessTimer;
    public DateTime lastRecordedSleepStartTime, lastRecordedDate;

    public bool isEatingTimerOn, isRubTimerOn, isPlayTimerOn;
    public TimeSpan eatCountdown, rubCountdown, playCountdown;
    public DateTime eatCountdownLastTick, rubCountdownLastTick, playCountdownLastTick;

}
