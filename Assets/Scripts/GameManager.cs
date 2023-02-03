using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{// normal bir script neden görünüþü farklý bilmiyorum.

    [SerializeField] private GameObject levelFinishParent;   // restart panelimiz
    private bool levelFinish = false;

    public bool GetLevelFinish
    {
        get { return levelFinish;}
    }

    private Target playerHealth;
    
    private void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Target>();
    }

    // Update is called once per frame
    void Update()
    {

        

        int enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount <= 0 || playerHealth.GetHealth<=0)
        {
            levelFinishParent.gameObject.SetActive(true);
            levelFinish=true;
            
        }
        else
        {
            levelFinishParent.gameObject.SetActive(false);
            levelFinish= false;
        }

    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

}
