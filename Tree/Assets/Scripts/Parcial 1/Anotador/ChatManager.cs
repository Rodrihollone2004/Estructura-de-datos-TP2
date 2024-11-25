using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ChatManager : MonoBehaviour
{
    [SerializeField] GameObject textPrefab; // prefab de texto (para el submiteado y el de el bottomPanel)
    [SerializeField] Transform contentParent; // panel donde van a estar los mensajes submiteados
    [SerializeField] Transform bottomPanel; // panel chiquito de abajo donde se escribe
    [SerializeField] private int maxCharacters = 100;


    private TMP_Text currentText;
    public bool isWriting = false;
    private bool chatJustActivated = false;

    private Stack<char> characterStack = new Stack<char>();
    private Queue<GameObject> messages = new Queue<GameObject>();
    private int maxMessages = 8;


    void Update()
    {
        Inputs();
    }

    void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (!isWriting)
            {
                CreateWritingArea();
                chatJustActivated = true; 
            }
        }

        KeyboardInputs();

    }

    void KeyboardInputs()
    {
        if (isWriting && currentText != null)
        {

            foreach (char c in Input.inputString)
            {
                if (chatJustActivated)
                {
                    chatJustActivated = false;
                    continue;
                }

                if (c == '\r')
                {
                    SubmitMessage(BuildString());
                    isWriting = false;
                    break;
                }
                else if (c == '\b')
                {
                    if (characterStack.Count > 0)
                    {
                        characterStack.Pop();
                        UpdateCurrentText();
                    }
                }
                else if (characterStack.Count < maxCharacters)
                {
                    characterStack.Push(c);
                    UpdateCurrentText();
                }
            }
        }
    }


    void CreateWritingArea()
    {
        GameObject newTextObject = Instantiate(textPrefab, bottomPanel);
        currentText = newTextObject.GetComponent<TMP_Text>();
        currentText.text = "";
        characterStack.Clear();
        isWriting = true;
    }

    void SubmitMessage(string message)
    {
        GameObject newSubmittedText = Instantiate(textPrefab, contentParent);
        TMP_Text newText = newSubmittedText.GetComponent<TMP_Text>();
        newText.text = "You: " + message;

        messages.Enqueue(newSubmittedText);

        if (messages.Count > maxMessages)
        {
            RemoveFirstMessage();
        }

        Destroy(currentText.gameObject);
        isWriting = false;
    }

    void RemoveFirstMessage()
    {
        if (messages.Count > 0)
        {
            GameObject oldestMessage = messages.Dequeue();
            Destroy(oldestMessage);
        }
    }

    string BuildString()
    {
        Stack<char> tempStack = new Stack<char>();
        string result = "";

        while (characterStack.Count > 0)
        {
            tempStack.Push(characterStack.Pop());
        }

        while (tempStack.Count > 0)
        {
            char c = tempStack.Pop();
            result += c;
            characterStack.Push(c);
        }

        return result;
    }

    void UpdateCurrentText()
    {
        currentText.text = BuildString();
    }

}