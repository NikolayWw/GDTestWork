using UnityEngine;

namespace Infrastructure
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Game _gamePrefab;

        private void Awake()
        {
            Game findGame = FindObjectOfType<Game>();
            if (findGame == null)
                Instantiate(_gamePrefab).StartGame();
        }
    }
}