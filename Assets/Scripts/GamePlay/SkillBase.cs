using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBase : MonoBehaviour
{//bool skillState = false
    [SerializeField] protected Button button;
    [SerializeField] protected GameObject popupSkill;
    [SerializeField] protected int numberSkill;
    [SerializeField] protected Text textNumberSkill;
    [SerializeField] protected GameObject fxSkill;
    protected virtual void Awake()
    {
        if (this.button != null) return;
        this.button = GetComponent<Button>();
        this.LoadTextNumberSkill();
    }
    protected virtual void Start()
    {
        //this.numberSkill = 5;
        //this.textNumberSkill.text = numberSkill.ToString();
        this.AddOnClickEvent();
    }
    protected virtual void LoadTextNumberSkill()
    {
        if (textNumberSkill != null) return;
        this.textNumberSkill = GetComponentInChildren<Text>();
    }
    protected virtual void AddOnClickEvent()
    {
        if (numberSkill <= 0) return;
        this.button.onClick.AddListener(this.ToggleSkill);
    }
    protected virtual void ToggleSkill()
    {

        if (GamePlayManager.GM_Instance.isBombSkillded || GamePlayManager.GM_Instance.isHumerSkillded)
            this.ActiveSkill();
        else
            this.DisableSkill();
    }
    protected virtual void ActiveSkill()
    {
        if (numberSkill <= 0) return;
        popupSkill.SetActive(true);
    }
    public virtual void DisableSkill()
    {
        //GamePlayManager.GM_Instance.isBombSkillded = false;
        popupSkill.SetActive(false);
    }

}
