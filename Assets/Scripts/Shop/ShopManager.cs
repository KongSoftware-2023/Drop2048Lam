using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    protected int coinBuyHammer=200;
    protected int coinBuyBomb = 500;
    protected int coinBuyX2 = 200;
    [SerializeField] protected UiMainMenuCtrl uiMainMenuCtrl;
  public void BuyItemHammer()
    {
        MusicManager.Instance.PlaySoundBySkill();
        bool can= this.CanMinusCoin(this.coinBuyHammer);
        if (!can) return;
        GameManager.Instance.SetItemNumberHammer(GameManager.Instance.ItemNumberHammer + 1);
        uiMainMenuCtrl.SetPrefsValueAndShowOnText();
        Debug.Log(GameManager.Instance.ItemNumberHammer);

    }
    public void BuyItemBomb()
    {
        MusicManager.Instance.PlaySoundBySkill();
        bool can = this.CanMinusCoin(this.coinBuyBomb);
        if (!can) return;
        GameManager.Instance.SetItemNumberBommb(GameManager.Instance.ItemNumberBommb + 1);
        uiMainMenuCtrl.SetPrefsValueAndShowOnText();
    }
    public void BuyItemX2()
    {
        MusicManager.Instance.PlaySoundBySkill();
        bool can = this.CanMinusCoin(this.coinBuyX2);
        if (!can) return;
        GameManager.Instance.SetItemNumberX2(GameManager.Instance.ItemNumberX2    + 1);
        uiMainMenuCtrl.SetPrefsValueAndShowOnText();
    }
    public void BuyMoreCoin()
    {
        MusicManager.Instance.PlaySoundBySkill();
        int NumberCoin = GameManager.Instance.NumberCoin + 1000;
        GameManager.Instance.SetNumberCoin(NumberCoin);
        uiMainMenuCtrl.SetPrefsValueAndShowOnText();
    }

    protected bool CanMinusCoin(int coinMinus)
    {
        int NumberCoint = GameManager.Instance.NumberCoin - coinMinus;
        if (NumberCoint < 0) return false;
        GameManager.Instance.SetNumberCoin(NumberCoint);
        return true;
    }

}
