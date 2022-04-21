using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuSceneManager : MonoBehaviour
{
    [SerializeField] private Button startBtn;
    [SerializeField] private TMP_InputField heightInp, widthInp, treeCountInp, ageInp;
    [SerializeField] private TMP_Text sliderTxt;
    [SerializeField] private Slider mutationSlider;
    private void Awake()
    {
        startBtn.onClick.AddListener(StartGame);
        mutationSlider.onValueChanged.AddListener(UpdateSliderText);
        UpdateSliderText(mutationSlider.value);
    }
    private void UpdateSliderText(float value)
    {
        sliderTxt.text = value.ToString();
    }
    private void StartGame()
    {
        int height;
        int.TryParse(heightInp.text, out height);
        Global.Height = height <= 0 ? 15 : height;
        int width;
        int.TryParse(widthInp.text, out width);
        width = width < 25 ? 25 : width;
        Global.Width = width;
        int treeCount;
        int.TryParse(treeCountInp.text, out treeCount);
        Global.TreeCount = treeCount <= 0 ? 1 : treeCount > width / 2 ? width / 2 : treeCount;
        int age;
        int.TryParse(ageInp.text, out age);
        Global.MaxAge = age <= 0 ? 15 : age;
        Global.MutationChance = (int)mutationSlider.value;

        SceneManager.LoadScene("GameScene");
    }
}
