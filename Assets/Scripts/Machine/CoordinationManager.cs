using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Tools;
using UnityEngine;

[Serializable]
public class DashboardObj
{
    public GameObject obj;
    public TMP_Text coordinationText;
    public Animator coordinationAni;

    private bool hasText, hasAni;
    
    private int num;
    public int Num
    {
        get => num;
        set
        {
            if(value != num) FlipOne();
            num = value;
        }
    }
    public void Init(GameObject dashboardObj)
    {
        this.obj = dashboardObj;
        coordinationText = dashboardObj.GetComponentInChildren<TMP_Text>();
        coordinationAni = dashboardObj.GetComponent<Animator>();
        hasText = coordinationText != null;
        hasAni = coordinationAni != null;
        num = 0;
    }

    public void Update()
    {
        if(hasText) coordinationText.text = Num.ToString();
    }
    
    private void FlipOne()
    {
        if (!hasAni) return;
        coordinationAni.SetTrigger(AnimatorParam.Flip);
    }
}

public class CoordinationManager : MonoBehaviour
{
    // latitude: 表示緯度(y軸)
    [SerializeField]private GameObject[] latitudeObjs = new GameObject[4];
    // longitude: 表示經度(x軸)
    [SerializeField] private GameObject[] longitudeObjs = new GameObject[4];
    
    [SerializeField] private DashboardObj[] latitude = new DashboardObj[4];
    [SerializeField] private DashboardObj[] longitude = new DashboardObj[4];

    //[SerializeField] private float latitudeCoordination;
    //[SerializeField] private float longitudeCoordination;
    
    
    
    void Awake()
    {
        for(var i = 0; i < latitudeObjs.Length; i++) latitude[i].Init(latitudeObjs[i]);
        for(var i = 0; i < longitudeObjs.Length; i++) longitude[i].Init(longitudeObjs[i]);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var obj in latitude) obj.Update();
        foreach(var obj in longitude) obj.Update();
    }

    public void SetCoordination(Vector2 coordination)
    {
        ChangeCoordinationNum(ref latitude, coordination.y);
        ChangeCoordinationNum(ref longitude, coordination.x);
    }

    private void ChangeCoordinationNum(ref DashboardObj[] coordination, float latNum)
    {
        coordination[0].Num = (int) latNum / 10;
        coordination[1].Num = Mathf.Abs( (int) latNum % 10 );
        coordination[2].Num = Mathf.Abs( (int) (latNum * 10) % 10 );
        coordination[3].Num = Mathf.Abs( (int) (latNum * 100) % 10 );
    }
}
