using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UICtrl : MonoBehaviour
{
    [SerializeField] protected int soundEnabled ;
    [SerializeField] protected Text itemNumberBommb ;
    [SerializeField] protected Text itemNumberHammer;
    [SerializeField] protected Text itemNumberX2;
    [SerializeField] protected Text numberCoin ;
    [SerializeField] protected Text numberBlockBest ;


    [SerializeField] protected GameObject EnableMusic;
    [SerializeField] protected GameObject DisableMusic;
    protected bool isOpen = false;

    protected virtual void Start()
    {
        SetPrefsValueAndShowOnText();
    }
    public void LoadChoiseScene(string Pr_SceneName)
    {
        MusicManager.Instance.PlayButtonClickSound();
        SceneManager.LoadScene(Pr_SceneName);
    }
    public virtual void DialogeOpenFun(GameObject db)
    {
        MusicManager.Instance.PlayButtonClickSound();
        db.SetActive(true);
        this.isOpen = true;
    }

    public virtual void DialogeCloseFun(GameObject db)
    {
        MusicManager.Instance.PlayButtonClickSound();
        db.SetActive(false);
        this.isOpen = false;
    }

    public void ReplayGame()
    {
        MusicManager.Instance.PlayButtonClickSound();
        SceneManager.LoadScene("GamePlay");
    }
    public void LoadMainScreen()
    {
        MusicManager.Instance.PlayButtonClickSound();
        SceneManager.LoadScene("MainMenu");
    }
    public virtual void UiToggle(GameObject obj)
    {
        MusicManager.Instance.PlayButtonClickSound();
        this.isOpen = !this.isOpen;
        if (this.isOpen) DialogeOpenFun(obj);
        else DialogeCloseFun(obj);
    }
    public virtual void SetPrefsValueAndShowOnText()
    {

    }
    public virtual void ToggleUiMusic ()
    {
        if(this.soundEnabled==1)
        {
 
            this.EnableMusic.SetActive(false);
            this.DisableMusic.SetActive(true);
            GameManager.Instance.SetSoundEnabled(false);
            this.soundEnabled = 0;
            MusicManager.Instance.ToggleSound();
        }
        else
        {

            this.DisableMusic.SetActive(false);
            this.EnableMusic.SetActive(true);
            GameManager.Instance.SetSoundEnabled(true);
            this.soundEnabled = 1;
            MusicManager.Instance.ToggleSound();
        }    
    }  
}
