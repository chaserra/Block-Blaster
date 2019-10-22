﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour {

    public static AdManager instance;
    private BannerView bannerView;

    //LIVE IDs!!!
    //-----iOS-----
    //private string appID = "ca-app-pub-2809056167239385~4202093992";
    //private string bannerID = "ca-app-pub-2809056167239385/5323603976";
    //---Android---
    //private string appID = "ca-app-pub-2809056167239385~2241615805";
    //private string bannerID = "ca-app-pub-2809056167239385/9391695128";

    //TEST IDs
    //-----iOS-----
    //private string appID = "ca-app-pub-3940256099942544~1458002511";
    //private string bannerID = "ca-app-pub-3940256099942544/2934735716";
    //---Android---
    //private string appID = "ca-app-pub-3940256099942544~3347511713";
    //private string bannerID = "ca-app-pub-3940256099942544/6300978111";

    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        #if UNITY_ANDROID
            string appId = "ca-app-pub-2809056167239385~2241615805";
        #elif UNITY_IPHONE
            string appId = "ca-app-pub-2809056167239385~4202093992";
        #else
            string appId = "unexpected_platform";
        #endif

        MobileAds.Initialize(appId);
    }

    public void RequestBannerAd() {
        #if UNITY_ANDROID
            string bannerId = "ca-app-pub-2809056167239385/9391695128";
        #elif UNITY_IPHONE
            string bannerId = "ca-app-pub-2809056167239385/5323603976";
        #else
            string bannerId = "unexpected_platform";
        #endif

        bannerView = new BannerView(bannerId, AdSize.Banner, AdPosition.Top);

        AdRequest request = new AdRequest.Builder()
            .AddTestDevice("d429435251a0328d716ca135a4661bfc")
            .AddTestDevice("219b5280c42442add2717345b8710ad1")
            .AddTestDevice("5724C5D85CA97C01B8FD6F0F3CBAB642")
            .Build();

        bannerView.LoadAd(request);
    }

    public void ShowBanner() {
        bannerView.Show();
    }

    public void HideBanner() {
        bannerView.Hide();
    }

    public void DestroyBanner() {
        bannerView.Destroy();
    }

}
