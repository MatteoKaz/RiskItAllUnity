using TMPro;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    [SerializeField] SO_PlayerDatas playerDatas;
    public PlayerStruct playerStruct;
    public int currentMoney = 25;
    public ManagerCircuit Mc;
    public TextMeshProUGUI textMeshPro;

    private void OnEnable()
    {
        Mc.AddMoney += AddMoney;
        Mc.RemoveMoney += RemoveMoney;
    }

    private void OnDisable()
    {
        Mc.AddMoney -= AddMoney;
        Mc.RemoveMoney -= RemoveMoney;
    }
    //Iinitialisation des valeurs 
    void Start()
    {
        playerStruct = playerDatas.playerStruct;
        currentMoney = playerStruct.Money;
        UpdateText();
    }

    // ajoute des sous
    public void AddMoney(int money)
    {
        currentMoney +=  money;
        playerDatas.playerStruct.Money = currentMoney;
        UpdateText();
    }
    // enleve et Max entre current et 0 prend la plus grande valeur entre les 2 un clamp de flemme
    public void RemoveMoney(int money)
    {
        currentMoney = Mathf.Max(currentMoney - money, 0);
        playerDatas.playerStruct.Money = currentMoney;
        UpdateText();
    }
    //Update ui
    void UpdateText()
    {
        textMeshPro.text = "Money: " + currentMoney;
    }
}
