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
    [SerializeField] private ItemSpawnController itemSpawnController;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Show()
    {
        var nearestItemTransform = itemSpawnController.Resources.Where(x => x != null).OrderBy(x => Vector3.Distance(x.position, playerObject.transform.position)).First();
        arrow.DestinationObject = nearestItemTransform.gameObject;
        animator.SetTrigger("animate");
    }
    
}
