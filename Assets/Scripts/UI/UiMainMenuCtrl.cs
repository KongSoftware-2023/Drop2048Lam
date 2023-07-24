using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UiMainMenuCtrl : UICtrl
{
    public override void SetPrefsValueAndShowOnText()
    {
        this.soundEnabled = GameManager.Instance.SoundEnabled ? 1 : 0;
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
        this.itemNumberBommb.text = GameManager.Instance.ItemNumberBommb.ToString();
        this.itemNumberHammer.text = GameManager.Instance.ItemNumberHammer.ToString();
        this.itemNumberX2.text = GameManager.Instance.ItemNumberX2.ToString();
        this.numberCoin.text = GameManager.Instance.NumberCoin.ToString();

        //this.numberBlockBest.text = PlayerPrefs.GetInt("NumberBlockBest", 2).ToString();
    }

}
