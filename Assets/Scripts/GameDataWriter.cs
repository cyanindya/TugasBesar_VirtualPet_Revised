using System.IO;
using UnityEngine;

public class GameDataWriter{
    BinaryWriter writer;
    FileStream file;

    public GameDataWriter (string filePath)
    {
        this.file = File.Open(filePath, FileMode.Create);
        this.writer = new BinaryWriter(this.file);
    }
    
    public void SaveData(GameStateData stateData)
    {
        writer.Write(stateData.petName);

        writer.Write(stateData.energyLevel);
        writer.Write(stateData.happinessLevel);

        writer.Write(stateData.isSleep);
        writer.Write(stateData.isSick);

        writer.Write(stateData.hasTakenBath);
        writer.Write(stateData.totalSleepDuration.Ticks);
        writer.Write(stateData.foodCount);
        writer.Write(stateData.rubCount);

        writer.Write(stateData.lastRecordedEnergyTimer.ToBinary());
        writer.Write(stateData.lastRecordedHappinessTimer.ToBinary());
        writer.Write(stateData.lastRecordedSleepStartTime.ToBinary());
        writer.Write(stateData.lastRecordedDate.ToBinary());

        writer.Write(stateData.isEatingTimerOn);
        writer.Write(stateData.isRubTimerOn);
        writer.Write(stateData.isPlayTimerOn);

        writer.Write(stateData.eatCountdown.Ticks);
        writer.Write(stateData.rubCountdown.Ticks);
        writer.Write(stateData.playCountdown.Ticks);

        writer.Write(stateData.eatCountdownLastTick.ToBinary());
        writer.Write(stateData.rubCountdownLastTick.ToBinary());
        writer.Write(stateData.playCountdownLastTick.ToBinary());



        writer.Close();
        file.Close();
    }
}
