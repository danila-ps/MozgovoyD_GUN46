using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Netologia.Gameplay
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private GameObject _restartPanel;
        [SerializeField] private Image _restartFillImage;
        [SerializeField] private float _restartDuration = 2f;

        private Controls.GameActions _game;
        private float _restartProgress;
        private bool _restartHeld;
        private bool _controlsBound;

        [Inject]
        private void Construct(Controls.GameActions game)
        {
            _game = game;
            _game.Restart.performed += OnRestartPerformed;
            _game.Restart.canceled += OnRestartCanceled;
            _game.Enable();
            _controlsBound = true;
        }

        private void OnDestroy()
        {
            if (!_controlsBound)
                return;

            _game.Restart.performed -= OnRestartPerformed;
            _game.Restart.canceled -= OnRestartCanceled;
            _game.Disable();
        }

        private void Update()
        {
            if (!_restartHeld)
                return;

            _restartProgress += Time.deltaTime / _restartDuration;
            _restartFillImage.fillAmount = _restartProgress;
        }

        private void OnRestartPerformed(InputAction.CallbackContext context)
        {
            _restartHeld = true;
            _restartProgress = 0f;
            _restartFillImage.fillAmount = 0f;
            _restartPanel.SetActive(true);
        }

        private void OnRestartCanceled(InputAction.CallbackContext context)
        {
            _restartHeld = false;

            if (_restartProgress >= 1f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                return;
            }

            _restartProgress = 0f;
            _restartFillImage.fillAmount = 0f;
            _restartPanel.SetActive(false);
        }
    }
}
