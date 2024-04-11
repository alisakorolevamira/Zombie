using System.Collections;
using UnityEngine;

namespace Architecture
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}