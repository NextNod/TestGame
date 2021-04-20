using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameOver : MonoBehaviour
{
    private RewardedAd rewardedAd;
    private RectTransform Transform;
    private bool tween;
    private bool show;

    public TextMeshProUGUI text;
    public GameObject ButtonYes;
    public GameObject ButtonNo;
    public GameObject Person;

    void Start()
    {
        Person.GetComponent<PersonEng>().enabled = false;
        Transform = ButtonYes.GetComponent<RectTransform>();
        DOTween.SetTweensCapacity(500, 50);
        WaitWindow(true);
        show = false;
        Ad();
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

        if (rewardedAd.IsLoaded() && show)
            rewardedAd.Show();
    }

    void Ad() 
    {
        rewardedAd = new RewardedAd("ca-app-pub-3940256099942544/5224354917");
        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);

        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        rewardedAd.OnUserEarnedReward += (o, e) =>
        {
            PersonEng.HP = 50;
            Person.GetComponent<PersonEng>().enabled = true;
            Person.GetComponent<PersonEng>().EnablePerson(true);
            gameObject.SetActive(false);
        };

        ButtonYes.GetComponent<Button>().onClick.AddListener(() =>
        {
            show = true;
            WaitWindow(false);
        });

        ButtonNo.GetComponent<Button>().onClick.AddListener(() =>
        {
            Person.GetComponent<PersonEng>().EnablePerson(true);
            Scene mainScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(0);
        });
    }

    private void WaitWindow(bool enable) 
    {
        if (enable) text.text = "Продолжить?";
        else text.text = "Подождите";

        ButtonNo.SetActive(enable);
        ButtonYes.SetActive(enable);
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
