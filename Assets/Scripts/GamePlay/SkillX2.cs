using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillX2 : SkillBase
{
    protected override void Start()
    {
        this.numberSkill = GameManager.Instance.ItemNumberX2;
        this.textNumberSkill.text = numberSkill.ToString();
        base.Start();
    }
    protected override void ToggleSkill()
    {
        this.ActiveSkillX2();
    }
    protected virtual void ActiveSkillX2()
    {
        if (numberSkill <= 0) return;
        this.numberSkill--;
        GameManager.Instance.SetItemNumberX2(numberSkill);
        MusicManager.Instance.PlaySoundX2();
        this.X2Tile();
        this.textNumberSkill.text = numberSkill.ToString();

    }
    protected virtual void X2Tile()
    {
        GameObject tileObj = GamePlayManager.GM_Instance.GetTopActiveTile();// new title spawn
        if (tileObj == null) return;
        TileScripts tileSPawn = tileObj.GetComponent<TileScripts>();
        Instantiate(fxSkill, tileSPawn.transform.position, fxSkill.transform.rotation);
        tileSPawn.TileNumber *= 2;
        tileSPawn.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = tileSPawn.TileNumber.ToString();
        tileSPawn.transform.GetComponent<SpriteRenderer>().sprite = GamePlayManager.GM_Instance.initializerScripts.GetTileColorAtNumberWise(tileSPawn.TileNumber);
    }
}
