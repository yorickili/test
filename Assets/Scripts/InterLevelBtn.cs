using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InterLevelBtn : MonoBehaviour
{

    private GameObject btn_BackToMenu;
    private GameObject btn_TryAgain;

    float BtnType=0f;


    // Use this for initialization
    void Start()
    {
        btn_BackToMenu = GameObject.Find("Btn_BackToMenu");
        btn_TryAgain = GameObject.Find("Btn_TryAgain");

       if(btn_BackToMenu)
        btn_BackToMenu.GetComponent<Button>().onClick.AddListener(clickToBackToMenu);
       if(btn_TryAgain)
        btn_TryAgain.GetComponent<Button>().onClick.AddListener(clickToTryAgain);

    }

    void clickToBackToMenu()
    {
       
        BtnType = 2f; print(BtnType);
        StartCoroutine("btnManage");
    }
    void clickToTryAgain()
    {
       
        BtnType = 3f; print(BtnType);
        StartCoroutine("btnManage");
    }

    IEnumerator btnManage()
    {
        yield return new WaitForSeconds(1);
        if (BtnType == 2)  //clickToBackToMenu
        {
            Application.LoadLevel("welcome");
        }
        else if (BtnType == 3)  //clickToTryAgain
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        else
        {
           
        }

    }



}
