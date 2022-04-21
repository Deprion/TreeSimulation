using UnityEngine;

[CreateAssetMenu(menuName = "Managers/Render")]
public class RenderSO : ScriptableObject
{
    public static RenderSO inst { get; private set; }
    public Sprite EmptySprite, FullSprite;
    public void Init() 
    {
        inst = this;
    }
    public Color32 GetRandomColor()
    {
        return new Color32((byte)Random.Range(0, 256), (byte)Random.Range(0, 256),
            (byte)Random.Range(0, 256), 255);
    }
}
