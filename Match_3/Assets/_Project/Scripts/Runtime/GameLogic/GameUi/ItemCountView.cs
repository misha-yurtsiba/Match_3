using TMPro;
using UnityEngine;
using Zenject;

public class ItemCountView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _odstacleCountText;

    private IItemProvider _itemProvider;

    [Inject]
    private void Construct(ObstecleProvider obstecleProvider)
    {
        _itemProvider = obstecleProvider;
    }

    private void OnEnable()
    {
        _itemProvider.OnValueChanged += UpdateOdstacleCountText;
    }

    private void OnDisable()
    {
        _itemProvider.OnValueChanged -= UpdateOdstacleCountText;
    }
    public void UpdateOdstacleCountText(int count)
    {
        _odstacleCountText.text = count.ToString();
    }
}
