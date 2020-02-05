using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public float fadingDuration, timer, displayImageDuration;
    public GameObject player;
    public bool playerAtExit, isPlayerCaught, audioPlayed;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    public AudioSource exitAudio, caughtAudio;
    private void Update()
    {
        if (playerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
        }
        else if (isPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);
        }
    }
    public void CaughtPlayer()
    {
        isPlayerCaught = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerAtExit = true;
        }
    }
    private void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if (!audioPlayed)
        {
            audioSource.Play();
            audioPlayed = true;
        }
        timer += Time.deltaTime;
        imageCanvasGroup.alpha = timer / fadingDuration;
        if (timer > fadingDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                Debug.Log("Finish!");
                Application.Quit();
            }

        }
    }
}
