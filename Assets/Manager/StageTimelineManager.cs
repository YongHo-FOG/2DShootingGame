using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTimelineManager : MonoBehaviour
{
    [Header("스폰 관리")]
    public EnemySpawner spawner;            // 몬스터 스포너 참조

    [Header("보스 연출")]
    public GameObject bossPrefab;           // 보스 프리팹
    public GameObject warningUI;            // 보스 등장 경고 UI
    public GameObject clearUI;              // 클리어 시 UI

    private float elapsedTime = 0f;                         // 경과 시간
    private List<StageEvent> timeline = new();              // 시간 이벤트 리스트
    private int currentEventIndex = 0;                      // 현재 진행 중인 이벤트 인덱스
    private bool bossSpawned = false;                       // 중복 스폰 방지

    void Start()
    {
        // 몬스터 A, B, C 스폰 시작 (스테이지 시작 시)
        timeline.Add(new StageEvent(0f, () => spawner.StartSpawning("A", 2f)));
        timeline.Add(new StageEvent(0f, () => spawner.StartSpawning("B", 4f)));
        timeline.Add(new StageEvent(0f, () => spawner.StartSpawning("C", 8f)));

        // 40초에 보스 등장 (테스트용, 실제는 240f = 4분)
        timeline.Add(new StageEvent(20f, SpawnBoss));
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;  // 경과 시간 누적

        // 다음 이벤트 실행 여부 확인
        while (currentEventIndex < timeline.Count &&
               elapsedTime >= timeline[currentEventIndex].triggerTime)
        {
            timeline[currentEventIndex].action.Invoke();  // 이벤트 실행
            currentEventIndex++;
        }
    }

    // 보스 등장 트리거
    void SpawnBoss()
    {
        if (bossSpawned) return;
        bossSpawned = true;

        spawner.StopAll();                       // 일반 몬스터 스폰 중지
        StartCoroutine(ShowWarningUI(2.5f));     // 경고 UI 잠시 표시 → 자동 숨김
        StartCoroutine(SpawnBossAfterDelay(2.5f)); // 보스 등장 딜레이
    }

    // 경고 UI 일정 시간 동안 표시하고 자동 숨김
    private IEnumerator ShowWarningUI(float duration)
    {
        warningUI.SetActive(true);
        yield return new WaitForSeconds(duration);
        warningUI.SetActive(false);
    }

    // 보스 UI 이후 실제 보스 등장 처리
    private IEnumerator SpawnBossAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(bossPrefab, new Vector3(0, 6f, 0), Quaternion.identity);
    }

    // 보스 처치 시 클리어 UI 출력 (보스 스크립트에서 호출할 예정)
    public void OnBossDefeated()
    {
        clearUI.SetActive(true);
        // 이후: 씬 전환, 연출 추가 등 가능
    }

    // 시간 기반 이벤트 구조 정의
    private class StageEvent
    {
        public float triggerTime;
        public System.Action action;

        public StageEvent(float t, System.Action a)
        {
            triggerTime = t;
            action = a;
        }
    }
}
