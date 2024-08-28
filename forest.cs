using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forest : MonoBehaviour //forest
{
    public GameObject tree, stone, soil, house, unbreakablesoil, trashCan, iron, titanum, cu;
    public List<GameObject> GOL = new List<GameObject>();

    public int count = 3; // ������Ʈ ����
    public BoxCollider2D forestArea;
    public BoxCollider2D houseArea;
    public Vector3 forestPos; //forest �߽� ��ǥ
    public Vector3 housePos; //house �߽� ��ǥ
    public List<GameObject> ObjectList = new List<GameObject>(); // �ΰ��� forest�� �򸮴� objectlist
    public float delayTime; // �ΰ��� forest�� �򸮴� object ���½ð�
    public List<Vector2> PosList = new List<Vector2>(); // ������ ������ǥ���� list
    public IEnumerator Spawn;

    void Start()
    {
        forestArea = GetComponent<BoxCollider2D>();
        if (house != null)
        {
            houseArea = house.GetComponent<BoxCollider2D>();
        }
        else
        {
            return;
        }
        GOL.Add(tree);
        GOL.Add(stone);
        GOL.Add(soil);
        GOL.Add(unbreakablesoil);
        GOL.Add(trashCan);
        GOL.Add(iron);
        GOL.Add(titanum);
        GOL.Add(cu);
        forestPos = transform.position;
        housePos = house.transform.position;
        Spawn = mySpawn();
        StartCoroutine(Spawn);
        delayTime = 180f;
    }

    IEnumerator mySpawn()
    {
        while (true)
        {
            for (int i = 0; i < GOL.Count; i++) // ����, ��, ��, �μ������ʴ� ��
            {
                if (i == 4)
                {
                    count = 5;
                }
                else if (i > 4)
                {
                    count = 2;
                }
                for (int j = 0; j < count; j++) //count������ŭ ����
                {
                    int attempts = 0;
                    bool validPosition = false;
                    Vector2 SpawnPos = Vector2.zero;
                    do
                    {
                        float RandX = Random.Range(-forestArea.size.x / 2f, forestArea.size.x / 2f);
                        float RandY = Random.Range(-forestArea.size.y / 2f, forestArea.size.y / 2f);
                        if (i == 4)
                        {
                            RandX = Random.Range(0, forestArea.size.x / 2f);
                        }
                        SpawnPos = new Vector2(forestPos.x + RandX, forestPos.y + RandY);//���� ������Ʈ ��ǥ ����
                        validPosition = Poscheck(SpawnPos);
                        attempts++;
                    } while (!validPosition && attempts < 100);

                    if (validPosition)
                    {
                        GameObject instance = Instantiate(GOL[i], SpawnPos, Quaternion.identity);
                        //Debug.Log(SpawnPos + "���� ����");
                        ObjectList.Add(instance);
                        PosList.Add(SpawnPos);
                    }
                    else
                    {
                        Debug.LogError("�ùٸ� ������ġ�� ã�µ��� �����߽��ϴ�");
                    }
                }
            }
            forestArea.enabled = false;
            yield return new WaitForSecondsRealtime(delayTime);

            for (int i = 0; i < ObjectList.Count; i++)
            {
                Destroy(ObjectList[i]); //�ΰ��ӿ� �� object���� list �ʱ�ȭ
            }
            ObjectList.Clear();
            PosList.Clear();
            forestArea.enabled = true;
        }
    }
    bool Poscheck(Vector2 SpawnPos) //������ ������ǥ�� �ùٸ��� Ȯ���ϴ� �Լ�
    {
        for (int i = 0; i < PosList.Count; i++)
        {
            float absX = Mathf.Abs(SpawnPos.x - PosList[i].x);
            float absY = Mathf.Abs(SpawnPos.y - PosList[i].y);
            if (absX < 2 && absY < 2)
            {
                return false;
            }
        }
        if (housePos.x - houseArea.size.x / 2f < SpawnPos.x && SpawnPos.x < housePos.x + houseArea.size.x / 2f &&
            housePos.y - houseArea.size.y / 2f < SpawnPos.y && SpawnPos.y < housePos.y + houseArea.size.y / 2f)
        {
            return false;
        }
        return true;
    }
}