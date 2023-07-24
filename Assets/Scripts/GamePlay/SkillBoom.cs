using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//numberSkillBomb
public class SkillBoom : SkillBase
{
    protected override void Start()
    {

        this.numberSkill = GameManager.Instance.ItemNumberBommb;
        this.textNumberSkill.text = numberSkill.ToString();
        base.Start();
    }
    protected override void ToggleSkill()
    {
        if (this.numberSkill <= 0) return;
        GamePlayManager.GM_Instance.isBombSkillded = !GamePlayManager.GM_Instance.isBombSkillded;
        base.ToggleSkill();
    }
    protected override void ActiveSkill()
    {
        base.ActiveSkill();
        GamePlayManager.GM_Instance.isBombSkillded = true;
    }
    public override void DisableSkill()
    {
        base.DisableSkill();
        GamePlayManager.GM_Instance.isBombSkillded = false;
    }
    protected virtual List<GridTileScripts> CheckTileBoom(int numberTile)
    {
        Debug.Log("vao day ch");
        if (GamePlayManager.GM_Instance.initializerScripts.gridMainParent == null || numberTile == null) return null;
        List<GridTileScripts> ListNumberTileSame = new List<GridTileScripts>();
        foreach (Transform child in GamePlayManager.GM_Instance.initializerScripts.gridMainParent.transform)
        {
            int number;
            GridTileScripts gridTile = child.GetComponent<GridTileScripts>();

            if (child.childCount > 0)
            {
                number = child.GetChild(0).gameObject.GetComponent<TileScripts>().TileNumber;
                if (number == numberTile) ListNumberTileSame.Add(gridTile);
            }

        }
        return ListNumberTileSame;
    }
    public virtual void Bombing(int numberTile)
    {
        if (numberSkill <= 0) return;
        MusicManager.Instance.PlaySoundBomb();
        this.numberSkill--;
        GameManager.Instance.SetItemNumberBommb(numberSkill);
        List<GridTileScripts> listTileEqual = new List<GridTileScripts>();
        listTileEqual = CheckTileBoom(numberTile);
        foreach (GridTileScripts child in listTileEqual)
        {
            Destroy(child.transform.GetChild(0).gameObject);
            StartCoroutine(GamePlayManager.GM_Instance.RecusiveGirdAround(child));
            Instantiate(fxSkill, child.transform.position, fxSkill.transform.rotation);
        }
        this.DisableSkill();
        this.textNumberSkill.text = numberSkill.ToString();
    }

}
