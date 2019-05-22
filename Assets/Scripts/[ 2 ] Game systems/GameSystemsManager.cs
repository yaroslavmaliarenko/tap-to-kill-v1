using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystemsManager : MonoBehaviour
{
    static GameSystemsManager instance;

    public static GameSystemsManager Instance { get => instance;}

    Dictionary<System.Type, object> gameSystems;

    private void Awake()
    {
        if(instance != null)
        {
            DestroyImmediate(gameObject);            
            return;
        }

        instance = this;

        gameSystems = new Dictionary<System.Type, object>();
        //Find game systems object
        for (int i = 0; i < transform.childCount; i++)
        {
            GameSystem system = transform.GetChild(i).GetComponent<GameSystem>();
            if (system != null)
            {                
                AddSystem(system);                
            }
        }        
    }

    

    void AddSystem(object manager)
    {
        object value = null;
        if(!gameSystems.TryGetValue(manager.GetType(),out value))
        {
            gameSystems.Add(manager.GetType(), manager);
        }
    }

    public T GetSystem<T>()
    {
        object value = null;
        gameSystems.TryGetValue(typeof(T), out value);

        return (T)value;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
