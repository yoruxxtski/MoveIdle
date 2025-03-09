using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private DynamicJoystick dynamicJoystick;
    public Vector3 GetPlayerInput() {
        return new Vector3(dynamicJoystick.Horizontal, 0, dynamicJoystick.Vertical);
    }
}