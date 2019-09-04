using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class SaveLoad {

    public static void Save(GameController controller) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerProgress.dat";
        PlayerData saveData = new PlayerData();

        if (File.Exists(path)) {
            FileStream loadFile = new FileStream(path, FileMode.Open);
            PlayerData loadData = (PlayerData)formatter.Deserialize(loadFile);

            if (loadData.highestScore < controller.GetHighScore()) {
                saveData.highestScore = controller.GetHighScore();
            } else {
                saveData.highestScore = loadData.highestScore;
            }

            if (loadData.highestCombo < controller.GetHighestCombo()) {
                saveData.highestCombo = controller.GetHighestCombo();
            } else {
                saveData.highestCombo = loadData.highestCombo;
            }

            loadFile.Close();
        }
        FileStream saveFile = new FileStream(path, FileMode.Create);

        formatter.Serialize(saveFile, saveData);
        saveFile.Close();
    }

    public static PlayerData Load() {
        string path = Application.persistentDataPath + "/playerProgress.dat";
        BinaryFormatter formatter = new BinaryFormatter();

        if (File.Exists(path)) {
            FileStream file = new FileStream(path, FileMode.Open);

            PlayerData data = (PlayerData)formatter.Deserialize(file);
            file.Close();

            return data;
        } else {
            FileStream file = new FileStream(path, FileMode.Create);

            PlayerData data = new PlayerData();
            formatter.Serialize(file, data);
            file.Close();

            return data;
        }
    }

}