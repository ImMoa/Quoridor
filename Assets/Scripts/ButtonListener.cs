using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonListener : MonoBehaviour, IPointerClickHandler
{
    public void StartQuoridor()
    {
        SceneManager.LoadScene("QuoridorScene");
        
    }

    public void AddGame()
    {

    }

    public void StartCommunity()
    {
        Application.OpenURL("https://bgcommunity-1fe2b.web.app/");
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        //if (eventData.clickCount == 1)
        //{
        //    string name = EventSystem.current.currentSelectedGameObject.name;

        //    switch(name)
        //    {
        //        case "QuoridorButton":
        //    }

        //}

        if (eventData.clickCount == 2)
        {
            string name=EventSystem.current.currentSelectedGameObject.name;
            
            switch(name)
            {
                case "CommunityButton":
                    StartCommunity();
                    break;
                case "QuoridorButton":
                    StartQuoridor();
                    break;
                case "GameAddButton":
                    AddGame();
                    break;
            }
        }
                throw new System.NotImplementedException();
    }
}
