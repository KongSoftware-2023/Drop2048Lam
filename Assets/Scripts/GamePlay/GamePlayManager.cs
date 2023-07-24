using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GamePlayManager : MonoBehaviour
{
    public GameObject newTileSpawnHolder;
    public GameObject initialzerGameObj;
    public GameObject gridTilesParent;
    public bool isFinal;
    public int bestNumberBlock;
    public InitializerScripts initializerScripts;
    public UiGamePlayCtrl uiGamePlayCtrl;
    public static GamePlayManager GM_Instance;
    public SkillBoom skillBoom;
    public bool isBombSkillded = false;
    public SkillHumer skillHumer;
    public bool isHumerSkillded = false;
    public bool isReady = false;
    public float timeInit = 1f;

    //public GameObject[] LastTilesOFGrid;

    void CheckInstance()
    {
        if (GM_Instance == null)
            GM_Instance = this;
        else
            Destroy(GM_Instance);
    }

     void Start()
    {
        Application.targetFrameRate = 180;
        uiGamePlayCtrl.bestBlock.transform.GetComponent<Image>().sprite = initializerScripts.GetTileColorAtNumberWise(GameManager.Instance.NumberBlockBest);
        uiGamePlayCtrl.bestBlock.transform.GetChild(0).GetComponent<Text>().text = GameManager.Instance.NumberBlockBest.ToString();
    }
    void Awake()
    {
        CheckInstance();
    }


    public GameObject GetTopActiveTile()
    {
        if (newTileSpawnHolder.transform.childCount > 0)
        {
            return newTileSpawnHolder.transform.GetChild(0).gameObject;
        }

        return null;
    }

    public GameObject GetBottomlastActiveTile_FromTileHolder()
    {
        if (newTileSpawnHolder.transform.childCount > 3)
        {
            return newTileSpawnHolder.transform.GetChild(newTileSpawnHolder.transform.childCount - 1).gameObject;
        }

        return null;
    }
    //tile new spawn /set pos,scale
    public void MoveSecondTileToFirstPositon()
    {
        StartCoroutine(InnitTile(timeInit));
    }
    private IEnumerator InnitTile(float time)
    {
        Debug.Log(time);
        yield return new WaitForSeconds(time);
        initializerScripts.SpawnPlayerTile();
    }    

    public bool CheckedTileHolder_ChildGreaterThan3()
    {
        if (newTileSpawnHolder.transform.childCount > 3)
        {
            return true;
        }
        return false;
    }
    public IEnumerator  SetNewTile_NumberAfterMerged(GameObject DestinationCardObj, GameObject MergedTile)
    {
        //int numberMerge =0;
        int nextNumber = 0;
        if (DestinationCardObj==null || MergedTile == null)
        {
             yield break ;
        }

        GridTileScripts GridMergeTile = MergedTile.transform.parent.GetComponent<GridTileScripts>();
        if (MergedTile.transform.parent != null && DestinationCardObj.transform.parent != null)
        MergedTile.transform.SetParent(DestinationCardObj.transform.parent);

        int mergedTileNumber = DestinationCardObj.GetComponent<TileScripts>().TileNumber;
        nextNumber = mergedTileNumber * 2;
        Debug.Log("numberMerge"+ nextNumber);
        Destroy(MergedTile);       
        DestinationCardObj.GetComponent<TileScripts>().TileNumber = nextNumber;
        DestinationCardObj.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = nextNumber.ToString();
        DestinationCardObj.transform.GetComponent<SpriteRenderer>().sprite = initializerScripts.GetTileColorAtNumberWise(nextNumber);
        BestBlock(DestinationCardObj.GetComponent<TileScripts>().TileNumber);
        //yield return new WaitForSeconds(0.05f);
        //Check Bottom tile
        if (DestinationCardObj.transform.parent.GetComponent<GridTileScripts>().GetMyLastBottomEptyTile() != null)
        {
            timeInit += 0.1f;
            DestinationCardObj.transform.SetParent(DestinationCardObj.transform.parent.GetComponent<GridTileScripts>().GetMyLastBottomEptyTile().transform);
            yield return new WaitForSeconds(0.03f);
            DestinationCardObj.transform.GetComponent<TileScripts>().MoveToNextGridPos();
        }
        timeInit += 0.1f;
        StartCoroutine(GamePlayManager.GM_Instance.RecusiveGird(GridMergeTile, DestinationCardObj.transform.parent.GetComponent<GridTileScripts>()));
        yield return new WaitForSeconds(0.05f);
        //Fix
        if (DestinationCardObj == null) yield break;
        bool isFound = DestinationCardObj.transform.parent.GetComponent<GridTileScripts>().CheckAroundNeighbout_ToGetFatchedCard();
        //if (DestinationCardObj == null) 
       
            if (isFound == null) yield break;
                if (!isFound)
                {
                    GamePlayManager.GM_Instance.GameOverChecked();
                    //if(isFinal)uIController.popupDefeat.SetActive(true);
                }
        else
        {
            timeInit += 0.1f;
            yield return new WaitForSeconds(0.04f);
            //DestinationCardObj.transform.parent.GetComponent<GridTileScripts>().CheckAroundNeighbout_ToGetFatchedCard();
            

        }
        timeInit += 0.05f;
        DestinationCardObj.GetComponent<TileScripts>().MoveToNextGridPos();


    }
    public bool GameOverChecked()
    {
        if (gridTilesParent.transform.childCount > 0)
        {
            //int countercheck = 0;
            for (int i = gridTilesParent.transform.childCount-1; i >24; i--)
            {
                if(gridTilesParent.transform.GetChild(i).childCount>0)
                {
                    print("GAME OVER ");
                    MusicManager.Instance.PlayGameOver();
                    uiGamePlayCtrl.popupDefeat.SetActive(true);
                    DisableAllColliders(initializerScripts.gridMainParent);
                    return true;
                }
            }

        }

        return false;
    }

    void BestBlock(int tileNumberBlock)
    {

        if (tileNumberBlock > GameManager.Instance.NumberBlockBest)
        {
            bestNumberBlock = tileNumberBlock;
             GameManager.Instance.SetNumberBlockBest(bestNumberBlock);
            uiGamePlayCtrl.bestBlock.transform.GetComponent<Image>().sprite = initializerScripts.GetTileColorAtNumberWise(bestNumberBlock);
            uiGamePlayCtrl.bestBlock.transform.GetChild(0).GetComponent<Text>().text = bestNumberBlock.ToString();
        }
    }
    public GameObject GetGridEmptyTile()
    {

        for (int i = 0; i < gridTilesParent.transform.childCount; i++)
        {
            if (gridTilesParent.transform.GetChild(i).transform.childCount == 0)
            {
                return gridTilesParent.transform.GetChild(i).gameObject;
            }
        }
        return null;
    }
    public IEnumerator RecusiveGird(GridTileScripts Obj, GridTileScripts Bbj)
    {
        yield return new WaitForSeconds(0.01f);
        if (Obj.ListGridTileAround().Count > 0)
        {
            List<GridTileScripts> gridTileScriptsArounds = Obj.ListGridTileAround();
            GridTileScripts bbj = gridTileScriptsArounds.Find(item => item == Bbj);
            foreach (GridTileScripts obj in gridTileScriptsArounds)
            {
                if (obj == bbj)
                {
                    Debug.Log("da vao day");
                    continue;
                }
                if (obj.GetMyLastBottomEptyTile() == null && obj.transform.childCount != 0) continue;
                if (obj.transform.childCount == 0) continue;
                TileScripts tileSPawn = obj.transform.GetChild(0).GetComponent<TileScripts>();
                GameObject girdtile = obj.GetMyLastBottomEptyTile();
                tileSPawn.transform.SetParent(girdtile.transform);
                //tileSPawn.MoveToNextGridPos();
                timeInit += 0.1f;
                yield return new WaitForSeconds(0.03f);
                tileSPawn.MoveToNextGridPost();
                yield return new WaitForSeconds(0.07f);
                timeInit += 0.1f;
                //tileSPawn.MoveToNextGridPos();


                if (obj.ListGridTileAround().Count > 0)
                {

                    StartCoroutine(GamePlayManager.GM_Instance.RecusiveGird(obj, bbj));
    
                }
            }
            //}
        }
    }
    public IEnumerator RecusiveGirdAround(GridTileScripts Obj)
    {
        yield return new WaitForSeconds(0.02f);
        if (Obj.ListGridTileAround().Count > 0)
        {
            List<GridTileScripts> gridTileScriptsArounds = new List<GridTileScripts>();
            gridTileScriptsArounds = Obj.ListGridTileAround();
            Debug.Log(gridTileScriptsArounds.Count + "bao nhieu phan tu");
            foreach (GridTileScripts obj in gridTileScriptsArounds)
            {
                if (obj.GetMyLastBottomEptyTile() == null) continue;
                Debug.Log(obj.name + " " + obj.transform.childCount);
                if (obj.transform.childCount== 0) continue;
                TileScripts tileSPawn = obj.transform.GetChild(0).GetComponent<TileScripts>();
                tileSPawn.transform.SetParent(obj.GetMyLastBottomEptyTile().transform);
                yield return new WaitForSeconds(0.03f);
                tileSPawn.MoveToNextGridPost();
                yield return new WaitForSeconds(0.05f);
                //.MoveToNextGridPos();

                if (obj.ListGridTileAround().Count > 0)
                {
                    StartCoroutine(GamePlayManager.GM_Instance.RecusiveGirdAround(obj));

                }
            }
        }
    }

    public void DisableAllColliders(GameObject gameObject)
        {
        if (gameObject == null) return;
      
        foreach (Transform child in gameObject.transform)
            {
            BoxCollider2D collider = child.gameObject.GetComponent<BoxCollider2D>();
            collider.enabled = false;
            
            }
        }
    public void OnableAllColliders(GameObject gameObject)
    {
        if (gameObject == null) return;

        foreach (Transform child in gameObject.transform)
        {
            BoxCollider2D collider = child.gameObject.GetComponent<BoxCollider2D>();
            collider.enabled = true;

        }
    } 

}