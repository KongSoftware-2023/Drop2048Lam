using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTileScripts : MonoBehaviour
{
    void OnMouseDown()
    {

        if ((GamePlayManager.GM_Instance.isBombSkillded) || (GamePlayManager.GM_Instance.isHumerSkillded))
        {
            Debug.Log("di chuot");
            if (transform.childCount <= 0) return;
            Debug.Log("vao day");
            if (GamePlayManager.GM_Instance.isBombSkillded)
                GamePlayManager.GM_Instance.skillBoom.Bombing(transform.GetChild(0).GetComponent<TileScripts>().TileNumber);
            else
                GamePlayManager.GM_Instance.skillHumer.Humerming(transform.GetChild(0).gameObject);
        }
        else if(GamePlayManager.GM_Instance.isReady)
        {
 
            GridTileScripts tile = CheckTopClickCollum();
            if (tile == null) return;
            GamePlayManager.GM_Instance.isReady = false;
            MusicManager.Instance.PlaySoundDrop();
            tile.ClickCollum();

        }
    }
    protected virtual GridTileScripts CheckTopClickCollum()
    {
       
        if (transform.childCount == 0)
        {
            return this;
        }
        else
        {   // Kiểm tra nếu topNeighbour không null
            if (topNeighbour != null)
            {
                if (topNeighbour.transform.childCount == 0)
                {
                    // topNeighbour có childCount = 0, trả về topNeighbour hiện tại
                    return topNeighbour;
                }
                else
                {
                    // Gọi đệ quy để kiểm tra topNeighbour của topNeighbour
                    return topNeighbour.CheckTopClickCollum();
                }
            }
            else
            {
                // topNeighbour không tồn tại
                return null;
            }
        }
    }

    protected void ClickCollum()
    {

        GameObject tileObj = GamePlayManager.GM_Instance.GetTopActiveTile();// new title spawn
        if (tileObj == null) return;
        GamePlayManager.GM_Instance.timeInit = 1f;
       TileScripts tileSPawn = tileObj.GetComponent<TileScripts>();
        tileSPawn.transform.SetParent(GetMyLastBottomEptyTile().transform);
        tileSPawn.MoveToNextGridPost();
        GamePlayManager.GM_Instance.MoveSecondTileToFirstPositon();
 
    }
    public bool CheckMyBottomFil()
    {
        if (bottomNeighbour != null && bottomNeighbour.transform.childCount == 0)
        {
            return true;
        }
        return false;
    }

    //return gridtile 
    public GameObject GetMyLastBottomEptyTile()
    {
        GameObject TempObj = null;
        if (bottomNeighbour != null && bottomNeighbour.transform.childCount == 0)
        {
            GameObject BotmNibour = bottomNeighbour.transform.gameObject;
            int counterB = 0;
            while (BotmNibour != null && BotmNibour.transform.childCount == 0 && counterB < 6)
            {
                TempObj = BotmNibour.transform.gameObject;

                counterB++;
                if (BotmNibour.GetComponent<GridTileScripts>().bottomNeighbour != null
                            && BotmNibour.GetComponent<GridTileScripts>().bottomNeighbour.transform.childCount == 0)
                {
                    BotmNibour = BotmNibour.GetComponent<GridTileScripts>().bottomNeighbour.gameObject;
                }
            }
        }
        ///else if() thêm vào để bấm vào cột nào là xếp luôn cột đó không phải bấm vào ô đó nữa
        else
        {
            if (transform.childCount == 0)
            {
                TempObj = this.gameObject;
            }
        }

        return TempObj;
    }


    #region ++++++++++++ NEIGHBOUR FINDING MODULE  +++++++++++++++ 
    // important dont delete 
    public int QuantityAroundGridTile = 0;
    [Header("==Neighbour Check Values===")]
    public int left = -1;
    public int right = -1;
    public int up = -1;
    public int bottom = -1;

    public int topRight = -1;
    public int topLeft = -1;
    public int botRight = -1;
    public int botLeft = -1;

    // horizental verticals 
    public GridTileScripts leftNeighbour = null;
    public GridTileScripts rightNeighbour = null;

    public GridTileScripts topNeighbour = null;
    public GridTileScripts bottomNeighbour = null;
    // Diagonal 
    public GridTileScripts bottomLeftNeighbour = null;
    public GridTileScripts bottomRightNeighbour = null;

    public GridTileScripts topLeft_Neighbour = null;
    public GridTileScripts topRight_Neighbour = null;


    public void FindNeihbourForMe(int countNumberOfBlock, int numOfRows, int numOfCols)
    {

        if ((countNumberOfBlock + 1) % numOfCols != 0) //numofcols=4
            right = countNumberOfBlock + 1;// right 

        if (countNumberOfBlock % numOfCols != 0)
            left = countNumberOfBlock - 1; //left

        if ((countNumberOfBlock - numOfCols) > -1)
            bottom = countNumberOfBlock - numOfCols;  //bottom

        if ((countNumberOfBlock + numOfCols) < ((numOfRows * numOfCols)))
        {
            up = countNumberOfBlock + numOfCols;  // top 
        }
        //=======Top Diagonals ==============================================================
        if ((countNumberOfBlock + numOfCols) < ((numOfRows * numOfCols)) && (countNumberOfBlock + 1) % numOfCols != 0)
            topRight = countNumberOfBlock + numOfCols + 1;   // top Diagonals right 

        if ((countNumberOfBlock + numOfCols) < ((numOfRows * numOfCols)) && (countNumberOfBlock) % numOfCols != 0)
            topLeft = countNumberOfBlock + numOfCols - 1; //  top Diagonals left


        //=========== Bottom Diagonals ==================================================
        if (countNumberOfBlock - numOfCols >= 0 && (countNumberOfBlock + 1) % numOfCols != 0 && (countNumberOfBlock - numOfCols) < ((numOfRows * numOfCols) - 1))
            botRight = (countNumberOfBlock - numOfCols) + 1;   // Bottom  Diagonals right 

        if (countNumberOfBlock - numOfCols >= 0 && (countNumberOfBlock) % numOfCols != 0 && (countNumberOfBlock - numOfCols) < ((numOfRows * numOfCols) - 1))
            botLeft = (countNumberOfBlock - numOfCols) - 1;//  top Diagonals left

        Invoke("SetNighbour_ObjectValue", 0.1f);

    }

    void SetNighbour_ObjectValue()
    {
        leftNeighbour = GetNeighbour(left);
        rightNeighbour = GetNeighbour(right);

        topNeighbour = GetNeighbour(up);
        bottomNeighbour = GetNeighbour(bottom);
        // Diagonal 
        bottomLeftNeighbour = GetNeighbour(botLeft);
        bottomRightNeighbour = GetNeighbour(botRight);

        topLeft_Neighbour = GetNeighbour(topLeft);
        topRight_Neighbour = GetNeighbour(topRight);
    }

    GridTileScripts GetNeighbour(int childIndex)
    {
        GameObject parentObject_Grid = this.gameObject.transform.parent.gameObject;
        GridTileScripts NeighbourOf_Block = null;
        if (childIndex >= 0)
        {
            NeighbourOf_Block = parentObject_Grid.gameObject.transform.GetChild(childIndex).gameObject.GetComponent<GridTileScripts>();
        }
        return NeighbourOf_Block;
    }

    #endregion

    #region Check MergedCard MOVE TO FACTHED POS Func 

    public bool CheckAroundNeighbout_ToGetFatchedCard()
    {
        bool IsFound = false;
        bool a = IsFound_Right();
        bool b = IsFound_Left();
        bool c = IsFound_Bottom();
        bool d = IsFound_Top();

        if (d)
        {
            QuantityAroundGridTile++;
            Debug.Log("Top0");
            MoveToFatchedCardFunbottomPos(topNeighbour.transform.GetChild(0).gameObject, transform.GetChild(0).gameObject);
            Debug.Log("Top1");
            IsFound = true;
        }
        if (c)
        {
            QuantityAroundGridTile++;
            Debug.Log("Left0");
            MoveToFatchedCardFunbottomPos(transform.GetChild(0).gameObject, bottomNeighbour.transform.GetChild(0).gameObject);
            Debug.Log("Left1");
            IsFound = true;
        }
        if (b)
        {
            QuantityAroundGridTile++;
            Debug.Log("Right0");
            MoveToFatchedCardFunbottomPos(transform.GetChild(0).gameObject, leftNeighbour.transform.GetChild(0).gameObject);
            Debug.Log("Right1");
            IsFound = true;
        }
        if (a)
        {
            QuantityAroundGridTile++;
            Debug.Log("Bottom0");
            MoveToFatchedCardFunbottomPos(transform.GetChild(0).gameObject, rightNeighbour.transform.GetChild(0).gameObject);
            Debug.Log("Bottom1");
            IsFound = true;
        }
        return IsFound;
    }

    public List<GridTileScripts> ListGridTileAround()
    {
        List<GridTileScripts> gridTileScriptsAround = new List<GridTileScripts>();

        if (bottomNeighbour != null && bottomNeighbour.transform.childCount != 0) gridTileScriptsAround.Add(bottomNeighbour);
        if (leftNeighbour != null && leftNeighbour.transform.childCount != 0) gridTileScriptsAround.Add(leftNeighbour);
        if (rightNeighbour != null && rightNeighbour.transform.childCount != 0) gridTileScriptsAround.Add(rightNeighbour);
        if (topNeighbour != null && topNeighbour.transform.childCount != 0) gridTileScriptsAround.Add(topNeighbour);

        return gridTileScriptsAround;
    }
    bool IsFound_Bottom()
    {
        if (bottomNeighbour != null)
        {
            if (IsChildFound(bottomNeighbour.transform.gameObject))
            {
                if (IsTileNumberSame(bottomNeighbour.transform.gameObject))
                {
                    return true;
                }
            }
            //else
            //{
            //    MoveTwoTile_EquallyWithDifferentPos();
            //}
        }
        return false;
    }
    bool IsFound_Right()
    {
        if (rightNeighbour != null)
        {

            if (IsChildFound(rightNeighbour.transform.gameObject))
            {
                if (IsTileNumberSame(rightNeighbour.transform.gameObject))
                {
                    return true;
                }

            }

        }
        return false;
    }
    bool IsFound_Left()
    {
        if (leftNeighbour != null)
        {

            if (IsChildFound(leftNeighbour.transform.gameObject))
            {
                if (IsTileNumberSame(leftNeighbour.transform.gameObject))
                {
                    return true;
                }

            }

        }   
        return false;
    }
    bool IsFound_Top()
    {

        if (topNeighbour != null)
        {

            if (IsChildFound(topNeighbour.transform.gameObject))
            {
                if (IsTileNumberSame(topNeighbour.transform.gameObject))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool IsChildFound(GameObject Obj)
    {
        if (Obj.transform.childCount > 0)
        {
            return true;
        }
        return false;
    }

    bool IsTileNumberSame(GameObject NeighbourObj)
    {
        if (transform.childCount > 0)
        {
            int MyChildCardRank = transform.GetChild(0).GetComponent<TileScripts>().TileNumber;
            int MyNeighbour_ChildCardRank = NeighbourObj.transform.GetChild(0).GetComponent<TileScripts>().TileNumber;
            if (MyChildCardRank == MyNeighbour_ChildCardRank)
            {
                return true;
            }
        }
        return false;
    }
    void MoveToFatchedCardFunbottomPos(GameObject pr_CardDestinationObj, GameObject fatchedCard)
    {
        //fatchedCard.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        Debug.Log("vao day");
        fatchedCard.GetComponent<TileScripts>().FatchedCardPosObj = pr_CardDestinationObj;
        fatchedCard.GetComponent<TileScripts>().MoveToFatchedPos();
    }

    #endregion

}
