using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class SaveLoad : MonoBehaviour {

    private static SaveLoad _instance;
    public static SaveLoad Instance { get { return _instance; } }

    private void Awake() {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void Save(GameController controller) {
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

            saveData.ach01Code = loadData.ach01Code;
            saveData.ach02Code = loadData.ach02Code;
            saveData.ach03Code = loadData.ach03Code;
            saveData.ach04Code = loadData.ach04Code;
            saveData.ach05Code = loadData.ach05Code;
            saveData.ach06Code = loadData.ach06Code;
            saveData.ach07Code = loadData.ach07Code;
            saveData.ach08Code = loadData.ach08Code;
            saveData.ach09Code = loadData.ach09Code;
            saveData.ach10Code = loadData.ach10Code;
            saveData.ach11Code = loadData.ach11Code;
            saveData.ach12Code = loadData.ach12Code;
            saveData.ach13Code = loadData.ach13Code;
            saveData.ach14Code = loadData.ach14Code;
            saveData.ach01Obtained = loadData.ach01Obtained;
            saveData.ach02Obtained = loadData.ach02Obtained;
            saveData.ach03Obtained = loadData.ach03Obtained;
            saveData.ach04Obtained = loadData.ach04Obtained;
            saveData.ach05Obtained = loadData.ach05Obtained;
            saveData.ach06Obtained = loadData.ach06Obtained;
            saveData.ach07Obtained = loadData.ach07Obtained;
            saveData.ach08Obtained = loadData.ach08Obtained;
            saveData.ach09Obtained = loadData.ach09Obtained;
            saveData.ach10Obtained = loadData.ach10Obtained;
            saveData.ach11Obtained = loadData.ach11Obtained;
            saveData.ach12Obtained = loadData.ach12Obtained;
            saveData.ach13Obtained = loadData.ach13Obtained;
            saveData.ach14Obtained = loadData.ach14Obtained;

            loadFile.Close();
        }
        FileStream saveFile = new FileStream(path, FileMode.Create);

        formatter.Serialize(saveFile, saveData);
        saveFile.Close();
    }

    public void SaveAchievements(string achievementCode, int achievementIndex) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerProgress.dat";
        PlayerData saveData = new PlayerData();

        if(File.Exists(path)) {
            FileStream loadFile = new FileStream(path, FileMode.Open);
            PlayerData loadData = (PlayerData)formatter.Deserialize(loadFile);

            //Achievement Code String check
            if(achievementIndex == 01) {
                saveData.ach01Code = achievementCode;
                saveData.ach01Obtained = true;
            } else {
                saveData.ach01Code = loadData.ach01Code;
                saveData.ach01Obtained = loadData.ach01Obtained;
            }

            if (achievementIndex == 02) {
                saveData.ach02Code = achievementCode;
                saveData.ach02Obtained = true;
            } else {
                saveData.ach02Code = loadData.ach02Code;
                saveData.ach02Obtained = loadData.ach02Obtained;
            }

            if (achievementIndex == 03) {
                saveData.ach03Code = achievementCode;
                saveData.ach03Obtained = true;
            } else {
                saveData.ach03Code = loadData.ach03Code;
                saveData.ach03Obtained = loadData.ach03Obtained;
            }

            if (achievementIndex == 04) {
                saveData.ach04Code = achievementCode;
                saveData.ach04Obtained = true;
            } else {
                saveData.ach04Code = loadData.ach04Code;
                saveData.ach04Obtained = loadData.ach04Obtained;
            }

            if (achievementIndex == 05) {
                saveData.ach05Code = achievementCode;
                saveData.ach05Obtained = true;
            } else {
                saveData.ach05Code = loadData.ach05Code;
                saveData.ach05Obtained = loadData.ach05Obtained;
            }

            if (achievementIndex == 06) {
                saveData.ach06Code = achievementCode;
                saveData.ach06Obtained = true;
            } else {
                saveData.ach06Code = loadData.ach06Code;
                saveData.ach06Obtained = loadData.ach06Obtained;
            }

            if (achievementIndex == 07) {
                saveData.ach07Code = achievementCode;
                saveData.ach07Obtained = true;
            } else {
                saveData.ach07Code = loadData.ach07Code;
                saveData.ach07Obtained = loadData.ach07Obtained;
            }

            if (achievementIndex == 08) {
                saveData.ach08Code = achievementCode;
                saveData.ach08Obtained = true;
            } else {
                saveData.ach08Code = loadData.ach08Code;
                saveData.ach08Obtained = loadData.ach08Obtained;
            }

            if (achievementIndex == 09) {
                saveData.ach09Code = achievementCode;
                saveData.ach09Obtained = true;
            } else {
                saveData.ach09Code = loadData.ach09Code;
                saveData.ach09Obtained = loadData.ach09Obtained;
            }

            if (achievementIndex == 10) {
                saveData.ach10Code = achievementCode;
                saveData.ach10Obtained = true;
            } else {
                saveData.ach10Code = loadData.ach10Code;
                saveData.ach10Obtained = loadData.ach10Obtained;
            }

            if (achievementIndex == 11) {
                saveData.ach11Code = achievementCode;
                saveData.ach11Obtained = true;
            } else {
                saveData.ach11Code = loadData.ach11Code;
                saveData.ach11Obtained = loadData.ach11Obtained;
            }

            if (achievementIndex == 12) {
                saveData.ach12Code = achievementCode;
                saveData.ach12Obtained = true;
            } else {
                saveData.ach12Code = loadData.ach12Code;
                saveData.ach12Obtained = loadData.ach12Obtained;
            }

            if (achievementIndex == 13) {
                saveData.ach13Code = achievementCode;
                saveData.ach13Obtained = true;
            } else {
                saveData.ach13Code = loadData.ach13Code;
                saveData.ach13Obtained = loadData.ach13Obtained;
            }

            if (achievementIndex == 14) {
                saveData.ach14Code = achievementCode;
                saveData.ach14Obtained = true;
            } else {
                saveData.ach14Code = loadData.ach14Code;
                saveData.ach14Obtained = loadData.ach14Obtained;
            }

            saveData.highestScore = loadData.highestScore;
            saveData.highestCombo = loadData.highestCombo;
            loadFile.Close();
        }
        FileStream saveFile = new FileStream(path, FileMode.Create);

        formatter.Serialize(saveFile, saveData);
        saveFile.Close();
    }

    public PlayerData Load() {
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