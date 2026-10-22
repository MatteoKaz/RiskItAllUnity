using UnityEngine;

public class Dice : MonoBehaviour
{
    public int value = 1;
    [SerializeField] int diceLenght = 12;
    
    // simple event pour avoir la valeur du dés dans la rangé entre 1 et longeur du dés 
    public int RollDice ()
    {
        value = Random.Range(1, diceLenght);

        return value;
    }
}
