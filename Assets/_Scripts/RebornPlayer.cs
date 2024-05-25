using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebornPlayer : MonoBehaviour
{
    public Transform rebornTransform;
    public GameObject player;
    public GameObject effect;

    void Start()
    {
        rebornTransform = GetComponent<Transform>();
    }
    public void Reborn()
    {
        GameManager.Ins.gameoverDiolog.SetActive(false);
        
        Player.Ins.isDead = false;
        player.gameObject.SetActive(true);
        
        GameManager.Ins.isGamePlaying = true;
        AudioController.Ins.PlayBackgroundMusic();
        if (effect) 
        {
            var effectI = Instantiate(effect, player.transform.position, Quaternion.identity);
            Destroy(effectI, 0.5f);
        }
    }
}
