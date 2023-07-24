using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHumer : SkillBase
{

    //numberSkillHumer
    protected override void Start()
    {
        this.numberSkill =GameManager.Instance.ItemNumberHammer;
        this.textNumberSkill.text = numberSkill.ToString();
        base.Start();

    }
    protected override void ToggleSkill()
    {
        if (this.numberSkill <= 0) return;
        GamePlayManager.GM_Instance.isHumerSkillded = !GamePlayManager.GM_Instance.isHumerSkillded;
        base.ToggleSkill();
    }
    protected override void ActiveSkill()
    {
        base.ActiveSkill();
        GamePlayManager.GM_Instance.isHumerSkillded = true;
    }
    public override void DisableSkill()
    {
        base.DisableSkill();
        GamePlayManager.GM_Instance.isHumerSkillded = false;
    }
    public virtual void Humerming(GameObject GridTile)
    {
        if (numberSkill <= 0) return;
        this.numberSkill--;
        MusicManager.Instance.PlaySoundHammer();
        GameManager.Instance.SetItemNumberHammer(numberSkill);
        Destroy(GridTile);
        StartCoroutine(GamePlayManager.GM_Instance.RecusiveGirdAround(GridTile.transform.parent.GetComponent<GridTileScripts>()));
        Instantiate(fxSkill, GridTile.transform.position, fxSkill.transform.rotation);
        this.DisableSkill();
        this.textNumberSkill.text = numberSkill.ToString();

       
    }
}
