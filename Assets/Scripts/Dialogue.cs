using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class Dialogue : MonoBehaviour
{
    #region Variables
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public GameObject continueButon;

    public int indexScene;

    string sceneName;
    Scene currentScene;

    public GameObject NavigationUI;
    public GameObject Itemsui;
    public GameObject DialogueUI;
    public GameObject Flashback;
    public GameObject CurrentCase;
    public GameObject PuzzlePart;

    public bool EndScene;
    public bool FlahBackBool;
    public bool Casesolved;
    #endregion

    void Start()
    {
        StartCoroutine(Type());
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        DialogueUI.SetActive(true);
        NavigationUI.SetActive(false);
        Itemsui.SetActive(false);


    }
    private void Update()
    {
        if (textDisplay.text == sentences[index])
        {
            continueButon.SetActive(true);

        }

    }
    //affichage lettre par lettre
    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    //affichage de chaque phrase
    public void NextSentence()
    {
        continueButon.SetActive(false);

        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            continueButon.SetActive(false);
            //Change scene when the last dilogue in the case is over

            if (EndScene)
            {
                if (Casesolved)
                {
                    int levelInd = PlayerPrefs.GetInt("CurrentLevel");
                    PlayerPrefs.SetInt("CurrentLevel", levelInd + 1);
                }
                SceneManager.LoadScene(indexScene);
                Debug.Log("next");
            }

            else if (!EndScene)
            {
                Debug.Log("not end");

                if (FlahBackBool == true)
                {
                    if (CurrentCase)
                    {
                        DialogueUI.SetActive(false);
                        NavigationUI.SetActive(true);
                        Itemsui.SetActive(true);

                        if (PuzzlePart != null)
                            PuzzlePart.SetActive(true);
                    }
                    //return from the case to the flashabck
                    if (!Flashback.activeInHierarchy)
                    {
                        Flashback.SetActive(true);
                        CurrentCase.SetActive(false);
                    }
                    //return from te flashback to the case
                    else if (Flashback.activeInHierarchy)
                    {

                        Flashback.SetActive(false);
                        CurrentCase.SetActive(true);

                    }
                }
                else if (FlahBackBool == false)
                {
                    Debug.Log("not flashback");
                    DialogueUI.SetActive(false);
                    NavigationUI.SetActive(true);
                    Itemsui.SetActive(true); // show an item after finished the flashback and going back to the actual case
                }

            }

        }
    }

}
