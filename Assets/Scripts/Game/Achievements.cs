﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievements : MonoBehaviour {

    //Score Achievements
    private int ach01Requirement = 100000;
    private string ach01Code = "01sA";
    private int ach02Requirement = 250000;
    private string ach02Code = "02sA";
    private int ach03Requirement = 500000;
    private string ach03Code = "03sA";
    private int ach04Requirement = 1000000;
    private string ach04Code = "04sA";
    private int ach05Requirement = 5000000;
    private string ach05Code = "05sA";
    private int ach06Requirement = 7500000;
    private string ach06Code = "06sA";
    private int ach07Requirement = 10000000;
    private string ach07Code = "07sA";

    //Combo Achievements
    private int ach08Requirement = 100;
    private string ach08Code = "08cA";
    private int ach09Requirement = 150;
    private string ach09Code = "09cA";
    private int ach10Requirement = 200;
    private string ach10Code = "10cA";
    private int ach11Requirement = 300;
    private string ach11Code = "11cA";
    private int ach12Requirement = 500;
    private string ach12Code = "12cA";
    private int ach13Requirement = 750;
    private string ach13Code = "13cA";
    private int ach14Requirement = 1000;
    private string ach14Code = "14cA";

    public void CheckAchievements(int score, int combo) {
        PlayerData data = SaveLoad.Load();

        if(score >= ach01Requirement && ach01Code != data.ach01Code) {
            SaveLoad.SaveAchievements(ach01Code, 01);
            Debug.Log("Achievement " + ach01Code + " unlocked!");
        }
        if (score >= ach02Requirement && ach02Code != data.ach02Code) {
            SaveLoad.SaveAchievements(ach02Code, 02);
            Debug.Log("Achievement " + ach02Code + " unlocked!");
        }
        if (score >= ach03Requirement && ach03Code != data.ach03Code) {
            SaveLoad.SaveAchievements(ach03Code, 03);
            Debug.Log("Achievement " + ach03Code + " unlocked!");
        }
        if (score >= ach04Requirement && ach04Code != data.ach04Code) {
            SaveLoad.SaveAchievements(ach04Code, 04);
            Debug.Log("Achievement " + ach04Code + " unlocked!");
        }
        if (score >= ach05Requirement && ach05Code != data.ach05Code) {
            SaveLoad.SaveAchievements(ach05Code, 05);
            Debug.Log("Achievement " + ach05Code + " unlocked!");
        }
        if (score >= ach06Requirement && ach06Code != data.ach06Code) {
            SaveLoad.SaveAchievements(ach06Code, 06);
            Debug.Log("Achievement " + ach06Code + " unlocked!");
        }
        if (score >= ach07Requirement && ach07Code != data.ach07Code) {
            SaveLoad.SaveAchievements(ach07Code, 07);
            Debug.Log("Achievement " + ach07Code + " unlocked!");
        }
        if (combo >= ach08Requirement && ach08Code != data.ach08Code) {
            SaveLoad.SaveAchievements(ach08Code, 08);
            Debug.Log("Achievement " + ach08Code + " unlocked!");
        }
        if (combo >= ach09Requirement && ach09Code != data.ach09Code) {
            SaveLoad.SaveAchievements(ach09Code, 09);
            Debug.Log("Achievement " + ach09Code + " unlocked!");
        }
        if (combo >= ach10Requirement && ach10Code != data.ach10Code) {
            SaveLoad.SaveAchievements(ach10Code, 10);
            Debug.Log("Achievement " + ach10Code + " unlocked!");
        }
        if (combo >= ach11Requirement && ach11Code != data.ach11Code) {
            SaveLoad.SaveAchievements(ach11Code, 11);
            Debug.Log("Achievement " + ach11Code + " unlocked!");
        }
        if (combo >= ach12Requirement && ach12Code != data.ach12Code) {
            SaveLoad.SaveAchievements(ach12Code, 12);
            Debug.Log("Achievement " + ach12Code + " unlocked!");
        }
        if (combo >= ach13Requirement && ach13Code != data.ach13Code) {
            SaveLoad.SaveAchievements(ach13Code, 13);
            Debug.Log("Achievement " + ach13Code + " unlocked!");
        }
        if (combo >= ach14Requirement && ach14Code != data.ach14Code) {
            SaveLoad.SaveAchievements(ach14Code, 14);
            Debug.Log("Achievement " + ach14Code + " unlocked!");
        }
    }

}