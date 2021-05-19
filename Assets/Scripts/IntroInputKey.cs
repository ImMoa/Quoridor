using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroInputKey : MonoBehaviour
{
    public Button _button;
    public Text _buttonText;
    private GameObject firstCanvas;

    private void Start()
    {
        _button = GetComponent<Button>();
        _buttonText = GetComponent<Text>();
        StartCoroutine(FadeToFull());
        firstCanvas = GameObject.Find("FirstCanvas");
    }

    void Update()
    {
        if (Input.anyKeyDown && !Input.GetKey("escape"))
            firstCanvas.SetActive(false);
    }

    public IEnumerator FadeToFull()
    {
        _buttonText.color = new Color(_buttonText.color.r, _buttonText.color.g, _buttonText.color.b, 0);
        while (_buttonText.color.a < 1.0f)
        {
            _buttonText.color = new Color(_buttonText.color.r, _buttonText.color.g, _buttonText.color.b, _buttonText.color.a + (Time.deltaTime / 1.0f));
            yield return null;
        }
        StartCoroutine(FadeToZero());
    }

    public IEnumerator FadeToZero()
    {
        _buttonText.color = new Color(_buttonText.color.r, _buttonText.color.g, _buttonText.color.b, 1);
        while (_buttonText.color.a > 0.0f)
        {
            _buttonText.color = new Color(_buttonText.color.r, _buttonText.color.g, _buttonText.color.b, _buttonText.color.a - (Time.deltaTime / 1.0f));
            yield return null;
        }
        StartCoroutine(FadeToFull());
    }
}