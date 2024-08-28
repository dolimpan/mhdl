using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forest : MonoBehaviour //forest
{
    public GameObject tree, stone, soil, house, unbreakablesoil, trashCan, iron, titanum, cu;
    public List<GameObject> GOL = new List<GameObject>();

    public int count = 3; // 오브젝트 갯수
    public BoxCollider2D forestArea;
    public BoxCollider2D houseArea;
    public Vector3 forestPos; //forest 중심 좌표
    public Vector3 housePos; //house 중심 좌표
    public List<GameObject> ObjectList = new List<GameObject>(); // 인게임 forest에 깔리는 objectlist
    public float delayTime; // 인게임 forest에 깔리는 object 리셋시간
    public List<Vector2> PosList = new List<Vector2>(); // 생성된 랜덤좌표들의 list
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
            for (int i = 0; i < GOL.Count; i++) // 나무, 돌, 흙, 부셔지지않는 흙
            {
                if (i == 4)
                {
                    count = 5;
                }
                else if (i > 4)
                {
                    count = 2;
                }
                for (int j = 0; j < count; j++) //count갯수만큼 생성
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
                        SpawnPos = new Vector2(forestPos.x + RandX, forestPos.y + RandY);//랜덤 오브젝트 좌표 생성
                        validPosition = Poscheck(SpawnPos);
                        attempts++;
                    } while (!validPosition && attempts < 100);

                    if (validPosition)
                    {
                        GameObject instance = Instantiate(GOL[i], SpawnPos, Quaternion.identity);
                        //Debug.Log(SpawnPos + "에다 생성");
                        ObjectList.Add(instance);
                        PosList.Add(SpawnPos);
                    }
                    else
                    {
                        Debug.LogError("올바른 생성위치를 찾는데에 실패했습니다");
                    }
                }
            }
            forestArea.enabled = false;
            yield return new WaitForSecondsRealtime(delayTime);

            for (int i = 0; i < ObjectList.Count; i++)
            {
                Destroy(ObjectList[i]); //인게임에 깔린 object들의 list 초기화
            }
            ObjectList.Clear();
            PosList.Clear();
            forestArea.enabled = true;
        }
    }
    bool Poscheck(Vector2 SpawnPos) //생성한 랜덤좌표가 올바른지 확인하는 함수
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