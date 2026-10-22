using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Case : MonoBehaviour
{
    public int caseValue = 0;
    [SerializeField] public CaseIdentities identity;
    public int specialvalue = 0;  
    private Material material;
    [SerializeField] public int[] NextID; 

    void Start()
    {
        
        material = GetComponent<MeshRenderer>().material;
        InitializeType(identity.caseType);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void InitializeType(CaseType type)
    {
        switch (type) 
        {
            case CaseType.Base:
                 break;
            case CaseType.BonusCase:
                specialvalue = identity.effectValue;
                material.color = identity.color;
                break;
            case CaseType.MalusCase:
                specialvalue = identity.effectValue;
                material.color = identity.color;
                break;
            case CaseType.MoveToSpecificCase:
                specialvalue = identity.effectValue;
                material.color = identity.color;
                break;
            case CaseType.BonusMoney:
                specialvalue = identity.effectValue;
                material.color = identity.color;
                break;
            case CaseType.MalusMoney:
                specialvalue = identity.effectValue;
                material.color = identity.color;
                break;
            



        }

    }
}
