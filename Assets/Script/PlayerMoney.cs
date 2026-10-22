using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    [SerializeField] SO_PlayerDatas playerDatas;
    public PlayerStruct playerStruct;
    public int currentMoney = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStruct = playerDatas.playerStruct;
        currentMoney = playerStruct.Money;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
