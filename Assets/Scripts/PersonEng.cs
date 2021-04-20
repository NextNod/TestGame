using UnityEngine;
using DG.Tweening;
using TMPro;

public class PersonEng : MonoBehaviour
{   
    public static int HP { set; get; }
    private bool past = false;
    private bool shoot = false;
    private float sum = 0;
    private int jumps = 0;

    private new Rigidbody2D rigidbody2D;
    private new BoxCollider2D collider2D;
    private SpriteRenderer sprite;

    public float radius = 2;
    public float jump = 4;
    public Vector2 startPosition;
    public GameObject crossHair;
    public Camera Camera;
    public Transform PCameraTransform;
    public TextMeshProUGUI PersonName;
    public SpriteRenderer spriteColor;
    public GameObject Panel;

    Touch finger = new Touch();

    void Start() 
    {
        HP = 100;
        spriteColor.enabled = PersonName.enabled = true;
        PersonName.text = StartGame.Name;
        rigidbody2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<BoxCollider2D>();
        PCameraTransform.DOMove(transform.position, 1);

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
    }

    void Update()
    {
        if (DOTween.IsTweening(PCameraTransform)) return;
        sum += Time.deltaTime;
        bool touch;
        PCameraTransform.position = transform.position;
        if (Input.touchCount > 0)
        {
            finger = Input.GetTouch(0);
            touch = true;
            if ((finger.position.x > (Screen.width - 40) / 2 &&
                finger.position.x < (Screen.width + 40) / 2) ||
                shoot)
            {
                shoot = true;
                crossHair.SetActive(true);
                Vector2 fingerPosition = Camera.main.ScreenToWorldPoint(finger.position);
                crossHair.transform.position = new Ray2D(transform.position, fingerPosition).GetPoint(radius);
            }
            else
            {
                if(finger.position.y < 480f)
                    if (finger.position.x > (Screen.width + 40) / 2)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, transform.position + transform.right, 2f * Time.deltaTime);
                        sprite.flipX = true;
                    }
                    else if (finger.position.x < (Screen.width - 40) / 2)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, transform.position + transform.right * -1, 2f * Time.deltaTime);
                        sprite.flipX = false;
                    }
            }
        }
        else touch = false;

        if (touch != past)
        {
            jumps++;
            if (past && shoot)
            {
                shoot = false;
                crossHair.SetActive(false);
                PersonName.enabled = spriteColor.enabled = false;
                MainEng.EndTurn(gameObject);
            }
        }    

        if(sum > 0.5f)
        { 
            if (jumps > 2 && collider2D.IsTouchingLayers())
                rigidbody2D.AddForce(transform.up * jump, ForceMode2D.Impulse);
            
            sum = jumps = 0;
        }
        past = touch;

        if (HP <= 0 || transform.position.y < -5)
        {
            transform.DOMove(startPosition, 1);
            EnablePerson(false);
        }
    }

    public void EnablePerson(bool enabled) 
    {
        sprite.enabled = enabled;
        spriteColor.enabled = enabled;
        PersonName.enabled = enabled;
        Panel.SetActive(!enabled);
    }
}