using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolsManager : Singleton<ObjectPoolsManager>
{
    Dictionary<Component, ObjectPool> pools = new Dictionary<Component, ObjectPool>(); 

    // Doesn't need to generic... the type gets lost in all the juggling, unfornately
    //public Handle Instantiate<T>(Transform parent, T prefab, Vector3? position = null, Quaternion? rotation = null) where T : MonoBehaviour
    //public Handle Instantiate(Transform parent, Component prefab, Vector3? position = null, Quaternion? rotation = null)
    public Handle Borrow(Transform parent, Component prefab, Vector3? position = null, Quaternion? rotation = null)
    {
        if (!pools.ContainsKey(prefab)){
            pools[prefab] = new ObjectPool(prefab);
        }
        var pool = pools.GetOrNull(prefab);
    
        // acquire the object
        var borrowed = pool.Borrow();
        var obj = borrowed.value;

        obj.gameObject.transform.parent = parent;
        obj.gameObject.transform.localPosition = position ?? prefab.transform.localPosition;
        obj.gameObject.transform.localRotation = rotation ?? prefab.transform.localRotation;

        return borrowed;
    }

    //public void Destroy(ref Handle handle)
    public void Return(ref Handle handle)
    {
        if (handle is HandleImpl){
            var impl = handle as HandleImpl;
            // Debug.Log(impl);
            // foreach (var kv in pools){
            //     Debug.Log(kv.Key);
            // }
            var pool = pools[impl.key];
            impl.value.transform.parent = transform;
            pool.Return(impl.id);
            handle = null;
        }
    }

    public abstract class Handle{
        public readonly Component value;

        public Handle(Component val){
            value = val;
        }
    }

    sealed class HandleImpl : Handle{
        public readonly int id;
        public readonly Component key;

        public HandleImpl(Component key, Component value) : base(value){
            id = Random.Range(10000, 99999);
            this.key = key;
        }

        
        public override string ToString(){
            return 
                $"HandleImpl(id = {id}, key = {key}, value = {value})";   
        }
    }

    class ObjectPool{

        Component prototype;
        Stack<HandleImpl> pool = new Stack<HandleImpl>();
        Dictionary<int, HandleImpl> activeObjs = new Dictionary<int, HandleImpl>();

        public ObjectPool(Component proto){
            prototype = proto;
        }

        public HandleImpl Borrow(){
            if (pool.Count == 0){
                var obj = Instantiate(prototype);
                pool.Push(new HandleImpl(prototype, obj));
            }
            var borrowed = pool.Pop();
            borrowed.value.gameObject.SetActive(true);
            activeObjs[borrowed.id] = borrowed;
            Debug.Log($"Object Borrowed. Pool of {prototype} has {pool.Count} with {activeObjs.Count} active objects. {borrowed} was borrowed");
            return borrowed;
        }

        public void Return(int id){
            var handle = activeObjs.GetOrNull(id);
            if (handle != null){
                handle.value.gameObject.SetActive(false);
                activeObjs.Remove(id);
                pool.Push(handle);
                Debug.Log($"Object Returned. Pool of {prototype} has {pool.Count} with {activeObjs.Count} active objects. {handle} was returned");
            }
        }
    }
}

public static class DictExt{
    public static TValue GetOrNull<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key) where TValue: class {
        if (!dict.ContainsKey(key)) return null;
        else return dict[key];
    }

    public static TValue? GetOrNull<TKey, TValue>(this Dictionary<TKey, TValue?> dict, TKey key) where TValue: struct {
        if (!dict.ContainsKey(key)) return null;
        else return dict[key];
    }
}
