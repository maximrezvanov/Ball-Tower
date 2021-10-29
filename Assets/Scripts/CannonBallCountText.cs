using UnityEngine;
using UnityEngine.UI;

public class CannonBallCountText : MonoBehaviour
{
    [SerializeField] private TextMesh counterText;
    private Color transparentСolor = new Color(1, 1, 1, 0);

    void Start()
    {
        Init();
    }

    public void Init()
    {
        SceneController.Instance.OnBullCount += OnBullCountHandler;
    }

    public void OnBullCountHandler(int number)
    {
        counterText = FindObjectOfType<TextMesh>();
        counterText.text = number.ToString();
        if (SceneController.Instance.rings.Count == 0 || SceneController.Instance.totalBullets < 0)
        {
            counterText.color = transparentСolor;
        }
    }
}
