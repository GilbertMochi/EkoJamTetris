using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpawnPoint : MonoBehaviour
{
    public List<GameObject> SpawnLocations = new List<GameObject>();

    [SerializeField]
    GameObject _SpawnLocation;
    [SerializeField]
    ParticleSystem SmallCloudSpawner;
    [SerializeField]
    ParticleSystem InitialCloudSpawner;

    int _Index;

    private void OnEnable() 
    {
        _Index = 0;
        if (SpawnLocations.Count < 1)
            return;

        _SpawnLocation.transform.position = SpawnLocations[_Index].transform.position;
        InitialCloudSpawner.transform.position = _SpawnLocation.transform.position;
        SmallCloudSpawner.Play();
        InitialCloudSpawner.Play();
        StartCoroutine(GoToHere());

    }

    private void FixedUpdate()
    {
        if (SpawnLocations.Count < 1)
            return;
        if (Vector3.Distance(SpawnLocations[_Index].transform.position, _SpawnLocation.transform.position) < 0.2)
            return;
        if (_Index > 1 && _Index < SpawnLocations.Count)
            _SpawnLocation.transform.position = Vector3.Lerp
                    (SpawnLocations[_Index - 1].transform.position, SpawnLocations[_Index].transform.position, Time.deltaTime / 50f);
    }

    IEnumerator GoToHere()
    { 
            SmallCloudSpawner.Play();
            _SpawnLocation.transform.position = SpawnLocations[_Index].transform.position;
            yield return new WaitForSeconds(4f);
            InitialCloudSpawner.Stop();
            SpawnLocations[_Index].SetActive(true);

        if (_Index + 1 < SpawnLocations.Count)
        {
            _Index++;
            StartCoroutine(GoToHere());
        }
        else
        {
            SmallCloudSpawner.Stop();
            InitialCloudSpawner.Stop();
        }
 
    }
}
