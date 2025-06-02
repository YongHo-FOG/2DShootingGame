using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // �ν����Ϳ��� ���� Ÿ���� �����ϱ� ���� ����ü
    [System.Serializable]
    public class EnemySpawnInfo
    {
        public string type;            // Ÿ�� �̸� (��: "A", "B")
        public GameObject prefab;      // �ش� Ÿ���� ������
    }

    public List<EnemySpawnInfo> enemyTypes;  // Ÿ�� ����Ʈ�� �ν����Ϳ��� ����
    private Dictionary<string, Coroutine> activeSpawns = new(); // Ÿ�Ժ� �ڷ�ƾ ����

    // �ش� Ÿ�� ���͸� ���� �ð����� ���� ����
    public void StartSpawning(string type, float interval)
    {
        if (!activeSpawns.ContainsKey(type)) // �̹� ���� ���� Ÿ���̸� �ߺ� ����
        {
            Coroutine c = StartCoroutine(SpawnLoop(type, interval));
            activeSpawns[type] = c;
        }
    }

    // ��� ���� ���� �ߴ�
    public void StopAll()
    {
        foreach (var c in activeSpawns.Values)
            StopCoroutine(c);

        activeSpawns.Clear(); // ��ųʸ� �ʱ�ȭ
    }

    // �ݺ������� ���� �����ϴ� ����
    private IEnumerator SpawnLoop(string type, float interval)
    {
        // �ش� Ÿ���� ������ ã��
        GameObject prefab = enemyTypes.Find(e => e.type == type).prefab;

        while (true)
        {
            // ȭ�� ���� ������ X ��ġ�� ����
            Vector3 pos = new Vector3(Random.Range(-6f, 6f), 6f, 0);
            Instantiate(prefab, pos, Quaternion.identity);
            yield return new WaitForSeconds(interval); // ���ݸ�ŭ ���
        }
    }
}
