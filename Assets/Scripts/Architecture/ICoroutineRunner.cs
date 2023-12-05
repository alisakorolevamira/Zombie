using System.Collections;
using UnityEngine;

namespace Scripts.Architecture
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}