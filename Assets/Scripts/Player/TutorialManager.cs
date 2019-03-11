using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TutorialManager {

    [Header("Déplacement de base")]
    [SerializeField] InputTutorial zqsdTuto;

    [Header("Saut")]
    [SerializeField] InputTutorial jumpTuto;

    [Header("S'accroupir")]
    [SerializeField] InputTutorial crouchTuto;

    [Header("Intéragir")]
    [SerializeField] InputTutorial interactTuto;

    [Header("Lentille")]
    [SerializeField] InputTutorial lensTuto;

    [Header("Kaléidoscope")]
    [SerializeField] InputTutorial lampTuto;
    [SerializeField] InputTutorial removeLampTuto;

	public void Update () {
        if (!zqsdTuto.Ended)
        {
            if (!zqsdTuto.Started)
                zqsdTuto.StartToShow();
            else
                zqsdTuto.CheckForInputDone();
        }
        else if (!jumpTuto.Ended)
        {
            if (!jumpTuto.Started)
                jumpTuto.StartToShow();
            else
                jumpTuto.CheckForInputDone();
        }
        else if (!crouchTuto.Ended)
        {
            if (!crouchTuto.Started)
                crouchTuto.StartToShow();
            else
                crouchTuto.CheckForInputDone();
        }
        else if (!interactTuto.Ended)
        {
            if (!interactTuto.Started)
                interactTuto.StartToShow();
            //else
            //interactTuto.CheckForInputDone();
        }
        else if (!lensTuto.Ended)
        {
            if (GameManager.gameManager.player.HasLens)
            {
                if (!lensTuto.Started)
                    lensTuto.StartToShow();
                else
                    lensTuto.CheckForInputDone();
            }
        }
        else if (!lampTuto.Ended)
        {
            if (GameManager.gameManager.player.HasLamp)
            {
                if (!lampTuto.Started)
                    lampTuto.StartToShow();
            }
        }
        else if (!removeLampTuto.Ended)
        {
            if (!removeLampTuto.Started)
                removeLampTuto.StartToShow();
        }
    }

    public void ValidateInteraction()
    {
        if (!interactTuto.Ended && interactTuto.Started)
            interactTuto.StopToShow();
    }

    public void ValidateLampUse()
    {
        if (!lampTuto.Ended && lampTuto.Started)
            lampTuto.StopToShow();
    }

    public void ValidateLampRemove()
    {
        if (!removeLampTuto.Ended && removeLampTuto.Started)
            removeLampTuto.StopToShow();
    }
}

[System.Serializable]
public class InputTutorial
{
    [SerializeField] Text text;
    [SerializeField] string line;
    [SerializeField] List<TutorialKeyChecker> keysToCheck;
    [SerializeField] bool checkJustOne;
    
    public void StartToShow()
    {
        text.text = line;
        started = true;
    }

    public void StopToShow()
    {
        text.text = "";
        ended = true;
    }

    public void CheckForInputDone()
    {
        if (ended)
            return;

        bool done = checkJustOne ? false : true;

        foreach(TutorialKeyChecker checker in keysToCheck)
        {
            if (checkJustOne)
            {
                if (checker.CheckIfKeyOK())
                {
                    done = true;
                    break;
                }
            }
            else
            {
                if (!checker.CheckIfKeyOK())
                    done = false;
            }
        }

        /*if (done)
            Debug.Log("Oui");*/

        if (done)
            StopToShow();
    }

    bool started;
    public bool Started
    {
        get
        {
            return started;
        }
    }

    bool ended;
    public bool Ended
    {
        get
        {
            return ended;
        }
    }
}

[System.Serializable]
public class TutorialKeyChecker
{
    [SerializeField] KeyCode keyToPress;
    bool keyChecked;

    public bool CheckIfKeyOK()
    {
        if (Input.GetKey(keyToPress) && !keyChecked)
            keyChecked = true;

        return keyChecked;
    }
}