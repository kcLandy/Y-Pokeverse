using UnityEngine;
using System.Collections;
using System;
using Unity.VisualScripting;

public class Character : MonoBehaviour
{
    public float moveSpeed;

    public bool IsMoving { get; private set; }
    CharacterAnimator animator;
    private void Awake()
    {
        animator = GetComponent<CharacterAnimator>();
    }
    public IEnumerator Move(Vector2 moveVec, Action OnMoveOver = null)
    {

        animator.MoveX = Mathf.Clamp(moveVec.x, -1f, 1f);
        animator.MoveY = Mathf.Clamp(moveVec.y, -1f, 1f);

        var targetPos = transform.position;
        targetPos.x += moveVec.x;
        targetPos.y += moveVec.y;

        if(!IsPathClear(targetPos))
        yield break;

        IsMoving = true;

        // Move the player to the target position
        while ((targetPos - transform.position).sqrMagnitude > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Set the player position to the target position
        transform.position = targetPos;

        IsMoving = false;

        OnMoveOver?.Invoke();
    }

    public void HandleUpdate()
    {
        animator.IsMoving = IsMoving;
    }
    
    private bool IsPathClear(Vector3 targetPosition)
    {
        var diff = targetPosition - transform.position;
        var dir = diff.normalized;

        if (Physics2D.BoxCast(transform.position + dir, new Vector2(0.2f, 0.2f), 0f,dir, diff.magnitude - 1, GameLayers.i.SolidLayer | GameLayers.i.InteractableLayer | GameLayers.i.PlayerLayer) == true)
        return false;

        return true;
        
    }
    private bool IsWalkable(Vector3 targetPosition)
    {
       if (Physics2D.OverlapCircle(targetPosition, 0.3f, GameLayers.i.SolidLayer | GameLayers.i.InteractableLayer) !=null)
       {
           return false;
       }
         return true;
    }

    public void LookTowards(Vector3 targetPosition)
    {
        var xdiff = Mathf.Floor(targetPosition.x) - Mathf.Floor(transform.position.x);
        var ydiff = Mathf.Floor(targetPosition.y) - Mathf.Floor(transform.position.y);

        if (xdiff == 0 || ydiff == 0)
        {
            animator.MoveX = Mathf.Clamp(xdiff, -1f, 1f);
            animator.MoveY = Mathf.Clamp(ydiff, -1f, 1f);
        }
        else
        Debug.LogError("Diagonal movement is not allowed");

    }
    
    public CharacterAnimator Animator
    {
        get => animator;
    }
}
