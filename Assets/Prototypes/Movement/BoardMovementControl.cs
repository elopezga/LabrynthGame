using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class BoardMovementControl : MonoBehaviour
{
    private void Awake()
    {
        this.FixedUpdateAsObservable();
         var Joystick = this.FixedUpdateAsObservable()
            .Select(_ => {
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");
                return new Vector2(horizontal, vertical).normalized;
            })
            .Subscribe(joystick => {
                Debug.Log(joystick);
            });
    }
}
