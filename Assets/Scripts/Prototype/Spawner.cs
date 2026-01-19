using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _spawnPrefab;
    [SerializeField] private float _spawnTimeInterval;

    private Coroutine _spawnTimeCoroutine;

    private void OnEnable()
    {
        _spawnTimeCoroutine = StartCoroutine(_spawnTimer());
    }

    private void OnDisable()
    {
        if (_spawnTimeCoroutine != null)
        {
            StopCoroutine(_spawnTimeCoroutine);
            _spawnTimeCoroutine = null;
        }
    }

    private IEnumerator _spawnTimer()
    {
        while (true)
        {
            GameObject spawned = Instantiate(_spawnPrefab, transform.position, Quaternion.identity);
            spawned.SetActive(true);

            yield return new WaitForSeconds(_spawnTimeInterval);
        }
    }
}
