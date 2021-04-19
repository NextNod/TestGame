using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveEng : MonoBehaviour
{
    public enum Weapon { Rocket, Pistol, Bow };
    
    public static int HP { set; get; }
    private bool past = false;
    private bool shoot = false;
    private float sum = 0;
    private int jumps = 0;

    public static Weapon selectedWeapon = Weapon.Rocket;
    public float radius = 2;
    public float jump = 4;
    public GameObject crossHair;
    public Button buttonRocket;
    public Button buttonPistol;
    public Button buttonBow;
    public Camera Camera;
    public TextMeshProUGUI PersonName;
    public SpriteRenderer spriteColor;
    public GameObject Panel;

    ColorBlock activeColor;
    ColorBlock passiveColor;
    Touch finger = new Touch();

    void Start() 
    {
        HP = 100;
        PersonName.text = StartGame.Name;
        switch (StartGame.color) 
        {
            case StartGame.Color.Red:
                spriteColor.color = new Color(255, 0, 0);
                break;
            case StartGame.Color.Green:
                spriteColor.color = new Color(0, 255, 0);
                break;
            case StartGame.Color.Blue:
                spriteColor.color = new Color(0, 0, 255);
                break;
        }
        activeColor = buttonRocket.colors;
        passiveColor = buttonPistol.colors;

        buttonRocket.onClick.AddListener(() => 
        {
            selectedWeapon = Weapon.Rocket;
            buttonRocket.colors = activeColor;
            buttonPistol.colors = passiveColor;
            buttonBow.colors = passiveColor;
        });
        
        buttonPistol.onClick.AddListener(() => 
        {
            selectedWeapon = Weapon.Pistol;
            buttonRocket.colors = passiveColor;
            buttonPistol.colors = activeColor;
            buttonBow.colors = passiveColor;
        });
        
        buttonBow.onClick.AddListener(() => 
        {
            selectedWeapon = Weapon.Bow;
            buttonRocket.colors = passiveColor;
            buttonPistol.colors = passiveColor;
            buttonBow.colors = activeColor;
        });
    }

    void Update()
    {
        sum += Time.deltaTime;
        bool touch;
        if (Input.touchCount > 0)
        {
            finger = Input.GetTouch(0);
            touch = true;
            if ((finger.position.x > (Screen.width - 40) / 2 &&
                finger.position.x < (Screen.width + 40) / 2) ||
                shoot)
            {
                shoot = true;
                Vector2 fingerPosition = Camera.main.ScreenToWorldPoint(finger.position);
                crossHair.transform.position = new Ray2D(transform.position, fingerPosition).GetPoint(radius);
            }
            else
            {
                if(finger.position.y < 480f)
                    if (finger.position.x > (Screen.width + 40) / 2)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, transform.position + transform.right, 2f * Time.deltaTime);
                        GetComponent<SpriteRenderer>().flipX = true;
                    }
                    else if (finger.position.x < (Screen.width - 40) / 2)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, transform.position + transform.right * -1, 2f * Time.deltaTime);
                        GetComponent<SpriteRenderer>().flipX = false;
                    }
            }
        }
        else touch = shoot = false;
        crossHair.SetActive(shoot);

        if (touch != past)
        {
            jumps++;
            if (past && shoot)
                shoot = false;
        }    

        if(sum > 0.5f)
        { 
            if (jumps > 2) GetComponent<Rigidbody2D>().AddForce(transform.up * jump, ForceMode2D.Impulse);
            sum = jumps = 0;
        }
        past = touch;

        if (HP <= 0) 
            EnablePerson(false);
    }

    public void EnablePerson(bool enabled) 
    {
        GetComponent<SpriteRenderer>().enabled = enabled;
        spriteColor.enabled = enabled;
        PersonName.enabled = enabled;
        Panel.SetActive(!enabled);
    }
}