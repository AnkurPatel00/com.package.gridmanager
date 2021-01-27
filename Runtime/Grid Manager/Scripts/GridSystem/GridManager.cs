using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MBSingleton<GridManager>
{
    public GameObject _GridPrefab;

    public Sprite _GridImage;

    [Range(0, 99)]
    public int _HorizontalLength, _VerticalLength;

    [Range(0, 99)]
    [Tooltip("difference between each grid")]
    public float _Offset;

    public string _Pattern;

    internal int[] _PatternArray;

    internal List<GameObject> _RowParentList;//= new List<GameObject>();

    internal List<Grid> m_GridList = new List<Grid>();

    private CameraManager pCurrentCamera;

    private float _SpriteWidth;

    private float LeftSide => pCurrentCamera.LeftX + (_SpriteWidth / 2f);

    private float DownSide => pCurrentCamera.DownY + (_SpriteWidth / 2f);

    private bool DrawByWidth;

    public delegate void voidDelegate();
    public voidDelegate OnGridDraw;

    void Start()
    {

        /* 
         *  For n*n Grid lets create a virtual screen in Game. (i.e. 8*8)

         Maximum resolution:
               Resolution 1536*2048 (so orthographic Camera have size will be 10.24 (2048/200))
         * Lets we keep 9:16 (minimum Aspect Ratio)
         * 
         * width will be 11.52 and height will be 20.48 
         * 
         * 
        */

        _RowParentList = new List<GameObject>();
        InitializeVariables();
        //  SetCamera();
        // Will change the orthographic size based on horizontal and vertical grid numbers.
    }

    void Init()
    {
        pCurrentCamera = Camera.main.GetComponent<CameraManager>();
        UpdatePattern();
    }

    void UpdatePattern()
    {
        if (_Pattern != string.Empty)
        {
            try
            {
                string[] pattern = _Pattern.Split(',');
                _PatternArray = Array.ConvertAll(pattern, s => int.Parse(s));
                _HorizontalLength = _PatternArray.Max();
                _VerticalLength = _PatternArray.Length;
            }
            catch (Exception)
            {

            }
        }
        else
        {
            _PatternArray = new int[_VerticalLength];
            for (int i = 0; i < _PatternArray.Length; i++)
                _PatternArray[i] = _HorizontalLength;
        }
    }

    public void SetCamera()
    {
        if (pCurrentCamera == null || pCurrentCamera._Camera == null)
            return;

        float totalcoveredwidth, totalcoveredheight;

        // will decide to draw grid by horizontall or vertically

        // Considering by width wise
        if ((pCurrentCamera._Camera.aspect * _VerticalLength / _HorizontalLength) <= 1)
        {
            totalcoveredwidth = _SpriteWidth * _HorizontalLength; // 1.28*8 = 10.24

            totalcoveredwidth += (_HorizontalLength + 1) * _Offset;

            pCurrentCamera.SetOrthoGraphicSize(totalcoveredwidth / (pCurrentCamera._Camera.aspect * 2));
            DrawByWidth = true;
            Draw();
        }

        //Considering by height wise
        else
        {
            totalcoveredheight = _SpriteWidth * _VerticalLength; // 1.28*12 = 15.36

            totalcoveredheight += (_VerticalLength + 1) * _Offset;

            pCurrentCamera.SetOrthoGraphicSize(totalcoveredheight / 2);
            DrawByWidth = false;
            Draw();
        }
    }

    public void Draw()
    {
        float offset = _Offset;
        for (int j = 0; j < _VerticalLength; j++)
        {
            GameObject row;
            string rowName = "Row " + j;
            if (!_RowParentList.Any(m => m.name == rowName))
            {
                Transform t = transform.Find(rowName);
                row = t != null ? t.gameObject : new GameObject(rowName);
                DefaultFunction.Instance.SetParent(row.transform, transform);
                row.transform.position = GetPosition(0, j);
                _RowParentList.Add(row);
            }
            else
                row = _RowParentList.Find(m => m.name == rowName);

            for (int i = 0; i < _PatternArray[_VerticalLength - j - 1]; i++)
            {
                string gridName = i + "," + j;
                if (!m_GridList.Any(m => m._Id == gridName))
                {
                    Transform t = row.transform.Find(gridName);
                    GameObject g = t != null ? t.gameObject : Instantiate(_GridPrefab);
                    g.transform.position = GetPosition(i, j);
                    g.name = gridName;
                    DefaultFunction.Instance.SetParent(g.transform, row.transform);
                    g.GetComponent<Grid>()._Id = gridName;
                    g.GetComponent<Grid>().SetSprite(_GridImage);
                    m_GridList.Add(g.GetComponent<Grid>());
                }
            }
        }
    }

    private Vector3 GetPosition(int i, int j)
    {
        float offset = _Offset;

        float downstarter = -1 * ((_VerticalLength - 1) * (_SpriteWidth + offset)) / 2f;
        float leftstarter = -1 * ((_HorizontalLength - 1) * (_SpriteWidth + offset)) / 2f;
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
        SetCamera();
        float offset = _Offset;

        m_GridList.ForEach(t => t.gameObject.SetActive(false));

        for (int j = 0; j < _VerticalLength; j++)
        {
            for (int i = 0; i < _PatternArray[_VerticalLength - j - 1]; i++)
            {
                Grid grid = m_GridList.Find(m => m._Id == i + "," + j);
                if (grid != null)
                {
                    grid.gameObject.SetActive(true);
                    grid.transform.position = GetPosition(i, j);
                    grid.SetSprite(_GridImage);
                }
            }
        }

        OnGridDraw?.Invoke();
    }

    public void ChangeSprite()
    {
        _SpriteWidth = _GridImage.rect.width / _GridImage.pixelsPerUnit;
    }

    private void OnValidate()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        Init();
        ChangeSprite();
        UpdateValues();
    }
}