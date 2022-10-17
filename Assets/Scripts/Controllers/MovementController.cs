using UnityEngine;

namespace SpaceShooter
{

    public class MovementController : MonoBehaviour
    {
        public enum ControllMode
        {
            Keyboard,
            Mobile
        }

        [SerializeField] private SpaceShip m_TargetShip;
        public void SetTargetShip(SpaceShip ship) => m_TargetShip = ship;

        [SerializeField] private VirtualJoystick m_MobileJoystick;

        [SerializeField] private ControllMode m_ControllMode;

        [SerializeField] private PointerClickHold m_MobileFirePrimary;
        [SerializeField] private PointerClickHold m_MobileFireSecondary;

        private void Start()
        {
            if (m_ControllMode == ControllMode.Keyboard)
            {
                m_MobileJoystick.gameObject.SetActive(false);

                m_MobileFireSecondary.gameObject.SetActive(false);
                m_MobileFirePrimary.gameObject.SetActive(false);
            }
            else
            {
                m_MobileJoystick.gameObject.SetActive(true);

                m_MobileFirePrimary.gameObject.SetActive(true);
                m_MobileFireSecondary.gameObject.SetActive(true);
            }
        }

        private void Update()
        {
            if (m_TargetShip == null) return;

            if (m_ControllMode == ControllMode.Keyboard)
                ControllKeyboard();

            if (m_ControllMode == ControllMode.Mobile)
                ControllMobile();

        }

        private void ControllMobile()
        {
            Vector3 dir = m_MobileJoystick.Value;
            m_TargetShip.ThrustControll = dir.y;
            m_TargetShip.TorqueControl = -dir.x;

            if (m_MobileFirePrimary.IsHold)
                m_TargetShip.Fire(TurretMode.Primary);

            if (m_MobileFireSecondary.IsHold)
                m_TargetShip.Fire(TurretMode.Secondary);

        }

        private void ControllKeyboard()
        {
            float thrust = 0;
            float torque = 0;

            if (Input.GetKey(KeyCode.UpArrow))
                thrust = 1.0f;

            if (Input.GetKey(KeyCode.DownArrow))
                thrust = -1.0f;

            if (Input.GetKey(KeyCode.LeftArrow))
                torque = 1.0f;

            if (Input.GetKey(KeyCode.RightArrow))
                torque = -1.0f;

            if (Input.GetKey(KeyCode.Space))
                m_TargetShip.Fire(TurretMode.Primary);

            if (Input.GetKey(KeyCode.X))
                m_TargetShip.Fire(TurretMode.Secondary);

            m_TargetShip.ThrustControll = thrust;
            m_TargetShip.TorqueControl = torque;
        }

    }

}