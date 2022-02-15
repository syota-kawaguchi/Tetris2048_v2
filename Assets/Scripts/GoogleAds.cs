using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using System;

public class GoogleAds : SingletonMonoBehaviour<GoogleAds>
{
    private string adUnitId = "ca-app-pub-5728922507498585/7667680068";
    private string testAdUnitId = "ca-app-pub-3940256099942544/6300978111";

    private BannerView bannerView;

    private int[] hideAdsSceneIndexes = new int[] { 2 };

    void Start() {
        MobileAds.Initialize(InitializationStatus => { });
        RequestBanner();
    }

    private void RequestBanner() {
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        if (IsHideAds()) {
            HideBannerAds();
        }
        else {
            var request = new AdRequest.Builder().Build();
            bannerView.LoadAd(request);
            Debug.Log("bannerRequested");
        }
    }

    private bool IsHideAds() {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        foreach (int index in hideAdsSceneIndexes) {
            if (currentIndex == index) return true;
        }
        return false;
    }

    public void HideBannerAds() {
        if (bannerView == null) {
            Debug.Log("bannerView is null");
            return;
        }

        Debug.Log("bannerAds hide");

        bannerView.Hide();
    }

    public void ShowBannerAds() {
        if (bannerView == null) {
            Debug.Log("bannerView is null");
            return;
        }

        bannerView.Show();
    }
}
