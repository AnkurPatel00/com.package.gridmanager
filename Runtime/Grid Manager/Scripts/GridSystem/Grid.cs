using UnityEngine;

public class Grid : MonoBehaviour
{
    internal string _Id;

    public void SetSprite(Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
