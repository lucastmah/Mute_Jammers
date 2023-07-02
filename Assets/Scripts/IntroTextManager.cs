using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class IntroTextManager : MonoBehaviour
{
    public GameObject introText;
    public GameObject stage0Img;
    // Start is called before the first frame update
    void Start()
    {
        if (PowerupsList.GetInstance().hasDoubleJump)
        {
            Debug.Log("test");
        }
        int currLevel = PowerupsList.GetInstance().currentLevel;
        string text = "Default";
        switch (currLevel)
        {
            case 1:
                text = "You've peaked in life, owning your own company! Let's finish our daily tasks.";
                stage0Img.SetActive(true);
                break;
            case 2: 
                text = "Another proud day as CEO! Work is easy, if not very interesting. Let's keep this quick..."; 
                break;
            case 3: 
                text = "VHS has been released and you have been replaced as CEO. President is still good though, right?";
                break;
            case 4: 
                text = "The Executive Board thinks you've been stressed lately, so perhaps Vice President would be better for you.";
                break;
            case 5:
                text = "From Vice President to Senior Director… well, at least you’re still in a Senior position.";
                break;
            case 6:
                text = "Microsoft Word is out, and apparently you’ve been slacking with it. Associate Director it is.";
                break;
            case 7:
                text = "What's so great about Excel? You don't get it, and coincidentally you're Senior Manager now.";
                break;
            case 8:
                text = "The first laptop released, and you hate the 24/7 emails. Enjoy being Assistant Manager.";
                break;
            case 9:
                text = "PowerPoint is a hit for business presentations, but your slides don't cut it. Say hello to Supervisor.";
                break;
            case 10:
                text = "With the release of Zoom, you're now stuck as a Junior Assistant. How low can you go?";
                break;
            case 11:
                text = "From CEO all the way to Trainee, and now the new intern is threatening your job!?";
                break;
            default:
                Debug.LogWarning("IntroTextManager.Start: Invalid level passed. Valid range [1, 10], index passed = " + currLevel);
                break;
        }
        introText.GetComponent<TMPro.TextMeshProUGUI>().text = text;
    }
}
