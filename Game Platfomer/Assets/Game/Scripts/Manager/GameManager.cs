using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("--- Spawn Enemy---")]
    [SerializeField] Transform transSpawnLeft;
    [SerializeField] Transform transSpawnRight;

    [SerializeField] GameObject[] goPreEnemies;
    [SerializeField] Wave[] waves;
    [SerializeField] int lvMap = 1;
    int indexWave = 0;
    Wave wave;

    [Header("\n--- Spawn Item---")]
    [SerializeField] ItemSO[] dataItems;
    [SerializeField] Vector2 velocityInitItem;
    [SerializeField] GameObject goPreItem;


    [Header("\n---- UI ----")]
    public UiManager uiManager;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        InitWave();
    }
    #region Spawn Enemy
    void InitWave()
    {
        wave = waves[indexWave];
        for(int i = 0; i< wave._initEnemy; i++)
        {
            float x = Random.value * 100;
            List<int> indexSpawn = new List<int>();
            for (int j = 0; j < wave._typeEnemys.Length; j++) {
                if (wave._typeEnemys[j]._ratio > x)
                {
                    indexSpawn.Add(j);
                }
            }
            if (indexSpawn.Count > 0) {
                Spawn(goPreEnemies[Random.Range(0, indexSpawn.Count)]);
            }
            else
            {
                i--;
            }
        }

        StartCoroutine(SpawnWave());
    }
    IEnumerator SpawnWave()
    {
        int n = wave._totalEnemy - wave._initEnemy;
        while (n > 0)
        {
            yield return new WaitForSeconds(wave._timeDelaySpawn*Random.Range(0.8f, 1.2f));
            int k = Random.Range(0, 2);
            for (int i = 0; i < k; i++) {
                float x = Random.value * 100;
                List<int> indexSpawn = new List<int>();
                for (int j = 0; j < wave._typeEnemys.Length; j++)
                {
                    if (wave._typeEnemys[j]._ratio > x)
                    {
                        indexSpawn.Add(j);
                    }
                }
                if (indexSpawn.Count > 0)
                {
                    n--;
                    Spawn(goPreEnemies[Random.Range(0, indexSpawn.Count)]);
                }
                else
                {
                    i--;
                }
            }
        }
        yield return new WaitForSeconds(wave._timeDelayWave);

        indexWave++;
        if(indexWave < waves.Length)
            InitWave();
    }
    void Spawn(GameObject go)
    {
        GameObject goI = Instantiate(go, new Vector3(Random.Range(transSpawnLeft.position.x, transSpawnRight.position.x), transSpawnLeft.position.y, transSpawnLeft.position.z), Quaternion.identity);
        goI.transform.GetComponent<CharacterStats>().SetLv(lvMap);
    }
    #endregion

    #region Spawn item
    public void SpawnItem(Vector3 p)
    {
        float x = Random.value * 100;
        List<int> indexSpawn = new List<int>();
        for (int j = 0; j < dataItems.Length; j++)
        {
            if (dataItems[j]._ratio > x)
            {
                indexSpawn.Add(j);
            }
        }
        if (indexSpawn.Count > 0) {
            GameObject goI = Instantiate(goPreItem, p, Quaternion.identity);
            goI.transform.GetComponent<ItemController>().InitItem(dataItems[indexSpawn[Random.Range(0, indexSpawn.Count - 1)]], new Vector2(Random.Range(-1f, 1f)* velocityInitItem.x, Random.value* velocityInitItem.y));
        }
    }
    public void SpawnItem(ItemSO _itemSo, Vector3 p)
    {
        GameObject goI = Instantiate(goPreItem, p, Quaternion.identity);
        goI.transform.GetComponent<ItemController>().InitItem(_itemSo, new Vector2(Random.Range(-1f, 1f) * velocityInitItem.x, Random.value * velocityInitItem.y));
    }
    #endregion
    public void PauseGame(bool value)
    {
        Time.timeScale = value ? 0f : 1f;   
    }

}
[System.Serializable]
public struct SpawnEnemy
{
    public int _type;
    public float _ratio;
}
[System.Serializable]
public class Wave
{
    public string _name;
    public float _time;
    public int _totalEnemy;
    public int _initEnemy;
    public SpawnEnemy[] _typeEnemys;
    public float _timeDelaySpawn;
    public float _timeDelayWave;
}
