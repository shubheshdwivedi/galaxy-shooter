using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private const float Speed = 3.0f;
    [SerializeField]
    private int powerUpId;

    [SerializeField] private AudioClip audioClip;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Time.deltaTime *  Speed * Vector3.down );
        if(transform.position.y < -7f)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            switch (powerUpId)  
            {
                case 0:
                    player.TripleShotPowerUp();
                    break;
                case 1:
                    player.SpeedPowerUp();
                    break;
                case 2:
                    player.AddShield();
                    break;
                default:
                    Debug.Log("Invalid ID");
                    break;
            }
        }
        AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
        Destroy(this.gameObject);
    }
}
