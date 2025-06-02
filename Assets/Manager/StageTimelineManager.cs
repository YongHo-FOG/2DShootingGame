using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTimelineManager : MonoBehaviour
{
    [Header("���� ����")]
    public EnemySpawner spawner;            // ���� ������ ����

    [Header("���� ����")]
    public GameObject bossPrefab;           // ���� ������
    public GameObject warningUI;            // ���� ���� ��� UI
    public GameObject clearUI;              // Ŭ���� �� UI

    private float elapsedTime = 0f;                         // ��� �ð�
    private List<StageEvent> timeline = new();              // �ð� �̺�Ʈ ����Ʈ
    private int currentEventIndex = 0;                      // ���� ���� ���� �̺�Ʈ �ε���
    private bool bossSpawned = false;                       // �ߺ� ���� ����

    void Start()
    {
        // ���� A, B, C ���� ���� (�������� ���� ��)
        timeline.Add(new StageEvent(0f, () => spawner.StartSpawning("A", 2f)));
        timeline.Add(new StageEvent(0f, () => spawner.StartSpawning("B", 4f)));
        timeline.Add(new StageEvent(0f, () => spawner.StartSpawning("C", 8f)));

        // 40�ʿ� ���� ���� (�׽�Ʈ��, ������ 240f = 4��)
        timeline.Add(new StageEvent(20f, SpawnBoss));
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;  // ��� �ð� ����

        // ���� �̺�Ʈ ���� ���� Ȯ��
        while (currentEventIndex < timeline.Count &&
               elapsedTime >= timeline[currentEventIndex].triggerTime)
        {
            timeline[currentEventIndex].action.Invoke();  // �̺�Ʈ ����
            currentEventIndex++;
        }
    }

    // ���� ���� Ʈ����
    void SpawnBoss()
    {
        if (bossSpawned) return;
        bossSpawned = true;

        spawner.StopAll();                       // �Ϲ� ���� ���� ����
        StartCoroutine(ShowWarningUI(2.5f));     // ��� UI ��� ǥ�� �� �ڵ� ����
        StartCoroutine(SpawnBossAfterDelay(2.5f)); // ���� ���� ������
    }

    // ��� UI ���� �ð� ���� ǥ���ϰ� �ڵ� ����
    private IEnumerator ShowWarningUI(float duration)
    {
        warningUI.SetActive(true);
        yield return new WaitForSeconds(duration);
        warningUI.SetActive(false);
    }

    // ���� UI ���� ���� ���� ���� ó��
    private IEnumerator SpawnBossAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(bossPrefab, new Vector3(0, 6f, 0), Quaternion.identity);
    }

    // ���� óġ �� Ŭ���� UI ��� (���� ��ũ��Ʈ���� ȣ���� ����)
    public void OnBossDefeated()
    {
        clearUI.SetActive(true);
        // ����: �� ��ȯ, ���� �߰� �� ����
    }

    // �ð� ��� �̺�Ʈ ���� ����
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
