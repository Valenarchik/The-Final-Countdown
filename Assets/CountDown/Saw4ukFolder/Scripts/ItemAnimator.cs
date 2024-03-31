using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform parentTransform;

    public void RestoreDefaults()
    {
        transform.SetParent(parentTransform);
    }
    
    public void AnimatePickItemUp(Vector3 itemTransformPosition)
    {
        transform.SetParent(null);
        transform.position = itemTransformPosition;
        animator.SetTrigger("TakeItem");
    }
    
    public void AnimatePlaceItem(Vector3 itemTransformPosition)
    {
        transform.SetParent(null);
        transform.position = itemTransformPosition;
        animator.SetTrigger("PlaceItem");
    }
}
