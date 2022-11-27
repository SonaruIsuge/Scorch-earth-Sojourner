using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.Events;

public class R_Slime : CameraRecordableBehaviour
{
    [SerializeField] private float detectRange;
    [SerializeField] private Vector2 escapeDest;
    [SerializeField] private float timeToDestination;
    private Player player;
    private Animator slimeAni;
    private bool isMove;
    
    public UnityEvent OnSlimeEscape;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        slimeAni = GetComponent<Animator>();
    }
    

    private void Update()
    {
        if (!isMove && !IsClone && Vector2.Distance(player.transform.position, transform.position) <= detectRange)
        {
            StartCoroutine(moveSelf());
        }
        
        slimeAni.SetBool(AnimatorParam.SlimeMove, isMove);
    }


    private IEnumerator moveSelf()
    {
        isMove = true;
        var origin = transform.localPosition;
        var currentMovementTime = 0f;
        while (Vector2.Distance(transform.localPosition, escapeDest) > 0.001f) 
        {
            currentMovementTime += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(origin, escapeDest, currentMovementTime / timeToDestination);
            yield return null;
        }

        isMove = false;
        OnSlimeEscape?.Invoke();
    }


    public void SetDetectRange(float range)
    {
        detectRange = range;
    }
    
    
    public override void CameraHit()
    {
        base.CameraHit();
    }
    

    public override void ItemUse(M_ProjectMachine machine)
    {
        base.ItemUse(machine);
    }
}
