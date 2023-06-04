using UnityEngine;

namespace Core._Common.Extensions
{
    
/// <summary>
/// Inherit from this base class to create a singleton.
/// e.g. public class MyClassName : Singleton<MyClassName> {}
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // Check to see if we're about to be destroyed.
    private static bool m_ShuttingDown = false;
    private static object m_Lock = new object();
    private static T m_Instance;
 
    /// <summary>
    /// Access singleton instance through this propriety.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (m_ShuttingDown)
            {
//                Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
//                                 "' already destroyed. Returning null.");
                return null;
            }
 
            lock (m_Lock)
            {
                if (m_Instance == null)
                {
                    // Search for existing instance.
                    m_Instance = (T)FindObjectOfType(typeof(T));
 
                    // Create new instance if one doesn't already exist.
                    if (m_Instance == null)
                    {
                        // Need to create a new GameObject to attach the singleton to.
                        var singletonObject = new GameObject();
                        m_Instance = singletonObject.AddComponent<T>();
                        string name = typeof(T).ToString();
                        int idx = name.LastIndexOf('.');
                        if (idx >= 0)
                        {
                            name = name.Substring(++idx);
                        }

                        singletonObject.name = name;
                        Place();
                        // Make instance persistent.
                        //DontDestroyOnLoad(singletonObject);
                    }
                }
 
                return m_Instance;
            }
        }
    }
    private static void Place() {
        Transform parent = GameObject.FindGameObjectWithTag("SingletonHolder").transform;
        string self = Instance.gameObject.name;
        int index = -1;
        for (int i = 0; i < parent.childCount; i++)
        {
            string other = parent.GetChild(i).gameObject.name;
            int res = self.CompareTo(other);
            if(res < 0)
            {
                index = i;
                break;
            }
        }
        Instance.transform.parent = parent;
        if(index >= 0)
            Instance.transform.SetSiblingIndex(index);
        else
            Instance.transform.SetAsLastSibling();
    }
 
    private void OnDisable()
    {
        m_ShuttingDown = true;
    }
 
 
    private void OnDestroy()
    {
        m_ShuttingDown = true;
    }
}

public static class UnityEngineExtensions
{

}

}