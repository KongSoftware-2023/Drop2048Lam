using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuyNextTile : MonoBehaviour
{
    protected int coinBuy= 100;
    [SerializeField]protected Button button;
    [SerializeField]protected bool isBuyed = false;
    [SerializeField] protected GameObject Image;

    protected virtual void Awake()
    {
        if (this.button != null) return;
        this.button = GetComponent<Button>();
    }
    protected virtual void Start()
    {
        this.AddOnClickEvent();
    }
    protected virtual void AddOnClickEvent()
    {
        this.button.onClick.AddListener(this.BuyTileNext);
    }
    protected void BuyTileNext()
    {
        Debug.Log("vao chưa");
        if (this.isBuyed ) return;
        int coinNow = GameManager.Instance.NumberCoin - this.coinBuy;
        if (coinNow < 0) return;
        GameManager.Instance.SetNumberCoin(coinNow);
        GamePlayManager.GM_Instance.uiGamePlayCtrl.SetPrefsValueAndShowOnText();
        this.OffImage();
        this.isBuyed = true;

    }
    protected void OffImage()
    {
        Image.SetActive(false);

    }    
}
