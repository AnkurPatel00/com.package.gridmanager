using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultFunction : MBSingleton<DefaultFunction>
{
    public delegate void ButtonHandler();

    public void SetString(GameObject g, string a)
    {
        try
        {
            if (g != null)
            {
                if (g.GetComponent<TextMesh>() != null)
                {
                    g.GetComponent<TextMesh>().text = a;
                }
                else if (g.GetComponent<Text>() != null)
                {
                    g.GetComponent<Text>().text = a;
                }
            }
        }

        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
        }
    }

    public string GetString(GameObject g)
    {
        try
        {
            if (g != null)
            {
                if (g.GetComponent<TextMesh>() != null)
                {
                    return g.GetComponent<TextMesh>().text;
                }
                else if (g.GetComponent<Text>() != null)
                {
                    return g.GetComponent<Text>().text;
                }
                else
                    return null;
            }
            else
                return null;
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
            return null;
        }
    }

    public void SetTexture(GameObject g, Texture2D Texture)
    {
        try
        {
            if (g != null && g.GetComponent<MeshRenderer>() != null)
            {
                g.GetComponent<Renderer>().material.mainTexture = Texture;
            }
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
        }
    }

    public void SetSprite(GameObject g, Sprite Sprite)
    {
        try
        {
            if (g != null)
            {
                if (g.GetComponent<SpriteRenderer>() != null)
                {
                    g.GetComponent<SpriteRenderer>().sprite = Sprite;
                }
                else if (g.GetComponent<Image>() != null)
                {
                    g.GetComponent<Image>().sprite = Sprite;
                }
            }
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
        }
    }

    public void SetSprite(GameObject g, Texture2D Texture)
    {
        try
        {
            if (g != null && Texture != null)
            {
                Sprite _Sprite = Texture2DToSprite(Texture);
                if (g.GetComponent<SpriteRenderer>() != null)
                {
                    g.GetComponent<SpriteRenderer>().sprite = _Sprite;
                }
                else if (g.GetComponent<Image>() != null)
                {
                    g.GetComponent<Image>().sprite = _Sprite;
                }
            }
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
        }
    }

    public Sprite GetSprite(GameObject g)
    {
        try
        {
            if (g != null && g.GetComponent<SpriteRenderer>() != null)
            {
                if (g.GetComponent<SpriteRenderer>().sprite != null)
                    return g.GetComponent<SpriteRenderer>().sprite;
                else
                    return null;
            }
            else
                return null;
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
            return null;
        }
    }

    public Texture2D GetTexture(GameObject g)
    {
        try
        {
            if (g != null && g.GetComponent<MeshRenderer>() != null)
            {
                if (g.GetComponent<Renderer>().material.mainTexture != null)
                    return (Texture2D)g.GetComponent<Renderer>().material.mainTexture;
                else
                    return null;
            }
            else
                return null;
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
            return null;
        }
    }

    public Sprite Texture2DToSprite(Texture2D _Tex)
    {
        try
        {
            Rect rect = new Rect(0, 0, _Tex.width, _Tex.height);
            Vector2 Pivot = new Vector2(0.5f, 0.5f);
            return Sprite.Create(_Tex, rect, Pivot);
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
            return null;
        }
    }

    public void ChangeAlpha(GameObject g, float value)
    {
        try
        {
            if (g.GetComponent<SpriteRenderer>() != null)
            {
                Color defaultcolor = g.GetComponent<SpriteRenderer>().color;
                g.GetComponent<SpriteRenderer>().color = new Color(defaultcolor.r, defaultcolor.g, defaultcolor.b, value);
            }
            else if (g.GetComponent<TextMesh>() != null)
            {
                Color defaultcolor = g.GetComponent<TextMesh>().color;
                g.GetComponent<TextMesh>().color = new Color(defaultcolor.r, defaultcolor.g, defaultcolor.b, value);
            }
            else if (g.GetComponent<Text>() != null)
            {
                Color defaultcolor = g.GetComponent<Text>().color;
                g.GetComponent<Text>().color = new Color(defaultcolor.r, defaultcolor.g, defaultcolor.b, value);
            }
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
        }
    }

    public Vector3 SetCanvasPosFromWorldPos(Vector3 pos)
    {
        try
        {
            Vector3 RefreshedPos = new Vector3(pos.x, pos.y, pos.z + Camera.main.transform.position.z);
            Vector3 cd = Camera.main.WorldToScreenPoint(RefreshedPos);
            return cd;
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
            return Vector3.zero;
        }
    }

    public Vector3 SetCanvasPosFromWorldPos(Vector3 pos, Camera cam)
    {
        try
        {
            Vector3 RefreshedPos = new Vector3(pos.x, pos.y, pos.z + cam.transform.position.z);
            Vector3 cd = cam.WorldToScreenPoint(RefreshedPos);
            return cd;
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
            return Vector3.zero;
        }
    }

    public Vector3 GetWorldPosFromCanvasPos(Vector3 pos)
    {
        try
        {
            Vector3 RefreshedPos = Camera.main.ScreenToWorldPoint(pos);
            Vector3 cd = new Vector3(RefreshedPos.x, RefreshedPos.y, RefreshedPos.z - Camera.main.transform.position.z);
            return cd;
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
            return Vector3.zero;
        }
    }

    public static Toggle GetActive(ToggleGroup myToggleGroup)
    {
        try
        {
            IEnumerator<Toggle> toggleEnum = myToggleGroup.ActiveToggles().GetEnumerator();
            toggleEnum.MoveNext();
            return toggleEnum.Current;
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
            return null;
        }
    }

    public void SetAtPosition(GameObject g, Vector3 pos)
    {
        try
        {
            RectTransform rect = g.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(pos.x, pos.y);
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
        }
    }

    public Vector2 GetPosition(GameObject g)
    {
        try
        {
            RectTransform rect = g.GetComponent<RectTransform>();
            return rect.anchoredPosition;
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
            return Vector2.zero;
        }
    }

    public static float GetRectHeight(GameObject g)
    {
        try
        {
            return g.GetComponent<RectTransform>().rect.height;
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
            return 0;
        }
    }

    public void SetAtTopCenter(GameObject g, float Height)
    {
        try
        {
            g.GetComponent<RectTransform>().sizeDelta = new Vector2(0, Height);
            g.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -Height / 2f);
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
        }
    }

    public void SetPositionExpandShrink(GameObject g, float height, bool Expand = true)
    {
        try
        {
            float SizeDeltaY = g.GetComponent<RectTransform>().sizeDelta.y;
            float anchoredPositionY = g.GetComponent<RectTransform>().anchoredPosition.y;
            if (Expand)
            {
                g.GetComponent<RectTransform>().sizeDelta = new Vector2(0, SizeDeltaY + height);
                g.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, anchoredPositionY - (height / 2f));
            }
            else
            {
                g.GetComponent<RectTransform>().sizeDelta = new Vector2(0, SizeDeltaY - height);
                g.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, anchoredPositionY + (height / 2f));
            }
        }
        catch (Exception e)
        {
            ExceptionHandling.ExceptionHandler(e);
        }
    }

    public IEnumerator CodeExecuteAfterDelay(float time, ButtonHandler B)
    {
        yield return new WaitForSeconds(time);
        B();
    }

    public IEnumerator CodeExecuteAfterDelay(int frames, ButtonHandler B)
    {
        yield return frames;
        B();
    }

    //#region GameRelated

    //public void Move(GameObject g, Vector3 pos, ButtonHandler cd = null, float time = 1)
    //{
    //    try
    //    {
    //        g.SetActive(true);
    //        EscapeButton.CanBack = false;
    //        RectTransform rect = g.GetComponent<RectTransform>();

    //        float Time = time;
    //        iTween.EaseType DefaultEase = iTween.EaseType.easeOutQuart;

    //        ArrayList _List = new ArrayList();
    //        _List.Add(g);
    //        _List.Add(cd);

    //        iTween.ValueTo(g, iTween.Hash("from", rect.anchoredPosition.x, "to", pos.x, "time", Time, "easeType", DefaultEase, "onupdate", (Action<object>)(val =>
    //        {
    //            rect.anchoredPosition = new Vector2((float)val, rect.anchoredPosition.y);
    //        }), "oncomplete", "MoveComplete", "oncompletetarget", gameObject, "oncompleteparams", _List));

    //        iTween.ValueTo(g, iTween.Hash("from", rect.anchoredPosition.y, "to", pos.y, "time", Time, "onupdate", (Action<object>)(val =>
    //        {
    //            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, (float)val);
    //        })));
    //    }
    //    catch (Exception e)
    //    {
    //        ExceptionHandling.ExceptionHandler(e);
    //    }
    //}

    //public void MoveComplete(ArrayList AfterList)
    //{
    //    try
    //    {
    //        GameObject_Find.Finder_Obj.UIPlacement_Obj.SetObjectLeftRightAfterMove();
    //        if (AfterList[1] != null)
    //        {
    //            ((ButtonHandler)AfterList[1])();
    //        }
    //        EscapeButton.CanBack = true;
    //    }
    //    catch (Exception e)
    //    {
    //        ExceptionHandling.ExceptionHandler(e);
    //    }
    //}

    //public void MoveBy(GameObject g, Vector3 pos, ButtonHandler cd = null, float time = 1, bool Horizontal = true)
    //{
    //    try
    //    {
    //        g.SetActive(true);
    //        EscapeButton.CanBack = false;
    //        RectTransform rect = g.GetComponent<RectTransform>();

    //        float Time = time;
    //        iTween.EaseType DefaultEase = iTween.EaseType.easeOutQuart;

    //        ArrayList _List = new ArrayList();
    //        _List.Add(g);
    //        _List.Add(cd);

    //        float To_X = rect.anchoredPosition.x + pos.x;
    //        float To_Y = rect.anchoredPosition.y + pos.y;

    //        if (Horizontal)
    //        {
    //            iTween.ValueTo(g, iTween.Hash("from", rect.anchoredPosition.x, "to", To_X, "time", Time, "easeType", DefaultEase, "onupdate", (Action<object>)(val =>
    //            {
    //                rect.anchoredPosition = new Vector2((float)val, rect.anchoredPosition.y);
    //            }), "oncomplete", "MoveByComplete", "oncompletetarget", gameObject, "oncompleteparams", _List));

    //            iTween.ValueTo(g, iTween.Hash("from", rect.anchoredPosition.y, "to", To_Y, "time", Time, "onupdate", (Action<object>)(val =>
    //            {
    //                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, (float)val);
    //            })));
    //        }
    //        else
    //        {
    //            iTween.ValueTo(g, iTween.Hash("from", rect.anchoredPosition.x, "to", To_X, "time", Time, "easeType", DefaultEase, "onupdate", (Action<object>)(val =>
    //            {
    //                rect.anchoredPosition = new Vector2((float)val, rect.anchoredPosition.y);
    //            })));

    //            iTween.ValueTo(g, iTween.Hash("from", rect.anchoredPosition.y, "to", To_Y, "time", Time, "onupdate", (Action<object>)(val =>
    //            {
    //                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, (float)val);
    //            }), "oncomplete", "MoveByComplete", "oncompletetarget", gameObject, "oncompleteparams", _List));
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        ExceptionHandling.ExceptionHandler(e);
    //    }
    //}

    //public void MoveByComplete(ArrayList AfterList)
    //{
    //    try
    //    {
    //        if (AfterList[1] != null)
    //        {
    //            ((ButtonHandler)AfterList[1])();
    //        }
    //        EscapeButton.CanBack = true;
    //    }
    //    catch (Exception e)
    //    {
    //        ExceptionHandling.ExceptionHandler(e);
    //    }
    //}

    //#endregion

    public GameObject LoadPrefab(string s)
    {
        GameObject g = Resources.Load(CGameConstants.Prefab_root + s) as GameObject;
        return g;
    }

    public Sprite LoadSprite(String s)
    {
        Sprite T = Resources.Load<Sprite>(CGameConstants.Sprite_root + s);
        return T;
    }

    public Texture2D LoadTexture(String s)
    {
        Texture2D g = Resources.Load(CGameConstants.Texture_root + s) as Texture2D;
        return g;
    }

    public Material LoadMaterial(string s)
    {
        Material M = Resources.Load(CGameConstants.Material_root + s) as Material;
        return M;
    }

    public string GetIMEIAndroid()
    {
#if UNNITY_ANDROID
        AndroidJavaObject TM = new AndroidJavaObject("android.telephony.TelephonyManager");
        return TM.Call<string>("getDeviceId");       
       
#else
        return "";
#endif
    }

    public void SetParent(Transform g, Transform parent, bool IsUIComponent = false)
    {
        if (IsUIComponent)
        {
            g.SetParent(parent, false);
        }
        else
        {
            g.parent = parent;
        }
    }
}
