using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiGamePlayCtrl : UICtrl
{

    public GameObject bestBlock;
    public GameObject nextBlock;

    public GameObject popupDefeat;
    public override void DialogeOpenFun(GameObject db)
    {
        MusicManager.Instance.PlayButtonClickSound();
        Debug.Log("vao duoc chua");
        GamePlayManager.GM_Instance.DisableAllColliders(GamePlayManager.GM_Instance.initializerScripts.gridMainParent);
        db.SetActive(true);
        this.isOpen = true;
    }

    public override void DialogeCloseFun(GameObject db)
    {
        MusicManager.Instance.PlayButtonClickSound();
        GamePlayManager.GM_Instance.OnableAllColliders(GamePlayManager.GM_Instance.initializerScripts.gridMainParent);
        db.SetActive(false);
        this.isOpen = false;
    }

    public void UiToggle(GameObject obj)
    {
        this.isOpen = !this.isOpen;
        if (this.isOpen) DialogeOpenFun(obj);
        else DialogeCloseFun(obj);
    }


    public override void SetPrefsValueAndShowOnText()
    {
        this.soundEnabled = GameManager.Instance.SoundEnabled ? 1 : 0;
        this.numberCoin.text =GameManager.Instance.NumberCoin.ToString();
        this.SetUiMusic();
    }
    protected virtual void SetUiMusic()
    {
        if (this.soundEnabled == 1)
        {
            this.EnableMusic.gameObject.SetActive(true);
            this.DisableMusic.gameObject.SetActive(false);
        }
        else
        {
            this.EnableMusic.gameObject.SetActive(false);
            this.DisableMusic.gameObject.SetActive(true);
        }
    }
}
