using System;

using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int currentCase = 1;
    public Dice dice;
    public int rollValue = 0;
    public ManagerCircuit Mc;
    Vector3 locOffset = new Vector3(0f, 1.5f, 0f);
    [SerializeField] Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        transform.position = Mc.circuit[0].transform.position + locOffset;
        currentCase = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move()
    {
      
       rollValue = dice.RollDice();
        Debug.Log(rollValue);
        int location = currentCase + rollValue;
        
        StartCoroutine(MoveAnim(location));


    }

    public IEnumerator MoveAnim(int location)
    {
        
        for (int i = currentCase; i < location; i++)
        {
            if (i == Mc.circuit.Count)
            {
                Vector3 position = transform.position;
                location = 0 + (location - i);
                
                i = 0;
                float duration = 0.2f;
                float elapsedTime = 0f;
                
                while (elapsedTime < duration)
                {
                    
                    elapsedTime += Time.deltaTime;  
                    transform.position = Vector3.Lerp(position, Mc.circuit[i].transform.position + locOffset, elapsedTime);
                    yield return new WaitForEndOfFrame();
                }
               

            }
            else
            {
                Vector3 position = transform.position;
                float t = 0f;
                float duration = 0.25f;
                Vector3 locOffset = new Vector3(0f, 1.5f, 0f);
                float elapsedTime = 0f;
                Debug.Log(i);
                while (elapsedTime < duration)
                {
                    
                    
                    
                    transform.position = Vector3.Lerp(position, Mc.circuit[i].transform.position + locOffset, elapsedTime / duration);
                    elapsedTime += Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }

            }
            yield return new WaitForSeconds(0.5f);
        }

        currentCase = location;
        
    }
}
