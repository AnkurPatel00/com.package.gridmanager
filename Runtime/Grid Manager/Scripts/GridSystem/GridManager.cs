using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MBSingleton<GridManager>
{
    public GridImage _GridImage;

    public DrawGrid _DrawGrid;

    [Range(0, 99)]
    public int _HorizontalLength, _VerticalLength;

    [Range(0, 99)]
    [Tooltip("difference between each grid")]
    public float _Offset;

    public string _Pattern;

    internal int[] _PatternArray;

    internal List<GameObject> _RowParentList;

    internal List<Grid> m_GridList = new List<Grid>();

    private CameraManager pCurrentCamera;

    public enum GridImage
    {
        NONE,
        SQUARE,
        DOT,
        FRAME,
        RED,
        WOOD,
        OAK,
        GLASS,
        BLACK_DOT,
    }

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

        Init();
        SetCamera();
        // Will change the orthographic size based on horizontal and vertical grid numbers.
    }

    void Init()
    {
        _RowParentList = new List<GameObject>();
        pCurrentCamera = Camera.main.GetComponent<CameraManager>();
        UpdatePattern();
        _DrawGrid.RefreshVariables();
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
        float totalcoveredwidth, totalcoveredheight;

        // will decide to draw grid by horizontall or vertically

        // Considering by width wise
        if ((pCurrentCamera._Camera.aspect * _VerticalLength / _HorizontalLength) <= 1)
        {
            totalcoveredwidth = _DrawGrid._SpriteWidth * _HorizontalLength; // 1.28*8 = 10.24

            totalcoveredwidth += (_HorizontalLength + 1) * _Offset;

            pCurrentCamera.SetOrthoGraphicSize(totalcoveredwidth / (pCurrentCamera._Camera.aspect * 2));
            _DrawGrid.DrawByWidth = true;
            _DrawGrid.Draw();
        }

        //Considering by height wise
        else
        {
            totalcoveredheight = _DrawGrid._SpriteWidth * _VerticalLength; // 1.28*12 = 15.36

            totalcoveredheight += (_VerticalLength + 1) * _Offset;

            pCurrentCamera.SetOrthoGraphicSize(totalcoveredheight / 2);
            _DrawGrid.DrawByWidth = false;
            _DrawGrid.Draw();
        }
    }

    private void OnValidate()
    {
        UpdatePattern();
        _DrawGrid?.ChangeSprite();
        _DrawGrid?.UpdateValues();
    }
}