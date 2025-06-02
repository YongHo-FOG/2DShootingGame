using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // 인스펙터에서 몬스터 타입을 구분하기 위한 구조체
    [System.Serializable]
    public class EnemySpawnInfo
    {
        public string type;            // 타입 이름 (예: "A", "B")
        public GameObject prefab;      // 해당 타입의 프리팹
    }

    public List<EnemySpawnInfo> enemyTypes;  // 타입 리스트를 인스펙터에서 설정
    private Dictionary<string, Coroutine> activeSpawns = new(); // 타입별 코루틴 저장

    // 해당 타입 몬스터를 일정 시간마다 생성 시작
    public void StartSpawning(string type, float interval)
    {
        if (!activeSpawns.ContainsKey(type)) // 이미 실행 중인 타입이면 중복 방지
        {
            Coroutine c = StartCoroutine(SpawnLoop(type, interval));
            activeSpawns[type] = c;
        }
    }

    // 모든 몬스터 스폰 중단
    public void StopAll()
    {
        foreach (var c in activeSpawns.Values)
            StopCoroutine(c);

        activeSpawns.Clear(); // 딕셔너리 초기화
    }

    // 반복적으로 몬스터 생성하는 루프
    private IEnumerator SpawnLoop(string type, float interval)
    {
        // 해당 타입의 프리팹 찾기
        GameObject prefab = enemyTypes.Find(e => e.type == type).prefab;

        while (true)
        {
            // 화면 위쪽 임의의 X 위치에 스폰
            Vector3 pos = new Vector3(Random.Range(-6f, 6f), 6f, 0);
            Instantiate(prefab, pos, Quaternion.identity);
            yield return new WaitForSeconds(interval); // 간격만큼 대기
        }
    }
}
