using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public enum Color { Red, Blue, Green }
    public enum Weapon { Rocket, Pistol, Bow };

    public Button startButton;
    public InputField nameText;
    public Dropdown unitColor;
    public Dropdown unitWeapon;

    public static string Name;
    public static Color color;
    public static Weapon selectedWeapon;


    void Start()
    {
        var button = startButton.GetComponent<Button>();
        var inputField = nameText.GetComponent<InputField>();
        var dropDown = unitColor.GetComponent<Dropdown>();
        var dropDownW = unitWeapon.GetComponent<Dropdown>();

        button.onClick.AddListener(() => 
        {
            if (inputField.text != "") 
            {
                Name = inputField.text;
                color = (Color)dropDown.value;
                selectedWeapon = (Weapon)dropDownW.value;
                Debug.Log($"Name: {name}, Color: {color}");
                SceneManager.LoadScene("GameScene");
            }
        });
    }
}
