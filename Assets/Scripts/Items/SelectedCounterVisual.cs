using System;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    private BaseCounter baseCounter;
    [SerializeField] private GameObject[] selectedVisualGameObjectArray;


    private void Awake()
    {
        Hide();
        baseCounter = GetComponentInParent<BaseCounter>();
    }

    private void Start()
    {
        
        Player.Instance.OnSelectedCounterChangedWithArgs += Player_OnSelectedCounterChangedWithArgs;
    }

    private void Player_OnSelectedCounterChangedWithArgs(object sender, Player.SelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
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
        foreach(GameObject selectedVisualGameObject in selectedVisualGameObjectArray)
        {

            if (selectedVisualGameObject != null)
                selectedVisualGameObject.SetActive(true);
        }
        
    }

    private void Hide()
    {
        foreach (GameObject selectedVisualGameObject in selectedVisualGameObjectArray)
        {
            if (selectedVisualGameObject != null)
                selectedVisualGameObject.SetActive(false);
        }
        
    }
}
