using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffSelectorScript : MonoBehaviour
{
    public GameObject debuff1;
    public GameObject debuff2;
    public GameObject debuff3;
    private int[] debuffIndices;

    // Start is called before the first frame update
    void Start()
    {
        debuffIndices = new int[3];
        //set text on the 3 buttons based on the debuffs assigned to each 
        SortedSet<int> validDebuffIndices = new SortedSet<int>();
        PowerupsList list = PowerupsList.GetInstance();
        if (list.hasInvincibility)
        {
            //special case: set all to remove invincibility 
            debuff1.SetActive(false);
            debuff3.SetActive(false);
            debuffIndices[1] = 0;
        }
        else
        {
            bool[] powerupArray = PowerupsList.GetInstance().GetPowerupArray();
            for (int i = 1; i < powerupArray.Length; i++)
            {
                if (powerupArray[i])
                {
                    //add all indices that still contain an unremoved powerup to the array 
                    validDebuffIndices.Add(i);
                }
            }
            int numDebuffsLeft = validDebuffIndices.Count;
            if (numDebuffsLeft < 3)
            {
                //select only 2/1 debuffs
            }
            else
            {
                //select 3 random debuffs
                for(int i = 0; i < 3; i++)
                {
                    int randomDebuffIndex = Random.Range(0, numDebuffsLeft);
                    //int randomDebuffIndex = numDebuffsLeft - 1;
                    //Debug.Log("Chose index: " + randomDebuffIndex + ", numDebuffsLeft = " + numDebuffsLeft);
                    int[] temp = new int[numDebuffsLeft]; //array so that we can access by index bc sortedset cant do that
                    validDebuffIndices.CopyTo(temp);
                    /*Debug.Log("Values starting in temp:");
                    foreach (int k in temp)
                    {
                        Debug.Log(k);
                    }*/
                    debuffIndices[i] = temp[randomDebuffIndex];
                    temp = removeAt(temp, randomDebuffIndex);
                    /*Debug.Log("Values left in temp:");
                    foreach (int k in temp)
                    {
                        Debug.Log(k);
                    }
                    Debug.Log("Chosen index = " + debuffIndices[i]);*/
                    validDebuffIndices.Clear();
                    for (int j = 0; j < temp.Length; j++)
                    {
                        validDebuffIndices.Add(temp[j]);
                    }
                    numDebuffsLeft--;
                }
                Debug.Log("Debuff indices: " + debuffIndices[0] + " " + debuffIndices[1] + " " + debuffIndices[2]);
            }
        }
    }

    private int[] removeAt(int[] arr, int index)
    {
        var foos = new List<int>(arr);
        foos.RemoveAt(index);
        return foos.ToArray();
    }

    public void RemoveBuff(int buttonIndex)
    {
        int buffIndex = debuffIndices[buttonIndex];
        switch (buffIndex)
        {
            case 0:
                PowerupsList.GetInstance().hasInvincibility = false;
                break;
            case 1:
                PowerupsList.GetInstance().hasDoubleJump = false;
                break;
            case 2:
                PowerupsList.GetInstance().hasBiggerProjectiles = false;
                break;
            case 3:
                PowerupsList.GetInstance().hasDoubleProjectiles = false;
                break;
            case 4:
                PowerupsList.GetInstance().hasHomingProjectiles = false;
                break;
            case 5:
                PowerupsList.GetInstance().hasBonusAtk = false;
                break;
            case 6:
                PowerupsList.GetInstance().hasBonusMvspd = false;
                break;
            case 7:
                PowerupsList.GetInstance().hasBonusMaxHp = false;
                break;
            case 8:
                PowerupsList.GetInstance().hasBonusJumpHeight = false;
                break;
            case 9:
                PowerupsList.GetInstance().hasRegen = false;
                break;
            case 10:
                PowerupsList.GetInstance().hasAcceleration = false;
                break;
            case 11:
                PowerupsList.GetInstance().hasNoFallDmg = false;
                break;
            case 12:
                PowerupsList.GetInstance().hasUnlimitedAmmo = false;
                break;
            default:
                Debug.LogWarning("RemoveBuff: Invalid index. Valid range is [0,12]; value passed = " + buffIndex);
                break;

        }
        SceneLoader.LoadScene("FightLevel");
    }
}
