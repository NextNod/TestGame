using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private RewardedAd rewardedAd;
    private RectTransform Transform;
    private bool tween;

    public Button ButtonYes;
    public Button ButtonNo;
    void Start()
    {
        // Ad();
        Transform = ButtonYes.GetComponent<RectTransform>();
        DOTween.SetTweensCapacity(500, 50);
    }

    void Update()
    {
        if (!DOTween.IsTweening(Transform))
        {
            if (tween)
                Transform.DOMoveX(1135, 0.15f);
            else
                Transform.DOMoveX(1115, 0.15f);
            tween = !tween;
        }
    }

    void Ad() 
    {
        rewardedAd = new RewardedAd("ca-app-pub-4583590981102835~2970632670");
        AdRequest request = new AdRequest.Builder().Build();

        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        rewardedAd.OnUserEarnedReward += (o, e) =>
        {
            var gameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (GameObject gameObject in gameObjects)
                if (gameObject.name == "Person")
                    gameObject.GetComponent<MoveEng>().EnablePerson(true);
        };

        ButtonYes.onClick.AddListener(() =>
        {
            rewardedAd.LoadAd(request);
        });

        ButtonNo.onClick.AddListener(() =>
        {
            var gameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (GameObject gameObject in gameObjects)
                if (gameObject.name == "Person")
                    gameObject.GetComponent<MoveEng>().EnablePerson(true);
            Scene mainScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(0);
        });
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);
    }
}
