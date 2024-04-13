using System;
using System.Collections;
using UnityEngine;

namespace CountDown
{
    public class CanyonEjector: MonoBehaviour
    {
        private CompositeCollider2D _collider2D;
        private bool coroutineStarted;

        private void Awake()
        {
            _collider2D = GetComponent<CompositeCollider2D>();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if(!other.TryGetComponent<Player>(out var player))
                return;
         
            if (coroutineStarted)
                return;
            
            if (_collider2D.OverlapPoint(player.transform.position))
            {
                if (player.Item != null)
                {
                    var item = player.Item;
                    player.DropItem();
                    player.IntersectingObjects.Remove(item.GetComponent<Collider2D>());
                }
                player.Fall();
                
                StartCoroutine(DelayCoroutine(0.7f));
            }

            IEnumerator DelayCoroutine(float time)
            {
                player.LockInput(time);
                coroutineStarted = true;
                yield return new WaitForSeconds(time);
                player.LockMovement(0.1f);
                player.Rb2D.MovePosition(GameRoot.Instance.Player1 == player
                    ? GameRoot.Instance.Player1StartPosition
                    : GameRoot.Instance.Player2StartPosition);
                coroutineStarted = false;
            }
        }
    }
}