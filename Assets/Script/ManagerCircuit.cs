using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class ManagerCircuit : MonoBehaviour
{
    public List<Case> circuit;
    private Dictionary<int, Case> casesById = new Dictionary<int, Case>();
    public event Action<int> SpecialMove;
    public event Action<int> SpecialMoney;
    public event Action<int> Teleport;


    void Awake()
    {
        foreach (var c in circuit)
            casesById[c.caseValue] = c;
    }

    public Case GetCase(int id)
    {
        casesById.TryGetValue(id, out Case result);
        return result;
    }

    // Case suivante (pathIndex = quelle branche si plusieurs NextID)
    public Case GetNextCase(int currentId, int pathIndex = 0)
    {
        Case current = GetCase(currentId);
        if (current == null || current.NextID.Length == 0) return null;
        pathIndex = Mathf.Clamp(pathIndex, 0, current.NextID.Length - 1);
        return GetCase(current.NextID[pathIndex]);
    }

    // Case précédente : celle dont un NextID pointe vers currentId
    public Case GetPreviousCase(int currentId)
    {
        foreach (var c in circuit)
            foreach (int next in c.NextID)
                if (next == currentId) return c;
        return null;
    }


    public void SpecialCheck(Case FinalCase)
    {
        CaseType type = FinalCase.identity.caseType; 
        int value = FinalCase.identity.effectValue;
        Debug.Log("Valeur de la case" + value);
        Debug.Log("Type"+ type);
        switch (type)
        {
            case CaseType.Base:
                break;
            case CaseType.BonusCase:
                MovementSpecial(value);
                break;
            case CaseType.MalusCase:
                MovementSpecial(-value);
                break;
            case CaseType.MoveToSpecificCase:
                Teleport(value);
                break;
            case CaseType.BonusMoney:
                MoneySpecial(value);
                break;
            case CaseType.MalusMoney:
                MoneySpecial(value);
                break;

        }
    }

    public void MovementSpecial(int value)
    {
        Debug.Log("MoveLancé");
        SpecialMove?.Invoke(value);
    }
    public void MoneySpecial(int value)
    {
        SpecialMoney?.Invoke(value);
    }
    public void TeleportThings(int value)
    {
        Teleport?.Invoke(value);
    }

}
