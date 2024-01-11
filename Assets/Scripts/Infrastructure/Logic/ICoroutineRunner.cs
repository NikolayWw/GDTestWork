using System.Collections;
using UnityEngine;

namespace Infrastructure.Logic
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator loadScene);
    }
}