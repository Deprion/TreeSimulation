using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TreeManager Trees;
    public static GameManager inst;
    public bool IsPause;
    private Coroutine play;
    private void Start()
    {
        inst = this;
        LevelManagerSO.inst.GenerateWorld();
        StartWorld();
    }
    public void ResetGame()
    {
        IsPause = false;
        Trees.Terminate();

        StopCoroutine(play);

        StartWorld();
    }
    public void Terminate()
    {
        Trees.Terminate();

        StopCoroutine(play);
    }
    private void StartWorld()
    {
        Trees.AddTree(Global.TreeCount);
        play = StartCoroutine(Play());
    }
    private IEnumerator Play()
    {
        while (true)
        {
            while (IsPause)
            {
                yield return GameParamSO.inst.WaitForUpdate;
            }

            Trees.Grow();

            yield return GameParamSO.inst.WaitForSec;
        }
    }
}
