using System;

using System.Collections;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class PlayerMovement : MonoBehaviour
{
    public int currentCase = 0;
    public Dice dice;
    public int rollValue = 0;
    public ManagerCircuit Mc;
    Vector3 locOffset = new Vector3(0f, 1.5f, 0f);
    private bool isMoving = false;
    public int goalCase = 0;
    [SerializeField] SO_PlayerDatas playerDatas;
    public PlayerStruct playerStruct;

    private void OnEnable()
    {
        Mc.SpecialMove += MoveAuto;
    }
    private void OnDisable()
    {
        Mc.SpecialMove -= MoveAuto;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStruct = playerDatas.playerStruct;
        MoveToSpecific(currentCase);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void MoveToSpecific(int caseId)
    {
        Case target = Mc.GetCase(caseId);
        if (target == null)
        {
            Debug.LogWarning($"Aucune case avec l'id {caseId}");
            return;
        }
        transform.position = target.transform.position + locOffset;
        currentCase = caseId;
        playerStruct.currentCase = caseId;
    }

    public void Move()
    {
        if (isMoving) return;
        rollValue = dice.RollDice();
        Debug.Log(rollValue);
        StartCoroutine(MoveAnim(rollValue, true));
    }

    public void MoveAuto(int value)
    {
        if (isMoving) return;
        // false = on ne relance pas l'effet de la case d'arrivée (évite les boucles infinies)
        StartCoroutine(MoveAnim(value, false));
    }

    public IEnumerator MoveAnim(int steps, bool checkEffect)
    {
        isMoving = true;
        bool forward = steps > 0;

        for (int i = 0; i < Mathf.Abs(steps); i++)
        {
            Case next = forward ? Mc.GetNextCase(currentCase)
                                : Mc.GetPreviousCase(currentCase);
            if (next == null) break;   // fin du circuit

            Vector3 start = transform.position;
            Vector3 end = next.transform.position + locOffset;
            float elapsed = 0f;
            const float duration = 0.25f;

            while (elapsed < duration)
            {
                transform.position = Vector3.Lerp(start, end, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            transform.position = end;

            currentCase = next.caseValue;
            playerStruct.currentCase = currentCase;

            yield return new WaitForSeconds(0.2f);
        }

        isMoving = false;

        if (checkEffect)
            Mc.SpecialCheck(Mc.GetCase(currentCase));
    }
}
