using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public enum CaseType
{
    Base,
    MalusCase,
    BonusCase,
    MoveToSpecificCase,
    MiniGame,
    Shop,
    BonusMoney,
    MalusMoney,
}

[System.Serializable]
public class CaseIdentities
{
    public CaseType caseType;
    public int effectValue;
    public Color color;

}

[CreateAssetMenu(fileName = "SO_CaseType", menuName = "Scriptable Objects/SO_CaseType")]
public class SO_ObjectsDatas : ScriptableObject
{

    public List<CaseIdentities> identities;

}