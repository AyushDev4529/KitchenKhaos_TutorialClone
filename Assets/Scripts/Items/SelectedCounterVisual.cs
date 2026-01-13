using System;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject selectedVisualGameObject;
    private void Start()
    {
        
        Player.Instance.OnSelectedCounterChangedWithArgs += Player_OnSelectedCounterChangedWithArgs;
    }

    private void Player_OnSelectedCounterChangedWithArgs(object sender, Player.SelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == clearCounter)
            Show();
        else
            Hide();

    }
    private void OnDestroy()
    {
        Player.Instance.OnSelectedCounterChangedWithArgs -= Player_OnSelectedCounterChangedWithArgs;
    }

    private void Show()
    {
        if (selectedVisualGameObject != null)
            selectedVisualGameObject.SetActive(true);
    }

    private void Hide()
    {
        if (selectedVisualGameObject != null)
            selectedVisualGameObject.SetActive(false);
    }
}
