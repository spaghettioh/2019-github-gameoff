using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Parcoun.Utilities
{
    public class HandleInput : MonoBehaviour
    {
        public UnityEvent anyKeyPressed;

        void Update()
        {
            if (Input.anyKeyDown)
            {
                anyKeyPressed.Invoke();
            }
        }

        public void LoadScene(string s)
        {
            SceneManager.LoadScene(s);
        }
    }
}
