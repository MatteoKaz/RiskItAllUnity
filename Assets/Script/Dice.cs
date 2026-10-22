using UnityEngine;

public class Dice : MonoBehaviour
{
    public int value = 1;
    [SerializeField] int diceLenght = 12;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int RollDice ()
    {
        value = Random.Range(1, diceLenght);

        return value;
    }
}
