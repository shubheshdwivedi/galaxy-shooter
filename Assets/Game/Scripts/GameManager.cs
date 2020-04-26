using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true;
    public GameObject player;
    private UIManager _uiManager;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(this.player, Vector3.zero, Quaternion.identity);
                this.gameOver = false;
                _uiManager.HideTitleScreen();
            }
        }
    }
}
