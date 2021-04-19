using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEng : MonoBehaviour
{
    public GameObject Person;
    public bool isRight;
    public static int HP { set; get; } = 100;
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, transform.position + (transform.right * (isRight ? 1 : -1)), 2f * Time.deltaTime);
        GetComponent<SpriteRenderer>().flipX = isRight;
        if (HP <= 0) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Person)
            MoveEng.HP = 0;
        else if (collision.gameObject.tag == "Finish")
            isRight = !isRight;
    }
}
