using UnityEngine;



[System.Serializable]
public struct PlayerStruct
{
    public int Money;
    public int currentCase;
    public int[] inventory; // objets du joueurs 
}



[CreateAssetMenu(fileName = "SO_PlayerDatas", menuName = "Scriptable Objects/SO_PlayerDatas")]
public class SO_PlayerDatas : ScriptableObject
{

    public PlayerStruct playerStruct;
}
