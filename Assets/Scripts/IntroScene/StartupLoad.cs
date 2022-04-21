using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupLoad : MonoBehaviour
{
    [SerializeField] private GlobalGenomeSO genome;
    [SerializeField] private LevelManagerSO level;
    [SerializeField] private RenderSO render;
    [SerializeField] private EventManagerSO eventSO;
    [SerializeField] private SunManager sun;
    [SerializeField] private GameParamSO gameParam;
    private void Start()
    {
        genome.Init();
        gameParam.Init();
        level.Init();
        render.Init();
        eventSO.Init();
        sun.Init();
        SceneManager.LoadScene("MenuScene");
    }
}
