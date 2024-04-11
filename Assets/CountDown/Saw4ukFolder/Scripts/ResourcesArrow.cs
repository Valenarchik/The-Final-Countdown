using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CountDown;
using UnityEngine;

public class ResourcesArrow : MonoBehaviour
{
    [SerializeField] private Arrow arrow;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private SpriteRenderer arrowSpriteRenderer;
    [SerializeField] private SpriteRenderer iconSpriteRenderer;
    [SerializeField] private ItemSpawnController itemSpawnController;

    [SerializeField] private Sprite boxSprite;
    [SerializeField] private Sprite detailSprite;
    [SerializeField] private Sprite capsuleSprite;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Show(ResourceTypeForArrow type)
    {
        switch (type)
        {
            case ResourceTypeForArrow.Recource:
                var nearestItemTransform = itemSpawnController.Resources.Where(x => x != null).OrderBy(x => Vector3.Distance(x.position, playerObject.transform.position)).First();
                arrow.DestinationObject = nearestItemTransform.gameObject;
                iconSpriteRenderer.sprite = boxSprite;
                break;
            case ResourceTypeForArrow.Detail:
                var nearestDetailTransform = itemSpawnController.Details.Where(x => x != null).OrderBy(x => Vector3.Distance(x.position, playerObject.transform.position)).First();
                arrow.DestinationObject = nearestDetailTransform.gameObject;
                iconSpriteRenderer.sprite = detailSprite;
                break;
            case ResourceTypeForArrow.Capsule:
                if (itemSpawnController.Capsule is not null)
                {
                    arrow.DestinationObject = itemSpawnController.Capsule.gameObject;
                    iconSpriteRenderer.sprite = capsuleSprite;
                }

                break;
        }
        animator.SetTrigger("animate");
    }
    
}

public enum ResourceTypeForArrow
{
    Recource,
    Detail,
    Capsule
}
