
using System;
using System.IO;

public class GameDataReader {
    BinaryReader reader;
    FileStream file;

    public GameDataReader (string filePath) {
        this.file = File.Open(filePath, FileMode.Open);
        this.reader = new BinaryReader(this.file);
    }

    public GameStateData LoadData()
    {
        GameStateData loadedState = new GameStateData {
            petName = reader.ReadString(),

            energyLevel = reader.ReadInt32(),
            happinessLevel = reader.ReadInt32(),

            isSleep = reader.ReadBoolean(),
            isSick = reader.ReadBoolean(),

            hasTakenBath = reader.ReadBoolean(),
            totalSleepDuration = TimeSpan.FromTicks(reader.ReadInt64()),
            foodCount = reader.ReadInt32(),
            rubCount = reader.ReadInt32(),

            lastRecordedEnergyTimer = DateTime.FromBinary(reader.ReadInt64()),
            lastRecordedHappinessTimer = DateTime.FromBinary(reader.ReadInt64()),
            lastRecordedSleepStartTime = DateTime.FromBinary(reader.ReadInt64()),
            lastRecordedDate = DateTime.FromBinary(reader.ReadInt64()),

            isEatingTimerOn = reader.ReadBoolean(),
            isRubTimerOn = reader.ReadBoolean(),
            isPlayTimerOn = reader.ReadBoolean(),

            eatCountdown = TimeSpan.FromTicks(reader.ReadInt64()),
            rubCountdown = TimeSpan.FromTicks(reader.ReadInt64()),
            playCountdown = TimeSpan.FromTicks(reader.ReadInt64()),

            eatCountdownLastTick = DateTime.FromBinary(reader.ReadInt64()),
            rubCountdownLastTick = DateTime.FromBinary(reader.ReadInt64()),
            playCountdownLastTick = DateTime.FromBinary(reader.ReadInt64())
        };

        reader.Close();
        file.Close();

        return loadedState;
    }
}
