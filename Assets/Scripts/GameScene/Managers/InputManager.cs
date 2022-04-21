using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera _camera, uiCamera;
    [SerializeField] private float speed, zoomScale, zoomMin, zoomMax;
    private Vector3 forward = new Vector3(0, 0, 15);
    private float minX, maxX, minY, maxY;
    private void Start()
    {
        CalculateClamp();
    }
    private void Update()
    {
        GameCheck();
        UICheck();
        SpeedCheck();
    }
    private void LateUpdate()
    {
        MoveCheck();
    }
    private void CalculateClamp()
    {
        float camHeight = uiCamera.orthographicSize;
        float camWidth = uiCamera.orthographicSize * uiCamera.aspect;

        minX = camWidth - LevelManagerSO.inst.Width / 2f * (0.3f * 1.15f) - 5;

        maxX = -camWidth + LevelManagerSO.inst.Width / 2f * (0.3f * 1.15f) + 5;

        minY = camHeight + LevelManagerSO.inst.minY - 5;

        maxY = -camHeight + LevelManagerSO.inst.Height / 2f * 0.3f + 5;
    }
    private void MoveCheck()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        _camera.transform.position = ClampCamera(_camera.transform.position
            + new Vector3(moveX * speed* Time.deltaTime, moveY * speed * Time.deltaTime, 0));

        /*if (Input.mouseScrollDelta.y != 0)
        {
            uiCamera.orthographicSize = Mathf.Clamp
                (uiCamera.orthographicSize + (zoomScale * -Input.mouseScrollDelta.y),
                zoomMin, zoomMax);
            CalculateClamp();
            _camera.transform.position = ClampCamera(_camera.transform.position);
        }*/ // scroll
    }
    private Vector3 ClampCamera(Vector3 target)
    {
        float endX = Mathf.Clamp(target.x, minX, maxX);

        float endY = Mathf.Clamp(target.y, minY, maxY);

        return new Vector3(endX, endY, target.z);
    }
    private void GameCheck()
    {
        if (Input.GetButtonDown("Space"))
        {
            GameManager.inst.IsPause = !GameManager.inst.IsPause;
        }
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit info;
            if (Physics.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition),
                forward, out info))
            {
                var cell = info.collider.GetComponent<Cell>().GetCell();
                if (cell != null)
                {
                    UIManager.inst.GenomeUpdate(cell.CurrentGenome,
                        cell.tree.Dna.MutationChance, cell.tree.age, cell.tree.Dna.MaxAge,
                        cell.tree.energy);
                }
            }
        }
    }
    private void UICheck()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            UIManager.inst.Menu();
        }
    }
    private void SpeedCheck()
    {
        if (Input.GetButtonDown("Minus"))
            GameParamSO.inst.ChangeSpeed(false);
        if (Input.GetButtonDown("Plus"))
            GameParamSO.inst.ChangeSpeed(true);
    }
}
