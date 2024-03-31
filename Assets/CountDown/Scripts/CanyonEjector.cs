using System;
using UnityEngine;

namespace CountDown
{
    public class CanyonEjector: MonoBehaviour
    {
        private CompositeCollider2D collider2D;

        private void Awake()
        {
            collider2D = GetComponent<CompositeCollider2D>();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if(!other.TryGetComponent<Player>(out var player))
                return;
            
            if (collider2D.OverlapPoint(player.transform.position))
            {
                if (player.Item != null)
                {
                    var item = player.Item;
                    player.DropItem();
                    player.IntersectingObjects.Remove(item.GetComponent<Collider2D>());
                }
                player.LockMovement(0.1f);
                player.Rb2D.MovePosition(GameRoot.Instance.Player1 == player
                    ? GameRoot.Instance.Player1StartPosition
                    : GameRoot.Instance.Player2StartPosition);
            }
        }
    }
}