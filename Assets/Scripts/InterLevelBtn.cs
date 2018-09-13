using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InterLevelBtn : MonoBehaviour
{

    private GameObject btn_NextLevel;
    private GameObject btn_BackToMenu;
    private GameObject btn_TryAgain;

    private GameObject btn_OffGuide;
    //private GameObject guideImage;
    float BtnType=0f;
    // Use this for initialization
    void Start()
    {
        btn_NextLevel = GameObject.Find("Btn_NextLevel");
        btn_BackToMenu = GameObject.Find("Btn_BackToMenu");
        btn_TryAgain = GameObject.Find("Btn_TryAgain");

        //btn_OffGuide = GameObject.Find("Btn_OffGuide");

        ///guideImage = GameObject.Find("GuideImage");

        // print("123");
        if (btn_NextLevel)
        btn_NextLevel.GetComponent<Button>().onClick.AddListener(clickToNextLevel);
       if(btn_BackToMenu)
        btn_BackToMenu.GetComponent<Button>().onClick.AddListener(clickToBackToMenu);
       if(btn_TryAgain)
        btn_TryAgain.GetComponent<Button>().onClick.AddListener(clickToTryAgain);

    }

    void clickToNextLevel()
    {
        //LoadLevel
       
        BtnType = 1f; print(BtnType);
        StartCoroutine("btnManage");
    }
    void clickToBackToMenu()
    {
        //LoadLevel
       
        BtnType = 2f; print(BtnType);
        StartCoroutine("btnManage");
    }
    void clickToTryAgain()
    {
        //LoadLevel
       
        BtnType = 3f; print(BtnType);
        StartCoroutine("btnManage");
    }

    IEnumerator btnManage()
    {
        yield return new WaitForSeconds(2);
        //Application.LoadLevel(Application.loadedLevel);
        if(BtnType == 1)  // clickToNextLevel
        {

        }
        else if (BtnType == 2)  //clickToBackToMenu
        {

        }
        else if (BtnType == 3)  //clickToTryAgain
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        else if(BtnType == 4)
        {
          
        }
        else
        {
           
        }
    }



}
