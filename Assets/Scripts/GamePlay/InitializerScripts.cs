using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InitializerScripts : MonoBehaviour
{
   

    public int[] GirdTileNumber;
    public Sprite[] TileColorNumberWises;
    [Header("Grid ITems")]
    public GameObject gridMainParent;
    public GameObject[] gridtilePrefabsObjArry;
    public float gridRowXOffset, gridColorumYOffset;
    public int Row,Colom;
    public float gridTParentTileScale;
    [Header("Player Tile ITems")]
    public GameObject NewTileHolderParent;
    public GameObject playerTileObject;
    public Vector3[] playerTileSpawninglocalPosArr;
    public Vector3[] playerTileScaleArry;
    private void Start()
    {

        GenerateGrid(gridtilePrefabsObjArry[0], Row, Colom, gridRowXOffset, gridColorumYOffset);
        SpawnTileStart();
    }

     //Grid Init
    private void GenerateGrid(GameObject TilePlacer, int Pr_numOfRow, int Pr_numOfCol,float xIncrementalValue, float yIncrementalValue)
    {
        int nameconuter = 0;
        float xOffset = 0;
        float YOffset = 0;
        for (int row = 0; row < Pr_numOfRow; row++)
        {
            xOffset = 0;
            for (int col = 0; col < Pr_numOfCol; col++)
            {
                GameObject currentTile = Instantiate(TilePlacer);
                currentTile.transform.SetParent(gridMainParent.transform);
                currentTile.transform.localPosition = new Vector3(currentTile.transform.position.x + xOffset, currentTile.transform.position.y + YOffset, 0);
                currentTile.transform.name = nameconuter.ToString();
                currentTile.GetComponent<SpriteRenderer>().enabled = true;
                xOffset += xIncrementalValue;
                currentTile.GetComponent<GridTileScripts>().FindNeihbourForMe(nameconuter, Pr_numOfRow, Pr_numOfCol);
                nameconuter++;

            }
            YOffset += yIncrementalValue;
        }

        gridMainParent.transform.localScale =
            new Vector3(gridTParentTileScale, gridTParentTileScale, gridTParentTileScale);
  
    }

    #region Init Player Tiles

    //spawn tile start game and spawn again
    public  void SpawnPlayerTile()
    {
        GamePlayManager.GM_Instance.isReady = true;
        GameObject TileObj = Instantiate(playerTileObject) as GameObject;
        TileObj.transform.SetParent(NewTileHolderParent.transform);
        TileObj.transform.localPosition = playerTileSpawninglocalPosArr[TileObj.transform.GetSiblingIndex()];
        int r = Random.Range(0, 4);
        TileObj.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = GirdTileNumber[r].ToString();
        TileObj.GetComponent<SpriteRenderer>().sprite = TileColorNumberWises[r];
        TileObj.transform.localScale = playerTileScaleArry[TileObj.transform.GetSiblingIndex()];
        TileObj.GetComponent<TileScripts>().TileNumber = GirdTileNumber[r];

        this.NewTileHolderParent.transform.GetChild(0).transform.localPosition = playerTileSpawninglocalPosArr[this.NewTileHolderParent.transform.GetChild(0).transform.GetSiblingIndex()];
        this.NewTileHolderParent.transform.GetChild(0).transform.localScale = playerTileScaleArry[this.NewTileHolderParent.transform.GetChild(0).transform.GetSiblingIndex()];

        GamePlayManager.GM_Instance.uiGamePlayCtrl.nextBlock.transform.GetComponent<Image>().sprite = GetTileColorAtNumberWise(NewTileHolderParent.transform.GetChild(1).GetComponent<TileScripts>().TileNumber);
        GamePlayManager.GM_Instance.uiGamePlayCtrl.nextBlock.transform.GetChild(0).GetComponent<Text>().text = NewTileHolderParent.transform.GetChild(1).GetComponent<TileScripts>().TileNumber.ToString();
    }
    public void SpawnTileStart()
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject TileObj = Instantiate(playerTileObject) as GameObject;
            TileObj.transform.SetParent(NewTileHolderParent.transform);
            TileObj.transform.localPosition = playerTileSpawninglocalPosArr[TileObj.transform.GetSiblingIndex()];
            int r = Random.Range(0, 4);
            TileObj.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = GirdTileNumber[r].ToString();
            TileObj.GetComponent<SpriteRenderer>().sprite = TileColorNumberWises[r];
            TileObj.transform.localScale = playerTileScaleArry[TileObj.transform.GetSiblingIndex()];
            TileObj.GetComponent<TileScripts>().TileNumber = GirdTileNumber[r];
        }
        GamePlayManager.GM_Instance.isReady = true;
        GamePlayManager.GM_Instance.uiGamePlayCtrl.nextBlock.transform.GetComponent<Image>().sprite = GetTileColorAtNumberWise(NewTileHolderParent.transform.GetChild(1).GetComponent<TileScripts>().TileNumber);
        GamePlayManager.GM_Instance.uiGamePlayCtrl.nextBlock.transform.GetChild(0).GetComponent<Text>().text = NewTileHolderParent.transform.GetChild(1).GetComponent<TileScripts>().TileNumber.ToString();
    }    

    #endregion
    //return color tile
    public Sprite GetTileColorAtNumberWise(int tileNumber)
    {
        switch (tileNumber)
        {
            case 2:
                return TileColorNumberWises[0];
            case 4:
                return TileColorNumberWises[1];
            case 8:
                return TileColorNumberWises[2];
            case 16:
                return TileColorNumberWises[3];
            case 32:
                return TileColorNumberWises[4];
            case 64:
                return TileColorNumberWises[5];
            case 128:
                return TileColorNumberWises[6];
            case 256:
                return TileColorNumberWises[7];
            case 512:
                return TileColorNumberWises[8];
            case 1024:
                return TileColorNumberWises[9];
            case 2048:
                return TileColorNumberWises[10];
        }
        return null;

    }

}
