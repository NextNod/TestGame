using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainEng : MonoBehaviour
{
    public GameObject lastPlayer;
    
    void Start()
    {
       // EndTurn(lastPlayer);
    }

    public static void EndTurn(GameObject lastPlayer) 
    {
        GameObject[] gameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        EnemyEng enemyEng;
        PersonEng personEng;

        if (lastPlayer.TryGetComponent(out personEng))
            personEng.enabled = false;
        else if (lastPlayer.TryGetComponent(out enemyEng))
            enemyEng.enabled = false;

        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.TryGetComponent(out enemyEng) && gameObject != lastPlayer)
            {
                enemyEng.enabled = true;
                break;
            }
            else if (gameObject.TryGetComponent(out personEng) && gameObject != lastPlayer)
            {
                personEng.enabled = true;
                break;
            }
        }           
    }
}
