using UnityEngine;

public class EnemySelectorIndicator : MonoBehaviour
{
    [SerializeField] private GameObject selectorCircle;
    public void TurnSelector(bool value) {
        selectorCircle.SetActive(value);
    }
}