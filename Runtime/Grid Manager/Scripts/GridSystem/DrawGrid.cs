using System.Linq;
using UnityEngine;

public class DrawGrid : MonoBehaviour
{
    private GameObject pSquare;

    private Sprite pGridSprite;

    private CameraManager pCameraManager;

    private GridManager pGridManager;

    internal float _SpriteWidth;

    private float LeftSide => pCameraManager.LeftX + (_SpriteWidth / 2f);

    public float DownSide => pCameraManager.DownY + (_SpriteWidth / 2f);

    internal bool DrawByWidth;

    private int pHorizontalLength, pVerticalLength;

    void Awake()
    {
        pCameraManager = Camera.main.GetComponent<CameraManager>();
        pGridManager = GridManager.Instance;
        LoadGameObjects();
    }

    void LoadGameObjects()
    {
        pSquare = DefaultFunction.Instance.LoadPrefab(KeyManager._SquarePrefab);
        ChangeSprite();
    }

    public void Draw()
    {
        float offset = pGridManager._Offset;
        RefreshVariables();
        for (int j = 0; j < pVerticalLength; j++)
        {
            GameObject row;
            if (!pGridManager._RowParentList.Any(m => m.name == "Row " + j))
            {
                row = new GameObject("Row " + j);
                DefaultFunction.Instance.SetParent(row.transform, pGridManager.transform);
                row.transform.position = GetPosition(0, j);
                pGridManager._RowParentList.Add(row);
            }
            else
                row = pGridManager._RowParentList.Find(m => m.name == "Row " + j);

            for (int i = 0; i < pGridManager._PatternArray[pVerticalLength - j - 1]; i++)
            {
                if (!pGridManager.m_GridList.Any(m => m._Id == i + "," + j))
                {
                    GameObject g = Instantiate(pSquare, GetPosition(i, j), Quaternion.identity);
                    DefaultFunction.Instance.SetParent(g.transform, row.transform);
                    g.GetComponent<Grid>()._Id = i + "," + j;
                    g.GetComponent<Grid>().SetSprite(pGridSprite);
                    pGridManager.m_GridList.Add(g.GetComponent<Grid>());
                }
            }
        }
    }

    private Vector3 GetPosition(int i, int j)
    {
        float offset = pGridManager._Offset;

        float downstarter = -1 * ((pVerticalLength - 1) * (_SpriteWidth + offset)) / 2f;
        float leftstarter = -1 * ((pHorizontalLength - 1) * (_SpriteWidth + offset)) / 2f;
        float x, y;

        if (DrawByWidth)
        {
            x = LeftSide + (i * _SpriteWidth) + (i + 1) * offset;
            y = downstarter + (j * (_SpriteWidth + offset));
        }
        else
        {
            x = leftstarter + i * (_SpriteWidth + offset);
            y = DownSide + (j * _SpriteWidth) + (j + 1) * offset;
        }

        return new Vector3(x, y, 0);
    }

    public void UpdateValues()
    {
        if (pGridManager != null)
        {
            pGridManager.SetCamera();
            float offset = pGridManager._Offset;

            pGridManager.m_GridList.ForEach(t => t.gameObject.SetActive(false));

            for (int j = 0; j < pVerticalLength; j++)
            {
                for (int i = 0; i < pGridManager._PatternArray[pVerticalLength - j - 1]; i++)
                {
                    Grid grid = pGridManager.m_GridList.Find(m => m._Id == i + "," + j);
                    if (grid != null)
                    {
                        grid.gameObject.SetActive(true);
                        grid.transform.position = GetPosition(i, j);
                        grid.SetSprite(pGridSprite);
                    }
                }
            }
        }
    }

    public void ChangeSprite()
    {
        if (pGridManager != null)
        {
            pGridSprite = DefaultFunction.Instance.LoadSprite(EnumUtility.EnumToString(pGridManager._GridImage));
            _SpriteWidth = pGridSprite.rect.width / pGridSprite.pixelsPerUnit;
        }
    }

    public void RefreshVariables()
    {
        pHorizontalLength = pGridManager._HorizontalLength;
        pVerticalLength = pGridManager._VerticalLength;
    }
}
