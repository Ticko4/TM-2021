using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    public int cherries = 0;
    public int health = 10;
    public TextMeshProUGUI healthAmount;
    public TextMeshProUGUI cherryText;

    public static PlayerUI playerUI;

        private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            gameObject.SetActive(false);
        }
      
        DontDestroyOnLoad(gameObject);
        if (!playerUI)
        {
            playerUI = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Reset()
    {
        cherries = 0;
        cherryText.text = cherries.ToString();
    }
}
