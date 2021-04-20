using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform Person;
    public GameObject bullet;
    public GameObject rocket;
    public GameObject arrow;
    public float distance;

    private void OnDisable()
    {
        GameObject temp = new GameObject();
        Quaternion rotarion = new Quaternion(0, 0, Vector2.Angle(Person.position, transform.position), 0);
        
        switch (StartGame.selectedWeapon)
        {
            case StartGame.Weapon.Rocket:
                temp = Instantiate(rocket, transform.position, rotarion);
                temp.GetComponent<Rigidbody2D>().AddForce(transform.localPosition * 50, ForceMode2D.Force);
                break;
            case StartGame.Weapon.Pistol:
                temp = Instantiate(bullet, transform.position, rotarion);
                temp.GetComponent<Rigidbody2D>().AddForce(transform.localPosition * 100, ForceMode2D.Force);
                break;
            case StartGame.Weapon.Bow:
                temp = Instantiate(arrow, transform.position, rotarion);
                temp.GetComponent<Rigidbody2D>().AddForce(transform.localPosition * 50, ForceMode2D.Force);
                break;
        }
        Destroy(temp, 6f);
    }
}
