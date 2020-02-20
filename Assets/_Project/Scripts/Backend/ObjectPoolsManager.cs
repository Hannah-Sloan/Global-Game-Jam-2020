using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolsManager : Singleton<ObjectPoolsManager>
{
    Dictionary<Component, ObjectPool> pools = new Dictionary<Component, ObjectPool>(); 

    public Handle Borrow(Transform parent, Component prefab, Vector3? position = null, Quaternion? rotation = null)
    {
        if (!pools.ContainsKey(prefab)){ // make a pool if it doesn't already exist
            pools[prefab] = new ObjectPool(prefab);
        }
        var pool = pools[prefab];
    
        // acquire the object
        var borrowed = pool.Borrow();
        var obj = borrowed.value;

        // Configure the object
        obj.gameObject.transform.parent = parent;
        obj.gameObject.transform.localPosition = position ?? prefab.transform.localPosition;
        obj.gameObject.transform.localRotation = rotation ?? prefab.transform.localRotation;

        return borrowed;
    }

    public void Return(ref Handle handle)
    {
        if (handle is HandleImpl){ // Make sure we're actually getting something back
            var impl = handle as HandleImpl;
            var pool = pools[impl.key]; // get the right object pool
            impl.value.transform.parent = transform; // return the object here, so it doesn't get destroyed
            pool.Return(impl.id);
            handle = null; // invalidate the callers reference to the handle
            // Should maybe make this a flag inside of HandleImpl, so that if they somehow keep a reference and try to use it it doesn't work
        }
    }

    // Not the preffered way of getting access to your component...
    public T UmanagedDeref<T>(Handle handle) where T : Component{
        if (handle is HandleImpl){ // Make sure we're actually getting something back
            var impl = handle as HandleImpl;
            if (impl.active) // make sure the reference is valid
                if (impl.value is T) // Make sure we're actually getting the thing asked for...
                    return impl.value as T;
            
        }

        return null; // default path
    }

    public void CallIfValid(Handle handle, System.Action<Component> callback){
        if (handle is HandleImpl){ // Make sure we're actually getting something back
            var impl = handle as HandleImpl;
            if (impl.active){ // make sure the reference is valid
                callback(impl.value);
            }
        }
    }
    
    public TResult CallIfValid<TResult>(Handle handle, System.Func<Component, TResult> callback) where TResult: class{
        if (handle is HandleImpl){ // Make sure we're actually getting something back
            var impl = handle as HandleImpl;
            if (impl.active){ // make sure the reference is valid
                return callback(impl.value);
            }
        }

        return null;
    }

    public TResult? CallIfValid<TResult>(Handle handle, System.Func<Component, TResult?> callback) where TResult: struct{
        if (handle is HandleImpl){ // Make sure we're actually getting something back
            var impl = handle as HandleImpl;
            if (impl.active){ // make sure the reference is valid
                return callback(impl.value);
            }
        }

        return null;
    }

#region classes
    public abstract class Handle{ }
#region Internal

    sealed internal class HandleImpl : Handle{
        public readonly int id;
        public readonly Component key;

        public readonly Component value;

        public bool active = false;
        public HandleImpl(Component key, Component value){
            id = Random.Range(10000, 99999);
            this.key = key;
            this.value = value;
        }

        
        public override string ToString(){
            return 
                $"HandleImpl(id = {id}, key = {key}, value = {value})";   
        }
    }

    sealed internal class ObjectPool {

        Component prototype; // what are we making
        Stack<HandleImpl> pool = new Stack<HandleImpl>(); // available objects
        Dictionary<int, HandleImpl> activeObjs = new Dictionary<int, HandleImpl>(); // objects on loan

        public ObjectPool(Component proto){
            prototype = proto;
        }

        public HandleImpl Borrow(){
            if (pool.Count == 0){ // if there aren't any available objects, make a new one
                var obj = Instantiate(prototype);
                pool.Push(new HandleImpl(prototype, obj));
            }
            var borrowed = pool.Pop(); // get the next available object
            borrowed.value.gameObject.SetActive(true);
            activeObjs[borrowed.id] = borrowed;
            borrowed.active = true;
            //Debug.Log($"Object Borrowed. Pool of {prototype} has {pool.Count} with {activeObjs.Count} active objects. {borrowed} was borrowed");
            return borrowed;
        }

        public void Return(int id){
            var handle = activeObjs[id]; // will throw exception if ID is bad
        
            handle.value.gameObject.SetActive(false);
            activeObjs.Remove(id);
            handle.active = false;
            pool.Push(handle);
            //Debug.Log($"Object Returned. Pool of {prototype} has {pool.Count} with {activeObjs.Count} active objects. {handle} was returned");
        }
    }
#endregion
#endregion
}
