using System;
using CountDown;
using UnityEngine;

public class PlayerFightTrigger : MonoBehaviour
{
    public Player Player { get; set; }
    public bool OtherEntered { get; private set; }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        var triggerContains = other.TryGetComponent<PlayerFightTrigger>(out var fightTrigger);
        if (triggerContains
            && other.gameObject.CompareTag(gameObject.tag)
            && fightTrigger.Player != Player)
        {
            OtherEntered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerFightTrigger>(out var fightTrigger) 
            && other.gameObject.CompareTag(gameObject.tag)
            && fightTrigger.Player != Player)
        {
            OtherEntered = false;
        }
    }
}
