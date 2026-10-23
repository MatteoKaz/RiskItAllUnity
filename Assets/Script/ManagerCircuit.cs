
using System;
using System.Collections.Generic;
using UnityEngine;

public class ManagerCircuit : MonoBehaviour
{
    public List<Case> circuit;
    private Dictionary<int, Case> casesById = new Dictionary<int, Case>();
    public event Action<int> SpecialMove;
    public event Action<int> AddMoney;
    public event Action<int> RemoveMoney;
    public event Action<int,bool> Teleport;

    //Construire le dictionnaire, pour chacune des cases de ma liste je l'integre au dico et je leur donne la clé qui est la valeur de ma case
    void Awake()
    {
        foreach (var cases in circuit)
        {
            casesById.Add(cases.caseValue, cases);
        }
            
    }
    //Obtenir la case grace à la clé 
    public Case GetCase(int id)
    {
        casesById.TryGetValue(id, out Case result);
        return result;
    }

    // Case suivante a partir de ma case si ma current est null ou que la longeur est 0 alors pas de suivante 
    public Case GetNextCase(int currentId, int pathIndex = 0)
    {
        //set de la current case grace à son id
        Case current = GetCase(currentId);
        if (current == null || current.NextID.Length == 0)
            return null;
        // Ici je clamp la valeur entre 0 et longeur -1 car on part de l'index 0
        pathIndex = Mathf.Clamp(pathIndex, 0, current.NextID.Length - 1);
        // Ici je retourne la valeur de la case suivante grace à la valeur de la case suivante qui est à mettre dans la case
        return GetCase(current.NextID[pathIndex]);
    }

    // Case précédente en gros pour chacune des cases de ma liste je regarde chacune des valeurs de leur liste Next.id et si une des valeurs est = a ma current alors je retour la case qui a la mienne ou je suis comme suivante 
    public Case GetPreviousCase(int currentId)
    {
        foreach (var cases in circuit)
        {
            foreach (int next in cases.NextID)
            {
                if (next == currentId)
                {
                    return cases;
                }
                
                    
            }
        }
        return null;
    }

    /*Check si la case ou j'ai fini à un type particulier si c'est le cas je lance l'event en fonction j'ai séparé les trois pour que les events similaire soit pas recodé ex movement special bah avant ou arriere c'est le meme principe money non pour feedback
     EN VRAI MIEUX DE FAIRE UN PART UN POUR FEEDBACK OU DES IF JSPPPPPPPPP*/
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
                Teleport(value,true);
                break;
            case CaseType.BonusMoney:
                MoneyAdd(value);
                break;
            case CaseType.MalusMoney:
                Remove_Money(value);
                break;

        }
    }

    // trois event pour les cas spéciaux qui invoke chez les scripts abonnée la fonction à appliqué
    public void MovementSpecial(int value)
    {
        Debug.Log("MoveLancé");
        SpecialMove?.Invoke(value);
    }
    public void MoneyAdd(int value)
    {
        AddMoney?.Invoke(value);
    }
    public void Remove_Money(int value)
    {
        RemoveMoney?.Invoke(value);
    }
    public void TeleportThings(int value)
    {
        Teleport?.Invoke(value,true);
    }

}
