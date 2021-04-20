using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyEng : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Collider2D lastCollider2D;
    private new BoxCollider2D collider2D;
    private new Rigidbody2D rigidbody2D;
    
    public GameObject Person;
    public GameObject Bullet;
    public Transform Camera;
    public LayerMask barrierMask;
    public LayerMask personMask;
    public float jump;
    public bool calc = true;
    public bool isRight;
    public static int HP { set; get; } = 100;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<BoxCollider2D>();
        lastCollider2D = GetComponent<BoxCollider2D>();
        Camera.transform.DOMove(transform.position, 1);
    }

    private void AI() 
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Person.transform.position, float.MaxValue, barrierMask);
        if (hit2D.collider == null)
        {
            hit2D = Physics2D.Raycast(transform.position, Person.transform.position, 3, personMask);
            if (hit2D.collider != null)
            {
                GameObject temp = Instantiate(Bullet, transform.position, Quaternion.identity);
                temp.GetComponent<Rigidbody2D>().AddForce(transform.localPosition * 50, ForceMode2D.Force);
                MainEng.EndTurn(gameObject);
            }
        }
        else isRight = Person.transform.position.x > transform.position.x;
    }

    void Update()
    {
        if(!DOTween.IsTweening(Camera))
        {
            Camera.position = transform.position;
            AI();
            if(!collider2D.IsTouching(lastCollider2D))
                transform.position = Vector2.MoveTowards(transform.position, transform.position + (transform.right * (isRight ? 1 : -1)), 2f * Time.deltaTime);
            spriteRenderer.flipX = isRight;
        }
        if (HP <= 0) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            rigidbody2D.AddForce(transform.up * jump, ForceMode2D.Impulse);
            lastCollider2D = collision;
        }
    }
}
