using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonListener : MonoBehaviour, IPointerClickHandler
{
    public GameObject PauseCanvas;
    private bool isPaused;

    public void StartQuoridor()
    {
        SceneManager.LoadScene("QuoridorScene");
        
    }

    public void StartMenu()
    {
        SceneManager.LoadScene("MenuScene");
        //ShareCanvas.FirstCanvas.SetActive(false);

    }

    public void AddGame()
    {

    }

    public void Restart()
    {
        AudioManager.Instance.ButtonSelectEffect();
        StartQuoridor();
        Time.timeScale = 1;
    }
     // Exit 처리 구현할것
    public void Exit()
    {
        AudioManager.Instance.ButtonSelectEffect();
        StartMenu();
        Time.timeScale = 1;
    }

    // 버튼 누를때 뒤에 게임도 같이 적용되는거 수정
    public void Resume()
    {
        AudioManager.Instance.ButtonSelectEffect();
        PauseCanvas.SetActive(false);
        Time.timeScale = 1;

        GameObject.Find("ChessBoard").GetComponent<BoardManager>().isPaused = false;
    }

    public void Pause()
    {
        AudioManager.Instance.ButtonSelectEffect();
        PauseCanvas.SetActive(true);
        Time.timeScale=0;

        GameObject.Find("ChessBoard").GetComponent<BoardManager>().isPaused = true;
    }

    public void Description()
    {
        AudioManager.Instance.ButtonSelectEffect();
        Application.OpenURL("https://bgcommunity-1fe2b.web.app/?page=thread&gallery=quoridor&status=view&thread=DygyaPDWphfkgPZ7j8eo");
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

        //    switch (name)
        //    {
        //        case "QuoridorButton":
        //            ShareCanvas.QuoridorPreview.SetActive(true);
        //            break;
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
