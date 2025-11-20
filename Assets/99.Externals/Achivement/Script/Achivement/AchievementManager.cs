using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance;

    private string savePath;
    public List<Achievement> achievements = new List<Achievement>();
    [SerializeField] private GameObject achievePopupPrefab;
    [SerializeField] private Image achieveUiImage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        savePath = Path.Combine(Application.persistentDataPath, "Achievements.json");
        Debug.Log(Application.persistentDataPath);

        LoadAchievements();
    }

    // JSON에서 불러오기
    void LoadAchievements()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            achievements = JsonUtility.FromJson<AchievementList>(json).list;
        }
        else
        {
            // 기본 데이터 로드 (Resources 폴더에서)
            TextAsset jsonFile = Resources.Load<TextAsset>("Achievements");
            achievements = JsonUtility.FromJson<AchievementList>(jsonFile.text).list;

            SaveAchievements();
        }

        Debug.Log($"achievements's count = {achievements.Count}");
    }


    // 저장
    public void SaveAchievements()
    {
        Debug.Log("Save achievements");
        Debug.Log(savePath);
        AchievementList wrapper = new AchievementList { list = achievements };
        string json = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(savePath, json);
    }

    // 도전과제 해금
    public void UnlockAchievement(string id)
    {
        Achievement achievement = achievements.Find(a => a.id == id);
        if (achievement != null && !achievement.isUnlocked)
        {
            achievement.isUnlocked = true;
            Debug.Log($"도전과제 해금: {achievement.title}");
            SaveAchievements();
        }
    }

    [System.Serializable]
    private class AchievementList
    {
        public List<Achievement> list;
    }

    public void AddProgress(string id, int amount)
    {
        Achievement a = achievements.Find(x => x.id == id);
        if (a == null) return;

        // 이미 달성한 도전과제면 팝업x
        bool alreadyUnlock = a.isUnlocked;

        bool unlocked = a.AddProgress(amount);
        if (unlocked)
        {
            Debug.Log($"도전과제 달성: {a.title}");
        }

        SaveAchievements();

        if (!alreadyUnlock)
        {
            if(achieveUiImage == null)
            {
                StartCoroutine(Popup(a));
            }
            else
            {
                StartCoroutine(PopupUI(a));
            }
            
        }

        AchievementUIManager ui = FindObjectOfType<AchievementUIManager>();
        if (ui != null)
            ui.RefreshUI();
    }

    public void AdjustProgress(string id, int amount)
    {
        Debug.Log("start adjust");
        Achievement a = achievements.Find(x => x.id == id);
        if (a == null) return;

        // 이미 달성한 도전과제면 팝업x
        bool alreadyUnlock = a.isUnlocked;

        bool unlocked = a.AdjustProgress(amount);
        Debug.Log($"amount adjust, {unlocked}");

        if (unlocked)
        {
            Debug.Log($"도전과제 달성: {a.title}");
        }

        SaveAchievements();

        if (!alreadyUnlock)
        {
            if(achieveUiImage == null)
            {
                StartCoroutine(Popup(a));
            }
            else
            {
                StartCoroutine(PopupUI(a));
            }
            
        }

        AchievementUIManager ui = FindObjectOfType<AchievementUIManager>();
        if (ui != null) ui.RefreshUI();
    }

    private IEnumerator Popup(Achievement achievement)
    {
        GameObject obj = Instantiate(achievePopupPrefab);
        AchievementPopup aPop = obj.GetComponent<AchievementPopup>();
        obj.transform.Find("Text").GetComponent<TextMeshPro>().text = achievement.title + "\n" + achievement.progress + " / " + achievement.goal;

        float time = 0f;
        while (time < aPop.moveTime)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / aPop.moveTime);   // Lerp 범위에 맞추기 위한 정규화

            try
            {
                obj.transform.position = Vector3.Lerp(aPop.startPos, aPop.startPos + aPop.moveVector, t);
            }
            catch
            {
                break;
            }
            
            yield return null;
        }

        yield return new WaitForSeconds(3f);

        time = 0f;
        while (time < aPop.moveTime)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / aPop.moveTime);   // Lerp 범위에 맞추기 위한 정규화

            try
            {
                obj.transform.position = Vector3.Lerp(aPop.startPos + aPop.moveVector, aPop.startPos, t);
            }
            catch
            {
                break;
            }            
            yield return null;
        }
    }

    private IEnumerator PopupUI(Achievement achievement)
    {
        Image obj = achieveUiImage;
        AchievementPopup aPop = obj.GetComponent<AchievementPopup>();
        obj.transform.Find("Text_AchieveName").GetComponent<TextMeshProUGUI>().text = achievement.title;
        obj.rectTransform.Find("Text_AchieveScript").GetComponent<TextMeshProUGUI>().text = achievement.description +"\n"+ achievement.progress + " / " + achievement.goal;

        float time = 0f;
        while (time < aPop.moveTime)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / aPop.moveTime);   // Lerp 범위에 맞추기 위한 정규화

            try
            {
                obj.rectTransform.anchoredPosition = Vector3.Lerp(aPop.startPos, aPop.startPos + aPop.moveVector, t);
            }
            catch
            {
                break;
            }
            
            yield return null;
        }

        yield return new WaitForSeconds(aPop.waitTime);

        time = 0f;
        while (time < aPop.moveTime)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / aPop.moveTime);   // Lerp 범위에 맞추기 위한 정규화

            try
            {
                obj.rectTransform.anchoredPosition = Vector3.Lerp(aPop.startPos + aPop.moveVector, aPop.startPos, t);
            }
            catch
            {
                break;
            }            
            yield return null;
        }
    }
}
