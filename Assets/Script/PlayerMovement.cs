
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int currentCase = 0;
    public Dice dice;
    public int rollValue = 0;
    public ManagerCircuit Mc;
    Vector3 locOffset = new Vector3(0f, 0.7f, 0f);
    private bool isMoving = false;
    
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

    // event qui bouge le joueur à une case spécific utile pour le start, ou encore la teleportation ou lorsqu'on vas relancer le niveau. 
    /*Fonctionnement en gros je recup en fonction de l'id donnée la case en question grace à la fonction dans le ManagerCircuit puis si la case est  null je retourne la fonction sinon, je set la position à celle de la case et je set les variable 
    comme le current case et la valeur du player struct
    */
    public void MoveToSpecific(int caseId)
    {
        Case target = Mc.GetCase(caseId);
        if (target == null)
        {
            return;
        }
        transform.position = target.transform.position + locOffset;
        currentCase = caseId;
        playerStruct.currentCase = caseId;
    }

    //la so moving est true je relance pas puis je lance ma fonction du dés pour avoir la valeur ensuite je lance ma coroutine de mouvement
    public void Move()
    {
        if (isMoving == true)
        {
            return;
        }
        rollValue = dice.RollDice();
        Debug.Log(rollValue);
        StartCoroutine(MoveAnim(rollValue, true));
    }
    //la so moving est true je relance pas puis je lance ma fonction du dés pour avoir la valeur ensuite je lance ma coroutine de mouvement
    public void MoveAuto(int value)
    {
        if (isMoving == true)
        {
            return;
        }
            
        StartCoroutine(MoveAnim(value, false));
    }
    /*Ici alors Move anim prend comme arg le nombre de pas a effectuer plus un bool qui permet de desac les effets speciaux pour l'event move auto, ensuite je set moving true je sais que l'anim se lance une seule fois
    puis je verifie le sens de marche, la pour chacun des pas a faire je set case en fonction ensuite je set les loc de depart et d'arrivé puis le temps de l'anim je fais mon lerp puis je set la pos final au cas ou, ensuite je set les variable pour 
    savoir sur quel case et mon joueur et un wait pour la pose entre chaque case enfin je set move a faux et je check pour lancer le check de l'effet*/
    public IEnumerator MoveAnim(int steps, bool checkEffect)
    {
        isMoving = true;
        bool forward = steps > 0;

        for (int i = 0; i < Mathf.Abs(steps); i++)
        {
            Case nextCase;
            if (forward)
            {
                nextCase = Mc.GetNextCase(currentCase);
            }
                
            else
            {
                nextCase = Mc.GetPreviousCase(currentCase);
            }
                
            if (nextCase == null)
            {
                break;  
            }
              

            Vector3 start = transform.position;
            Vector3 end = nextCase.transform.position + locOffset;
            float elapsed = 0f;
            const float duration = 0.25f;

            while (elapsed < duration)
            {
                transform.position = Vector3.Lerp(start, end, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            transform.position = end;

            currentCase = nextCase.caseValue;
            playerStruct.currentCase = currentCase;
            playerDatas.playerStruct.currentCase = currentCase;

            yield return new WaitForSeconds(0.2f);
        }

        isMoving = false;

        if (checkEffect== true)
        {
            Mc.SpecialCheck(Mc.GetCase(currentCase));
        }
           
    }
}
