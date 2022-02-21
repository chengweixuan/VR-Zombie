using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public bool hasTalkie;
    public bool hasCalledHelp;
    public GameObject promptBox;
    public Text promptText;

    public bool isPressToTalk;
    public bool isTalking;
    public bool isPressToPickUp;

    private DialogueManager dialogueManager;
    private new AudioSource audio;

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        hasTalkie = false;
        hasCalledHelp = false;

        isPressToTalk = false;
        isPressToPickUp = false;
        isTalking = false;

        dialogueManager = GetComponent<DialogueManager>();
        audio = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (hasTalkie && !hasCalledHelp)
            {
                Debug.Log("call help");
                audio.Play();
                // yield return new WaitForSeconds(3);
                hasCalledHelp = true;
            }
        }
    }

    public void StartDialog(Dialogue dialogue)
    {
        dialogueManager.StartDialogue(dialogue);
        isTalking = true;
        isPressToTalk = false;
    }

    public void ShowPrompt(string prompt)
    {
        promptBox.SetActive(true);
        promptText.text = prompt;
    }

    public void StopPrompt()
    {
        promptBox.SetActive(false);
        isPressToTalk = false;
        isPressToPickUp = false;
    }

    public void DisplayNextSentence()
    {
        dialogueManager.DisplayNextSentence();
    }
}
